﻿// <auto-generated />
using System;
using FootballDataDemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FootballDataDemo.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("FootballDataDemo.Model.Defence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AttemptingTeamId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AttemtingPlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DefenceTime")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DefendingTeamId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GoalkeeperId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MatchId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AttemptingTeamId");

                    b.HasIndex("AttemtingPlayerId");

                    b.HasIndex("DefendingTeamId");

                    b.HasIndex("GoalkeeperId");

                    b.HasIndex("MatchId");

                    b.ToTable("Defences");
                });

            modelBuilder.Entity("FootballDataDemo.Model.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ConcedingTeamId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GoalTime")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GoalkeeperId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MatchId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ScoringPlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ScoringTeamId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ConcedingTeamId");

                    b.HasIndex("GoalkeeperId");

                    b.HasIndex("MatchId");

                    b.HasIndex("ScoringPlayerId");

                    b.HasIndex("ScoringTeamId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("FootballDataDemo.Model.GoalPass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MatchId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PassTime")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PassingPlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PassingTeamId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RecievingPlayerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("PassingPlayerId");

                    b.HasIndex("PassingTeamId");

                    b.HasIndex("RecievingPlayerId");

                    b.ToTable("GoalPasses");
                });

            modelBuilder.Entity("FootballDataDemo.Model.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Team1Id")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Team2Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Team1Id");

                    b.HasIndex("Team2Id");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("FootballDataDemo.Model.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TeamId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("FootballDataDemo.Model.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoleType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("FootballDataDemo.Model.Tackle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MatchId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TackleTime")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TackledPlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TackledTeamId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TacklingPlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TacklingTeamId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("TackledPlayerId");

                    b.HasIndex("TackledTeamId");

                    b.HasIndex("TacklingPlayerId");

                    b.HasIndex("TacklingTeamId");

                    b.ToTable("Tackles");
                });

            modelBuilder.Entity("FootballDataDemo.Model.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Abbreviation")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("FootballDataDemo.Model.Defence", b =>
                {
                    b.HasOne("FootballDataDemo.Model.Team", "AttemptingTeam")
                        .WithMany()
                        .HasForeignKey("AttemptingTeamId");

                    b.HasOne("FootballDataDemo.Model.Player", "AttemtingPlayer")
                        .WithMany()
                        .HasForeignKey("AttemtingPlayerId");

                    b.HasOne("FootballDataDemo.Model.Team", "DefendingTeam")
                        .WithMany()
                        .HasForeignKey("DefendingTeamId");

                    b.HasOne("FootballDataDemo.Model.Player", "Goalkeeper")
                        .WithMany()
                        .HasForeignKey("GoalkeeperId");

                    b.HasOne("FootballDataDemo.Model.Match", "Match")
                        .WithMany("Defences")
                        .HasForeignKey("MatchId");
                });

            modelBuilder.Entity("FootballDataDemo.Model.Goal", b =>
                {
                    b.HasOne("FootballDataDemo.Model.Team", "ConcedingTeam")
                        .WithMany()
                        .HasForeignKey("ConcedingTeamId");

                    b.HasOne("FootballDataDemo.Model.Player", "Goalkeeper")
                        .WithMany()
                        .HasForeignKey("GoalkeeperId");

                    b.HasOne("FootballDataDemo.Model.Match", "Match")
                        .WithMany("Goals")
                        .HasForeignKey("MatchId");

                    b.HasOne("FootballDataDemo.Model.Player", "ScoringPlayer")
                        .WithMany()
                        .HasForeignKey("ScoringPlayerId");

                    b.HasOne("FootballDataDemo.Model.Team", "ScoringTeam")
                        .WithMany()
                        .HasForeignKey("ScoringTeamId");
                });

            modelBuilder.Entity("FootballDataDemo.Model.GoalPass", b =>
                {
                    b.HasOne("FootballDataDemo.Model.Match", "Match")
                        .WithMany("GoalPasses")
                        .HasForeignKey("MatchId");

                    b.HasOne("FootballDataDemo.Model.Player", "PassingPlayer")
                        .WithMany()
                        .HasForeignKey("PassingPlayerId");

                    b.HasOne("FootballDataDemo.Model.Team", "PassingTeam")
                        .WithMany()
                        .HasForeignKey("PassingTeamId");

                    b.HasOne("FootballDataDemo.Model.Player", "RecievingPlayer")
                        .WithMany()
                        .HasForeignKey("RecievingPlayerId");
                });

            modelBuilder.Entity("FootballDataDemo.Model.Match", b =>
                {
                    b.HasOne("FootballDataDemo.Model.Team", "Team1")
                        .WithMany()
                        .HasForeignKey("Team1Id");

                    b.HasOne("FootballDataDemo.Model.Team", "Team2")
                        .WithMany()
                        .HasForeignKey("Team2Id");
                });

            modelBuilder.Entity("FootballDataDemo.Model.Player", b =>
                {
                    b.HasOne("FootballDataDemo.Model.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("FootballDataDemo.Model.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("FootballDataDemo.Model.Tackle", b =>
                {
                    b.HasOne("FootballDataDemo.Model.Match", "Match")
                        .WithMany("Tackles")
                        .HasForeignKey("MatchId");

                    b.HasOne("FootballDataDemo.Model.Player", "TackledPlayer")
                        .WithMany()
                        .HasForeignKey("TackledPlayerId");

                    b.HasOne("FootballDataDemo.Model.Team", "TackledTeam")
                        .WithMany()
                        .HasForeignKey("TackledTeamId");

                    b.HasOne("FootballDataDemo.Model.Player", "TacklingPlayer")
                        .WithMany()
                        .HasForeignKey("TacklingPlayerId");

                    b.HasOne("FootballDataDemo.Model.Team", "TacklingTeam")
                        .WithMany()
                        .HasForeignKey("TacklingTeamId");
                });
#pragma warning restore 612, 618
        }
    }
}
