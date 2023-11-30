﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProjectManager.Data;

#nullable disable

namespace ProjectManager.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProjectManager.Data.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Instant>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreateBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Instant?>("DeleteAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeleteBy")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("Done")
                        .HasColumnType("boolean");

                    b.Property<Instant>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("ProjectManager.Data.Entities.Todo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Instant>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreateBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Instant?>("DeleteAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeleteBy")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("Done")
                        .HasColumnType("boolean");

                    b.Property<Instant>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Todo");
                });

            modelBuilder.Entity("ProjectManager.Data.Entities.Todo", b =>
                {
                    b.HasOne("ProjectManager.Data.Entities.Project", "Project")
                        .WithMany("Todos")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectManager.Data.Entities.Project", b =>
                {
                    b.Navigation("Todos");
                });
#pragma warning restore 612, 618
        }
    }
}
