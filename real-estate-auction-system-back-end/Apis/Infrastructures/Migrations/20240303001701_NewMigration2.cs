using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealtimeAuctions_Auctions_AuctionId",
                table: "RealtimeAuctions");

            migrationBuilder.RenameColumn(
                name: "AuctionId",
                table: "RealtimeAuctions",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_RealtimeAuctions_AuctionId",
                table: "RealtimeAuctions",
                newName: "IX_RealtimeAuctions_AccountId");

            migrationBuilder.AddColumn<int>(
                name: "AuctionId",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_AuctionId",
                table: "RealEstates",
                column: "AuctionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_Auctions_AuctionId",
                table: "RealEstates",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RealtimeAuctions_Accounts_AccountId",
                table: "RealtimeAuctions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_Auctions_AuctionId",
                table: "RealEstates");

            migrationBuilder.DropForeignKey(
                name: "FK_RealtimeAuctions_Accounts_AccountId",
                table: "RealtimeAuctions");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_AuctionId",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "AuctionId",
                table: "RealEstates");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "RealtimeAuctions",
                newName: "AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_RealtimeAuctions_AccountId",
                table: "RealtimeAuctions",
                newName: "IX_RealtimeAuctions_AuctionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RealtimeAuctions_Auctions_AuctionId",
                table: "RealtimeAuctions",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
