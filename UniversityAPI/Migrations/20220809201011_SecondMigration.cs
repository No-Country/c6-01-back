using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityAPI.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_careers_faculty_FacultyId",
                table: "careers");

            migrationBuilder.DropForeignKey(
                name: "FK_comment_careers_CareerId",
                table: "comment");

            migrationBuilder.DropForeignKey(
                name: "FK_faculty_universities_UniversityId",
                table: "faculty");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_careers_CareerId",
                table: "Stats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_universities",
                table: "universities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_faculty",
                table: "faculty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_comment",
                table: "comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_careers",
                table: "careers");

            migrationBuilder.RenameTable(
                name: "universities",
                newName: "Universities");

            migrationBuilder.RenameTable(
                name: "faculty",
                newName: "Faculty");

            migrationBuilder.RenameTable(
                name: "comment",
                newName: "Comment");

            migrationBuilder.RenameTable(
                name: "careers",
                newName: "Careers");

            migrationBuilder.RenameIndex(
                name: "IX_faculty_UniversityId",
                table: "Faculty",
                newName: "IX_Faculty_UniversityId");

            migrationBuilder.RenameIndex(
                name: "IX_comment_CareerId",
                table: "Comment",
                newName: "IX_Comment_CareerId");

            migrationBuilder.RenameIndex(
                name: "IX_careers_FacultyId",
                table: "Careers",
                newName: "IX_Careers_FacultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Universities",
                table: "Universities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faculty",
                table: "Faculty",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Careers",
                table: "Careers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Careers_Faculty_FacultyId",
                table: "Careers",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Careers_CareerId",
                table: "Comment",
                column: "CareerId",
                principalTable: "Careers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Faculty_Universities_UniversityId",
                table: "Faculty",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Careers_CareerId",
                table: "Stats",
                column: "CareerId",
                principalTable: "Careers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Careers_Faculty_FacultyId",
                table: "Careers");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Careers_CareerId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Faculty_Universities_UniversityId",
                table: "Faculty");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Careers_CareerId",
                table: "Stats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Universities",
                table: "Universities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faculty",
                table: "Faculty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Careers",
                table: "Careers");

            migrationBuilder.RenameTable(
                name: "Universities",
                newName: "universities");

            migrationBuilder.RenameTable(
                name: "Faculty",
                newName: "faculty");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "comment");

            migrationBuilder.RenameTable(
                name: "Careers",
                newName: "careers");

            migrationBuilder.RenameIndex(
                name: "IX_Faculty_UniversityId",
                table: "faculty",
                newName: "IX_faculty_UniversityId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CareerId",
                table: "comment",
                newName: "IX_comment_CareerId");

            migrationBuilder.RenameIndex(
                name: "IX_Careers_FacultyId",
                table: "careers",
                newName: "IX_careers_FacultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_universities",
                table: "universities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_faculty",
                table: "faculty",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_comment",
                table: "comment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_careers",
                table: "careers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_careers_faculty_FacultyId",
                table: "careers",
                column: "FacultyId",
                principalTable: "faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comment_careers_CareerId",
                table: "comment",
                column: "CareerId",
                principalTable: "careers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_faculty_universities_UniversityId",
                table: "faculty",
                column: "UniversityId",
                principalTable: "universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_careers_CareerId",
                table: "Stats",
                column: "CareerId",
                principalTable: "careers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
