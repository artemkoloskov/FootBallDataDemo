using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballDataDemo.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Abbreviation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Team1Id = table.Column<int>(nullable: true),
                    Team2Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_Team1Id",
                        column: x => x.Team1Id,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_Team2Id",
                        column: x => x.Team2Id,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: true),
                    TeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Defences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MatchId = table.Column<int>(nullable: true),
                    DefendingTeamId = table.Column<int>(nullable: true),
                    AttemptingTeamId = table.Column<int>(nullable: true),
                    GoalkeeperId = table.Column<int>(nullable: true),
                    AttemtingPlayerId = table.Column<int>(nullable: true),
                    DefenceTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Defences_Teams_AttemptingTeamId",
                        column: x => x.AttemptingTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Defences_Players_AttemtingPlayerId",
                        column: x => x.AttemtingPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Defences_Teams_DefendingTeamId",
                        column: x => x.DefendingTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Defences_Players_GoalkeeperId",
                        column: x => x.GoalkeeperId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Defences_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoalPasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MatchId = table.Column<int>(nullable: true),
                    PassingTeamId = table.Column<int>(nullable: true),
                    PassingPlayerId = table.Column<int>(nullable: true),
                    RecievingPlayerId = table.Column<int>(nullable: true),
                    PassTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalPasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoalPasses_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoalPasses_Players_PassingPlayerId",
                        column: x => x.PassingPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoalPasses_Teams_PassingTeamId",
                        column: x => x.PassingTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoalPasses_Players_RecievingPlayerId",
                        column: x => x.RecievingPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MatchId = table.Column<int>(nullable: true),
                    ScoringTeamId = table.Column<int>(nullable: true),
                    ConcedingTeamId = table.Column<int>(nullable: true),
                    ScoringPlayerId = table.Column<int>(nullable: true),
                    GoalkeeperId = table.Column<int>(nullable: true),
                    GoalTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_Teams_ConcedingTeamId",
                        column: x => x.ConcedingTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Goals_Players_GoalkeeperId",
                        column: x => x.GoalkeeperId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Goals_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Goals_Players_ScoringPlayerId",
                        column: x => x.ScoringPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Goals_Teams_ScoringTeamId",
                        column: x => x.ScoringTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tackles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MatchId = table.Column<int>(nullable: true),
                    TacklingTeamId = table.Column<int>(nullable: true),
                    TackledTeamId = table.Column<int>(nullable: true),
                    TacklingPlayerId = table.Column<int>(nullable: true),
                    TackledPlayerId = table.Column<int>(nullable: true),
                    TackleTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tackles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tackles_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tackles_Players_TackledPlayerId",
                        column: x => x.TackledPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tackles_Teams_TackledTeamId",
                        column: x => x.TackledTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tackles_Players_TacklingPlayerId",
                        column: x => x.TacklingPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tackles_Teams_TacklingTeamId",
                        column: x => x.TacklingTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Defences_AttemptingTeamId",
                table: "Defences",
                column: "AttemptingTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Defences_AttemtingPlayerId",
                table: "Defences",
                column: "AttemtingPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Defences_DefendingTeamId",
                table: "Defences",
                column: "DefendingTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Defences_GoalkeeperId",
                table: "Defences",
                column: "GoalkeeperId");

            migrationBuilder.CreateIndex(
                name: "IX_Defences_MatchId",
                table: "Defences",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalPasses_MatchId",
                table: "GoalPasses",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalPasses_PassingPlayerId",
                table: "GoalPasses",
                column: "PassingPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalPasses_PassingTeamId",
                table: "GoalPasses",
                column: "PassingTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalPasses_RecievingPlayerId",
                table: "GoalPasses",
                column: "RecievingPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ConcedingTeamId",
                table: "Goals",
                column: "ConcedingTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_GoalkeeperId",
                table: "Goals",
                column: "GoalkeeperId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_MatchId",
                table: "Goals",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ScoringPlayerId",
                table: "Goals",
                column: "ScoringPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ScoringTeamId",
                table: "Goals",
                column: "ScoringTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Team1Id",
                table: "Matches",
                column: "Team1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Team2Id",
                table: "Matches",
                column: "Team2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Players_RoleId",
                table: "Players",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Tackles_MatchId",
                table: "Tackles",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Tackles_TackledPlayerId",
                table: "Tackles",
                column: "TackledPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tackles_TackledTeamId",
                table: "Tackles",
                column: "TackledTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Tackles_TacklingPlayerId",
                table: "Tackles",
                column: "TacklingPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tackles_TacklingTeamId",
                table: "Tackles",
                column: "TacklingTeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Defences");

            migrationBuilder.DropTable(
                name: "GoalPasses");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Tackles");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
