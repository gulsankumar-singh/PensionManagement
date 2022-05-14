﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PensionerDetailModule.AppDbContext;

namespace PensionerDetailModule.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PensionerDetailModule.Models.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BankType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Banks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BankName = "HDFC Bank Ltd.",
                            BankType = 1
                        },
                        new
                        {
                            Id = 2,
                            BankName = "State Bank of India",
                            BankType = 0
                        },
                        new
                        {
                            Id = 3,
                            BankName = "ICICI Bank Ltd.",
                            BankType = 1
                        },
                        new
                        {
                            Id = 4,
                            BankName = "Kotak Mahindra Bank",
                            BankType = 1
                        },
                        new
                        {
                            Id = 5,
                            BankName = "Axis Bank Ltd.",
                            BankType = 1
                        },
                        new
                        {
                            Id = 6,
                            BankName = "Indusland Bank Ltd.",
                            BankType = 1
                        },
                        new
                        {
                            Id = 7,
                            BankName = "Yes Bank Ltd.",
                            BankType = 1
                        },
                        new
                        {
                            Id = 8,
                            BankName = "Panjab National Bank",
                            BankType = 0
                        },
                        new
                        {
                            Id = 9,
                            BankName = "Bank of Baroda",
                            BankType = 0
                        },
                        new
                        {
                            Id = 10,
                            BankName = "Bank of India",
                            BankType = 0
                        });
                });

            modelBuilder.Entity("PensionerDetailModule.Models.Pensioner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AadharNumber")
                        .HasColumnType("bigint");

                    b.Property<long>("AccountNumber")
                        .HasColumnType("bigint");

                    b.Property<long>("Allowances")
                        .HasColumnType("bigint");

                    b.Property<int>("BankId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PanNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PensionType")
                        .HasColumnType("int");

                    b.Property<long>("SalaryEarned")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("Pensioners");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AadharNumber = 791714947214L,
                            AccountNumber = 968557297810L,
                            Allowances = 19292L,
                            BankId = 5,
                            DateOfBirth = new DateTime(2000, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Ajay",
                            PanNumber = "KSMOC9374L",
                            PensionType = 0,
                            SalaryEarned = 436986L
                        },
                        new
                        {
                            Id = 2,
                            AadharNumber = 854728326906L,
                            AccountNumber = 81730197274L,
                            Allowances = 26704L,
                            BankId = 1,
                            DateOfBirth = new DateTime(1991, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Sourav",
                            PanNumber = "KDRTI7666A",
                            PensionType = 0,
                            SalaryEarned = 468418L
                        },
                        new
                        {
                            Id = 3,
                            AadharNumber = 974455078114L,
                            AccountNumber = 40740232906L,
                            Allowances = 36476L,
                            BankId = 9,
                            DateOfBirth = new DateTime(1974, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Sameer",
                            PanNumber = "ZWKOF2689R",
                            PensionType = 1,
                            SalaryEarned = 865912L
                        },
                        new
                        {
                            Id = 4,
                            AadharNumber = 461755449180L,
                            AccountNumber = 467028762867L,
                            Allowances = 31476L,
                            BankId = 5,
                            DateOfBirth = new DateTime(1980, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Vivaan",
                            PanNumber = "IXSUT5186S",
                            PensionType = 0,
                            SalaryEarned = 925642L
                        },
                        new
                        {
                            Id = 5,
                            AadharNumber = 389444809498L,
                            AccountNumber = 560710767357L,
                            Allowances = 1406L,
                            BankId = 7,
                            DateOfBirth = new DateTime(2002, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Saket",
                            PanNumber = "UOYCV0783O",
                            PensionType = 0,
                            SalaryEarned = 885577L
                        },
                        new
                        {
                            Id = 6,
                            AadharNumber = 937162602552L,
                            AccountNumber = 315827677945L,
                            Allowances = 38035L,
                            BankId = 4,
                            DateOfBirth = new DateTime(1991, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Rahul",
                            PanNumber = "GPPVI6213L",
                            PensionType = 1,
                            SalaryEarned = 619564L
                        },
                        new
                        {
                            Id = 7,
                            AadharNumber = 287244420393L,
                            AccountNumber = 2473327567L,
                            Allowances = 45100L,
                            BankId = 3,
                            DateOfBirth = new DateTime(1957, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Rohan",
                            PanNumber = "BXLEY3454O",
                            PensionType = 1,
                            SalaryEarned = 593845L
                        },
                        new
                        {
                            Id = 8,
                            AadharNumber = 874313859213L,
                            AccountNumber = 779371944069L,
                            Allowances = 29185L,
                            BankId = 4,
                            DateOfBirth = new DateTime(1987, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Pragya",
                            PanNumber = "GOWUN8949C",
                            PensionType = 0,
                            SalaryEarned = 461227L
                        },
                        new
                        {
                            Id = 9,
                            AadharNumber = 789953042547L,
                            AccountNumber = 265723038859L,
                            Allowances = 5890L,
                            BankId = 10,
                            DateOfBirth = new DateTime(1994, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Kunal",
                            PanNumber = "QKSXL5968C",
                            PensionType = 0,
                            SalaryEarned = 520820L
                        },
                        new
                        {
                            Id = 10,
                            AadharNumber = 337877837167L,
                            AccountNumber = 639415907726L,
                            Allowances = 14845L,
                            BankId = 8,
                            DateOfBirth = new DateTime(1995, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Anjali",
                            PanNumber = "VOVUQ4952B",
                            PensionType = 1,
                            SalaryEarned = 593084L
                        },
                        new
                        {
                            Id = 11,
                            AadharNumber = 888861335475L,
                            AccountNumber = 297580783520L,
                            Allowances = 39151L,
                            BankId = 2,
                            DateOfBirth = new DateTime(1994, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Ranjeet",
                            PanNumber = "BIOCM9234K",
                            PensionType = 1,
                            SalaryEarned = 752833L
                        },
                        new
                        {
                            Id = 12,
                            AadharNumber = 602310740678L,
                            AccountNumber = 943925554740L,
                            Allowances = 4419L,
                            BankId = 1,
                            DateOfBirth = new DateTime(1957, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Shubham",
                            PanNumber = "AJNZV5132D",
                            PensionType = 1,
                            SalaryEarned = 507340L
                        },
                        new
                        {
                            Id = 13,
                            AadharNumber = 869852758857L,
                            AccountNumber = 571881193550L,
                            Allowances = 3807L,
                            BankId = 5,
                            DateOfBirth = new DateTime(1984, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Sunidhi",
                            PanNumber = "OYGXW4443E",
                            PensionType = 0,
                            SalaryEarned = 866978L
                        },
                        new
                        {
                            Id = 14,
                            AadharNumber = 150242523686L,
                            AccountNumber = 657059136087L,
                            Allowances = 41707L,
                            BankId = 3,
                            DateOfBirth = new DateTime(1963, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Rohit",
                            PanNumber = "YIUGX7931I",
                            PensionType = 0,
                            SalaryEarned = 810046L
                        },
                        new
                        {
                            Id = 15,
                            AadharNumber = 960272640642L,
                            AccountNumber = 162572811085L,
                            Allowances = 14068L,
                            BankId = 6,
                            DateOfBirth = new DateTime(1976, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Aditya",
                            PanNumber = "UNVHI8166X",
                            PensionType = 0,
                            SalaryEarned = 710003L
                        },
                        new
                        {
                            Id = 16,
                            AadharNumber = 471714613539L,
                            AccountNumber = 204687301318L,
                            Allowances = 37704L,
                            BankId = 9,
                            DateOfBirth = new DateTime(1996, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Gulshan",
                            PanNumber = "ZEOSA8100V",
                            PensionType = 1,
                            SalaryEarned = 869460L
                        },
                        new
                        {
                            Id = 17,
                            AadharNumber = 653515346579L,
                            AccountNumber = 23368227618L,
                            Allowances = 24871L,
                            BankId = 1,
                            DateOfBirth = new DateTime(1978, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Ram",
                            PanNumber = "YOLLZ3272S",
                            PensionType = 1,
                            SalaryEarned = 606992L
                        },
                        new
                        {
                            Id = 18,
                            AadharNumber = 930139439179L,
                            AccountNumber = 713766531809L,
                            Allowances = 4976L,
                            BankId = 2,
                            DateOfBirth = new DateTime(1975, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Ujjwal",
                            PanNumber = "QAQBL8395S",
                            PensionType = 0,
                            SalaryEarned = 575598L
                        },
                        new
                        {
                            Id = 19,
                            AadharNumber = 280225974218L,
                            AccountNumber = 349195618403L,
                            Allowances = 28821L,
                            BankId = 8,
                            DateOfBirth = new DateTime(1999, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Akash",
                            PanNumber = "YEBSQ6580R",
                            PensionType = 0,
                            SalaryEarned = 629579L
                        },
                        new
                        {
                            Id = 20,
                            AadharNumber = 643359853704L,
                            AccountNumber = 972016531683L,
                            Allowances = 40949L,
                            BankId = 9,
                            DateOfBirth = new DateTime(1991, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Sunita",
                            PanNumber = "OKOKS6038K",
                            PensionType = 1,
                            SalaryEarned = 871959L
                        });
                });

            modelBuilder.Entity("PensionerDetailModule.Models.Pensioner", b =>
                {
                    b.HasOne("PensionerDetailModule.Models.Bank", "BankDetail")
                        .WithMany()
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankDetail");
                });
#pragma warning restore 612, 618
        }
    }
}
