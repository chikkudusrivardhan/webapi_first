using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.Migrations
{
    /// <inheritdoc />
    public partial class StudentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudent_Course_CoursesCourseId",
                table: "CourseStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudent_Student_StudentsStudentId",
                table: "CourseStudent");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Student_UserId",
                table: "Student",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudent_Course_CoursesCourseId",
                table: "CourseStudent",
                column: "CoursesCourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudent_Student_StudentsStudentId",
                table: "CourseStudent",
                column: "StudentsStudentId",
                principalTable: "Student",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Users_UserId",
                table: "Student",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudent_Course_CoursesCourseId",
                table: "CourseStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudent_Student_StudentsStudentId",
                table: "CourseStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Users_UserId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_UserId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Student");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudent_Course_CoursesCourseId",
                table: "CourseStudent",
                column: "CoursesCourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudent_Student_StudentsStudentId",
                table: "CourseStudent",
                column: "StudentsStudentId",
                principalTable: "Student",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
