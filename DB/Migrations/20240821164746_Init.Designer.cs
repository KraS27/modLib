﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using modLib.DB;

#nullable disable

namespace modLib.DB.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240821164746_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("modLib.DB.Relationships.ModModPack", b =>
                {
                    b.Property<int>("ModId")
                        .HasColumnType("integer");

                    b.Property<int>("ModPackId")
                        .HasColumnType("integer");

                    b.HasKey("ModId", "ModPackId");

                    b.HasIndex("ModPackId");

                    b.ToTable("mod_modPack", (string)null);
                });

            modelBuilder.Entity("modLib.Entities.DTO.Auth.RolePermissionDTO", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("PermissionId")
                        .HasColumnType("integer");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("role_permissions", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            PermissionId = 2
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 1
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 3
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 4
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 1
                        });
                });

            modelBuilder.Entity("modLib.Entities.DTO.Auth.UserRoleDTO", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("user_roles", (string)null);
                });

            modelBuilder.Entity("modLib.Entities.DbModels.GameModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("name");

                    b.Property<string>("Version")
                        .HasColumnType("text")
                        .HasColumnName("version");

                    b.HasKey("Id");

                    b.ToTable("games", (string)null);
                });

            modelBuilder.Entity("modLib.Entities.DbModels.PermissionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("permissions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Read"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Create"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Update"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Delete"
                        });
                });

            modelBuilder.Entity("modLib.Entities.DbModels.RoleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Rabotyaga"
                        });
                });

            modelBuilder.Entity("modLib.Entities.DbModels.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer")
                        .HasColumnName("age");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("userName");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("modLib.Entities.Models.ModPackModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("modPacks", (string)null);
                });

            modelBuilder.Entity("modLib.Models.Entities.ModModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("name");

                    b.Property<string>("Path")
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)")
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

            modelBuilder.Entity("modLib.Entities.DTO.Auth.RolePermissionDTO", b =>
                {
                    b.HasOne("modLib.Entities.DbModels.PermissionModel", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("modLib.Entities.DbModels.RoleModel", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("modLib.Entities.DTO.Auth.UserRoleDTO", b =>
                {
                    b.HasOne("modLib.Entities.DbModels.RoleModel", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("modLib.Entities.DbModels.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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