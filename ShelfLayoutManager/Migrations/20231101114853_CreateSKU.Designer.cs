﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ShelfLayoutManager.Entity;

#nullable disable

namespace ShelfLayoutManager.Migrations
{
    [DbContext(typeof(ShelfLayoutDbContext))]
    [Migration("20231101114853_CreateSKU")]
    partial class CreateSKU
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ShelfLayoutManager.Entity.Cabinet", b =>
                {
                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Number"));

                    b.Property<int>("Depth")
                        .HasColumnType("integer");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<int>("PositionX")
                        .HasColumnType("integer");

                    b.Property<int>("PositionY")
                        .HasColumnType("integer");

                    b.Property<int>("PositionZ")
                        .HasColumnType("integer");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.HasKey("Number");

                    b.ToTable("cabinet");
                });

            modelBuilder.Entity("ShelfLayoutManager.Entity.Lane", b =>
                {
                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Number"));

                    b.Property<string>("JanCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PositionX")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("RowId")
                        .HasColumnType("integer");

                    b.HasKey("Number");

                    b.HasIndex("RowId");

                    b.ToTable("lane");
                });

            modelBuilder.Entity("ShelfLayoutManager.Entity.Row", b =>
                {
                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Number"));

                    b.Property<int>("CabinetId")
                        .HasColumnType("integer");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<int>("PositionZ")
                        .HasColumnType("integer");

                    b.HasKey("Number");

                    b.HasIndex("CabinetId");

                    b.ToTable("row");
                });

            modelBuilder.Entity("ShelfLayoutManager.Entity.SKU", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("SKUs");
                });

            modelBuilder.Entity("ShelfLayoutManager.Entity.Lane", b =>
                {
                    b.HasOne("ShelfLayoutManager.Entity.Row", "Row")
                        .WithMany("Lanes")
                        .HasForeignKey("RowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Row");
                });

            modelBuilder.Entity("ShelfLayoutManager.Entity.Row", b =>
                {
                    b.HasOne("ShelfLayoutManager.Entity.Cabinet", "Cabinet")
                        .WithMany("Rows")
                        .HasForeignKey("CabinetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cabinet");
                });

            modelBuilder.Entity("ShelfLayoutManager.Entity.Cabinet", b =>
                {
                    b.Navigation("Rows");
                });

            modelBuilder.Entity("ShelfLayoutManager.Entity.Row", b =>
                {
                    b.Navigation("Lanes");
                });
#pragma warning restore 612, 618
        }
    }
}
