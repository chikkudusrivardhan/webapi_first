using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.Migrations
{
    /// <inheritdoc />
    public partial class seedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 10);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Age", "PasswordHash", "PasswordSalt", "Role", "UserName" },
                values: new object[] { 11, 21, new byte[] { 149, 247, 39, 241, 169, 44, 200, 177, 173, 96, 151, 223, 68, 137, 193, 230, 50, 89, 244, 24, 158, 30, 196, 15, 185, 108, 139, 118, 77, 148, 106, 151 }, new byte[] { 220, 109, 161, 61, 209, 103, 150, 133, 183, 191, 220, 45, 100, 156, 3, 87, 111, 51, 186, 250, 169, 192, 52, 40, 30, 121, 106, 11, 126, 163, 76, 167, 76, 31, 52, 81, 96, 244, 146, 213, 8, 41, 183, 160, 84, 108, 110, 69, 203, 164, 20, 244, 15, 145, 217, 136, 27, 127, 170, 191, 84, 26, 223, 75 }, 1, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 11);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Age", "PasswordHash", "PasswordSalt", "Role", "UserName" },
                values: new object[] { 10, 21, new byte[] { 188, 228, 216, 143, 254, 255, 190, 51, 121, 72, 76, 218, 246, 184, 133, 39, 31, 245, 42, 66, 35, 160, 104, 65, 173, 42, 170, 101, 18, 3, 46, 26 }, new byte[] { 220, 109, 161, 61, 209, 103, 150, 133, 183, 191, 220, 45, 100, 156, 3, 87, 111, 51, 186, 250, 169, 192, 52, 40, 30, 121, 106, 11, 126, 163, 76, 167, 76, 31, 52, 81, 96, 244, 146, 213, 8, 41, 183, 160, 84, 108, 110, 69, 203, 164, 20, 244, 15, 145, 217, 136, 27, 127, 170, 191, 84, 26, 223, 75 }, 1, "Admins" });
        }
    }
}
