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
import { SimpleItem } from '../types/simpleItem';

interface SimpleListScreenProps {
  title: string;
  loadItems: () => Promise<SimpleItem[]>;
  createItem: (
    name: string,
    description: string,
    extraValue?: string,
  ) => Promise<void>;

  updateItem: (
    id: number,
    name: string,
    description: string,
    extraValue?: string,
  ) => Promise<void>;

  deleteItem: (id: number) => Promise<void>;
  descriptionLabel?: string;
  extraLabel?: string;
  extraPlaceholder?: string;
}

const SimpleListScreen: React.FC<SimpleListScreenProps> = ({
  title,
  loadItems,
  createItem,
  deleteItem,
  updateItem,
  descriptionLabel = 'Description',
  extraLabel,
  extraPlaceholder,
}) => {
  const [items, setItems] = useState<SimpleItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [extraValue, setExtraValue] = useState('');
  const [editingItem, setEditingItem] = useState<SimpleItem | null>(null);

  const loadData = async () => {
    try {
      setLoading(true);
      const data = await loadItems();
      setItems(data);
    } catch (err) {
      console.error(err);
      Alert.alert('Error', `Could not load ${title}`);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadData();
  }, [title]);

  const handleSave = async () => {
    if (!name.trim()) {
      Alert.alert('Validation error', 'Name is required');
      return;
    }

    try {
      if (editingItem) {
        await updateItem(
          editingItem.id,
          name.trim(),
          description.trim(),
          extraValue.trim(),
        );
      } else {
        await createItem(name.trim(), description.trim(), extraValue.trim());
      }
      setName('');
      setDescription('');
      setExtraValue('');
      setShowForm(false);
      await loadData();
      Alert.alert('Success', editingItem ? 'Item updated' : 'Item created');
      setEditingItem(null);
    } catch (err) {
      console.error(err);
      Alert.alert(
        'Error',
        `Could not ${editingItem ? 'update' : 'create'} ${title} item`,
      );
    }
  };

  const handleDelete = (item: SimpleItem) => {
    Alert.alert('Delete item', `Delete "${item.title}"?`, [
      { text: 'Cancel', style: 'cancel' },
      {
        text: 'Delete',
        style: 'destructive',
        onPress: async () => {
          try {
            await deleteItem(item.id);
            await loadData();
            Alert.alert('Success', 'Item deleted');
          } catch (err) {
            console.error(err);
            Alert.alert('Error', 'Could not delete item');
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
          {editingItem ? `Edit ${title}` : `Add ${title}`}
        </Text>

        <Text style={styles.label}>Name</Text>
        <TextInput
          style={styles.input}
          value={name}
          onChangeText={setName}
          placeholder="Enter name"
          placeholderTextColor="#9b8fba"
        />

        <Text style={styles.label}>{descriptionLabel}</Text>
        <TextInput
          style={[styles.input, styles.textArea]}
          value={description}
          onChangeText={setDescription}
          placeholder="Enter details"
          placeholderTextColor="#9b8fba"
          multiline
        />
        {extraLabel ? (
          <>
            <Text style={styles.label}>{extraLabel}</Text>
            <TextInput
              style={styles.input}
              value={extraValue}
              onChangeText={setExtraValue}
              placeholder={extraPlaceholder ?? ''}
              placeholderTextColor="#9b8fba"
            />
          </>
        ) : null}

        <View style={styles.actions}>
          <Pressable
            style={styles.cancelButton}
            onPress={() => {
              setShowForm(false);
              setEditingItem(null);
              setName('');
              setDescription('');
              setExtraValue('');
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

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.headerRow}>
        <Text style={styles.title}>{title}</Text>

        <Pressable style={styles.addButton} onPress={() => setShowForm(true)}>
          <Text style={styles.addButtonText}>+ Add</Text>
        </Pressable>
      </View>

      <FlatList
        data={items}
        keyExtractor={item => item.id.toString()}
        contentContainerStyle={styles.listContent}
        renderItem={({ item }) => (
          <View style={styles.card}>
            <View style={styles.cardMain}>
              <Text style={styles.cardTitle}>{item.title}</Text>

              {item.subtitle ? (
                <Text style={styles.cardDescription}>{item.subtitle}</Text>
              ) : null}

              {item.meta ? (
                <Text style={styles.cardMeta}>{item.meta}</Text>
              ) : null}
            </View>

            <View style={styles.cardActions}>
              <Pressable
                style={styles.editButton}
                onPress={() => {
                  setEditingItem(item);

                  setName(item.title);
                  setDescription(item.subtitle ?? '');
                  setExtraValue(item.extraValue ?? '');
                  setShowForm(true);
                }}
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
        ListEmptyComponent={
          <Text style={styles.emptyText}>No data available</Text>
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
  //     color: '#b5333f',
  //     fontWeight: '700',
  //     fontSize: 13,
  //   },
  emptyText: {
    textAlign: 'center',
    color: '#7f6aa7',
    marginTop: 40,
    fontSize: 16,
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
});

export default SimpleListScreen;
