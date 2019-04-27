using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminSystem.Api.Infrastructure.Migrations
{
    public partial class InitApplicationContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Zmn_Ac_Menus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MenuNo = table.Column<string>(nullable: true),
                    PMenuNo = table.Column<string>(nullable: true),
                    MenuName = table.Column<string>(nullable: true),
                    Order = table.Column<string>(nullable: true),
                    MenuUrl = table.Column<string>(nullable: true),
                    MenuIcon = table.Column<string>(nullable: true),
                    IsVisible = table.Column<bool>(nullable: false),
                    DeepPath = table.Column<string>(nullable: true),
                    AlwaysShow = table.Column<bool>(nullable: false),
                    NoCache = table.Column<bool>(nullable: false),
                    Affix = table.Column<bool>(nullable: false),
                    PlatformType = table.Column<int>(nullable: false),
                    MenuFuntionType = table.Column<int>(nullable: false),
                    UpdateUserId = table.Column<int>(nullable: false),
                    UpdateDateTime = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zmn_Ac_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zmn_Ac_Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(nullable: true),
                    RoleDescr = table.Column<string>(nullable: true),
                    UpdateUserId = table.Column<int>(nullable: false),
                    UpdateDateTime = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zmn_Ac_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zmn_Ac_Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Pwd = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Address_Street = table.Column<string>(nullable: true),
                    Address_City = table.Column<string>(nullable: true),
                    Address_Province = table.Column<string>(nullable: true),
                    Address_Country = table.Column<string>(nullable: true),
                    Address_FullAddress = table.Column<string>(nullable: true),
                    UpdateUserId = table.Column<int>(nullable: false),
                    UpdateDateTime = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zmn_Ac_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zmn_Ac_Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: true),
                    RoleId = table.Column<int>(nullable: true),
                    MenuNo = table.Column<string>(nullable: true),
                    PermissionType = table.Column<int>(nullable: false),
                    PlatformType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zmn_Ac_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zmn_Ac_Permissions_Zmn_Ac_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Zmn_Ac_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zmn_Ac_Permissions_Zmn_Ac_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Zmn_Ac_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Zmn_Ac_UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zmn_Ac_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zmn_Ac_UserRoles_Zmn_Ac_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Zmn_Ac_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zmn_Ac_UserRoles_Zmn_Ac_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Zmn_Ac_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zmn_Ac_Menus_DeepPath",
                table: "Zmn_Ac_Menus",
                column: "DeepPath");

            migrationBuilder.CreateIndex(
                name: "IX_Zmn_Ac_Menus_MenuNo",
                table: "Zmn_Ac_Menus",
                column: "MenuNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Zmn_Ac_Menus_PMenuNo",
                table: "Zmn_Ac_Menus",
                column: "PMenuNo");

            migrationBuilder.CreateIndex(
                name: "IX_Zmn_Ac_Permissions_RoleId",
                table: "Zmn_Ac_Permissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Zmn_Ac_Permissions_UserId",
                table: "Zmn_Ac_Permissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Zmn_Ac_UserRoles_RoleId",
                table: "Zmn_Ac_UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Zmn_Ac_UserRoles_UserId",
                table: "Zmn_Ac_UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Zmn_Ac_Users_Phone",
                table: "Zmn_Ac_Users",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Zmn_Ac_Users_UserName",
                table: "Zmn_Ac_Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zmn_Ac_Menus");

            migrationBuilder.DropTable(
                name: "Zmn_Ac_Permissions");

            migrationBuilder.DropTable(
                name: "Zmn_Ac_UserRoles");

            migrationBuilder.DropTable(
                name: "Zmn_Ac_Roles");

            migrationBuilder.DropTable(
                name: "Zmn_Ac_Users");
        }
    }
}
