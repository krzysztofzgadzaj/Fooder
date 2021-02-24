﻿// <auto-generated />
using System;
using Fooder.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fooder.Persistence.Migrations
{
    [DbContext(typeof(FooderContext))]
    [Migration("20210127170120_PetAfflictionMigration")]
    partial class PetAfflictionMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fooder.Persistence.Entities.AfflictionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Afflictions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Affliction one"
                        });
                });

            modelBuilder.Entity("Fooder.Persistence.Entities.BrandEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Brand one"
                        });
                });

            modelBuilder.Entity("Fooder.Persistence.Entities.CommentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("CommentMark")
                        .HasColumnType("real");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("FeedEntityId")
                        .HasColumnType("int");

                    b.Property<int?>("RelatedCommentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FeedEntityId");

                    b.HasIndex("RelatedCommentId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Fooder.Persistence.Entities.CommentMarkEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CommentId")
                        .HasColumnType("int");

                    b.Property<string>("Mark")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("CommentMarks");
                });

            modelBuilder.Entity("Fooder.Persistence.Entities.FeedAffliction", b =>
                {
                    b.Property<int>("FeedEntityId")
                        .HasColumnType("int");

                    b.Property<int>("AfflictionEntityId")
                        .HasColumnType("int");

                    b.HasKey("FeedEntityId", "AfflictionEntityId");

                    b.HasIndex("AfflictionEntityId");

                    b.ToTable("FeedAffliction");
                });

            modelBuilder.Entity("Fooder.Persistence.Entities.FeedEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandNameId")
                        .HasColumnType("int");

                    b.Property<string>("DogActivityLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FeedType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("MaxWeight")
                        .HasColumnType("real");

                    b.Property<float>("MinWeight")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("ProducerSiteLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UniqueId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BrandNameId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Feeds");
                });

            modelBuilder.Entity("Fooder.Persistence.Entities.FeedMarkEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FeedId")
                        .HasColumnType("int");

                    b.Property<int>("Mark")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("FeedMarks");
                });

            modelBuilder.Entity("Fooder.Persistence.Entities.ImageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Fooder.Persistence.Entities.PetAffliction", b =>
                {
                    b.Property<int>("AfflictionEntityId")
                        .HasColumnType("int");

                    b.Property<int>("PetEntityId")
                        .HasColumnType("int");

                    b.HasKey("AfflictionEntityId", "PetEntityId");

                    b.HasIndex("PetEntityId");

                    b.ToTable("PetAffliction");
                });

            modelBuilder.Entity("Fooder.Persistence.Entities.PetEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActivityLevel")
                        .HasColumnType("int");

                    b.Property<float?>("BodyWeight")
                        .HasColumnType("real");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime");

                    b.Property<float?>("HeightInCentimetres")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<Guid>("UniqueId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("Fooder.Persistence.Entities.ReviewEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("FeedEntityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FeedEntityId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Fooder.Persistence.Entities.CommentEntity", b =>
                {
                    b.HasOne("Fooder.Persistence.Entities.FeedEntity", "FeedEntity")
                        .WithMany("Comments")
                        .HasForeignKey("FeedEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fooder.Persistence.Entities.CommentEntity", "RelatedComment")
                        .WithMany("RelatedComments")
                        .HasForeignKey("RelatedCommentId");
                });

            modelBuilder.Entity("Fooder.Persistence.Entities.FeedAffliction", b =>
                {
                    b.HasOne("Fooder.Persistence.Entities.AfflictionEntity", "AfflictionEntity")
                        .WithMany("Feeds")
                        .HasForeignKey("AfflictionEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fooder.Persistence.Entities.FeedEntity", "FeedEntity")
                        .WithMany("DogAfflictions")
                        .HasForeignKey("FeedEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fooder.Persistence.Entities.FeedEntity", b =>
                {
                    b.HasOne("Fooder.Persistence.Entities.BrandEntity", "BrandName")
                        .WithMany("Feeds")
                        .HasForeignKey("BrandNameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fooder.Persistence.Entities.PetAffliction", b =>
                {
                    b.HasOne("Fooder.Persistence.Entities.AfflictionEntity", "AfflictionEntity")
                        .WithMany("Pets")
                        .HasForeignKey("AfflictionEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fooder.Persistence.Entities.PetEntity", "PetEntity")
                        .WithMany("DogAfflictions")
                        .HasForeignKey("PetEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fooder.Persistence.Entities.ReviewEntity", b =>
                {
                    b.HasOne("Fooder.Persistence.Entities.FeedEntity", null)
                        .WithMany("Reviews")
                        .HasForeignKey("FeedEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
