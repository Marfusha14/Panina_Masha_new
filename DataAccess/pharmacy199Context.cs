using System;
using System.Collections.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Models
{
    public partial class pharmacy199Context : DbContext
    {
        public pharmacy199Context()
        {
        }

        public pharmacy199Context(DbContextOptions<pharmacy199Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Basket> Baskets { get; set; } = null!;
        public virtual DbSet<Filterr> Filterrs { get; set; } = null!;
        public virtual DbSet<Orderr> Orderrs { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<SavedAddress> SavedAddresses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>(entity =>
            {
                entity.HasKey(e => new { e.UserIdd, e.ProductId })
                    .HasName("PK__Basket__73B78D03653F4218");

                entity.ToTable("Basket");

                entity.Property(e => e.UserIdd).HasColumnName("User_idd");

                entity.Property(e => e.ProductId).HasColumnName("Product_id");

                entity.Property(e => e.QuantityOfGoods).HasColumnName("Quantity_of_goods");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Baskets)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Basket__Product___4316F928");

                entity.HasOne(d => d.UserIddNavigation)
                    .WithMany(p => p.Baskets)
                    .HasForeignKey(d => d.UserIdd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Basket__User_idd__4222D4EF");
            });

            modelBuilder.Entity<Filterr>(entity =>
            {
                entity.HasKey(e => e.IdCategories)
                    .HasName("PK__Filterr__26BE28459F97D3DC");

                entity.ToTable("Filterr");

                entity.Property(e => e.IdCategories).HasColumnName("Id_categories");

                entity.Property(e => e.AvailabilityOfDiscounts).HasColumnName("availability_of_discounts");

                entity.Property(e => e.BrandName)
                    .HasMaxLength(25)
                    .HasColumnName("brand_name");

                entity.Property(e => e.Discounts).HasColumnName("discounts");

                entity.Property(e => e.ExpirationDate)
                    .HasMaxLength(25)
                    .HasColumnName("expiration_date");

                entity.Property(e => e.Indications)
                    .HasMaxLength(50)
                    .HasColumnName("indications");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Producer)
                    .HasMaxLength(20)
                    .HasColumnName("producer");

                entity.Property(e => e.ProductAvailability).HasColumnName("product_availability");

                entity.Property(e => e.QuantityInPack).HasColumnName("quantity_in_pack");

                entity.Property(e => e.ReleaseForm)
                    .HasMaxLength(50)
                    .HasColumnName("release_form");

                entity.Property(e => e.VacationFromThePharmacy)
                    .HasMaxLength(20)
                    .HasColumnName("vacation_from_the_pharmacy");
            });

            modelBuilder.Entity<Orderr>(entity =>
            {
                entity.HasKey(e => new { e.OrderNumber, e.UserIdd, e.NumberProduct })
                    .HasName("PK__Orderr__B8CE93343714D952");

                entity.ToTable("Orderr");

                entity.Property(e => e.OrderNumber)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Order_Number");

                entity.Property(e => e.UserIdd).HasColumnName("User_idd");

                entity.Property(e => e.NumberProduct).HasColumnName("Number_product");

                entity.Property(e => e.DateReferences)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_references");

                entity.Property(e => e.Statuss).HasMaxLength(50);

                entity.HasOne(d => d.NumberProductNavigation)
                    .WithMany(p => p.Orderrs)
                    .HasForeignKey(d => d.NumberProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orderr__Number_p__46E78A0C");

                entity.HasOne(d => d.UserIddNavigation)
                    .WithMany(p => p.Orderrs)
                    .HasForeignKey(d => d.UserIdd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orderr__User_idd__45F365D3");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.NumberProduct)
                    .HasName("PK__Product__AA6BD16AA12F16DF");

                entity.ToTable("Product");

                entity.Property(e => e.NumberProduct).HasColumnName("Number_product");

                entity.Property(e => e.Article)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(CONVERT([varchar](255),newid()))");

                entity.Property(e => e.IdCategories).HasColumnName("Id_categories");

                entity.Property(e => e.Namee).HasMaxLength(50);

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(50)
                    .HasColumnName("Product_description");

                entity.Property(e => e.ProductPrice)
                    .HasColumnType("decimal(25, 2)")
                    .HasColumnName("Product_price");

                entity.HasOne(d => d.IdCategoriesNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdCategories)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Id_cate__3C69FB99");
            });

            modelBuilder.Entity<SavedAddress>(entity =>
            {
                entity.HasKey(e => new { e.UserIdd, e.AddressId })
                    .HasName("PK__Saved_ad__7A0C9D06E6D0B9AA");

                entity.ToTable("Saved_address");

                entity.Property(e => e.UserIdd).HasColumnName("User_idd");

                entity.Property(e => e.AddressId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Address_id");

                entity.Property(e => e.AddressName)
                    .HasMaxLength(100)
                    .HasColumnName("Address_name");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Street).HasMaxLength(50);

                entity.HasOne(d => d.UserIddNavigation)
                    .WithMany(p => p.SavedAddresses)
                    .HasForeignKey(d => d.UserIdd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Saved_add__User___3F466844");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserNumber)
                    .HasName("PK__Users__A949FB8DB54D7456");

                entity.Property(e => e.UserNumber).HasColumnName("User_number");

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("Last_name");

                entity.Property(e => e.Mail).HasMaxLength(50);

                entity.Property(e => e.Namee).HasMaxLength(50);

                entity.Property(e => e.Nickname).HasMaxLength(50);

                entity.Property(e => e.Patronymic).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(25)
                    .HasColumnName("Phone_number");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
