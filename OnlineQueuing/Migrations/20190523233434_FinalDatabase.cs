using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineQueuing.Migrations
{
    public partial class FinalDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Dates_DateId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_UserId",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "Appointment");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_UserId",
                table: "Appointment",
                newName: "IX_Appointment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_DateId",
                table: "Appointment",
                newName: "IX_Appointment_DateId");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentTimeSlot",
                table: "Appointment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ServiceTypeId",
                table: "Appointment",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment",
                column: "AppointmentId");

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    BusinessId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Adress = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.BusinessId);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    ServiceTypeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true),
                    BusinessId = table.Column<long>(nullable: true),
                    MaxAppointmentsPerDay = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.ServiceTypeId);
                    table.ForeignKey(
                        name: "FK_ServiceTypes_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceDates",
                columns: table => new
                {
                    ServiceTypeId = table.Column<long>(nullable: false),
                    DataId = table.Column<long>(nullable: false),
                    ServiceTypeDataId = table.Column<long>(nullable: true),
                    ServiceTypeId1 = table.Column<long>(nullable: true),
                    DateId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDates", x => new { x.DataId, x.ServiceTypeId });
                    table.ForeignKey(
                        name: "FK_ServiceDates_Dates_DateId",
                        column: x => x.DateId,
                        principalTable: "Dates",
                        principalColumn: "DateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceDates_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "ServiceTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceDates_ServiceDates_ServiceTypeDataId_ServiceTypeId1",
                        columns: x => new { x.ServiceTypeDataId, x.ServiceTypeId1 },
                        principalTable: "ServiceDates",
                        principalColumns: new[] { "DataId", "ServiceTypeId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_ServiceTypeId",
                table: "Appointment",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDates_DateId",
                table: "ServiceDates",
                column: "DateId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDates_ServiceTypeId",
                table: "ServiceDates",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDates_ServiceTypeDataId_ServiceTypeId1",
                table: "ServiceDates",
                columns: new[] { "ServiceTypeDataId", "ServiceTypeId1" });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypes_BusinessId",
                table: "ServiceTypes",
                column: "BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Dates_DateId",
                table: "Appointment",
                column: "DateId",
                principalTable: "Dates",
                principalColumn: "DateId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_ServiceTypes_ServiceTypeId",
                table: "Appointment",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "ServiceTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Users_UserId",
                table: "Appointment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Dates_DateId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_ServiceTypes_ServiceTypeId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Users_UserId",
                table: "Appointment");

            migrationBuilder.DropTable(
                name: "ServiceDates");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_ServiceTypeId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "AppointmentTimeSlot",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "ServiceTypeId",
                table: "Appointment");

            migrationBuilder.RenameTable(
                name: "Appointment",
                newName: "Appointments");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_UserId",
                table: "Appointments",
                newName: "IX_Appointments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_DateId",
                table: "Appointments",
                newName: "IX_Appointments_DateId");

            migrationBuilder.AddColumn<string>(
                name: "ServiceType",
                table: "Appointments",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Dates_DateId",
                table: "Appointments",
                column: "DateId",
                principalTable: "Dates",
                principalColumn: "DateId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_UserId",
                table: "Appointments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
