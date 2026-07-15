import { WorkoutExercise } from './workoutExercise';

export type Workout = {
  id: number;
  name: string;
  description: string;
  exercises: WorkoutExercise[];
};
