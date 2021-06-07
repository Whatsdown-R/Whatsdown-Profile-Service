﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Whatsdown_ProfileService.Data;

namespace Whatsdown_ProfileService.Migrations
{
    [DbContext(typeof(ProfileContext))]
    partial class ProfileContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Whatsdown_Authentication_Service.Models.Profile", b =>
                {
                    b.Property<string>("profileId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("displayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("gender")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("profileImage")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("status")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("profileId");

                    b.ToTable("Profiles");
                });
#pragma warning restore 612, 618
        }
    }
}
