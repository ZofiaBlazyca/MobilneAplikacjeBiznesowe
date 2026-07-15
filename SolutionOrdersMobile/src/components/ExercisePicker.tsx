import React, { useState } from 'react';
import {
  Alert,
  FlatList,
  Pressable,
  SafeAreaView,
  StyleSheet,
  Text,
  TextInput,
  View,
} from 'react-native';

import type { ExerciseDto } from '../types/models';

interface ExercisePickerProps {
  exercises: ExerciseDto[];
  onCancel: () => void;
  onSelectExercise: (
    exercise: ExerciseDto,
    sets: number,
    reps: number,
    durationSeconds: number | null,
  ) => void;
}

const ExercisePicker: React.FC<ExercisePickerProps> = ({
  exercises,
  onCancel,
  onSelectExercise,
}) => {
  const [selectedExercise, setSelectedExercise] = useState<ExerciseDto | null>(
    exercises[0] ?? null,
  );
  const [sets, setSets] = useState('3');
  const [reps, setReps] = useState('10');
  const [durationSeconds, setDurationSeconds] = useState('');

  const handleSave = () => {
    if (!selectedExercise) {
      Alert.alert('Validation error', 'Choose exercise');
      return;
    }

    if (!sets.trim() || !reps.trim()) {
      Alert.alert('Validation error', 'Sets and reps are required');
      return;
    }

    onSelectExercise(
      selectedExercise,
      Number(sets),
      Number(reps),
      durationSeconds.trim() ? Number(durationSeconds) : null,
    );
  };

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.topRow}>
        <Text style={styles.title}>Add Exercise</Text>

        <Pressable style={styles.cancelChip} onPress={onCancel}>
          <Text style={styles.cancelChipText}>Cancel</Text>
        </Pressable>
      </View>

      <Text style={styles.label}>Choose exercise</Text>

      <FlatList
        data={exercises}
        keyExtractor={item => item.idExercise.toString()}
        style={styles.exerciseList}
        contentContainerStyle={styles.exerciseListContent}
        renderItem={({ item }) => {
          const selected = selectedExercise?.idExercise === item.idExercise;

          return (
            <Pressable
              style={[
                styles.exerciseCard,
                selected && styles.exerciseCardActive,
              ]}
              onPress={() => setSelectedExercise(item)}
            >
              <Text
                style={[
                  styles.exerciseTitle,
                  selected && styles.exerciseTitleActive,
                ]}
              >
                {item.name}
              </Text>

              <Text
                style={[
                  styles.exerciseDescription,
                  selected && styles.exerciseDescriptionActive,
                ]}
              >
                {item.categoryName}
              </Text>
            </Pressable>
          );
        }}
        ListEmptyComponent={
          <Text style={styles.emptyText}>No exercises available</Text>
        }
      />

      <View style={styles.formCard}>
        <Text style={styles.label}>Sets</Text>
        <TextInput
          style={styles.input}
          value={sets}
          onChangeText={setSets}
          keyboardType="number-pad"
          placeholder="3"
          placeholderTextColor="#9b8fba"
        />

        <Text style={styles.label}>Reps</Text>
        <TextInput
          style={styles.input}
          value={reps}
          onChangeText={setReps}
          keyboardType="number-pad"
          placeholder="10"
          placeholderTextColor="#9b8fba"
        />

        <Text style={styles.label}>Duration seconds (optional)</Text>
        <TextInput
          style={styles.input}
          value={durationSeconds}
          onChangeText={setDurationSeconds}
          keyboardType="number-pad"
          placeholder="e.g. 60"
          placeholderTextColor="#9b8fba"
        />

        <View style={styles.actions}>
          <Pressable style={styles.cancelButton} onPress={onCancel}>
            <Text style={styles.cancelText}>Cancel</Text>
          </Pressable>

          <Pressable style={styles.saveButton} onPress={handleSave}>
            <Text style={styles.saveText}>Save</Text>
          </Pressable>
        </View>
      </View>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fbf7f7',
    paddingHorizontal: 20,
    paddingTop: 12,
  },
  topRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  title: {
    fontSize: 28,
    fontWeight: '800',
    color: '#322056',
    marginBottom: 16,
  },
  cancelChip: {
    backgroundColor: '#eee3ff',
    borderRadius: 999,
    paddingVertical: 8,
    paddingHorizontal: 14,
    marginBottom: 16,
  },
  cancelChipText: {
    color: '#5a359d',
    fontWeight: '800',
  },
  label: {
    fontSize: 14,
    fontWeight: '700',
    color: '#5a359d',
    marginBottom: 8,
  },
  exerciseList: {
    maxHeight: 230,
  },
  exerciseListContent: {
    paddingBottom: 8,
  },
  exerciseCard: {
    backgroundColor: '#fff',
    borderRadius: 14,
    padding: 12,
    marginBottom: 8,
    borderWidth: 1,
    borderColor: '#eee4ff',
  },
  exerciseCardActive: {
    backgroundColor: '#5d3ea8',
    borderColor: '#5d3ea8',
  },
  exerciseTitle: {
    fontSize: 16,
    fontWeight: '800',
    color: '#35215f',
  },
  exerciseTitleActive: {
    color: '#fff',
  },
  exerciseDescription: {
    fontSize: 12,
    color: '#6d5b92',
    marginTop: 3,
  },
  exerciseDescriptionActive: {
    color: '#efe6ff',
  },
  formCard: {
    marginTop: 12,
    backgroundColor: '#fff',
    borderRadius: 16,
    padding: 14,
    borderWidth: 1,
    borderColor: '#eee4ff',
  },
  input: {
    backgroundColor: '#fbf7f7',
    borderColor: '#eee4ff',
    borderWidth: 1,
    borderRadius: 12,
    padding: 12,
    fontSize: 15,
    color: '#35215f',
    marginBottom: 12,
  },
  actions: {
    flexDirection: 'row',
    gap: 12,
    marginTop: 4,
  },
  cancelButton: {
    flex: 1,
    backgroundColor: '#eee3ff',
    borderRadius: 12,
    paddingVertical: 13,
    alignItems: 'center',
  },
  saveButton: {
    flex: 1,
    backgroundColor: '#5d3ea8',
    borderRadius: 12,
    paddingVertical: 13,
    alignItems: 'center',
  },
  cancelText: {
    color: '#5a359d',
    fontWeight: '800',
  },
  saveText: {
    color: '#fff',
    fontWeight: '800',
  },
  emptyText: {
    textAlign: 'center',
    color: '#7f6aa7',
    marginTop: 20,
    fontSize: 16,
  },
});

export default ExercisePicker;
