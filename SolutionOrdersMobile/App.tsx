import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  Alert,
  SafeAreaView,
  StyleSheet,
  Text,
  View,
} from 'react-native';

import WorkoutsList from './src/components/WorkoutsList';
import WorkoutDetails from './src/components/WorkoutDetails';
import WorkoutsForm from './src/components/WorkoutsForm';
import AppMenu from './src/components/AppMenu';
import ExercisesScreen from './src/components/ExercisesScreen';
import ExercisePicker from './src/components/ExercisePicker';
import ProgressScreen from './src/components/ProgressScreen';
import SimpleListScreen from './src/components/SimpleListScreen';
import AboutScreen from './src/components/AboutScreen';

import apiService from './src/api/apiService';
import {
  WorkoutPlanDto,
  WorkoutPlanExerciseDto,
  ExerciseDto,
} from './src/types/models';
import { Workout } from './src/types/workout';
import { Screen } from './src/types/screen';

// export type Workout = {
//   id: number;
//   name: string;
//   description: string;
//   exercises: string[];
// };

function App(): React.JSX.Element {
  const [workouts, setWorkouts] = useState<Workout[]>([]);
  const [selectedWorkout, setSelectedWorkout] = useState<Workout | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [showWorkoutForm, setShowWorkoutForm] = useState(false);
  const [editedWorkout, setEditedWorkout] = useState<Workout | null>(null);
  const [screen, setScreen] = useState<Screen>('workouts');
  const [showExercisePicker, setShowExercisePicker] = useState(false);
  const [availableExercises, setAvailableExercises] = useState<ExerciseDto[]>(
    [],
  );

  const loadWorkoutPlans = async () => {
    try {
      setLoading(true);

      const [plans, planExercises] = await Promise.all([
        apiService.getWorkoutPlans(),
        apiService.getWorkoutPlanExercises(),
      ]);

      const mappedWorkouts: Workout[] = plans.map((plan: WorkoutPlanDto) => {
        const exercisesForPlan = planExercises
          .filter(
            (item: WorkoutPlanExerciseDto) =>
              item.idWorkoutPlan === plan.idWorkoutPlan,
          )
          .map((item: WorkoutPlanExerciseDto) => ({
            idWorkoutPlanExercise: item.idWorkoutPlanExercise,
            idExercise: item.idExercise,
            exerciseName: item.exerciseName,
            sets: item.sets,
            reps: item.reps,
            durationSeconds: item.durationSeconds,
            orderNumber: item.orderNumber,
          }));

        return {
          id: plan.idWorkoutPlan,
          name: plan.name,
          description: plan.description ?? '',
          exercises: exercisesForPlan,
        };
      });

      setWorkouts(mappedWorkouts);
      setError(null);
    } catch (err) {
      console.error(err);
      setError('Failed to load workout plans');
    } finally {
      setLoading(false);
    }
  };

  const handleAddWorkout = () => {
    setEditedWorkout(null);
    setShowWorkoutForm(true);
  };

  const handleSaveWorkout = async (name: string, description: string) => {
    try {
      if (editedWorkout) {
        await apiService.updateWorkoutPlan(editedWorkout.id, {
          idWorkoutPlan: editedWorkout.id,
          name,
          description,
          idUserProfile: 1,
          isActive: true,
        });

        Alert.alert('Success', 'Workout plan updated');
      } else {
        await apiService.createWorkoutPlan({
          name,
          description,
          idUserProfile: 1,
        });

        Alert.alert('Success', 'Workout plan created');
      }

      await loadWorkoutPlans();
      setShowWorkoutForm(false);
      setEditedWorkout(null);
    } catch (err) {
      console.error(err);
      Alert.alert('Error', 'Could not save workout plan');
    }
  };

  const handleEditWorkout = (workout: Workout) => {
    setEditedWorkout(workout);
    setShowWorkoutForm(true);
  };

  const handleDeleteWorkout = async (workout: Workout) => {
    Alert.alert(
      'Delete workout',
      `Are you sure you want to delete "${workout.name}"?`,
      [
        {
          text: 'Cancel',
          style: 'cancel',
        },
        {
          text: 'Delete',
          style: 'destructive',
          onPress: async () => {
            try {
              await apiService.deleteWorkoutPlan(workout.id);

              if (selectedWorkout?.id === workout.id) {
                setSelectedWorkout(null);
              }

              await loadWorkoutPlans();

              Alert.alert('Success', 'Workout plan deleted');
            } catch (err) {
              console.error(err);
              Alert.alert('Error', 'Could not delete workout plan');
            }
          },
        },
      ],
    );
  };

  const handleAddExerciseToWorkout = async (workout: Workout) => {
    try {
      const exercises = await apiService.getExercises();

      const filteredExercises = exercises.filter(
        exercise =>
          !workout.exercises.some(
            workoutExercise =>
              workoutExercise.idExercise === exercise.idExercise,
          ),
      );

      if (filteredExercises.length === 0) {
        Alert.alert('Info', 'All exercises are already added to this workout');
        return;
      }

      setAvailableExercises(filteredExercises);
      setShowExercisePicker(true);
    } catch (err) {
      console.error(err);
      Alert.alert('Error', 'Could not load exercises');
    }
  };

  const handleSelectExerciseForWorkout = async (
    exercise: ExerciseDto,
    sets: number,
    reps: number,
    durationSeconds: number | null,
  ) => {
    if (!selectedWorkout) {
      return;
    }

    try {
      await apiService.createWorkoutPlanExercise({
        idWorkoutPlan: selectedWorkout.id,
        idExercise: exercise.idExercise,
        sets,
        reps,
        durationSeconds,
        orderNumber: selectedWorkout.exercises.length + 1,
      });

      setShowExercisePicker(false);
      setAvailableExercises([]);

      await loadWorkoutPlans();

      setSelectedWorkout(null);

      Alert.alert('Success', 'Exercise added to workout');
    } catch (err) {
      console.error(err);
      Alert.alert('Error', 'Could not add exercise');
    }
  };

  const handleRemoveExerciseFromWorkout = async (
    idWorkoutPlanExercise: number,
  ) => {
    try {
      await apiService.deleteWorkoutPlanExercise(idWorkoutPlanExercise);

      await loadWorkoutPlans();

      setSelectedWorkout(null);

      Alert.alert('Success', 'Exercise removed from workout');
    } catch (err) {
      console.error(err);
      Alert.alert('Error', 'Could not remove exercise');
    }
  };

  useEffect(() => {
    loadWorkoutPlans();
  }, []);

  if (loading) {
    return (
      <View style={styles.center}>
        <ActivityIndicator size="large" />
      </View>
    );
  }

  if (error) {
    return (
      <View style={styles.center}>
        <Text>{error}</Text>
      </View>
    );
  }

  return (
    <SafeAreaView style={styles.container}>
      <AppMenu currentScreen={screen} onChangeScreen={setScreen} />

      {screen === 'exercises' ? (
        <ExercisesScreen />
      ) : screen === 'progress' ? (
        <ProgressScreen />
      ) : screen === 'about' ? (
        <AboutScreen />
      ) : screen === 'categories' ? (
        <SimpleListScreen
          key="categories"
          title="Exercise Categories"
          loadItems={async () => {
            const data = await apiService.getExerciseCategories();

            return data.map(item => ({
              id: item.idExerciseCategory,
              title: item.name,
              subtitle: item.description,
              meta: item.isActive ? 'Active' : 'Inactive',
            }));
          }}
          createItem={async (name, description) => {
            await apiService.createExerciseCategory({
              name,
              description,
            });
          }}
          updateItem={async (id, name, description) => {
            await apiService.updateExerciseCategory(id, {
              name,
              description,
              isActive: true,
            });
          }}
          deleteItem={async id => {
            await apiService.deleteExerciseCategory(id);
          }}
        />
      ) : screen === 'equipment' ? (
        <SimpleListScreen
          key="equipment"
          title="Equipment"
          loadItems={async () => {
            const data = await apiService.getEquipment();

            return data.map(item => ({
              id: item.idEquipment,
              title: item.name,
              subtitle: item.description,
              meta: item.isActive ? 'Active' : 'Inactive',
            }));
          }}
          createItem={async (name, description) => {
            await apiService.createEquipment({
              name,
              description,
            });
          }}
          updateItem={async (id, name, description) => {
            await apiService.updateEquipment(id, {
              name,
              description,
              isActive: true,
            });
          }}
          deleteItem={async id => {
            await apiService.deleteEquipment(id);
          }}
        />
      ) : screen === 'goals' ? (
        <SimpleListScreen
          key="goals"
          title="Goals"
          descriptionLabel="Description"
          extraLabel="Target date"
          extraPlaceholder="YYYY-MM-DD"
          loadItems={async () => {
            const data = await apiService.getGoals();

            return data.map(item => ({
              id: item.idGoal,
              title: item.name,
              subtitle: item.description,
              meta: item.targetDate
                ? `Target: ${new Date(item.targetDate).toLocaleDateString()}`
                : 'No target date',
              extraValue: item.targetDate ? item.targetDate.slice(0, 10) : '',
            }));
          }}
          createItem={async (name, description, targetDate) => {
            await apiService.createGoal({
              name,
              description,
              targetDate: targetDate ? `${targetDate}T00:00:00` : null,
            });
          }}
          updateItem={async (id, name, description, targetDate) => {
            await apiService.updateGoal(id, {
              name,
              description,
              targetDate: targetDate ? `${targetDate}T00:00:00` : null,
              isActive: true,
            });
          }}
          deleteItem={async id => {
            await apiService.deleteGoal(id);
          }}
        />
      ) : screen === 'users' ? (
        <SimpleListScreen
          key="users"
          title="Users"
          descriptionLabel="Email"
          loadItems={async () => {
            const data = await apiService.getUserProfiles();

            return data.map(item => ({
              id: item.idUserProfile,
              title: item.name,
              subtitle: item.email,
              meta: item.isActive ? 'Active' : 'Inactive',
            }));
          }}
          createItem={async (name, description) => {
            await apiService.createUserProfile({
              name,
              email: description,
            });
          }}
          updateItem={async (id, name, description) => {
            await apiService.updateUserProfile(id, {
              name,
              email: description,
              isActive: true,
            });
          }}
          deleteItem={async id => {
            await apiService.deleteUserProfile(id);
          }}
        />
      ) : showExercisePicker ? (
        <ExercisePicker
          exercises={availableExercises}
          onCancel={() => {
            setShowExercisePicker(false);
            setAvailableExercises([]);
          }}
          onSelectExercise={handleSelectExerciseForWorkout}
        />
      ) : showWorkoutForm ? (
        <WorkoutsForm
          workout={editedWorkout}
          onCancel={() => {
            setShowWorkoutForm(false);
            setEditedWorkout(null);
          }}
          onSave={handleSaveWorkout}
        />
      ) : selectedWorkout ? (
        <WorkoutDetails
          workout={selectedWorkout}
          onBack={() => setSelectedWorkout(null)}
          onAddExercise={handleAddExerciseToWorkout}
          onRemoveExercise={handleRemoveExerciseFromWorkout}
        />
      ) : (
        <WorkoutsList
          workouts={workouts}
          onSelectWorkout={setSelectedWorkout}
          onAddWorkout={handleAddWorkout}
          onEditWorkout={handleEditWorkout}
          onDeleteWorkout={handleDeleteWorkout}
        />
      )}
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#f6f2ff',
  },
  center: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  placeholder: {
    flex: 1,
    padding: 20,
    backgroundColor: '#fbf7f7',
  },
  placeholderTitle: {
    fontSize: 28,
    fontWeight: '800',
    color: '#322056',
    textTransform: 'capitalize',
  },
  placeholderText: {
    marginTop: 8,
    fontSize: 15,
    color: '#6d5b92',
  },
});

export default App;
