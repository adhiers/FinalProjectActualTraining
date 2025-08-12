using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    CarId = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    ModelType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    FuelType = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    BasePrice = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Car__68A0342E2DDE18FD", x => x.CarId);
                });

            migrationBuilder.CreateTable(
                name: "Dealer",
                columns: table => new
                {
                    DealerId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DealerName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DealerAddress = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Province = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TaxRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Dealer__CA2F8EB23B9D453B", x => x.DealerId);
                });

            migrationBuilder.CreateTable(
                name: "Guest",
                columns: table => new
                {
                    GuestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Guest__0C423C12FC1E9990", x => x.GuestId);
                });

            migrationBuilder.CreateTable(
                name: "LetterOfIntent",
                columns: table => new
                {
                    LOIId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    BookingFee = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LetterOf__E21E1B4C9E363136", x => x.LOIId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DealerCar",
                columns: table => new
                {
                    DealerCarId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CarId = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    DealerId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DealerCarPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DealerCa__7DD0B586BE23DD5E", x => x.DealerCarId);
                    table.ForeignKey(
                        name: "FK__DealerCar__CarId__4BAC3F29",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "CarId");
                    table.ForeignKey(
                        name: "FK__DealerCar__Deale__4CA06362",
                        column: x => x.DealerId,
                        principalTable: "Dealer",
                        principalColumn: "DealerId");
                });

            migrationBuilder.CreateTable(
                name: "SalesPerson",
                columns: table => new
                {
                    SPId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    SalesName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DealerId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SalesPer__F4306129AC01A97B", x => x.SPId);
                    table.ForeignKey(
                        name: "FK__SalesPers__Deale__3D5E1FD2",
                        column: x => x.DealerId,
                        principalTable: "Dealer",
                        principalColumn: "DealerId");
                });

            migrationBuilder.CreateTable(
                name: "Scheduling",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestId = table.Column<int>(type: "int", nullable: false),
                    DealerId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Program = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    AvailableStart = table.Column<DateTime>(type: "datetime", nullable: false),
                    AvailableEnd = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Scheduli__9C8A5B4930CCD65E", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK__Schedulin__Deale__4222D4EF",
                        column: x => x.DealerId,
                        principalTable: "Dealer",
                        principalColumn: "DealerId");
                    table.ForeignKey(
                        name: "FK__Schedulin__Guest__412EB0B6",
                        column: x => x.GuestId,
                        principalTable: "Guest",
                        principalColumn: "GuestId");
                });

            migrationBuilder.CreateTable(
                name: "Consultation",
                columns: table => new
                {
                    ConsultId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: true),
                    SPId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CustomerBudget = table.Column<int>(type: "int", nullable: false),
                    ConsultDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Consulta__28859B35C3BE7640", x => x.ConsultId);
                    table.ForeignKey(
                        name: "FK__Consultat__Sched__45F365D3",
                        column: x => x.ScheduleId,
                        principalTable: "Scheduling",
                        principalColumn: "ScheduleId");
                    table.ForeignKey(
                        name: "FK__Consultati__SPId__46E78A0C",
                        column: x => x.SPId,
                        principalTable: "SalesPerson",
                        principalColumn: "SPId");
                });

            migrationBuilder.CreateTable(
                name: "TestDrive",
                columns: table => new
                {
                    TDId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ConsultId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    ScheduleId = table.Column<int>(type: "int", nullable: true),
                    DealerCarId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    SPId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    TDDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TestDriv__B7317AA4CBF1953F", x => x.TDId);
                    table.ForeignKey(
                        name: "FK__TestDrive__Consu__5070F446",
                        column: x => x.ConsultId,
                        principalTable: "Consultation",
                        principalColumn: "ConsultId");
                    table.ForeignKey(
                        name: "FK__TestDrive__Deale__534D60F1",
                        column: x => x.DealerCarId,
                        principalTable: "DealerCar",
                        principalColumn: "DealerCarId");
                    table.ForeignKey(
                        name: "FK__TestDrive__SPId__52593CB8",
                        column: x => x.SPId,
                        principalTable: "SalesPerson",
                        principalColumn: "SPId");
                    table.ForeignKey(
                        name: "FK__TestDrive__Sched__5165187F",
                        column: x => x.ScheduleId,
                        principalTable: "Scheduling",
                        principalColumn: "ScheduleId");
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    TDId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    IdCardNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__A4AE64D8565529E2", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK__Customer__TDId__571DF1D5",
                        column: x => x.TDId,
                        principalTable: "TestDrive",
                        principalColumn: "TDId");
                });

            migrationBuilder.CreateTable(
                name: "Credit",
                columns: table => new
                {
                    CreditId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CustomerId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    NominalKredit = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Bunga = table.Column<int>(type: "int", nullable: false),
                    MonthlyPayment = table.Column<int>(type: "int", nullable: false),
                    StatusCredit = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Credit__ED5ED0BB4FDED891", x => x.CreditId);
                    table.ForeignKey(
                        name: "FK__Credit__Customer__5EBF139D",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "OrderCust",
                columns: table => new
                {
                    OCId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CustomerId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DCId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    LOIId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderCus__A2E5E9FC1A764012", x => x.OCId);
                    table.ForeignKey(
                        name: "FK__OrderCust__Custo__59FA5E80",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK__OrderCust__DCId__5AEE82B9",
                        column: x => x.DCId,
                        principalTable: "DealerCar",
                        principalColumn: "DealerCarId");
                    table.ForeignKey(
                        name: "FK__OrderCust__LOIId__5BE2A6F2",
                        column: x => x.LOIId,
                        principalTable: "LetterOfIntent",
                        principalColumn: "LOIId");
                });

            migrationBuilder.CreateTable(
                name: "Agreement",
                columns: table => new
                {
                    AgreementId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CustomerId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    SPId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CreditId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    DealerCarId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    LOIId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    MethodPayment = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    AgreementDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Agreemen__0A3082C3FD860975", x => x.AgreementId);
                    table.ForeignKey(
                        name: "FK__Agreement__Credi__6383C8BA",
                        column: x => x.CreditId,
                        principalTable: "Credit",
                        principalColumn: "CreditId");
                    table.ForeignKey(
                        name: "FK__Agreement__Custo__628FA481",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK__Agreement__Deale__66603565",
                        column: x => x.DealerCarId,
                        principalTable: "DealerCar",
                        principalColumn: "DealerCarId");
                    table.ForeignKey(
                        name: "FK__Agreement__LOIId__6477ECF3",
                        column: x => x.LOIId,
                        principalTable: "LetterOfIntent",
                        principalColumn: "LOIId");
                    table.ForeignKey(
                        name: "FK__Agreement__SPId__656C112C",
                        column: x => x.SPId,
                        principalTable: "SalesPerson",
                        principalColumn: "SPId");
                });

            migrationBuilder.CreateTable(
                name: "OtherBenefit",
                columns: table => new
                {
                    AgreementId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Benefit = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OtherBen__0A3082C32F234FA7", x => x.AgreementId);
                    table.ForeignKey(
                        name: "FK__OtherBene__Agree__6C190EBB",
                        column: x => x.AgreementId,
                        principalTable: "Agreement",
                        principalColumn: "AgreementId");
                });

            migrationBuilder.CreateTable(
                name: "PaymentHistory",
                columns: table => new
                {
                    PaymentId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    AgreementId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Nominal = table.Column<int>(type: "int", nullable: false),
                    PaymentLeft = table.Column<int>(type: "int", nullable: true),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentH__9B556A38248B13A0", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK__PaymentHi__Agree__693CA210",
                        column: x => x.AgreementId,
                        principalTable: "Agreement",
                        principalColumn: "AgreementId");
                });

            migrationBuilder.CreateTable(
                name: "Warranty",
                columns: table => new
                {
                    WarrantyId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    AgreementId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    WarrantyPeriodDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Warranty__2ED31813A2600B22", x => x.WarrantyId);
                    table.ForeignKey(
                        name: "FK__Warranty__Agreem__6FE99F9F",
                        column: x => x.AgreementId,
                        principalTable: "Agreement",
                        principalColumn: "AgreementId");
                });

            migrationBuilder.CreateTable(
                name: "WarrantyClaim",
                columns: table => new
                {
                    ClaimId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    WarrantyId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ServiceCenter = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ServiceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ServiceCost = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Warranty__EF2E139BB6417D70", x => x.ClaimId);
                    table.ForeignKey(
                        name: "FK__WarrantyC__Warra__72C60C4A",
                        column: x => x.WarrantyId,
                        principalTable: "Warranty",
                        principalColumn: "WarrantyId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_CreditId",
                table: "Agreement",
                column: "CreditId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_CustomerId",
                table: "Agreement",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_DealerCarId",
                table: "Agreement",
                column: "DealerCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_SPId",
                table: "Agreement",
                column: "SPId");

            migrationBuilder.CreateIndex(
                name: "UQ__Agreemen__E21E1B4DF2A39B1F",
                table: "Agreement",
                column: "LOIId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Consultation_SPId",
                table: "Consultation",
                column: "SPId");

            migrationBuilder.CreateIndex(
                name: "UQ__Consulta__9C8A5B485E7C1B57",
                table: "Consultation",
                column: "ScheduleId",
                unique: true,
                filter: "[ScheduleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Credit_CustomerId",
                table: "Credit",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "UQ__Customer__B7317AA5C135F192",
                table: "Customer",
                column: "TDId",
                unique: true,
                filter: "[TDId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DealerCar_CarId",
                table: "DealerCar",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_DealerCar_DealerId",
                table: "DealerCar",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCust_CustomerId",
                table: "OrderCust",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCust_DCId",
                table: "OrderCust",
                column: "DCId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCust_LOIId",
                table: "OrderCust",
                column: "LOIId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistory_AgreementId",
                table: "PaymentHistory",
                column: "AgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesPerson_DealerId",
                table: "SalesPerson",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Scheduling_DealerId",
                table: "Scheduling",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Scheduling_GuestId",
                table: "Scheduling",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestDrive_ConsultId",
                table: "TestDrive",
                column: "ConsultId");

            migrationBuilder.CreateIndex(
                name: "IX_TestDrive_DealerCarId",
                table: "TestDrive",
                column: "DealerCarId");

            migrationBuilder.CreateIndex(
                name: "IX_TestDrive_SPId",
                table: "TestDrive",
                column: "SPId");

            migrationBuilder.CreateIndex(
                name: "UQ__TestDriv__9C8A5B481FF293B3",
                table: "TestDrive",
                column: "ScheduleId",
                unique: true,
                filter: "[ScheduleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Warranty__0A3082C2D84397B5",
                table: "Warranty",
                column: "AgreementId",
                unique: true,
                filter: "[AgreementId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyClaim_WarrantyId",
                table: "WarrantyClaim",
                column: "WarrantyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OrderCust");

            migrationBuilder.DropTable(
                name: "OtherBenefit");

            migrationBuilder.DropTable(
                name: "PaymentHistory");

            migrationBuilder.DropTable(
                name: "WarrantyClaim");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Warranty");

            migrationBuilder.DropTable(
                name: "Agreement");

            migrationBuilder.DropTable(
                name: "Credit");

            migrationBuilder.DropTable(
                name: "LetterOfIntent");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "TestDrive");

            migrationBuilder.DropTable(
                name: "Consultation");

            migrationBuilder.DropTable(
                name: "DealerCar");

            migrationBuilder.DropTable(
                name: "Scheduling");

            migrationBuilder.DropTable(
                name: "SalesPerson");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "Guest");

            migrationBuilder.DropTable(
                name: "Dealer");
        }
    }
}
