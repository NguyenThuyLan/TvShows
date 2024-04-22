﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TvShows.Web.Common.Context;

#nullable disable

namespace TvShows.Web.Migrations
{
    [DbContext(typeof(TvShowContext))]
    [Migration("20240422064337_changeMaxValueOfMessage")]
    partial class changeMaxValueOfMessage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("TvShows.Web.Models.Review.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("createdDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("email");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("INTEGER")
                        .HasColumnName("isApproved");

                    b.Property<Guid?>("MemberKeyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("memberKeyId");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(1024)")
                        .HasColumnName("message");

                    b.Property<Guid?>("TvShowKeyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("tvShowKeyId");

                    b.Property<string>("TvShowTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("tvShowTitle");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("userName");

                    b.HasKey("Id");

                    b.ToTable("reviews", (string)null);
                });

            modelBuilder.Entity("TvShows.Web.Models.Review.TvShowReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("createdDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("email");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("INTEGER")
                        .HasColumnName("isApproved");

                    b.Property<Guid?>("MemberKeyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("memberKeyId");

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasColumnType("nvarchar(1024)")
                        .HasColumnName("review");

                    b.Property<Guid?>("TvShowKeyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("tvShowKeyId");

                    b.Property<string>("TvShowTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("tvShowTitle");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("userName");

                    b.HasKey("Id");

                    b.ToTable("tvShowReviews", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
