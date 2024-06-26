﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using modLib.DB;

#nullable disable

namespace modLib.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240515135233_Add-GameTable")]
    partial class AddGameTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("modLib.DB.Relationships.ModModPack", b =>
                {
                    b.Property<int>("ModId")
                        .HasColumnType("int");

                    b.Property<int>("ModPackId")
                        .HasColumnType("int");

                    b.HasKey("ModId", "ModPackId");

                    b.HasIndex("ModPackId");

                    b.ToTable("mod_modPack", (string)null);
                });

            modelBuilder.Entity("modLib.Entities.DbModels.GameModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("name");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("version");

                    b.HasKey("Id");

                    b.ToTable("games", (string)null);
                });

            modelBuilder.Entity("modLib.Entities.Models.ModPackModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("modPacks", (string)null);
                });

            modelBuilder.Entity("modLib.Models.Entities.ModModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)")
                        .HasColumnName("name");

                    b.Property<string>("Path")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasColumnName("path");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("mods", (string)null);
                });

            modelBuilder.Entity("modLib.DB.Relationships.ModModPack", b =>
                {
                    b.HasOne("modLib.Models.Entities.ModModel", "Mod")
                        .WithMany("ModModPacks")
                        .HasForeignKey("ModId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("modLib.Entities.Models.ModPackModel", "ModPack")
                        .WithMany("ModModPacks")
                        .HasForeignKey("ModPackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mod");

                    b.Navigation("ModPack");
                });

            modelBuilder.Entity("modLib.Entities.Models.ModPackModel", b =>
                {
                    b.HasOne("modLib.Entities.DbModels.GameModel", "Game")
                        .WithMany("ModPacks")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("modLib.Models.Entities.ModModel", b =>
                {
                    b.HasOne("modLib.Entities.DbModels.GameModel", "Game")
                        .WithMany("Mods")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("modLib.Entities.DbModels.GameModel", b =>
                {
                    b.Navigation("ModPacks");

                    b.Navigation("Mods");
                });

            modelBuilder.Entity("modLib.Entities.Models.ModPackModel", b =>
                {
                    b.Navigation("ModModPacks");
                });

            modelBuilder.Entity("modLib.Models.Entities.ModModel", b =>
                {
                    b.Navigation("ModModPacks");
                });
#pragma warning restore 612, 618
        }
    }
}
