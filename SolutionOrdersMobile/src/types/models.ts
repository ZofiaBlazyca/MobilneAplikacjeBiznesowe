export type WorkoutPlanDto = {
  idWorkoutPlan: number;
  name: string;
  description: string | null;
  idUserProfile: number;
  userName: string;
  isActive: boolean;
};

export type CreateWorkoutPlanRequest = {
  name: string;
  description?: string;
  idUserProfile: number;
};

export type UpdateWorkoutPlanRequest = {
  idWorkoutPlan: number;
  name: string;
  description?: string;
  idUserProfile: number;
  isActive: boolean;
};

export type WorkoutPlanExerciseDto = {
  idWorkoutPlanExercise: number;
  idWorkoutPlan: number;
  workoutPlanName: string;
  idExercise: number;
  exerciseName: string;
  sets: number;
  reps: number;
  durationSeconds: number | null;
  orderNumber: number;
};

export type CreateWorkoutPlanExerciseRequest = {
  idWorkoutPlan: number;
  idExercise: number;
  sets: number;
  reps: number;
  durationSeconds?: number | null;
  orderNumber: number;
};

export type ExerciseDto = {
  idExercise: number;
  name: string;
  description: string | null;
  idExerciseCategory: number;
  categoryName: string;
  isActive: boolean;
};

export type ExerciseCategoryDto = {
  idExerciseCategory: number;
  name: string;
  description: string | null;
  isActive: boolean;
};

export type CreateExerciseRequest = {
  name: string;
  description?: string;
  idExerciseCategory: number;
  idEquipment?: number | null;
};

export type UpdateExerciseRequest = {
  idExercise: number;
  name: string;
  description?: string;
  idExerciseCategory: number;
  idEquipment?: number | null;
  isActive: boolean;
};

export type ProgressEntryDto = {
  idProgressEntry: number;
  idWorkoutPlan: number;
  workoutPlanName: string;
  entryDate: string;
  durationMinutes: number;
  notes: string | null;
  isActive: boolean;
};

export type CreateProgressEntryRequest = {
  idWorkoutPlan: number;
  entryDate: string;
  durationMinutes: number;
  notes?: string;
};

export type EquipmentDto = {
  idEquipment: number;
  name: string;
  description: string | null;
  isActive: boolean;
};

export type GoalDto = {
  idGoal: number;
  name: string;
  description: string | null;
  targetDate: string | null;
  isActive: boolean;
};

export type UserProfileDto = {
  idUserProfile: number;
  name: string;
  email: string | null;
  isActive: boolean;
};
