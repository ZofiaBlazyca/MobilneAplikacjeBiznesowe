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
import type { ExerciseDto, ExerciseCategoryDto } from '../types/models';

const ExercisesScreen = (): React.JSX.Element => {
  const [exercises, setExercises] = useState<ExerciseDto[]>([]);
  const [categories, setCategories] = useState<ExerciseCategoryDto[]>([]);
  const [loading, setLoading] = useState(true);

  const [editedExercise, setEditedExercise] = useState<ExerciseDto | null>(
    null,
  );
  const [showForm, setShowForm] = useState(false);

  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [selectedCategoryId, setSelectedCategoryId] = useState<number | null>(
    null,
  );
  const [searchText, setSearchText] = useState('');

  const loadData = async () => {
    try {
      setLoading(true);

      const [exercisesData, categoriesData] = await Promise.all([
        apiService.getExercises(),
        apiService.getExerciseCategories(),
      ]);

      setExercises(exercisesData);
      setCategories(categoriesData);

      if (categoriesData.length > 0 && selectedCategoryId === null) {
        setSelectedCategoryId(categoriesData[0].idExerciseCategory);
      }
    } catch (err) {
      console.error(err);
      Alert.alert('Error', 'Could not load exercises');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadData();
  }, []);

  const openCreateForm = () => {
    setEditedExercise(null);
    setName('');
    setDescription('');
    setSelectedCategoryId(categories[0]?.idExerciseCategory ?? null);
    setShowForm(true);
  };

  const openEditForm = (exercise: ExerciseDto) => {
    setEditedExercise(exercise);
    setName(exercise.name);
    setDescription(exercise.description ?? '');
    setSelectedCategoryId(exercise.idExerciseCategory);
    setShowForm(true);
  };

  const handleSave = async () => {
    if (!name.trim()) {
      Alert.alert('Validation error', 'Exercise name is required');
      return;
    }

    if (!selectedCategoryId) {
      Alert.alert('Validation error', 'Exercise category is required');
      return;
    }

    try {
      if (editedExercise) {
        await apiService.updateExercise(editedExercise.idExercise, {
          idExercise: editedExercise.idExercise,
          name: name.trim(),
          description: description.trim(),
          idExerciseCategory: selectedCategoryId,
          idEquipment: null,
          isActive: true,
        });

        Alert.alert('Success', 'Exercise updated');
      } else {
        await apiService.createExercise({
          name: name.trim(),
          description: description.trim(),
          idExerciseCategory: selectedCategoryId,
          idEquipment: null,
        });

        Alert.alert('Success', 'Exercise created');
      }

      setShowForm(false);
      setEditedExercise(null);
      await loadData();
    } catch (err) {
      console.error(err);
      Alert.alert('Error', 'Could not save exercise');
    }
  };

  const handleDelete = (exercise: ExerciseDto) => {
    Alert.alert('Delete exercise', `Delete "${exercise.name}"?`, [
      { text: 'Cancel', style: 'cancel' },
      {
        text: 'Delete',
        style: 'destructive',
        onPress: async () => {
          try {
            await apiService.deleteExercise(exercise.idExercise);
            await loadData();
            Alert.alert('Success', 'Exercise deleted');
          } catch (err) {
            console.error(err);
            Alert.alert('Error', 'Could not delete exercise');
          }
        },
      },
    ]);
  };

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
        <Text style={styles.title}>
          {editedExercise ? 'Edit Exercise' : 'Add Exercise'}
        </Text>

        <Text style={styles.label}>Name</Text>
        <TextInput
          style={styles.input}
          value={name}
          onChangeText={setName}
          placeholder="e.g. Push-ups"
          placeholderTextColor="#9b8fba"
        />

        <Text style={styles.label}>Description</Text>
        <TextInput
          style={[styles.input, styles.textArea]}
          value={description}
          onChangeText={setDescription}
          placeholder="Describe this exercise"
          placeholderTextColor="#9b8fba"
          multiline
        />

        <Text style={styles.label}>Category</Text>
        <View style={styles.chips}>
          {categories.map(category => {
            const selected = selectedCategoryId === category.idExerciseCategory;

            return (
              <Pressable
                key={category.idExerciseCategory}
                style={[styles.chip, selected && styles.chipActive]}
                onPress={() =>
                  setSelectedCategoryId(category.idExerciseCategory)
                }
              >
                <Text
                  style={[styles.chipText, selected && styles.chipTextActive]}
                >
                  {category.name}
                </Text>
              </Pressable>
            );
          })}
        </View>

        <View style={styles.actions}>
          <Pressable
            style={styles.cancelButton}
            onPress={() => {
              setShowForm(false);
              setEditedExercise(null);
            }}
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

  const filteredExercises = exercises.filter(exercise => {
    const search = searchText.toLowerCase();

    return (
      exercise.name.toLowerCase().includes(search) ||
      exercise.categoryName.toLowerCase().includes(search) ||
      (exercise.description ?? '').toLowerCase().includes(search)
    );
  });

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.headerRow}>
        <Text style={styles.title}>Exercises</Text>

        <Pressable style={styles.addButton} onPress={openCreateForm}>
          <Text style={styles.addButtonText}>+ Add</Text>
        </Pressable>
      </View>

      <TextInput
        style={styles.searchInput}
        value={searchText}
        onChangeText={setSearchText}
        placeholder="Search exercises..."
        placeholderTextColor="#9b8fba"
      />

      <FlatList
        data={filteredExercises}
        keyExtractor={item => item.idExercise.toString()}
        contentContainerStyle={styles.listContent}
        renderItem={({ item }) => (
          <View style={styles.card}>
            <View style={styles.cardMain}>
              <Text style={styles.cardTitle}>{item.name}</Text>
              <Text style={styles.cardDescription}>
                {item.description || 'No description'}
              </Text>
              <Text style={styles.cardMeta}>{item.categoryName}</Text>
            </View>

            <View style={styles.cardActions}>
              <Pressable
                style={styles.editButton}
                onPress={() => openEditForm(item)}
              >
                <Text style={styles.editText}>Edit</Text>
              </Pressable>

              <Pressable
                style={styles.deleteButton}
                onPress={() => handleDelete(item)}
              >
                <Text style={styles.deleteText}>Delete</Text>
              </Pressable>
            </View>
          </View>
        )}
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
  cardMain: {
    marginBottom: 10,
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
  cardActions: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    gap: 8,
  },
  editButton: {
    backgroundColor: '#fff4cf',
    borderRadius: 10,
    paddingVertical: 7,
    paddingHorizontal: 14,
  },
  deleteButton: {
    backgroundColor: '#ffe0e2',
    borderRadius: 10,
    paddingVertical: 7,
    paddingHorizontal: 14,
  },
  editText: {
    color: '#8a6400',
    fontWeight: '700',
    fontSize: 13,
  },
  deleteText: {
    color: '#b5333f',
    fontWeight: '700',
    fontSize: 13,
  },
  searchInput: {
    backgroundColor: '#fff',
    borderColor: '#eee4ff',
    borderWidth: 1,
    borderRadius: 999,
    paddingVertical: 11,
    paddingHorizontal: 16,
    fontSize: 15,
    color: '#35215f',
    marginBottom: 14,
  },
});

export default ExercisesScreen;
