using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApp.Migrations
{
    public partial class IdentityRoleSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "91b34f4b-0c03-4d16-8144-3467bd17b94b", "b1968972-2b0f-4c89-9e09-736d209a343a", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "af1360ec-5fe2-4c98-98f5-28938b1eff47", "007d4147-4b1c-4b12-8f02-cf1e44282aff", "Editor", "EDITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f8d8a697-c1cb-4553-bf19-6610c038cfb3", "2e222e23-f996-41f0-9a60-3d7e0026ae1c", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91b34f4b-0c03-4d16-8144-3467bd17b94b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af1360ec-5fe2-4c98-98f5-28938b1eff47");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8d8a697-c1cb-4553-bf19-6610c038cfb3");
        }
    }
}
