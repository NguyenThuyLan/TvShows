using MailKit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Formatting;
using TvShows.Web.Models;
using TvShows.Web.Services.Interfaces;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace TvShows.Web.Utility
{
	public class TvShowService : ITvShowService
	{
		private readonly IUmbracoContextFactory _umbracoContextFactory;
		private readonly IContentService _contentService;
		private readonly IMediaService _mediaService;
		private readonly MediaFileManager _mediaFileManager;
		private readonly MediaUrlGeneratorCollection _mediaUrlGeneratorCollection;
		private readonly IShortStringHelper _shortStringHelper;
		private readonly IContentTypeBaseServiceProvider _contentTypeBaseServiceProvider;
		private readonly ILogger<TvShowService> _logger;
		private readonly IVariationContextAccessor _variationContextAccessor;
		private readonly Appsettings _appSettings;
		private readonly ILocalizationService _localizationService;
		private readonly IWebHostEnvironment _environment;

		Dictionary<string, string> Descriptions = new() {
		{ "en-US", "was a great TV Show" },
		{ "da", "som et godt tv-program" },
		{ "vi", "Một TV Show tuyệt vời" }
	 };

		public TvShowService(IUmbracoContextFactory umbracoContextFactory,
		IContentService contentService,
		IMediaService mediaService,
		MediaFileManager mediaFileManager,
		MediaUrlGeneratorCollection mediaUrlGeneratorCollection,
		IShortStringHelper shortStringHelper,
		IContentTypeBaseServiceProvider contentTypeBaseServiceProvider,
		ILogger<TvShowService> logger,
		IVariationContextAccessor variationContextAccessor,
		IOptions<Appsettings> appSettings,
		ILocalizationService localizationService,
		IWebHostEnvironment environment)
		{
			_umbracoContextFactory = umbracoContextFactory;
			_contentService = contentService;
			_mediaService = mediaService;
			_mediaFileManager = mediaFileManager;
			_mediaUrlGeneratorCollection = mediaUrlGeneratorCollection;
			_shortStringHelper = shortStringHelper;
			_contentTypeBaseServiceProvider = contentTypeBaseServiceProvider;
			_logger = logger;
			_variationContextAccessor = variationContextAccessor;
			_appSettings = appSettings.Value;
			_localizationService = localizationService;
			_environment = environment;
		}

		public string MoveTvShowsFromTvMazeToUmbraco()
		{
			int page = 0;

			Uri ShowsAPI(int page) => new($"https://api.tvmaze.com/shows?page={page}");

			HttpClient client = new();

			while (page < 1)
			{
				var response = client.GetAsync(ShowsAPI(page)).Result;
				var json = response.Content.ReadAsStringAsync().Result;
				var shows = response.Content.ReadAsAsync<TvShowModel[]>(new[] { new JsonMediaTypeFormatter() }).Result;
				try { response.EnsureSuccessStatusCode(); } catch { break; }
				if (shows.Any())
				{
					foreach (var show in shows)
					{
						InsertedOrUpdated(show);
					}
				}
				page++;
			}
			return $"Sync complete until page {page}";
		}

		public bool InsertedOrUpdated(TvShowModel show)
		{

			using (var umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext())
			{
				var TvshowLibrary = umbracoContextReference.UmbracoContext.Content.GetById(_contentService.GetByLevel(2)
					.ToList().Where(s=>s.ContentType.Alias == TvShowsLibrary.ModelTypeAlias).First().Id) as TvShowsLibrary;
				TvShow existingTvShowInUmbraco = null;
				var existingTvShowsInUmbraco = TvshowLibrary.Children<TvShow>(_variationContextAccessor)
					.Where(t => t.TvShowID == show.Id.ToString());

				if (existingTvShowsInUmbraco?.Any() ?? false)
				{
					if (existingTvShowsInUmbraco.Count() > 0)
					{
						existingTvShowInUmbraco = existingTvShowsInUmbraco.OrderBy(t => t.CreateDate).First();
						foreach (var showToDelete in existingTvShowsInUmbraco.Where(s => s.Id != existingTvShowInUmbraco.Id))
						{
							_contentService.Delete(_contentService.GetById(showToDelete.Id));
						}
					}
					else
					{
						existingTvShowInUmbraco = existingTvShowsInUmbraco.FirstOrDefault();
					}
				}

				if (existingTvShowInUmbraco == null)
				{
					var media = ImportMediaFromTVMazeToUmbraco(show);
					var newTvShow = _contentService.Create(show.Name, TvshowLibrary.Id, TvShow.ModelTypeAlias);
					newTvShow.SetValue(nameof(TvShow.TvShowID), show.Id);

					newTvShow.SetValue(nameof(TvShow.Premiered), show.Premiered);

					if (media != null)
					{
						newTvShow.SetValue(nameof(TvShow.PreImage), media.GetUdi());
					}

					foreach (var description in Descriptions)
					{
						newTvShow.SetValue(nameof(TvShow.ShowTitle), show.Name, description.Key);
						newTvShow.SetValue(nameof(TvShow.Summary), show.Summary, description.Key);
						newTvShow.SetCultureName(show.Name, description.Key);
						//newTvShow.SetValue(nameof(TvShow.Summary), $"{show.Name} {description.Value}", description.Key);
					}
					//newTvShow.SetValue(nameof(TvShow.Summary), $"{show.Summary}", null);

					_contentService.SaveAndPublish(newTvShow);
					return true;
				}
				return Updated(show, existingTvShowInUmbraco);
			}
		}
		public IMedia ImportMediaFromTVMazeToUmbraco(TvShowModel tvShow)
		{

			if (tvShow == null || string.IsNullOrEmpty(tvShow.Name) || string.IsNullOrEmpty(tvShow.Image?.Original))
			{
				return null;
			}

			var webRequest = (HttpWebRequest)WebRequest.Create(tvShow.Image.Original);
			webRequest.AllowWriteStreamBuffering = true;
			webRequest.Timeout = 30000;

			var fileName = $"{tvShow.Id}_{GetFileNameFromUrl(tvShow.Image.Original)}";

			var existingFolder = GetMediaFolderFromUmbraco(tvShow.Name);

			var webResponse = webRequest.GetResponse();
			var stream = webResponse.GetResponseStream();

			IMedia media = _mediaService.CreateMedia(fileName, existingFolder.Id, Constants.Conventions.MediaTypes.Image);

			media.SetValue(_mediaFileManager, _mediaUrlGeneratorCollection, _shortStringHelper, _contentTypeBaseServiceProvider, Constants.Conventions.Media.File, fileName, stream);
			try
			{
				_mediaService.Save(media);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, fileName);
				return null;
			}

			return media;
		}

		private IMedia GetMediaFolderFromUmbraco(string tvShowName)
		{
			var parentFolder = _mediaService.GetRootMedia().FirstOrDefault();
			if (parentFolder == null)
			{
				throw new FolderNotFoundException($"No Folder exists in the Media Library, please create one.");
			}
			return parentFolder;
		}

		private string GetFileNameFromUrl(string url)
		{
			// Get the last part of the URL after the last slash '/'
			int lastSlashIndex = url.LastIndexOf('/');
			string filenameWithExtension = url.Substring(lastSlashIndex + 1);

			return filenameWithExtension;
		}
		private bool Updated(TvShowModel show, TvShow existingTvShowInUmbraco)
		{
			var existTvSHow = _contentService.GetById(existingTvShowInUmbraco.Id);
			existTvSHow.SetValue(nameof(TvShow.ShowTitle), show.Name);
			existTvSHow.SetValue(nameof(TvShow.Premiered), show.Premiered);
			existTvSHow.SetValue(nameof(TvShow.Summary), show.Summary);

			_contentService.SaveAndPublish(existTvSHow);
			// todo
			return false;
		}

		public void DeleteAllTvShows()
		{
			try
			{
				using (var umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext())
				{
					var contentLevel2 = _contentService.GetByLevel(3).ToList();
					if(contentLevel2?.Any() ?? false)
					{
						var tvShowsContent = contentLevel2.Where(s => s.ContentType.Alias == TvShow.ModelTypeAlias);
						if (tvShowsContent?.Any() ?? false)
						{
							foreach (var item in tvShowsContent)
							{
								_contentService.Delete(_contentService.GetById(item.Id));
							}
						}
					}

					var mediaFile = _mediaService.GetByLevel(2);
					if (mediaFile?.Any() ?? false)
					{
						foreach (var item in mediaFile)
						{
							_mediaService.Delete(item);
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"DeleteTvShows: {ex.Message}");
			}

		}

		public bool SaveTvShow(ShowModel show, string currentCulture)
		{
			if (show == null)
			{
				_logger.LogError($"{nameof(SaveTvShow)} : parameter is null");
				return false;
			}

			try
			{
				using (var umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext())
				{
					var tvshowLibrary = umbracoContextReference?.UmbracoContext?.Content?.GetById(_contentService.GetByLevel(2)
						.ToList().Where(s => s.ContentType.Alias == TvShowsLibrary.ModelTypeAlias).First().Id) as TvShowsLibrary;
					if (tvshowLibrary == null)
					{
						_logger.LogWarning("CreateNewTvShow: Can not find TvShowsLibrary");
						return false;
					}
					else
					{
						var existingTvShowInUmbraco = tvshowLibrary.Children<TvShow>(_variationContextAccessor).ToList()
						.Where(t => t.TvShowGuidId == show.TvShowGuidId.ToString() && show.CreatedByForm).FirstOrDefault();

						if (existingTvShowInUmbraco == null)
						{
							var langugages = _localizationService.GetAllLanguages();
							var newTvShow = _contentService.Create(show.ShowTitle, tvshowLibrary.Id, TvShow.ModelTypeAlias);
							if (!string.IsNullOrEmpty(show.PreImage))
							{
								var strArray = show.PreImage.Split("/");
								var fileName = strArray[strArray.Length - 1];

								var existingFolder = GetMediaFolderFromUmbraco(show.ShowTitle);
								string wwwrootPath = _environment.WebRootPath;

								string fullPath = Path.Combine(wwwrootPath, show.PreImage);
								Uri uri = new Uri(wwwrootPath + show.PreImage);
								string test = uri.AbsolutePath;
								System.Drawing.Image image = System.Drawing.Image.FromFile(test);
								var stream = new MemoryStream();

								image.Save(stream, image.RawFormat);
								stream.Position = 0;
								//Import Media
								IMedia media = _mediaService.CreateMedia(fileName, existingFolder.Id, Constants.Conventions.MediaTypes.Image);
								media.SetValue(_mediaFileManager, _mediaUrlGeneratorCollection, _shortStringHelper, _contentTypeBaseServiceProvider, Constants.Conventions.Media.File, fileName, stream);
								try
								{
									_mediaService.Save(media);
								}
								catch (Exception ex)
								{
									_logger.LogError(ex, fileName);
								}
								if (media != null)
								{
									newTvShow.SetValue(nameof(TvShow.PreImage), media.GetUdi());
								}
							}
							newTvShow.SetValue(nameof(TvShow.TvShowID), show.TvShowID);
							newTvShow.SetValue(nameof(TvShow.TvShowGuidId), show.TvShowGuidId);
							newTvShow.SetValue(nameof(TvShow.CreatedByForm), show.CreatedByForm);
							newTvShow.SetValue(nameof(TvShow.Premiered), show.Premiered);
							newTvShow.SetValue(nameof(TvShow.ShowTitle), show.ShowTitle, currentCulture);
							newTvShow.SetValue(nameof(TvShow.Summary), show.Summary, currentCulture);
							newTvShow.SetCultureName(show.ShowTitle, currentCulture);

							_contentService.Save(newTvShow);
						}
						else
						{
							var existTvShow = _contentService.GetById(existingTvShowInUmbraco.Id);
							existTvShow.SetValue(nameof(TvShow.ShowTitle), show.ShowTitle, currentCulture);
							existTvShow.SetValue(nameof(TvShow.Premiered), show.Premiered, currentCulture);
							existTvShow.SetValue(nameof(TvShow.Summary), show.Summary, currentCulture);
							existTvShow.SetCultureName(show.ShowTitle, currentCulture);
							_contentService.Save(existTvShow);
						}
					}
				}

			}
			catch (Exception ex)
			{
				_logger.LogError($"SaveTvShow: {ex.Message}");
				return false;
			}

			return true;
		}

		public bool DeleteTvShow(Guid Id)
		{
			throw new NotImplementedException();
		}
	}
}
