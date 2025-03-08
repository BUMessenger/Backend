using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUMessenger.DataAccess.Context.Migrations
{
    /// <inheritdoc />
    public partial class MadeFixedMaxLengthOfMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUserInfos_Messages_LastReadMessageId",
                table: "ChatUserInfos");

            migrationBuilder.AlterColumn<string>(
                name: "MessageText",
                table: "Messages",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "LastReadMessageId",
                table: "ChatUserInfos",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUserInfos_Messages_LastReadMessageId",
                table: "ChatUserInfos",
                column: "LastReadMessageId",
                principalTable: "Messages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUserInfos_Messages_LastReadMessageId",
                table: "ChatUserInfos");

            migrationBuilder.AlterColumn<string>(
                name: "MessageText",
                table: "Messages",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<Guid>(
                name: "LastReadMessageId",
                table: "ChatUserInfos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUserInfos_Messages_LastReadMessageId",
                table: "ChatUserInfos",
                column: "LastReadMessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
