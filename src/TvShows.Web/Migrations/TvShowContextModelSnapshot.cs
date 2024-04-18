﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TvShows.Web.Common.Context;

#nullable disable

namespace TvShows.Web.Migrations
{
    [DbContext(typeof(TvShowContext))]
    partial class TvShowContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("TvShows.Web.Models.Review.TvShowReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("email");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("INTEGER")
                        .HasColumnName("isApproved");

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("review");

                    b.Property<Guid?>("TvShowKey")
                        .HasColumnType("TEXT")
                        .HasColumnName("tvShowKey");

                    b.Property<string>("TvShowTitle")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("tvShowTitle");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("userName");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("website");

                    b.HasKey("Id");

                    b.ToTable("tvShowReviews", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
