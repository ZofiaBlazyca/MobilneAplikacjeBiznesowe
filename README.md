# Workout Planner App

Aplikacja mobilna do planowania treningów, stworzona w React Native + TypeScript, z backendem ASP.NET Core Web API, bazą SQL Server uruchamianą w Dockerze oraz architekturą CQRS.

## Autor

Zofia Błażyca

## Funkcjonalności

- wyświetlanie planów treningowych,
- dodawanie, edycja i usuwanie planów treningowych,
- zarządzanie ćwiczeniami,
- przypisywanie ćwiczeń do planów treningowych,
- usuwanie ćwiczeń z planu,
- dodawanie wpisów postępu treningowego,
- podgląd kategorii ćwiczeń, sprzętu, celów i użytkowników,
- komunikacja aplikacji mobilnej z API.

## Technologie

### Frontend

- React Native
- TypeScript
- Fetch API
- Android Emulator / Pixel 7

### Backend

- ASP.NET Core Web API
- Entity Framework Core
- CQRS
- MediatR
- Swagger

### Baza danych

- SQL Server / Azure SQL Edge
- Docker
- EF Core Migrations

### CQRS

Projekt wykorzystuje wzorzec CQRS (Command Query Responsibility Segregation).

- Query odpowiadają za odczyt danych.
- Command odpowiadają za modyfikację danych.

Przykłady:

- GetWorkoutPlansQuery
- CreateWorkoutPlanCommand
- UpdateExerciseCommand
- DeleteGoalCommand

Dzięki temu logika odczytu i zapisu jest rozdzielona.

## Architektura

```text
React Native App
        │
        ▼
ASP.NET Core Web API
        │
        ▼
CQRS (Commands / Queries)
        │
        ▼
Entity Framework Core
        │
        ▼
SQL Server (Docker)
```

## Encje w projekcie

Projekt zawiera 8 klas:

1. UserProfile
2. WorkoutPlan
3. Exercise
4. ExerciseCategory
5. Equipment
6. Goal
7. ProgressEntry
8. WorkoutPlanExercise

## Relacje

### Relacje jeden-do-wielu

- UserProfile → WorkoutPlan
- ExerciseCategory → Exercise
- WorkoutPlan → ProgressEntry
- Equipment → Exercise

### Relacja wiele-do-wielu

Relacja wiele-do-wielu występuje pomiędzy:

- WorkoutPlan
- Exercise

Jest obsługiwana przez tabelę pośrednią:

- WorkoutPlanExercise

Dzięki temu jeden plan treningowy może mieć wiele ćwiczeń, a jedno ćwiczenie może należeć do wielu planów.

## Uruchomienie backendu

W katalogu głównym projektu:

```bash
docker compose up -d
```

Następnie:

```bash
cd WorkoutPlanner.API
dotnet run
```

Swagger dostępny jest pod adresem:

```text
http://localhost:5029/swagger
```

## Uruchomienie aplikacji mobilnej

W katalogu aplikacji React Native:

```bash
cd SolutionOrdersMobile
pnpm install
pnpm react-native run-android
```

Aplikacja na emulatorze Android łączy się z API przez adres:

```text
http://10.0.2.2:5029/api
```

## Uwagi

Backend zawiera pełny CRUD dla wszystkich 8 encji. Aplikacja mobilna obsługuje najważniejsze funkcje biznesowe: zarządzanie planami treningowymi, ćwiczeniami, postępem oraz przypisywanie ćwiczeń do treningów.
