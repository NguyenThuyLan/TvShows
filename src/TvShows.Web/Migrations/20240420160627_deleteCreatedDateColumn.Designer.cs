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
    [Migration("20240420160627_deleteCreatedDateColumn")]
    partial class deleteCreatedDateColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid?>("MemberKeyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("memberKeyId");

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("review");

                    b.Property<Guid?>("TvShowKeyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("tvShowKeyId");

                    b.Property<string>("TvShowTitle")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("tvShowTitle");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("userName");

                    b.HasKey("Id");

                    b.ToTable("tvShowReviews", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
