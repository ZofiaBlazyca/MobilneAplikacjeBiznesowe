import React from 'react';
import { SafeAreaView, StyleSheet, Text, View } from 'react-native';

const AboutScreen = (): React.JSX.Element => {
  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.card}>
        <Text style={styles.overline}>About</Text>
        <Text style={styles.title}>Workout Planner</Text>
        <Text style={styles.subtitle}>
          Application for planning workouts, managing exercises, and tracking
          progress.
        </Text>

        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Author</Text>
          <Text style={styles.text}>Zofia Błażyca</Text>
        </View>

        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Technologies</Text>
          <Text style={styles.text}>React Native + TypeScript</Text>
          <Text style={styles.text}>ASP.NET Core Web API</Text>
          <Text style={styles.text}>Entity Framework Core</Text>
          <Text style={styles.text}>CQRS + MediatR</Text>
          <Text style={styles.text}>SQL Server in Docker</Text>
        </View>

        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Main features</Text>
          <Text style={styles.text}>• Workout planning</Text>
          <Text style={styles.text}>• Exercise management</Text>
          <Text style={styles.text}>• Progress tracking</Text>
          <Text style={styles.text}>• Many-to-many relation support</Text>
          <Text style={styles.text}>• REST API integration</Text>
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
  card: {
    backgroundColor: '#fff',
    borderRadius: 22,
    padding: 22,
    borderWidth: 1,
    borderColor: '#eee4ff',
  },
  overline: {
    fontSize: 12,
    textTransform: 'uppercase',
    letterSpacing: 1.2,
    color: '#6a3fb8',
    fontWeight: '800',
    marginBottom: 6,
  },
  title: {
    fontSize: 28,
    fontWeight: '800',
    color: '#322056',
  },
  subtitle: {
    fontSize: 15,
    color: '#6d5b92',
    marginTop: 4,
    marginBottom: 18,
  },
  section: {
    marginTop: 18,
  },
  sectionTitle: {
    fontSize: 16,
    fontWeight: '800',
    color: '#5a359d',
    marginBottom: 6,
  },
  text: {
    fontSize: 15,
    color: '#35215f',
    lineHeight: 22,
  },
});

export default AboutScreen;
