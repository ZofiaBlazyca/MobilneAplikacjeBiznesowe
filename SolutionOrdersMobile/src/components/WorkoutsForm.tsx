import React, { useState } from 'react';
import {
  Alert,
  Pressable,
  SafeAreaView,
  StyleSheet,
  Text,
  TextInput,
  View,
} from 'react-native';
import { Workout } from '../types/workout';

interface WorkoutsFormProps {
  workout?: Workout | null;
  onCancel: () => void;
  onSave: (name: string, description: string) => Promise<void>;
}

const WorkoutsForm: React.FC<WorkoutsFormProps> = ({
  workout,
  onCancel,
  onSave,
}) => {
  const [name, setName] = useState(workout?.name ?? '');
  const [description, setDescription] = useState(workout?.description ?? '');
  const [saving, setSaving] = useState(false);

  const isEditMode = !!workout;

  const handleSave = async () => {
    if (!name.trim()) {
      Alert.alert('Validation error', 'Workout name is required');
      return;
    }

    try {
      setSaving(true);
      await onSave(name.trim(), description.trim());
    } finally {
      setSaving(false);
    }
  };

  return (
    <SafeAreaView style={styles.container}>
      <Text style={styles.title}>
        {isEditMode ? 'Edit Workout' : 'Add Workout'}
      </Text>

      <Text style={styles.label}>Name</Text>
      <TextInput
        style={styles.input}
        value={name}
        onChangeText={setName}
        placeholder="e.g. Full Body"
        placeholderTextColor="#9b8fba"
      />

      <Text style={styles.label}>Description</Text>
      <TextInput
        style={[styles.input, styles.textArea]}
        value={description}
        onChangeText={setDescription}
        placeholder="Describe this workout"
        placeholderTextColor="#9b8fba"
        multiline
      />

      <View style={styles.actions}>
        <Pressable style={styles.cancelButton} onPress={onCancel}>
          <Text style={styles.cancelText}>Cancel</Text>
        </Pressable>

        <Pressable
          style={styles.saveButton}
          onPress={handleSave}
          disabled={saving}
        >
          <Text style={styles.saveText}>{saving ? 'Saving...' : 'Save'}</Text>
        </Pressable>
      </View>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    backgroundColor: '#fbf7f7',
  },
  title: {
    fontSize: 28,
    fontWeight: '800',
    color: '#322056',
    marginBottom: 24,
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
    minHeight: 110,
    textAlignVertical: 'top',
  },
  actions: {
    flexDirection: 'row',
    gap: 12,
    marginTop: 12,
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
});

export default WorkoutsForm;
