import React, { useState } from 'react';
import {
  View,
  Text,
  FlatList,
  Pressable,
  StyleSheet,
  SafeAreaView,
  LayoutAnimation,
  Platform,
  UIManager,
} from 'react-native';
import { Workout } from '../types/workout';

if (
  Platform.OS === 'android' &&
  UIManager.setLayoutAnimationEnabledExperimental
) {
  UIManager.setLayoutAnimationEnabledExperimental(true);
}

interface WorkoutsListProps {
  workouts: Workout[];
  onSelectWorkout: (workout: Workout) => void;
  onAddWorkout: () => void;
  onEditWorkout: (workout: Workout) => void;
  onDeleteWorkout: (workout: Workout) => void;
}

const WorkoutsList: React.FC<WorkoutsListProps> = ({
  workouts,
  onSelectWorkout,
  onAddWorkout,
  onEditWorkout,
  onDeleteWorkout,
}) => {
  const [isHeroExpanded, setIsHeroExpanded] = useState(true);
  const totalExercises = workouts.reduce(
    (sum, workout) => sum + workout.exercises.length,
    0,
  );

  const toggleHero = () => {
    LayoutAnimation.configureNext(LayoutAnimation.Presets.easeInEaseOut);
    setIsHeroExpanded(prev => !prev);
  };

  return (
    <SafeAreaView style={styles.container}>
      <Pressable
        style={isHeroExpanded ? styles.heroCard : styles.heroCardCollapsed}
        onPress={toggleHero}
      >
        <View style={styles.heroHeaderRow}>
          <View>
            <Text style={styles.heroOverline}>Workout Planner App</Text>
            <Text
              style={
                isHeroExpanded ? styles.heroTitle : styles.heroTitleCollapsed
              }
            >
              Train with a clear plan
            </Text>
          </View>

          <Text style={styles.collapseIcon}>{isHeroExpanded ? '▲' : '▼'}</Text>
        </View>

        {isHeroExpanded ? (
          <>
            <Text style={styles.heroSubtitle}>
              Build routines, track exercises, and keep your training organized.
            </Text>

            <View style={styles.statsRow}>
              <View style={styles.statBox}>
                <Text style={styles.statValue}>{workouts.length}</Text>
                <Text style={styles.statLabel}>Workouts</Text>
              </View>
              <View style={styles.statBox}>
                <Text style={styles.statValue}>{totalExercises}</Text>
                <Text style={styles.statLabel}>Exercises</Text>
              </View>
            </View>

            <Pressable
              style={styles.addWorkoutButton}
              onPress={event => {
                event.stopPropagation();
                onAddWorkout();
              }}
            >
              <Text style={styles.addWorkoutButtonText}>+ Add Workout</Text>
            </Pressable>
          </>
        ) : (
          <Text style={styles.heroCollapsedText}>
            {workouts.length} workouts • {totalExercises} exercises
          </Text>
        )}
      </Pressable>

      <View style={styles.sectionHeader}>
        <Text style={styles.sectionTitle}>Your Workouts</Text>
        {/* <Pressable style={styles.sectionActionButton} onPress={() => {}}>
          <Text style={styles.sectionActionText}>Manage</Text>
        </Pressable> */}
      </View>

      <FlatList
        data={workouts}
        keyExtractor={item => item.id.toString()}
        showsVerticalScrollIndicator={false}
        contentContainerStyle={styles.listContent}
        renderItem={({ item }) => (
          <View style={styles.workoutCard}>
            <Pressable
              style={styles.workoutMain}
              onPress={() => onSelectWorkout(item)}
            >
              <Text style={styles.itemTitle}>{item.name}</Text>
              <Text style={styles.itemDesc}>{item.description}</Text>
              <Text style={styles.itemMeta}>
                {item.exercises.length} exercises
              </Text>
            </Pressable>
            <View style={styles.itemActions}>
              <Pressable
                style={styles.editButton}
                onPress={() => onEditWorkout(item)}
              >
                <Text style={styles.editActionText}>Edit</Text>
              </Pressable>
              <Pressable
                style={styles.deleteButton}
                onPress={() => onDeleteWorkout(item)}
              >
                <Text style={styles.deleteActionText}>Delete</Text>
              </Pressable>
            </View>
          </View>
        )}
        ListEmptyComponent={
          <Text style={styles.emptyText}>
            No workouts yet. Tap + Add to create one!
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
    paddingTop: 32,
    backgroundColor: '#fbf7f7',
  },
  heroCard: {
    marginTop: 12,
    marginBottom: 16,
    borderRadius: 20,
    padding: 20,
    backgroundColor: '#4d328b',
    shadowColor: '#4d2a96',
    shadowOpacity: 0.28,
    shadowRadius: 12,
    shadowOffset: { width: 0, height: 6 },
    elevation: 6,
  },
  heroOverline: {
    fontSize: 12,
    textTransform: 'uppercase',
    letterSpacing: 1.2,
    color: '#e6d8ff',
    marginBottom: 8,
  },
  heroTitle: {
    fontSize: 28,
    fontWeight: '800',
    color: '#ffffff',
    marginBottom: 8,
  },
  heroSubtitle: {
    fontSize: 15,
    color: '#efe6ff',
    lineHeight: 22,
    marginBottom: 16,
  },
  statsRow: {
    flexDirection: 'row',
    gap: 10,
    marginBottom: 16,
  },
  statBox: {
    flex: 1,
    borderRadius: 12,
    paddingVertical: 12,
    paddingHorizontal: 14,
    backgroundColor: '#7252bf',
  },
  statValue: {
    fontSize: 24,
    fontWeight: '800',
    color: '#fff',
  },
  statLabel: {
    fontSize: 12,
    color: '#e6d8ff',
    marginTop: 2,
  },
  addWorkoutButton: {
    alignSelf: 'flex-start',
    borderRadius: 12,
    backgroundColor: '#dcf6ea',
    paddingVertical: 10,
    paddingHorizontal: 16,
  },
  addWorkoutButtonText: {
    color: '#2c7a5f',
    fontWeight: '800',
    fontSize: 15,
  },
  sectionHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 10,
  },
  sectionTitle: {
    fontSize: 22,
    fontWeight: '700',
    color: '#322056',
  },
  sectionActionButton: {
    backgroundColor: '#eee3ff',
    borderRadius: 999,
    paddingVertical: 6,
    paddingHorizontal: 12,
  },
  sectionActionText: {
    color: '#5a359d',
    fontWeight: '700',
    fontSize: 12,
  },
  listContent: {
    paddingBottom: 28,
  },
  workoutCard: {
    marginBottom: 12,
    borderRadius: 16,
    backgroundColor: '#ffffff',
    padding: 14,
    shadowColor: '#8d72c8',
    shadowOpacity: 0.09,
    shadowRadius: 10,
    shadowOffset: { width: 0, height: 4 },
    elevation: 3,
    borderWidth: 1,
    borderColor: '#eee4ff',
  },
  workoutMain: {
    marginBottom: 10,
  },
  itemActions: {
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
  editActionText: {
    color: '#8a6400',
    fontWeight: '700',
    fontSize: 13,
  },
  deleteActionText: {
    color: '#b5333f',
    fontWeight: '700',
    fontSize: 13,
  },
  itemTitle: {
    fontSize: 20,
    fontWeight: '700',
    color: '#35215f',
  },
  itemDesc: {
    fontSize: 14,
    color: '#6d5b92',
    marginTop: 4,
  },
  itemMeta: {
    marginTop: 8,
    fontSize: 12,
    fontWeight: '600',
    color: '#6a3fb8',
  },
  emptyText: {
    textAlign: 'center',
    color: '#7f6aa7',
    marginTop: 40,
    fontSize: 16,
  },
  heroCardCollapsed: {
    marginTop: 12,
    marginBottom: 16,
    borderRadius: 20,
    padding: 16,
    backgroundColor: '#4d328b',
    shadowColor: '#4d2a96',
    shadowOpacity: 0.18,
    shadowRadius: 8,
    shadowOffset: { width: 0, height: 4 },
    elevation: 4,
  },

  heroHeaderRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },

  heroTitleCollapsed: {
    fontSize: 20,
    fontWeight: '800',
    color: '#ffffff',
  },

  collapseIcon: {
    color: '#ffffff',
    fontSize: 18,
    fontWeight: '800',
  },

  heroCollapsedText: {
    marginTop: 8,
    color: '#efe6ff',
    fontSize: 13,
    fontWeight: '600',
  },
});

export default WorkoutsList;
