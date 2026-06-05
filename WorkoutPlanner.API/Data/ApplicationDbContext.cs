using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Models;

namespace WorkoutPlanner.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<WorkoutPlan> WorkoutPlans => Set<WorkoutPlan>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<ExerciseCategory> ExerciseCategories => Set<ExerciseCategory>();
    public DbSet<Equipment> Equipment => Set<Equipment>();
    public DbSet<Goal> Goals => Set<Goal>();
    public DbSet<ProgressEntry> ProgressEntries => Set<ProgressEntry>();
    public DbSet<WorkoutPlanExercise> WorkoutPlanExercises => Set<WorkoutPlanExercise>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserProfile>()
            .HasKey(x => x.IdUserProfile);

        modelBuilder.Entity<WorkoutPlan>()
            .HasKey(x => x.IdWorkoutPlan);

        modelBuilder.Entity<Exercise>()
            .HasKey(x => x.IdExercise);

        modelBuilder.Entity<ExerciseCategory>()
            .HasKey(x => x.IdExerciseCategory);

        modelBuilder.Entity<Equipment>()
            .HasKey(x => x.IdEquipment);

        modelBuilder.Entity<Goal>()
            .HasKey(x => x.IdGoal);

        modelBuilder.Entity<ProgressEntry>()
            .HasKey(x => x.IdProgressEntry);

        modelBuilder.Entity<WorkoutPlanExercise>()
            .HasKey(x => x.IdWorkoutPlanExercise);

        modelBuilder.Entity<WorkoutPlan>()
            .HasOne(x => x.UserProfile)
            .WithMany(x => x.WorkoutPlans)
            .HasForeignKey(x => x.IdUserProfile);

        modelBuilder.Entity<Exercise>()
            .HasOne(x => x.ExerciseCategory)
            .WithMany(x => x.Exercises)
            .HasForeignKey(x => x.IdExerciseCategory);

        modelBuilder.Entity<Exercise>()
            .HasOne(x => x.Equipment)
            .WithMany(x => x.Exercises)
            .HasForeignKey(x => x.IdEquipment)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<ProgressEntry>()
            .HasOne(x => x.WorkoutPlan)
            .WithMany(x => x.ProgressEntries)
            .HasForeignKey(x => x.IdWorkoutPlan);

        modelBuilder.Entity<WorkoutPlanExercise>()
            .HasOne(x => x.WorkoutPlan)
            .WithMany(x => x.WorkoutPlanExercises)
            .HasForeignKey(x => x.IdWorkoutPlan);

        modelBuilder.Entity<WorkoutPlanExercise>()
            .HasOne(x => x.Exercise)
            .WithMany(x => x.WorkoutPlanExercises)
            .HasForeignKey(x => x.IdExercise);

        modelBuilder.Entity<UserProfile>().HasData(
            new UserProfile { IdUserProfile = 1, Name = "Demo User", Email = "demo@example.com", IsActive = true }
        );

        modelBuilder.Entity<ExerciseCategory>().HasData(
            new ExerciseCategory { IdExerciseCategory = 1, Name = "Strength", Description = "Strength exercises", IsActive = true },
            new ExerciseCategory { IdExerciseCategory = 2, Name = "Cardio", Description = "Cardio exercises", IsActive = true },
            new ExerciseCategory { IdExerciseCategory = 3, Name = "Mobility", Description = "Mobility exercises", IsActive = true }
        );

        modelBuilder.Entity<Equipment>().HasData(
            new Equipment { IdEquipment = 1, Name = "None", Description = "No equipment", IsActive = true },
            new Equipment { IdEquipment = 2, Name = "Dumbbells", Description = "Pair of dumbbells", IsActive = true },
            new Equipment { IdEquipment = 3, Name = "Resistance Band", Description = "Elastic training band", IsActive = true }
        );

        modelBuilder.Entity<Exercise>().HasData(
            new Exercise { IdExercise = 1, Name = "Squats", Description = "Lower body strength exercise", IdExerciseCategory = 1, IdEquipment = 1, IsActive = true },
            new Exercise { IdExercise = 2, Name = "Running", Description = "Cardio running session", IdExerciseCategory = 2, IdEquipment = 1, IsActive = true },
            new Exercise { IdExercise = 3, Name = "Stretching", Description = "Mobility and flexibility work", IdExerciseCategory = 3, IdEquipment = 1, IsActive = true }
        );

        modelBuilder.Entity<WorkoutPlan>().HasData(
            new WorkoutPlan { IdWorkoutPlan = 1, Name = "Leg Day", Description = "Focus on lower body strength.", IdUserProfile = 1, IsActive = true },
            new WorkoutPlan { IdWorkoutPlan = 2, Name = "Cardio Blast", Description = "High intensity cardio session.", IdUserProfile = 1, IsActive = true }
        );

        modelBuilder.Entity<WorkoutPlanExercise>().HasData(
            new WorkoutPlanExercise { IdWorkoutPlanExercise = 1, IdWorkoutPlan = 1, IdExercise = 1, Sets = 4, Reps = 10, OrderNumber = 1 },
            new WorkoutPlanExercise { IdWorkoutPlanExercise = 2, IdWorkoutPlan = 2, IdExercise = 2, Sets = 1, Reps = 1, DurationSeconds = 1800, OrderNumber = 1 }
        );

        modelBuilder.Entity<ProgressEntry>().HasData(
            new ProgressEntry { IdProgressEntry = 1, IdWorkoutPlan = 1, EntryDate = new DateTime(2026, 5, 31), DurationMinutes = 45, Notes = "Good training", IsActive = true }
        );

        modelBuilder.Entity<Goal>().HasData(
            new Goal { IdGoal = 1, Name = "Run 10 km", Description = "Improve endurance and run 10 km comfortably", TargetDate = new DateTime(2026, 8, 31), IsActive = true }
        );
    }
}