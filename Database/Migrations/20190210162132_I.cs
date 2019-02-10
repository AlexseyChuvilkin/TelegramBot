using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class I : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    InviteString = table.Column<string>(nullable: true),
                    StartEducation = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SubjectCalls",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StartLesson = table.Column<TimeSpan>(nullable: false),
                    EndLesson = table.Column<TimeSpan>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectCalls", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TelegramID = table.Column<int>(nullable: false),
                    GroupID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleFields",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Order = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    ParitySubjectInstanceID = table.Column<int>(nullable: true),
                    NotParitySubjectInstanceID = table.Column<int>(nullable: true),
                    SubjectID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleFields", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ScheduleFields_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectInstances",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SubjectType = table.Column<int>(nullable: false),
                    Audience = table.Column<string>(nullable: false),
                    Teacher = table.Column<string>(nullable: false),
                    SubjectID = table.Column<int>(nullable: true),
                    ScheduleSubjectID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectInstances", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubjectInstances_ScheduleFields_ScheduleSubjectID",
                        column: x => x.ScheduleSubjectID,
                        principalTable: "ScheduleFields",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectInstances_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Name",
                table: "Groups",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleFields_NotParitySubjectInstanceID",
                table: "ScheduleFields",
                column: "NotParitySubjectInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleFields_ParitySubjectInstanceID",
                table: "ScheduleFields",
                column: "ParitySubjectInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleFields_SubjectID",
                table: "ScheduleFields",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleFields_GroupID",
                table: "ScheduleFields",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectInstances_ScheduleSubjectID",
                table: "SubjectInstances",
                column: "ScheduleSubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectInstances_SubjectID",
                table: "SubjectInstances",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupID",
                table: "Users",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TelegramID",
                table: "Users",
                column: "TelegramID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleFields_SubjectInstances_NotParitySubjectInstanceID",
                table: "ScheduleFields",
                column: "NotParitySubjectInstanceID",
                principalTable: "SubjectInstances",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleFields_SubjectInstances_ParitySubjectInstanceID",
                table: "ScheduleFields",
                column: "ParitySubjectInstanceID",
                principalTable: "SubjectInstances",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleFields_SubjectInstances_SubjectID",
                table: "ScheduleFields",
                column: "SubjectID",
                principalTable: "SubjectInstances",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleFields_SubjectInstances_NotParitySubjectInstanceID",
                table: "ScheduleFields");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleFields_SubjectInstances_ParitySubjectInstanceID",
                table: "ScheduleFields");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleFields_SubjectInstances_SubjectID",
                table: "ScheduleFields");

            migrationBuilder.DropTable(
                name: "SubjectCalls");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SubjectInstances");

            migrationBuilder.DropTable(
                name: "ScheduleFields");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
