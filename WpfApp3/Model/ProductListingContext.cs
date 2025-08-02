using System.Diagnostics;
using System.Windows;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace WpfApp3.Model;

public partial class ProductListingContext : DbContext
{
    public ProductListingContext()
    {
    }

    public ProductListingContext(DbContextOptions<ProductListingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<certification_Model> Certifications { get; set; }

    public virtual DbSet<certification_image_Model> CertificationImages { get; set; }

    public virtual DbSet<Country_Model> Countries { get; set; }

    public virtual DbSet<Currencies_Model> Currencies { get; set; }

    public virtual DbSet<Customermain_Model> Customers { get; set; }

    public virtual DbSet<Customerbranch_Model> CustomerBranches { get; set; }

    public virtual DbSet<Inventorypostinggroup_Model> InventoryPostingGroups { get; set; }

    public virtual DbSet<item_Model> Items { get; set; }

    public virtual DbSet<item_image_Model> ItemImages { get; set; }

    public virtual DbSet<Itemunitsofmeasure_Model> ItemUnitsOfMeasures { get; set; }

    public virtual DbSet<Labelsize_Model> Labelsizes { get; set; }

    public virtual DbSet<Myorgcertification_Model> MyorgCertifications { get; set; }

    public virtual DbSet<PrintingHistory_Model> PrintingHistories { get; set; }

    public virtual DbSet<Specialdiscount_Model> SpecialDiscounts { get; set; }

    public virtual DbSet<Specialprice_Model> SpecialPrices { get; set; }

    public virtual DbSet<Unitsofmeasure_Model> UnitsOfMeasures { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<ProductListingContext>().Build();
        optionsBuilder.UseMySQL(config["ConnectionString"]);
        optionsBuilder.EnableSensitiveDataLogging();
        //.LogTo(s => System.Diagnostics.Debug.WriteLine(s));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<certification_Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("certification");

            entity.HasIndex(e => e.Code, "Code").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("DESCRIPTION");
        });

        modelBuilder.Entity<certification_image_Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("certification_image");

            entity.HasIndex(e => e.Code, "Code").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Image).HasColumnType("mediumblob");
        });

        modelBuilder.Entity<Country_Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("country");

            entity.HasIndex(e => e.Code, "Code").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
        });

        modelBuilder.Entity<Currencies_Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("currencies");

            entity.HasIndex(e => e.Code, "Code").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Exchange_Rate)
                .HasPrecision(64, 5)
                .HasColumnName("Exchange_Rate");
            entity.Property(e => e.ExchangeRateDate)
                .HasColumnType("date")
                .HasColumnName("Exchange_Rate_Date");
            entity.Property(e => e.Symbol).HasMaxLength(10);
        });

        modelBuilder.Entity<Customermain_Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("customer");

            entity.HasIndex(e => e.CustomerId, "CustomerID").IsUnique();

            entity.Property(e => e.Address1).HasMaxLength(30);
            entity.Property(e => e.Address2).HasMaxLength(30);
            entity.Property(e => e.Address3).HasMaxLength(30);
            entity.Property(e => e.CompanyRegNo).HasMaxLength(20);
            entity.Property(e => e.ContactPerson).HasMaxLength(30);
            entity.Property(e => e.CustomerId)
                .HasMaxLength(30)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerLabelCode).HasMaxLength(2);
            entity.Property(e => e.Description).HasMaxLength(40);
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.Fax).HasMaxLength(20);
            entity.Property(e => e.GSTRegNo)
                .HasMaxLength(20)
                .HasColumnName("GSTRegNo");
            entity.Property(e => e.LabelStyle).HasMaxLength(10);
            entity.Property(e => e.Phone1).HasMaxLength(20);
            entity.Property(e => e.Phone2).HasMaxLength(20);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.VehicleNo).HasMaxLength(10);
            entity.Property(e => e.Website).HasMaxLength(80);
        });

        modelBuilder.Entity<Customerbranch_Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("customer_branch");

            entity.HasIndex(e => e.BranchId, "BranchID").IsUnique();

            entity.HasIndex(e => e.CustomerId, "customer_branch_ibfk_1");

            entity.Property(e => e.Address1).HasMaxLength(30);
            entity.Property(e => e.Address2).HasMaxLength(30);
            entity.Property(e => e.Address3).HasMaxLength(30);
            entity.Property(e => e.BranchDescription).HasMaxLength(50);
            entity.Property(e => e.BranchId)
                .HasMaxLength(30)
                .HasColumnName("BranchID");
            entity.Property(e => e.CompanyRegNo).HasMaxLength(20);
            entity.Property(e => e.ContactPerson).HasMaxLength(30);
            entity.Property(e => e.CustomerDescription).HasMaxLength(50);
            entity.Property(e => e.CustomerId)
                .HasMaxLength(30)
                .HasColumnName("CustomerID");
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.Fax).HasMaxLength(20);
            entity.Property(e => e.GSTRegNo)
                .HasMaxLength(20)
                .HasColumnName("GSTRegNo");
            entity.Property(e => e.LabelStyle).HasMaxLength(10);
            entity.Property(e => e.Phone1).HasMaxLength(20);
            entity.Property(e => e.Phone2).HasMaxLength(20);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.VehicleNo).HasMaxLength(10);
            entity.Property(e => e.Website).HasMaxLength(80);
        });

        modelBuilder.Entity<Inventorypostinggroup_Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inventory_posting_group");

            entity.HasIndex(e => e.Code, "Code").IsUnique();

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("CODE");
            entity.Property(e => e.Description).HasMaxLength(50);
        });

        modelBuilder.Entity<item_Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("items");

            entity.HasIndex(e => e.Country, "Country");

            entity.HasIndex(e => e.ItemNo, "ITEMNO").IsUnique();

            entity.HasIndex(e => e.InventoryPostingGroup, "InventoryPostingGroup");

            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.InventoryPostingGroup).HasMaxLength(20);
            entity.Property(e => e.ItemNo)
                .HasMaxLength(10)
                .HasColumnName("ITEMNO");
        });

        modelBuilder.Entity<item_image_Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("item_image");

            entity.HasIndex(e => e.Code, "CODE").IsUnique();

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("CODE");
            entity.Property(e => e.Image)
                .HasColumnType("mediumblob")
                .HasColumnName("IMAGE");
        });

        modelBuilder.Entity<Itemunitsofmeasure_Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("item_units_of_measure");

            entity.HasIndex(e => new { e.ItemNo, e.UnitOfMeasureCode }, "ItemNo").IsUnique();

            entity.HasIndex(e => e.UnitOfMeasureCode, "UnitOfMeasureCode");

            entity.Property(e => e.ItemNo).HasMaxLength(10);
            entity.Property(e => e.QtyPerUnitOfMeasure).HasPrecision(64, 5);
            entity.Property(e => e.UnitOfMeasureCode).HasMaxLength(10);
        });

        modelBuilder.Entity<Labelsize_Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("labelsize");

            entity.HasIndex(e => e.Code, "Code").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.Description).HasMaxLength(50);
        });

        modelBuilder.Entity<Myorgcertification_Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("myorg_certification");

            entity.HasIndex(e => e.Code, "CODE").IsUnique();

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("CODE");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("DESCRIPTION");
        });

        modelBuilder.Entity<PrintingHistory_Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("printing_history");

            entity.HasIndex(e => e.PH_EncryptedQrdata, "PH_EncryptedQRData").IsUnique();

            entity.Property(e => e.CUST_CustomerLabelCode)
                .HasMaxLength(2)
                .HasColumnName("CUST_CustomerLabelCode");
            entity.Property(e => e.IM_Image)
                .HasColumnType("mediumblob")
                .HasColumnName("IM_Image");
            entity.Property(e => e.IT_Country)
                .HasMaxLength(50)
                .HasColumnName("IT_Country");
            entity.Property(e => e.IT_Description)
                .HasMaxLength(50)
                .HasColumnName("IT_Description");
            entity.Property(e => e.IT_InventoryPostingGroup)
                .HasMaxLength(20)
                .HasColumnName("IT_InventoryPostingGroup");
            entity.Property(e => e.IUOM_QtyPerUnitOfMeasure)
                .HasPrecision(64, 5)
                .HasColumnName("IUOM_QtyPerUnitOfMeasure");
            entity.Property(e => e.PH_DateAsAlphabetText)
                .HasMaxLength(50)
                .HasColumnName("PH_DateAsAlphabetText");
            entity.Property(e => e.PH_EncryptedQrdata)
                .HasMaxLength(50)
                .HasColumnName("PH_EncryptedQRData");
            entity.Property(e => e.PH_IpAddress)
                .HasMaxLength(50)
                .HasColumnName("PH_IpAddress");
            entity.Property(e => e.PH_Location)
                .HasMaxLength(50)
                .HasColumnName("PH_Location");
            entity.Property(e => e.PH_MyOrgCertText)
                .HasMaxLength(50)
                .HasColumnName("PH_MyOrgCertText");
            entity.Property(e => e.PH_OrgCertText)
                .HasMaxLength(50)
                .HasColumnName("PH_OrgCertText");
            entity.Property(e => e.PH_Price)
                .HasMaxLength(18)
                .HasDefaultValueSql("''")
                .HasColumnName("PH_Price");
            entity.Property(e => e.PH_PricePerKgText)
                .HasMaxLength(50)
                .HasColumnName("PH_PricePerKgText");
            entity.Property(e => e.PH_PrintingDate)
                .HasMaxLength(6)
                .HasColumnName("PH_PrintingDate");
            entity.Property(e => e.PH_ProductBarcode)
                .HasMaxLength(18)
                .HasDefaultValueSql("''")
                .HasColumnName("PH_ProductBarcode");
            entity.Property(e => e.PH_WeighingScaleData)
                .HasPrecision(64, 5)
                .HasColumnName("PH_WeighingScaleData");
            entity.Property(e => e.SD_EndingDate)
                .HasColumnType("date")
                .HasColumnName("SD_EndingDate");
            entity.Property(e => e.SD_ItemNo)
                .HasMaxLength(10)
                .HasColumnName("SD_ItemNo");
            entity.Property(e => e.SD_LineDiscount)
                .HasPrecision(64, 5)
                .HasColumnName("SD_LineDiscount%");
            entity.Property(e => e.SD_SalesCode)
                .HasMaxLength(20)
                .HasColumnName("SD_SalesCode");
            entity.Property(e => e.SD_StartingDate)
                .HasColumnType("date")
                .HasColumnName("SD_StartingDate");
            entity.Property(e => e.SD_UnitOfMeasureCode)
                .HasMaxLength(10)
                .HasColumnName("SD_UnitOfMeasureCode");
            entity.Property(e => e.SP_BarcodeFormat)
                .HasMaxLength(26)
                .HasDefaultValueSql("''")
                .HasColumnName("SP_Barcode_Format");
            entity.Property(e => e.SP_ChineseLabelDescription)
                .HasMaxLength(20)
                .HasColumnName("SP_ChineseLabelDescription");
            entity.Property(e => e.SP_Currencycode)
                .HasMaxLength(10)
                .HasColumnName("SP_CURRENCYCODE");
            entity.Property(e => e.SP_CustomerSku)
                .HasMaxLength(18)
                .HasColumnName("SP_CustomerSKU");
            entity.Property(e => e.SP_EndingDate)
                .HasColumnType("date")
                .HasColumnName("SP_EndingDate");
            entity.Property(e => e.SP_EnglishLabelDescription)
                .HasMaxLength(60)
                .HasColumnName("SP_EnglishLabelDescription");
            entity.Property(e => e.SP_Hidden).HasColumnName("SP_Hidden");
            entity.Property(e => e.SP_ItemNo)
                .HasMaxLength(10)
                .HasColumnName("SP_ItemNo");
            entity.Property(e => e.SP_LabelSize)
                .HasMaxLength(20)
                .HasColumnName("SP_LabelSize");
            entity.Property(e => e.SP_LabelUnitOfMeasure)
                .HasMaxLength(15)
                .HasColumnName("SP_LabelUnitOfMeasure");
            entity.Property(e => e.SP_MalayLabelDescription)
                .HasMaxLength(60)
                .HasColumnName("SP_MalayLabelDescription");
            entity.Property(e => e.SP_ProductBarcode)
                .HasMaxLength(18)
                .HasDefaultValueSql("''")
                .HasColumnName("SP_ProductBarcode");
            entity.Property(e => e.SP_SalesCode)
                .HasMaxLength(20)
                .HasColumnName("SP_SalesCode");
            entity.Property(e => e.SP_StartingDate)
                .HasColumnType("date")
                .HasColumnName("SP_StartingDate");
            entity.Property(e => e.SP_UnitOfMeasureCode)
                .HasMaxLength(10)
                .HasColumnName("SP_UnitOfMeasureCode");
            entity.Property(e => e.SP_UnitPrice)
                .HasPrecision(64, 5)
                .HasColumnName("SP_UnitPrice");
            entity.Property(e => e.SP_WeightItem).HasColumnName("SP_WeightItem");
            entity.Property(e => e.SP_WeightScale).HasColumnName("SP_WeightScale");
        });

        modelBuilder.Entity<Specialdiscount_Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("special_discount");

            entity.HasIndex(e => e.ItemNo, "ItemNo");

            entity.HasIndex(e => new { e.SalesCode, e.ItemNo, e.UnitOfMeasureCode, e.LineDiscount, e.StartingDate }, "SalesCode").IsUnique();

            entity.HasIndex(e => e.UnitOfMeasureCode, "UnitOfMeasureCode");

            entity.Property(e => e.EndingDate).HasColumnType("date");
            entity.Property(e => e.ItemNo).HasMaxLength(10);
            entity.Property(e => e.LineDiscount)
                .HasPrecision(64, 5)
                .HasColumnName("LineDiscount%");
            entity.Property(e => e.SalesCode).HasMaxLength(20);
            entity.Property(e => e.StartingDate).HasColumnType("date");
            entity.Property(e => e.UnitOfMeasureCode).HasMaxLength(10);
        });

        modelBuilder.Entity<Specialprice_Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("special_price");

            entity.HasIndex(e => e.CurrencyCode, "CURRENCYCODE");

            entity.HasIndex(e => e.ItemNo, "ItemNo");

            entity.HasIndex(e => e.LabelSize, "LabelSize");

            entity.HasIndex(e => new { e.SalesCode, e.ItemNo, e.UnitOfMeasureCode, e.StartingDate }, "SalesCode").IsUnique();

            entity.HasIndex(e => e.UnitOfMeasureCode, "UnitOfMeasureCode");

            entity.Property(e => e.Barcode_Format)
                .HasMaxLength(26)
                .HasDefaultValueSql("''")
                .HasColumnName("Barcode_Format");
            entity.Property(e => e.ChineseLabelDescription).HasMaxLength(20);
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(10)
                .HasColumnName("CURRENCYCODE");
            entity.Property(e => e.CustomerSKU)
                .HasMaxLength(18)
                .HasColumnName("CustomerSKU");
            entity.Property(e => e.EndingDate).HasColumnType("date");
            entity.Property(e => e.EnglishLabelDescription).HasMaxLength(60);
            entity.Property(e => e.ItemNo).HasMaxLength(10);
            entity.Property(e => e.LabelSize).HasMaxLength(20);
            entity.Property(e => e.LabelUnitOfMeasure).HasMaxLength(15);
            entity.Property(e => e.MalayLabelDescription).HasMaxLength(60);
            entity.Property(e => e.ProductBarcode)
                .HasMaxLength(18)
                .HasDefaultValueSql("''");
            entity.Property(e => e.SalesCode).HasMaxLength(20);
            entity.Property(e => e.StartingDate).HasColumnType("date");
            entity.Property(e => e.UnitOfMeasureCode).HasMaxLength(10);
            entity.Property(e => e.UnitPrice).HasPrecision(64, 5);
        });

        modelBuilder.Entity<Unitsofmeasure_Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("units_of_measure");

            entity.HasIndex(e => e.Code, "CODE").IsUnique();

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("CODE");
            entity.Property(e => e.Description).HasMaxLength(10);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
