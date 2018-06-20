﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TzMazeScraper.DbContext;

namespace TzMazeScraper.Migrations
{
    [DbContext(typeof(TzMazeContext))]
    [Migration("20180611011342_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799");

            modelBuilder.Entity("TzMazeScraper.DbContext.Dtos.ShowDbDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("JsonObject")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Show");
                });
#pragma warning restore 612, 618
        }
    }
}
