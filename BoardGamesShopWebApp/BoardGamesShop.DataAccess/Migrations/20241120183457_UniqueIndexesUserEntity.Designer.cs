﻿// <auto-generated />
using System;
using BoardGamesShop.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BoardGamesShop.DataAccess.Migrations
{
    [DbContext(typeof(BoardGamesShopDbContext))]
    [Migration("20241120183457_UniqueIndexesUserEntity")]
    partial class UniqueIndexesUserEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.2.24474.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BoardGameGameDay", b =>
                {
                    b.Property<int>("BoardGamesId")
                        .HasColumnType("integer");

                    b.Property<int>("GameDaysId")
                        .HasColumnType("integer");

                    b.HasKey("BoardGamesId", "GameDaysId");

                    b.HasIndex("GameDaysId");

                    b.ToTable("game_day_board_game", (string)null);
                });

            modelBuilder.Entity("BoardGamesShop.DataAccess.Entities.BoardGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uuid");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MinAge")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("board_games");
                });

            modelBuilder.Entity("BoardGamesShop.DataAccess.Entities.GameClub", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ClosingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("OpeningTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("game_clubs");
                });

            modelBuilder.Entity("BoardGamesShop.DataAccess.Entities.GameDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uuid");

                    b.Property<int>("GameClubId")
                        .HasColumnType("integer");

                    b.Property<int>("MinAge")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PeopleMax")
                        .HasColumnType("integer");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("GameClubId");

                    b.ToTable("game_days");
                });

            modelBuilder.Entity("BoardGamesShop.DataAccess.Entities.PurchaseHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BoardGameId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("integer");

                    b.Property<int>("Payment")
                        .HasColumnType("integer");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserEntityId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BoardGameId");

                    b.HasIndex("UserEntityId");

                    b.ToTable("purchase_history");
                });

            modelBuilder.Entity("BoardGamesShop.DataAccess.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ExternalId")
                        .IsUnique();

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("PasswordHash")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("users");
                });

            modelBuilder.Entity("GameDayUserEntity", b =>
                {
                    b.Property<int>("GameDaysId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("GameDaysId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("user_game_day", (string)null);
                });

            modelBuilder.Entity("BoardGameGameDay", b =>
                {
                    b.HasOne("BoardGamesShop.DataAccess.Entities.BoardGame", null)
                        .WithMany()
                        .HasForeignKey("BoardGamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoardGamesShop.DataAccess.Entities.GameDay", null)
                        .WithMany()
                        .HasForeignKey("GameDaysId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BoardGamesShop.DataAccess.Entities.GameDay", b =>
                {
                    b.HasOne("BoardGamesShop.DataAccess.Entities.GameClub", "GameClub")
                        .WithMany("GameDays")
                        .HasForeignKey("GameClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameClub");
                });

            modelBuilder.Entity("BoardGamesShop.DataAccess.Entities.PurchaseHistory", b =>
                {
                    b.HasOne("BoardGamesShop.DataAccess.Entities.BoardGame", "BoardGame")
                        .WithMany("PurchaseHistory")
                        .HasForeignKey("BoardGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoardGamesShop.DataAccess.Entities.UserEntity", "UserEntity")
                        .WithMany("PurchaseHistory")
                        .HasForeignKey("UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoardGame");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("GameDayUserEntity", b =>
                {
                    b.HasOne("BoardGamesShop.DataAccess.Entities.GameDay", null)
                        .WithMany()
                        .HasForeignKey("GameDaysId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoardGamesShop.DataAccess.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BoardGamesShop.DataAccess.Entities.BoardGame", b =>
                {
                    b.Navigation("PurchaseHistory");
                });

            modelBuilder.Entity("BoardGamesShop.DataAccess.Entities.GameClub", b =>
                {
                    b.Navigation("GameDays");
                });

            modelBuilder.Entity("BoardGamesShop.DataAccess.Entities.UserEntity", b =>
                {
                    b.Navigation("PurchaseHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
