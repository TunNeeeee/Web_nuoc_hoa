﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace Web_final.Models;

public partial class WebsiteNuocHoaContext : DbContext
{
    public WebsiteNuocHoaContext()
    {
    }

    public WebsiteNuocHoaContext(DbContextOptions<WebsiteNuocHoaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartDetail> CartDetails { get; set; }

    public virtual DbSet<Catology> Catologies { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Slider> Sliders { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Lh> Lhs { get; set; }
    public virtual DbSet<Ph> Phs { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-IO39T9O;Database=WebsiteNuocHoa;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.IdBlog).HasName("PK__BLOG__75519326DC2C3571");

            entity.ToTable("BLOG");

            entity.Property(e => e.IdBlog).HasColumnName("ID_BLOG");
            entity.Property(e => e.Datebegin)
                .HasColumnType("smalldatetime")
                .HasColumnName("DATEBEGIN");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Detail)
                .HasColumnType("ntext")
                .HasColumnName("DETAIL");
            entity.Property(e => e.Hide).HasColumnName("HIDE");
            entity.Property(e => e.IdUsers).HasColumnName("ID_USERS");
            entity.Property(e => e.Img)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMG");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .HasColumnName("LINK");
            entity.Property(e => e.Order).HasColumnName("ORDER");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.IdUsersNavigation).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.IdUsers)
                .HasConstraintName("FK__BLOG__ID_USERS__44FF419A");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.IdCart).HasName("PK__CART__7A1680A5FBCCCB01");

            entity.ToTable("CART");

            entity.Property(e => e.IdCart).HasColumnName("ID_CART");
            entity.Property(e => e.Datebegin)
                .HasColumnType("smalldatetime")
                .HasColumnName("DATEBEGIN");
            entity.Property(e => e.Hide).HasColumnName("HIDE");
            entity.Property(e => e.IdUsers).HasColumnName("ID_USERS");

            entity.HasOne(d => d.IdUsersNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.IdUsers)
                .HasConstraintName("FK__CART__ID_USERS__45F365D3");
        });

        modelBuilder.Entity<CartDetail>(entity =>
        {
            entity.HasKey(e => e.IdCd).HasName("PK__CART_DET__8B622F8F5C733E4B");

            entity.ToTable("CART_DETAIL");

            entity.Property(e => e.IdCd).HasColumnName("ID_CD");
            entity.Property(e => e.Hide).HasColumnName("HIDE");
            entity.Property(e => e.IdCart).HasColumnName("ID_CART");
            entity.Property(e => e.IdPro).HasColumnName("ID_PRO");
            entity.Property(e => e.SoldNum).HasColumnName("SOLD_NUM");

            entity.HasOne(d => d.IdCartNavigation).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.IdCart)
                .HasConstraintName("FK__CART_DETA__ID_CA__48CFD27E");

            entity.HasOne(d => d.IdProNavigation).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.IdPro)
                .HasConstraintName("FK__CART_DETA__ID_PR__46E78A0C");
        });

        modelBuilder.Entity<Catology>(entity =>
        {
            entity.HasKey(e => e.IdCat).HasName("PK__CATOLOGY__2BF8FA1C4A396283");

            entity.ToTable("CATOLOGY");

            entity.Property(e => e.IdCat).HasColumnName("ID_CAT");
            entity.Property(e => e.Hide).HasColumnName("HIDE");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .HasColumnName("LINK");
            entity.Property(e => e.NameCat)
                .HasMaxLength(50)
                .HasColumnName("NAME_CAT");
            entity.Property(e => e.Order).HasColumnName("ORDER");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__MENU__4728FC60A5AA3DC4");

            entity.ToTable("MENU");

            entity.Property(e => e.IdMenu).HasColumnName("ID_MENU");
            entity.Property(e => e.Hide).HasColumnName("HIDE");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .HasColumnName("LINK");
            entity.Property(e => e.Order).HasColumnName("ORDER");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdPro).HasName("PK__PRODUCTS__20AF98FDABED64F9");

            entity.ToTable("PRODUCTS");

            entity.Property(e => e.IdPro).HasColumnName("ID_PRO");
            entity.Property(e => e.Detail)
                .HasColumnType("ntext")
                .HasColumnName("DETAIL");
            entity.Property(e => e.Hide).HasColumnName("HIDE");
            entity.Property(e => e.IdCat).HasColumnName("ID_CAT");
            entity.Property(e => e.Img1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMG1");
            entity.Property(e => e.Img2)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMG2");
            entity.Property(e => e.Img3)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMG3");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .HasColumnName("LINK");
            entity.Property(e => e.NamePro)
                .HasMaxLength(50)
                .HasColumnName("NAME_PRO");
            entity.Property(e => e.Nums).HasColumnName("NUMS");
            entity.Property(e => e.Order).HasColumnName("ORDER");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("PRICE");

            entity.HasOne(d => d.IdCatNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCat)
                .HasConstraintName("FK__PRODUCTS__ID_CAT__47DBAE45");
        });

        modelBuilder.Entity<Slider>(entity =>
        {
            entity.HasKey(e => e.IdSlide).HasName("PK__SLIDER__22D1BB10E4764EE2");

            entity.ToTable("SLIDER");

            entity.Property(e => e.IdSlide).HasColumnName("ID_SLIDE");
            entity.Property(e => e.Hide).HasColumnName("HIDE");
            entity.Property(e => e.Img)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMG");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .HasColumnName("LINK");
            entity.Property(e => e.Order).HasColumnName("ORDER");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUsers).HasName("PK__USERS__1DDB35C3E427FA00");

            entity.ToTable("USERS");

            entity.Property(e => e.IdUsers).HasColumnName("ID_USERS");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Hide).HasColumnName("HIDE");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Permission).HasColumnName("PERMISSION");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("PHONE");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("USERNAME");
            entity.Property(e => e.Img)
                .HasMaxLength(255)
                .HasColumnName("img");
        });
        modelBuilder.Entity<Lh>(entity =>
        {
            entity.HasKey(e => e.IdLh);

            entity.ToTable("LH");

            entity.Property(e => e.IdLh).HasColumnName("ID_LH");

            entity.Property(e => e.LhTieuDe).HasMaxLength(1000).HasColumnName("LH_TieuDe");

            entity.Property(e => e.LhNoiDung)
                .HasMaxLength(1000)
                .HasColumnName("LH_NoiDung");
        });
        modelBuilder.Entity<Ph>(entity =>
        {
            entity.HasKey(e => e.ID_PH);

            entity.ToTable("PH");

            entity.Property(e => e.IdUsers).HasColumnName("ID_USERS");

            entity.Property(e => e.NdPh).HasMaxLength(1000).HasColumnName("ND_PH");
            entity.Property(e => e.UsersName)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.img)
                .HasMaxLength(255)
                .HasColumnName("img");
            entity.Property(e => e.Hide)
                .HasColumnName("Hide");
            entity.HasOne(d => d.IdUsersNavigation).WithMany(p => p.Phs)
                .HasForeignKey(d => d.IdUsers)
                .HasConstraintName("FK_PH_USERS");

        });
     
    OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
