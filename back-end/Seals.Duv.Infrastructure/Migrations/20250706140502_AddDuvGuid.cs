﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seals.Duv.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDuvGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DuvGuid",
                table: "Duvs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DuvGuid",
                table: "Duvs");
        }
    }
}
