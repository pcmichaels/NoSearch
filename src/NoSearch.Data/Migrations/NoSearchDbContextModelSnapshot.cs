﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NoSearch.Data.DataAccess;

#nullable disable

namespace NoSearch.Data.Migrations
{
    [DbContext(typeof(NoSearchDbContext))]
    partial class NoSearchDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NoSearch.Data.Resources.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Rank")
                        .HasColumnType("int");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("NoSearch.Data.Resources.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsRestricted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ResourceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ResourceId");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            IsRestricted = false,
                            Name = "Blog"
                        },
                        new
                        {
                            Id = -2,
                            IsRestricted = false,
                            Name = "News"
                        },
                        new
                        {
                            Id = -3,
                            IsRestricted = false,
                            Name = "Programming"
                        },
                        new
                        {
                            Id = -4,
                            IsRestricted = false,
                            Name = "Tutorial"
                        },
                        new
                        {
                            Id = -5,
                            IsRestricted = false,
                            Name = "Video"
                        });
                });

            modelBuilder.Entity("NoSearch.Data.Validation.RestrictedWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Reason")
                        .HasColumnType("int");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RestrictedWords");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Reason = 0,
                            Word = "shit"
                        },
                        new
                        {
                            Id = -2,
                            Reason = 0,
                            Word = "fuck"
                        });
                });

            modelBuilder.Entity("NoSearch.Data.Resources.Tag", b =>
                {
                    b.HasOne("NoSearch.Data.Resources.Resource", null)
                        .WithMany("Tags")
                        .HasForeignKey("ResourceId");
                });

            modelBuilder.Entity("NoSearch.Data.Resources.Resource", b =>
                {
                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
