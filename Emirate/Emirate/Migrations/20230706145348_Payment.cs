using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emirate.Migrations
{
    public partial class Payment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        { 

            migrationBuilder.CreateTable(
                name: "StudentPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PaymentDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchoolFeesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatePaid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentPayments_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentPayments_SchoolFees_SchoolFeesId",
                        column: x => x.SchoolFeesId,
                        principalTable: "SchoolFees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                });



            migrationBuilder.CreateIndex(
                name: "IX_StudentPayments_ApplicationUserId",
                table: "StudentPayments",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPayments_SchoolFeesId",
                table: "StudentPayments",
                column: "SchoolFeesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
    

            migrationBuilder.DropTable(
                name: "StudentPayments");



           
        }
    }
}
