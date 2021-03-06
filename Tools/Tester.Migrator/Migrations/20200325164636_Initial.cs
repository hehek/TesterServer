﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Tester.Core.Common;

namespace Tester.Migrator.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "app");

            migrationBuilder.EnsureSchema(
                name: "client");

            migrationBuilder.EnsureSchema(
                name: "report");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:gender", "undefined,man,woman")
                .Annotation("Npgsql:Enum:question_type", "open,multiple_selection,ordered_list,conformity,single_selection")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "role",
                schema: "client",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "client",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Login = table.Column<string>(nullable: false),
                    Password = table.Column<byte[]>(nullable: false),
                    Salt = table.Column<byte[]>(nullable: false),
                    SecurityTimestamp = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    UpdatedUtc = table.Column<DateTime>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "test",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    UpdatedUtc = table.Column<DateTime>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test", x => x.Id);
                    table.ForeignKey(
                        name: "FK_test_user_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "client",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "topic",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true),
                    AuthorId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    UpdatedUtc = table.Column<DateTime>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_topic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_topic_user_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "client",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_topic_topic_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "app",
                        principalTable: "topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_data",
                schema: "client",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Gender = table.Column<Gender>(nullable: false),
                    UpdatedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_data", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_user_data_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "client",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                schema: "client",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_user_role_role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "client",
                        principalTable: "role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_role_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "client",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_test",
                schema: "report",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TestId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ExaminerId = table.Column<Guid>(nullable: true),
                    Time = table.Column<TimeSpan>(nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_test", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_test_user_ExaminerId",
                        column: x => x.ExaminerId,
                        principalSchema: "client",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_test_test_TestId",
                        column: x => x.TestId,
                        principalSchema: "app",
                        principalTable: "test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_test_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "client",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "question",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TopicId = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Hint = table.Column<string>(nullable: true),
                    Type = table.Column<QuestionType>(nullable: false),
                    Answer = table.Column<string>(type: "jsonb", nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_question_user_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "client",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_question_topic_TopicId",
                        column: x => x.TopicId,
                        principalSchema: "app",
                        principalTable: "topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "test_topic",
                schema: "app",
                columns: table => new
                {
                    TestId = table.Column<Guid>(nullable: false),
                    TopicId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_topic", x => new { x.TestId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_test_topic_test_TestId",
                        column: x => x.TestId,
                        principalSchema: "app",
                        principalTable: "test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_test_topic_topic_TopicId",
                        column: x => x.TopicId,
                        principalSchema: "app",
                        principalTable: "topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_answer",
                schema: "report",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserTestId = table.Column<Guid>(nullable: false),
                    QuestionId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(type: "jsonb", nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_answer_question_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "app",
                        principalTable: "question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_answer_user_test_UserTestId",
                        column: x => x.UserTestId,
                        principalSchema: "report",
                        principalTable: "user_test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "client",
                table: "role",
                columns: new[] { "Id", "CreatedUtc", "Name" },
                values: new object[,]
                {
                    { new Guid("f7b718a4-535f-4e91-8b1e-8c65012c8960"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin" },
                    { new Guid("427912c2-1327-463d-a0fc-5e864528aeb0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Moderator" },
                    { new Guid("6cb9dc41-d6b5-4cd9-b0b8-a085f5742449"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lecturer" },
                    { new Guid("3638240a-0a54-42a6-81fb-3393d7336684"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Student" }
                });

            migrationBuilder.InsertData(
                schema: "client",
                table: "user",
                columns: new[] { "Id", "CreatedUtc", "DeletedUtc", "Login", "Password", "Salt", "SecurityTimestamp", "UpdatedUtc" },
                values: new object[] { new Guid("60396f59-dcd2-4045-9029-793df7cee7ea"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin", new byte[] { 252, 29, 38, 134, 205, 60, 211, 7, 6, 166, 238, 109, 252, 219, 114, 230, 31, 160, 3, 66, 95, 52, 43, 254, 67, 79, 170, 23, 245, 25, 139, 232, 83, 176, 200, 234, 163, 191, 147, 143, 72, 111, 76, 207, 76, 172, 183, 154, 145, 160, 161, 114, 55, 204, 25, 110, 216, 186, 201, 126, 123, 70, 31, 253 }, new byte[] { 64, 132, 145, 88, 10, 194, 112, 102, 76, 212, 254, 162, 125, 138, 223, 21, 4, 81, 183, 120, 84, 126, 249, 91, 62, 130, 18, 98, 178, 248, 222, 142, 22, 154, 45, 110, 115, 84, 65, 117, 211, 184, 143, 18, 163, 142, 61, 51, 186, 90, 171, 19, 13, 43, 237, 203, 240, 200, 26, 220, 197, 203, 107, 79, 195, 28, 216, 213, 98, 204, 10, 100, 217, 22, 188, 30, 11, 252, 76, 60, 200, 196, 215, 43, 249, 16, 30, 132, 0, 251, 117, 32, 179, 220, 242, 96, 191, 183, 113, 36, 81, 51, 136, 119, 252, 31, 230, 119, 167, 130, 190, 7, 18, 209, 30, 75, 216, 129, 55, 27, 98, 27, 108, 163, 176, 33, 19, 118 }, new Guid("b73df749-8157-4f3f-880e-a083e0d90b4c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                schema: "client",
                table: "user_data",
                columns: new[] { "UserId", "Gender", "LastName", "Name", "UpdatedUtc" },
                values: new object[] { new Guid("60396f59-dcd2-4045-9029-793df7cee7ea"), Gender.Undefined, null, "admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                schema: "client",
                table: "user_role",
                columns: new[] { "UserId", "RoleId", "CreatedUtc", "DeletedUtc" },
                values: new object[] { new Guid("60396f59-dcd2-4045-9029-793df7cee7ea"), new Guid("f7b718a4-535f-4e91-8b1e-8c65012c8960"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.CreateIndex(
                name: "IX_question_AuthorId",
                schema: "app",
                table: "question",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_question_CreatedUtc",
                schema: "app",
                table: "question",
                column: "CreatedUtc");

            migrationBuilder.CreateIndex(
                name: "IX_question_DeletedUtc",
                schema: "app",
                table: "question",
                column: "DeletedUtc");

            migrationBuilder.CreateIndex(
                name: "IX_question_Name",
                schema: "app",
                table: "question",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_question_TopicId",
                schema: "app",
                table: "question",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_question_Type",
                schema: "app",
                table: "question",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_test_AuthorId",
                schema: "app",
                table: "test",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_test_CreatedUtc",
                schema: "app",
                table: "test",
                column: "CreatedUtc");

            migrationBuilder.CreateIndex(
                name: "IX_test_DeletedUtc",
                schema: "app",
                table: "test",
                column: "DeletedUtc");

            migrationBuilder.CreateIndex(
                name: "IX_test_Name",
                schema: "app",
                table: "test",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_test_topic_DeletedUtc",
                schema: "app",
                table: "test_topic",
                column: "DeletedUtc");

            migrationBuilder.CreateIndex(
                name: "IX_test_topic_TopicId",
                schema: "app",
                table: "test_topic",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_topic_AuthorId",
                schema: "app",
                table: "topic",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_topic_CreatedUtc",
                schema: "app",
                table: "topic",
                column: "CreatedUtc");

            migrationBuilder.CreateIndex(
                name: "IX_topic_DeletedUtc",
                schema: "app",
                table: "topic",
                column: "DeletedUtc");

            migrationBuilder.CreateIndex(
                name: "IX_topic_Name",
                schema: "app",
                table: "topic",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_topic_ParentId",
                schema: "app",
                table: "topic",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_role_CreatedUtc",
                schema: "client",
                table: "role",
                column: "CreatedUtc");

            migrationBuilder.CreateIndex(
                name: "IX_role_Name",
                schema: "client",
                table: "role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_CreatedUtc",
                schema: "client",
                table: "user",
                column: "CreatedUtc");

            migrationBuilder.CreateIndex(
                name: "IX_user_DeletedUtc",
                schema: "client",
                table: "user",
                column: "DeletedUtc");

            migrationBuilder.CreateIndex(
                name: "IX_user_Login",
                schema: "client",
                table: "user",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_role_DeletedUtc",
                schema: "client",
                table: "user_role",
                column: "DeletedUtc");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_RoleId",
                schema: "client",
                table: "user_role",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_user_answer_CreatedUtc",
                schema: "report",
                table: "user_answer",
                column: "CreatedUtc");

            migrationBuilder.CreateIndex(
                name: "IX_user_answer_QuestionId",
                schema: "report",
                table: "user_answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_user_answer_UserTestId",
                schema: "report",
                table: "user_answer",
                column: "UserTestId");

            migrationBuilder.CreateIndex(
                name: "IX_user_test_CreatedUtc",
                schema: "report",
                table: "user_test",
                column: "CreatedUtc");

            migrationBuilder.CreateIndex(
                name: "IX_user_test_ExaminerId",
                schema: "report",
                table: "user_test",
                column: "ExaminerId");

            migrationBuilder.CreateIndex(
                name: "IX_user_test_TestId",
                schema: "report",
                table: "user_test",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_user_test_UserId",
                schema: "report",
                table: "user_test",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "test_topic",
                schema: "app");

            migrationBuilder.DropTable(
                name: "user_data",
                schema: "client");

            migrationBuilder.DropTable(
                name: "user_role",
                schema: "client");

            migrationBuilder.DropTable(
                name: "user_answer",
                schema: "report");

            migrationBuilder.DropTable(
                name: "role",
                schema: "client");

            migrationBuilder.DropTable(
                name: "question",
                schema: "app");

            migrationBuilder.DropTable(
                name: "user_test",
                schema: "report");

            migrationBuilder.DropTable(
                name: "topic",
                schema: "app");

            migrationBuilder.DropTable(
                name: "test",
                schema: "app");

            migrationBuilder.DropTable(
                name: "user",
                schema: "client");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:gender", "undefined,man,woman")
                .OldAnnotation("Npgsql:Enum:question_type", "open,multiple_selection,ordered_list,conformity,single_selection")
                .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");
        }
    }
}
