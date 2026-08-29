using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uploading_Certificate.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CertificateName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CertificateNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IssueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    RenewalDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CertificateType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificates");
        }
    }
}
