﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookStore.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AuthorBook", b =>
                {
                    b.Property<Guid>("AuthorsId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("BooksId")
                        .HasColumnType("char(36)");

                    b.HasKey("AuthorsId", "BooksId");

                    b.HasIndex("BooksId");

                    b.ToTable("BookAuthors", (string)null);
                });

            modelBuilder.Entity("BookGenre", b =>
                {
                    b.Property<Guid>("BooksId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("GenresId")
                        .HasColumnType("char(36)");

                    b.HasKey("BooksId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("BookGenres", (string)null);
                });

            modelBuilder.Entity("Models.Author", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Models.Book", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<double?>("Price")
                        .HasColumnType("double");

                    b.Property<int?>("Stock")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Models.Cart", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Models.CartItem", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("BookId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CartId")
                        .HasColumnType("char(36)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("CartId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("Models.Genre", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Models.Order", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<double?>("TotalPrice")
                        .HasColumnType("double");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Models.OrderItem", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("BookId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("char(36)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("SubTotal")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AuthorBook", b =>
                {
                    b.HasOne("Models.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookGenre", b =>
                {
                    b.HasOne("Models.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Cart", b =>
                {
                    b.HasOne("Models.User", "User")
                        .WithOne("Cart")
                        .HasForeignKey("Models.Cart", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.CartItem", b =>
                {
                    b.HasOne("Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId");

                    b.HasOne("Models.Cart", null)
                        .WithMany("Items")
                        .HasForeignKey("CartId");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Models.Order", b =>
                {
                    b.HasOne("Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.OrderItem", b =>
                {
                    b.HasOne("Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId");

                    b.HasOne("Models.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Models.Cart", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Models.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Navigation("Cart");
                });
#pragma warning restore 612, 618
        }
    }
}
