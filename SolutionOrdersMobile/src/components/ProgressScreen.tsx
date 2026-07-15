import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  Alert,
  FlatList,
  Pressable,
  SafeAreaView,
  StyleSheet,
  Text,
  TextInput,
  View,
} from 'react-native';

import apiService from '../api/apiService';
import type { ProgressEntryDto, WorkoutPlanDto } from '../types/models';

const ProgressScreen = (): React.JSX.Element => {
  const [entries, setEntries] = useState<ProgressEntryDto[]>([]);
  const [workouts, setWorkouts] = useState<WorkoutPlanDto[]>([]);
  const [loading, setLoading] = useState(true);

  const [showForm, setShowForm] = useState(false);
  const [selectedWorkoutId, setSelectedWorkoutId] = useState<number | null>(
    null,
  );
  const [durationMinutes, setDurationMinutes] = useState('');
  const [notes, setNotes] = useState('');

  const loadData = async () => {
    try {
      setLoading(true);

      const [progressData, workoutData] = await Promise.all([
        apiService.getProgressEntries(),
        apiService.getWorkoutPlans(),
      ]);

      setEntries(progressData);
      setWorkouts(workoutData);

      if (workoutData.length > 0 && selectedWorkoutId === null) {
        setSelectedWorkoutId(workoutData[0].idWorkoutPlan);
      }
    } catch (err) {
      console.error(err);
      Alert.alert('Error', 'Could not load progress entries');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadData();
  }, []);

  const openForm = () => {
    setSelectedWorkoutId(workouts[0]?.idWorkoutPlan ?? null);
    setDurationMinutes('');
    setNotes('');
    setShowForm(true);
  };

  const handleSave = async () => {
    if (!selectedWorkoutId) {
      Alert.alert('Validation error', 'Choose workout');
      return;
    }

    if (!durationMinutes.trim()) {
      Alert.alert('Validation error', 'Duration is required');
      return;
    }

    try {
      await apiService.createProgressEntry({
        idWorkoutPlan: selectedWorkoutId,
        entryDate: new Date().toISOString(),
        durationMinutes: Number(durationMinutes),
        notes: notes.trim() || undefined,
      });

      setShowForm(false);
      await loadData();

      Alert.alert('Success', 'Progress entry created');
    } catch (err) {
      console.error(err);
      Alert.alert('Error', 'Could not create progress entry');
    }
  };

  const handleDelete = (id: number) => {
    Alert.alert('Delete progress entry', 'Are you sure?', [
      { text: 'Cancel', style: 'cancel' },
      {
        text: 'Delete',
        style: 'destructive',
        onPress: async () => {
          try {
            await apiService.deleteProgressEntry(id);
            await loadData();
            Alert.alert('Success', 'Progress entry deleted');
          } catch (err) {
            console.error(err);
            Alert.alert('Error', 'Could not delete progress entry');
          }
        },
      },
    ]);
  };

  const totalSessions = entries.length;

  const totalMinutes = entries.reduce(
    (sum, entry) => sum + entry.durationMinutes,
    0,
  );

  const averageMinutes =
    totalSessions > 0 ? Math.round(totalMinutes / totalSessions) : 0;

  if (loading) {
    return (
      <View style={styles.center}>
        <ActivityIndicator size="large" />
      </View>
    );
  }

  if (showForm) {
    return (
      <SafeAreaView style={styles.container}>
        <Text style={styles.title}>Add Progress</Text>

        <Text style={styles.label}>Workout</Text>
        <View style={styles.chips}>
          {workouts.map(workout => {
            const selected = selectedWorkoutId === workout.idWorkoutPlan;

            return (
              <Pressable
                key={workout.idWorkoutPlan}
                style={[styles.chip, selected && styles.chipActive]}
                onPress={() => setSelectedWorkoutId(workout.idWorkoutPlan)}
              >
                <Text
                  style={[styles.chipText, selected && styles.chipTextActive]}
                >
                  {workout.name}
                </Text>
              </Pressable>
            );
          })}
        </View>

        <Text style={styles.label}>Duration minutes</Text>
        <TextInput
          style={styles.input}
          value={durationMinutes}
          onChangeText={setDurationMinutes}
          placeholder="e.g. 45"
          placeholderTextColor="#9b8fba"
          keyboardType="number-pad"
        />

        <Text style={styles.label}>Notes</Text>
        <TextInput
          style={[styles.input, styles.textArea]}
          value={notes}
          onChangeText={setNotes}
          placeholder="How was the training?"
          placeholderTextColor="#9b8fba"
          multiline
        />

        <View style={styles.actions}>
          <Pressable
            style={styles.cancelButton}
            onPress={() => setShowForm(false)}
          >
            <Text style={styles.cancelText}>Cancel</Text>
          </Pressable>

          <Pressable style={styles.saveButton} onPress={handleSave}>
            <Text style={styles.saveText}>Save</Text>
          </Pressable>
        </View>
      </SafeAreaView>
    );
  }

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.headerRow}>
        <Text style={styles.title}>Progress</Text>

        <Pressable style={styles.addButton} onPress={openForm}>
          <Text style={styles.addButtonText}>+ Add</Text>
        </Pressable>
      </View>

      <View style={styles.statsCard}>
        <View style={styles.statBox}>
          <Text style={styles.statValue}>{totalSessions}</Text>
          <Text style={styles.statLabel}>Sessions</Text>
        </View>

        <View style={styles.statBox}>
          <Text style={styles.statValue}>{totalMinutes}</Text>
          <Text style={styles.statLabel}>Minutes</Text>
        </View>

        <View style={styles.statBox}>
          <Text style={styles.statValue}>{averageMinutes}</Text>
          <Text style={styles.statLabel}>Avg min</Text>
        </View>
      </View>

      <FlatList
        data={entries}
        keyExtractor={item => item.idProgressEntry.toString()}
        contentContainerStyle={styles.listContent}
        renderItem={({ item }) => (
          <View style={styles.card}>
            <Text style={styles.cardTitle}>{item.workoutPlanName}</Text>
            <Text style={styles.cardDescription}>
              {item.durationMinutes} minutes
            </Text>
            <Text style={styles.cardMeta}>
              {new Date(item.entryDate).toLocaleDateString()}
            </Text>
            {item.notes ? (
              <Text style={styles.cardNotes}>{item.notes}</Text>
            ) : null}
            <Pressable
              style={styles.deleteButton}
              onPress={() => handleDelete(item.idProgressEntry)}
            >
              <Text style={styles.deleteText}>Delete</Text>
            </Pressable>
          </View>
        )}
        ListEmptyComponent={
          <Text style={styles.emptyText}>No progress entries yet</Text>
        }
      />
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
  center: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  headerRow: {
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
  label: {
    fontSize: 14,
    fontWeight: '700',
    color: '#5a359d',
    marginBottom: 8,
  },
  input: {
    backgroundColor: '#fff',
    borderColor: '#eee4ff',
    borderWidth: 1,
    borderRadius: 14,
    padding: 14,
    fontSize: 16,
    color: '#35215f',
    marginBottom: 18,
  },
  textArea: {
    minHeight: 90,
    textAlignVertical: 'top',
  },
  chips: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
    marginBottom: 18,
  },
  chip: {
    backgroundColor: '#eee3ff',
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderRadius: 999,
  },
  chipActive: {
    backgroundColor: '#5d3ea8',
  },
  chipText: {
    color: '#5a359d',
    fontWeight: '700',
    fontSize: 12,
  },
  chipTextActive: {
    color: '#fff',
  },
  actions: {
    flexDirection: 'row',
    gap: 12,
  },
  cancelButton: {
    flex: 1,
    backgroundColor: '#eee3ff',
    borderRadius: 12,
    paddingVertical: 14,
    alignItems: 'center',
  },
  saveButton: {
    flex: 1,
    backgroundColor: '#5d3ea8',
    borderRadius: 12,
    paddingVertical: 14,
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
  addButton: {
    backgroundColor: '#dcf6ea',
    borderRadius: 10,
    paddingVertical: 8,
    paddingHorizontal: 14,
    marginBottom: 16,
  },
  addButtonText: {
    color: '#2c7a5f',
    fontSize: 13,
    fontWeight: '800',
  },
  listContent: {
    paddingBottom: 32,
  },
  card: {
    backgroundColor: '#fff',
    borderRadius: 16,
    padding: 14,
    marginBottom: 12,
    borderWidth: 1,
    borderColor: '#eee4ff',
  },
  cardTitle: {
    fontSize: 18,
    fontWeight: '800',
    color: '#35215f',
  },
  cardDescription: {
    fontSize: 14,
    color: '#6d5b92',
    marginTop: 4,
  },
  cardMeta: {
    marginTop: 8,
    fontSize: 12,
    fontWeight: '700',
    color: '#6a3fb8',
  },
  cardNotes: {
    marginTop: 8,
    fontSize: 13,
    color: '#6d5b92',
  },
  emptyText: {
    textAlign: 'center',
    color: '#7f6aa7',
    marginTop: 40,
    fontSize: 16,
  },
  deleteButton: {
    alignSelf: 'flex-end',
    backgroundColor: '#ffe0e2',
    borderRadius: 10,
    paddingVertical: 7,
    paddingHorizontal: 14,
    marginTop: 10,
  },
  deleteText: {
    color: '#b5333f',
    fontWeight: '700',
    fontSize: 13,
  },
  statsCard: {
    flexDirection: 'row',
    gap: 10,
    backgroundColor: '#4d328b',
    borderRadius: 18,
    padding: 14,
    marginBottom: 16,
  },

  statBox: {
    flex: 1,
    backgroundColor: '#7252bf',
    borderRadius: 12,
    paddingVertical: 12,
    paddingHorizontal: 10,
  },

  statValue: {
    fontSize: 22,
    fontWeight: '800',
    color: '#fff',
  },

  statLabel: {
    fontSize: 12,
    color: '#e6d8ff',
    marginTop: 2,
  },
});

export default ProgressScreen;
