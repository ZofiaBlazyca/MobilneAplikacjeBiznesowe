import { API_BASE_URL } from './config';
import type {
  WorkoutPlanDto,
  CreateWorkoutPlanRequest,
  UpdateWorkoutPlanRequest,
  WorkoutPlanExerciseDto,
  CreateWorkoutPlanExerciseRequest,
  ExerciseDto,
  ExerciseCategoryDto,
  CreateExerciseRequest,
  UpdateExerciseRequest,
  ProgressEntryDto,
  CreateProgressEntryRequest,
  EquipmentDto,
  GoalDto,
  UserProfileDto,
} from '../types/models';

class ApiService {
  private async request<T>(
    endpoint: string,
    options: RequestInit = {},
  ): Promise<T> {
    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
      ...options,
      headers: {
        'Content-Type': 'application/json',
        ...(options.headers || {}),
      },
    });

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`HTTP ${response.status}: ${errorText}`);
    }

    if (response.status === 204) {
      return {} as T;
    }

    return response.json();
  }

  getWorkoutPlans(): Promise<WorkoutPlanDto[]> {
    return this.request<WorkoutPlanDto[]>('/WorkoutPlans');
  }

  getWorkoutPlanExercises(): Promise<WorkoutPlanExerciseDto[]> {
    return this.request<WorkoutPlanExerciseDto[]>('/WorkoutPlanExercises');
  }

  createWorkoutPlan(data: CreateWorkoutPlanRequest): Promise<{ id: number }> {
    return this.request<{ id: number }>('/WorkoutPlans', {
      method: 'POST',
      body: JSON.stringify(data),
    });
  }

  updateWorkoutPlan(id: number, data: UpdateWorkoutPlanRequest): Promise<void> {
    return this.request<void>(`/WorkoutPlans/${id}`, {
      method: 'PUT',
      body: JSON.stringify(data),
    });
  }

  deleteWorkoutPlan(id: number): Promise<void> {
    return this.request<void>(`/WorkoutPlans/${id}`, {
      method: 'DELETE',
    });
  }

  getExercises(): Promise<ExerciseDto[]> {
    return this.request<ExerciseDto[]>('/Exercises');
  }

  createExercise(data: CreateExerciseRequest): Promise<{ id: number }> {
    return this.request<{ id: number }>('/Exercises', {
      method: 'POST',
      body: JSON.stringify(data),
    });
  }

  updateExercise(id: number, data: UpdateExerciseRequest): Promise<void> {
    return this.request<void>(`/Exercises/${id}`, {
      method: 'PUT',
      body: JSON.stringify(data),
    });
  }

  deleteExercise(id: number): Promise<void> {
    return this.request<void>(`/Exercises/${id}`, {
      method: 'DELETE',
    });
  }

  getExerciseCategories(): Promise<ExerciseCategoryDto[]> {
    return this.request<ExerciseCategoryDto[]>('/ExerciseCategories');
  }

  createWorkoutPlanExercise(
    data: CreateWorkoutPlanExerciseRequest,
  ): Promise<{ id: number }> {
    return this.request<{ id: number }>('/WorkoutPlanExercises', {
      method: 'POST',
      body: JSON.stringify(data),
    });
  }

  deleteWorkoutPlanExercise(id: number): Promise<void> {
    return this.request<void>(`/WorkoutPlanExercises/${id}`, {
      method: 'DELETE',
    });
  }

  getProgressEntries(): Promise<ProgressEntryDto[]> {
    return this.request<ProgressEntryDto[]>('/ProgressEntries');
  }

  createProgressEntry(
    data: CreateProgressEntryRequest,
  ): Promise<{ id: number }> {
    return this.request<{ id: number }>('/ProgressEntries', {
      method: 'POST',
      body: JSON.stringify(data),
    });
  }

  getEquipment(): Promise<EquipmentDto[]> {
    return this.request<EquipmentDto[]>('/Equipment');
  }

  getGoals(): Promise<GoalDto[]> {
    return this.request<GoalDto[]>('/Goals');
  }

  getUserProfiles(): Promise<UserProfileDto[]> {
    return this.request<UserProfileDto[]>('/UserProfiles');
  }

  createExerciseCategory(data: {
    name: string;
    description?: string;
  }): Promise<{ id: number }> {
    return this.request<{ id: number }>('/ExerciseCategories', {
      method: 'POST',
      body: JSON.stringify(data),
    });
  }

  deleteExerciseCategory(id: number): Promise<void> {
    return this.request<void>(`/ExerciseCategories/${id}`, {
      method: 'DELETE',
    });
  }

  createEquipment(data: {
    name: string;
    description?: string;
  }): Promise<{ id: number }> {
    return this.request<{ id: number }>('/Equipment', {
      method: 'POST',
      body: JSON.stringify(data),
    });
  }

  deleteEquipment(id: number): Promise<void> {
    return this.request<void>(`/Equipment/${id}`, {
      method: 'DELETE',
    });
  }

  createGoal(data: {
    name: string;
    description?: string;
    targetDate?: string | null;
  }): Promise<{ id: number }> {
    return this.request<{ id: number }>('/Goals', {
      method: 'POST',
      body: JSON.stringify(data),
    });
  }

  deleteGoal(id: number): Promise<void> {
    return this.request<void>(`/Goals/${id}`, {
      method: 'DELETE',
    });
  }

  createUserProfile(data: {
    name: string;
    email?: string;
  }): Promise<{ id: number }> {
    return this.request<{ id: number }>('/UserProfiles', {
      method: 'POST',
      body: JSON.stringify(data),
    });
  }

  deleteUserProfile(id: number): Promise<void> {
    return this.request<void>(`/UserProfiles/${id}`, {
      method: 'DELETE',
    });
  }

  deleteProgressEntry(id: number): Promise<void> {
    return this.request<void>(`/ProgressEntries/${id}`, {
      method: 'DELETE',
    });
  }

  updateExerciseCategory(
    id: number,
    data: {
      name: string;
      description?: string;
      isActive: boolean;
    },
  ): Promise<void> {
    return this.request<void>(`/ExerciseCategories/${id}`, {
      method: 'PUT',
      body: JSON.stringify({
        idExerciseCategory: id,
        ...data,
      }),
    });
  }

  updateEquipment(
    id: number,
    data: {
      name: string;
      description?: string;
      isActive: boolean;
    },
  ): Promise<void> {
    return this.request<void>(`/Equipment/${id}`, {
      method: 'PUT',
      body: JSON.stringify({
        idEquipment: id,
        ...data,
      }),
    });
  }

  updateGoal(
    id: number,
    data: {
      name: string;
      description?: string;
      targetDate?: string | null;
      isActive: boolean;
    },
  ): Promise<void> {
    return this.request<void>(`/Goals/${id}`, {
      method: 'PUT',
      body: JSON.stringify({
        idGoal: id,
        ...data,
      }),
    });
  }

  updateUserProfile(
    id: number,
    data: {
      name: string;
      email?: string;
      isActive: boolean;
    },
  ): Promise<void> {
    return this.request<void>(`/UserProfiles/${id}`, {
      method: 'PUT',
      body: JSON.stringify({
        idUserProfile: id,
        ...data,
      }),
    });
  }
}

export default new ApiService();
