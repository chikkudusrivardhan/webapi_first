using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.Migrations
{
    /// <inheritdoc />
    public partial class seedData3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 11);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Age", "PasswordHash", "PasswordSalt", "Role", "UserName" },
                values: new object[] { 12, 21, new byte[] { 149, 247, 39, 241, 169, 44, 200, 177, 173, 96, 151, 223, 68, 137, 193, 230, 50, 89, 244, 24, 158, 30, 196, 15, 185, 108, 139, 118, 77, 148, 106, 151 }, new byte[] { 81, 168, 20, 234, 62, 195, 162, 25, 138, 88, 201, 74, 40, 222, 35, 74, 243, 248, 5, 169, 219, 73, 187, 147, 156, 91, 58, 156, 241, 7, 249, 62, 99, 204, 101, 106, 102, 47, 30, 222, 132, 239, 176, 59, 44, 56, 81, 14, 141, 165, 107, 122, 26, 5, 190, 208, 58, 92, 26, 230, 218, 80, 27, 56 }, 1, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 12);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Age", "PasswordHash", "PasswordSalt", "Role", "UserName" },
                values: new object[] { 11, 21, new byte[] { 149, 247, 39, 241, 169, 44, 200, 177, 173, 96, 151, 223, 68, 137, 193, 230, 50, 89, 244, 24, 158, 30, 196, 15, 185, 108, 139, 118, 77, 148, 106, 151 }, new byte[] { 220, 109, 161, 61, 209, 103, 150, 133, 183, 191, 220, 45, 100, 156, 3, 87, 111, 51, 186, 250, 169, 192, 52, 40, 30, 121, 106, 11, 126, 163, 76, 167, 76, 31, 52, 81, 96, 244, 146, 213, 8, 41, 183, 160, 84, 108, 110, 69, 203, 164, 20, 244, 15, 145, 217, 136, 27, 127, 170, 191, 84, 26, 223, 75 }, 1, "Admin" });
        }
    }
}
