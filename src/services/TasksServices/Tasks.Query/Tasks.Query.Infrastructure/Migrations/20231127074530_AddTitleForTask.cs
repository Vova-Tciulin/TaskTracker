﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasks.Query.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTitleForTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Tasks");
        }
    }
}
