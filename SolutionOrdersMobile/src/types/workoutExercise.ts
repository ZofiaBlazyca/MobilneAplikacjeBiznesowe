export type WorkoutExercise = {
  idWorkoutPlanExercise: number;
  idExercise: number;
  exerciseName: string;
  sets: number;
  reps: number;
  durationSeconds: number | null;
  orderNumber: number;
};
