import React from 'react';
import {
  View,
  Text,
  StyleSheet,
  Pressable,
  FlatList,
  SafeAreaView,
} from 'react-native';
import { Workout } from '../types/workout';

interface WorkoutDetailsProps {
  workout: Workout;
  onBack: () => void;
  onAddExercise: (workout: Workout) => void;
  onRemoveExercise: (idWorkoutPlanExercise: number) => void;
}

const WorkoutDetails: React.FC<WorkoutDetailsProps> = ({
  workout,
  onBack,
  onAddExercise,
  onRemoveExercise,
}) => {
  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.topBar}>
        <Pressable onPress={onBack} style={styles.backButton}>
          <Text style={styles.backText}>Back</Text>
        </Pressable>
        {/* <View style={styles.headerActions}>
          <Pressable style={styles.editButton} onPress={() => {}}>
            <Text style={styles.editActionText}>Edit</Text>
          </Pressable>
          <Pressable style={styles.deleteButton} onPress={() => {}}>
            <Text style={styles.deleteActionText}>Delete</Text>
          </Pressable>
        </View> */}
      </View>

      <View style={styles.workoutCard}>
        <Text style={styles.overline}>Workout</Text>
        <Text style={styles.title}>{workout.name}</Text>
        <Text style={styles.description}>{workout.description}</Text>
        <View style={styles.countChip}>
          <Text style={styles.countChipText}>
            {workout.exercises.length} exercises
          </Text>
        </View>
      </View>

      <View style={styles.exercisesHeaderRow}>
        <Text style={styles.sectionTitle}>Exercise List</Text>
        <Pressable
          style={styles.addButton}
          onPress={() => onAddExercise(workout)}
        >
          <Text style={styles.addButtonText}>+ Add Exercise</Text>
        </Pressable>
      </View>

      <FlatList
        data={workout.exercises}
        keyExtractor={(item, idx) => idx.toString()}
        contentContainerStyle={styles.listContent}
        showsVerticalScrollIndicator={false}
        renderItem={({ item }) => (
          <View style={styles.exerciseCard}>
            <View style={styles.exerciseItem}>
              <Text style={styles.exerciseText}>{item.exerciseName}</Text>
              <Text style={styles.exerciseSubtext}>
                {item.sets} sets x {item.reps} reps
              </Text>
            </View>
            <View style={styles.exerciseActions}>
              {/* <Pressable style={styles.editButtonSmall} onPress={() => {}}>
                <Text style={styles.editActionTextSmall}>Edit</Text>
              </Pressable> */}
              <Pressable
                style={styles.deleteButtonSmall}
                onPress={() => onRemoveExercise(item.idWorkoutPlanExercise)}
              >
                <Text style={styles.deleteActionTextSmall}>Delete</Text>
              </Pressable>
            </View>
          </View>
        )}
        ListEmptyComponent={
          <Text style={styles.emptyText}>
            No exercises yet. Tap + Add Exercise to create one.
          </Text>
        }
      />
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    paddingHorizontal: 20,
    paddingTop: 12,
    backgroundColor: '#fbf7f7',
  },
  topBar: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    marginTop: 24,
    marginBottom: 12,
    zIndex: 2,
  },
  backButton: {
    borderRadius: 999,
    backgroundColor: '#eee3ff',
    paddingVertical: 8,
    paddingHorizontal: 16,
  },
  backText: {
    color: '#5a359d',
    fontSize: 14,
    fontWeight: '700',
  },
  headerActions: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  editButton: {
    backgroundColor: '#fff4cf',
    borderRadius: 10,
    paddingVertical: 8,
    paddingHorizontal: 14,
  },
  deleteButton: {
    backgroundColor: '#ffe0e2',
    borderRadius: 10,
    paddingVertical: 8,
    paddingHorizontal: 14,
  },
  editActionText: {
    color: '#8a6400',
    fontSize: 13,
    fontWeight: '700',
  },
  deleteActionText: {
    color: '#b5333f',
    fontSize: 13,
    fontWeight: '700',
  },
  workoutCard: {
    backgroundColor: '#5d3ea8',
    borderRadius: 20,
    padding: 20,
    marginBottom: 16,
    shadowColor: '#4d2a96',
    shadowOpacity: 0.28,
    shadowRadius: 12,
    shadowOffset: { width: 0, height: 6 },
    elevation: 6,
  },
  overline: {
    fontSize: 12,
    color: '#e6d8ff',
    textTransform: 'uppercase',
    letterSpacing: 1.2,
    marginBottom: 6,
  },
  title: {
    fontSize: 28,
    fontWeight: '800',
    color: '#fff',
    marginBottom: 8,
  },
  description: {
    fontSize: 15,
    color: '#efe6ff',
    lineHeight: 22,
    marginBottom: 12,
  },
  countChip: {
    alignSelf: 'flex-start',
    borderRadius: 999,
    paddingVertical: 6,
    paddingHorizontal: 12,
    backgroundColor: '#7252bf',
  },
  countChipText: {
    fontSize: 12,
    color: '#e6d8ff',
    fontWeight: '600',
  },
  exercisesHeaderRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    marginBottom: 10,
  },
  sectionTitle: {
    fontSize: 22,
    fontWeight: '700',
    color: '#322056',
  },
  addButton: {
    backgroundColor: '#dcf6ea',
    borderRadius: 10,
    paddingVertical: 8,
    paddingHorizontal: 14,
  },
  addButtonText: {
    color: '#2c7a5f',
    fontSize: 13,
    fontWeight: '800',
  },
  listContent: {
    paddingBottom: 24,
  },
  exerciseCard: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#fff',
    borderRadius: 14,
    padding: 12,
    marginBottom: 10,
    shadowColor: '#8d72c8',
    shadowOpacity: 0.08,
    shadowRadius: 8,
    shadowOffset: { width: 0, height: 3 },
    elevation: 2,
    borderWidth: 1,
    borderColor: '#eee4ff',
  },
  exerciseItem: {
    flex: 1,
  },
  exerciseActions: {
    flexDirection: 'row',
    gap: 6,
  },
  editButtonSmall: {
    backgroundColor: '#fff4cf',
    borderRadius: 8,
    paddingVertical: 6,
    paddingHorizontal: 10,
  },
  deleteButtonSmall: {
    backgroundColor: '#ffe0e2',
    borderRadius: 8,
    paddingVertical: 6,
    paddingHorizontal: 10,
  },
  editActionTextSmall: {
    color: '#8a6400',
    fontWeight: '700',
    fontSize: 12,
  },
  deleteActionTextSmall: {
    color: '#b5333f',
    fontWeight: '700',
    fontSize: 12,
  },
  exerciseText: {
    fontSize: 16,
    color: '#35215f',
    fontWeight: '700',
  },
  exerciseSubtext: {
    marginTop: 3,
    fontSize: 12,
    color: '#7f6aa7',
  },
  emptyText: {
    textAlign: 'center',
    color: '#7f6aa7',
    marginTop: 20,
    fontSize: 15,
  },
});

export default WorkoutDetails;
