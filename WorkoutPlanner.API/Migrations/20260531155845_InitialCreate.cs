using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorkoutPlanner.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    IdEquipment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.IdEquipment);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseCategories",
                columns: table => new
                {
                    IdExerciseCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseCategories", x => x.IdExerciseCategory);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    IdGoal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.IdGoal);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    IdUserProfile = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.IdUserProfile);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    IdExercise = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdExerciseCategory = table.Column<int>(type: "int", nullable: false),
                    IdEquipment = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.IdExercise);
                    table.ForeignKey(
                        name: "FK_Exercises_Equipment_IdEquipment",
                        column: x => x.IdEquipment,
                        principalTable: "Equipment",
                        principalColumn: "IdEquipment",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Exercises_ExerciseCategories_IdExerciseCategory",
                        column: x => x.IdExerciseCategory,
                        principalTable: "ExerciseCategories",
                        principalColumn: "IdExerciseCategory",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPlans",
                columns: table => new
                {
                    IdWorkoutPlan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdUserProfile = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlans", x => x.IdWorkoutPlan);
                    table.ForeignKey(
                        name: "FK_WorkoutPlans_UserProfiles_IdUserProfile",
                        column: x => x.IdUserProfile,
                        principalTable: "UserProfiles",
                        principalColumn: "IdUserProfile",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgressEntries",
                columns: table => new
                {
                    IdProgressEntry = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdWorkoutPlan = table.Column<int>(type: "int", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressEntries", x => x.IdProgressEntry);
                    table.ForeignKey(
                        name: "FK_ProgressEntries_WorkoutPlans_IdWorkoutPlan",
                        column: x => x.IdWorkoutPlan,
                        principalTable: "WorkoutPlans",
                        principalColumn: "IdWorkoutPlan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPlanExercises",
                columns: table => new
                {
                    IdWorkoutPlanExercise = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdWorkoutPlan = table.Column<int>(type: "int", nullable: false),
                    IdExercise = table.Column<int>(type: "int", nullable: false),
                    Sets = table.Column<int>(type: "int", nullable: false),
                    Reps = table.Column<int>(type: "int", nullable: false),
                    DurationSeconds = table.Column<int>(type: "int", nullable: true),
                    OrderNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlanExercises", x => x.IdWorkoutPlanExercise);
                    table.ForeignKey(
                        name: "FK_WorkoutPlanExercises_Exercises_IdExercise",
                        column: x => x.IdExercise,
                        principalTable: "Exercises",
                        principalColumn: "IdExercise",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutPlanExercises_WorkoutPlans_IdWorkoutPlan",
                        column: x => x.IdWorkoutPlan,
                        principalTable: "WorkoutPlans",
                        principalColumn: "IdWorkoutPlan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Equipment",
                columns: new[] { "IdEquipment", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "No equipment", true, "None" },
                    { 2, "Pair of dumbbells", true, "Dumbbells" },
                    { 3, "Elastic training band", true, "Resistance Band" }
                });

            migrationBuilder.InsertData(
                table: "ExerciseCategories",
                columns: new[] { "IdExerciseCategory", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "Strength exercises", true, "Strength" },
                    { 2, "Cardio exercises", true, "Cardio" },
                    { 3, "Mobility exercises", true, "Mobility" }
                });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "IdGoal", "Description", "IsActive", "Name", "TargetDate" },
                values: new object[] { 1, "Improve endurance and run 10 km comfortably", true, "Run 10 km", new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "IdUserProfile", "Email", "IsActive", "Name" },
                values: new object[] { 1, "demo@example.com", true, "Demo User" });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "IdExercise", "Description", "IdEquipment", "IdExerciseCategory", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "Lower body strength exercise", 1, 1, true, "Squats" },
                    { 2, "Cardio running session", 1, 2, true, "Running" },
                    { 3, "Mobility and flexibility work", 1, 3, true, "Stretching" }
                });

            migrationBuilder.InsertData(
                table: "WorkoutPlans",
                columns: new[] { "IdWorkoutPlan", "Description", "IdUserProfile", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "Focus on lower body strength.", 1, true, "Leg Day" },
                    { 2, "High intensity cardio session.", 1, true, "Cardio Blast" }
                });

            migrationBuilder.InsertData(
                table: "ProgressEntries",
                columns: new[] { "IdProgressEntry", "DurationMinutes", "EntryDate", "IdWorkoutPlan", "IsActive", "Notes" },
                values: new object[] { 1, 45, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, true, "Good training" });

            migrationBuilder.InsertData(
                table: "WorkoutPlanExercises",
                columns: new[] { "IdWorkoutPlanExercise", "DurationSeconds", "IdExercise", "IdWorkoutPlan", "OrderNumber", "Reps", "Sets" },
                values: new object[,]
                {
                    { 1, null, 1, 1, 1, 10, 4 },
                    { 2, 1800, 2, 2, 1, 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_IdEquipment",
                table: "Exercises",
                column: "IdEquipment");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_IdExerciseCategory",
                table: "Exercises",
                column: "IdExerciseCategory");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressEntries_IdWorkoutPlan",
                table: "ProgressEntries",
                column: "IdWorkoutPlan");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlanExercises_IdExercise",
                table: "WorkoutPlanExercises",
                column: "IdExercise");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlanExercises_IdWorkoutPlan",
                table: "WorkoutPlanExercises",
                column: "IdWorkoutPlan");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlans_IdUserProfile",
                table: "WorkoutPlans",
                column: "IdUserProfile");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "ProgressEntries");

            migrationBuilder.DropTable(
                name: "WorkoutPlanExercises");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "WorkoutPlans");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "ExerciseCategories");

            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
