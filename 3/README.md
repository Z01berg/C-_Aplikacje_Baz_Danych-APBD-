# Ćwiczenia 3

## Refaktoryzacja istniejącej aplikacji

W niniejszym zadaniu musimy zrefaktoryzować kod istniejącej aplikacji. Po otwarciu solucji zobaczymy dwa projekty:

- **LegacyApp** - to jest aplikacja, którą będziemy chcieli zrefaktoryzować.
- **LegacyAppConsumer** - to jest przykład aplikacji, która wykorzystuje **LegacyApp**.

### Cel refaktoryzacji

Refaktoryzacja polega na tym, że **nie zmieniamy działania istniejącej aplikacji**. Zakładamy, że aplikacja działa poprawnie. Naszym zadaniem jest zrefaktoryzowanie klasy `UserService` wraz z metodą `AddUser`.

**Uwaga:** W `LegacyApp` znajdziecie klasy, które symulują odpytywanie zewnętrznych źródeł danych poprzez użycie `Thread.Wait`.

### Ograniczenia

1. **Klasa `UserDataAccess`** - ta klasa reprezentuje przykład spadkowej biblioteki, której z różnych powodów nie możemy edytować. Nie wolno modyfikować tej klasy.
2. **Aplikacja `LegacyAppConsumer`** - kod w tej aplikacji musi się cały czas kompilować i działać po procesie refaktoryzacji. Nie możemy modyfikować kodu w tym projekcie.

### Wymagania

1. **Zasady SOLID** - kieruj się zasadami SOLID podczas refaktoryzacji.
2. **Testowalność kodu** - staraj się, aby kod był łatwy do testowania.
3. **Czytelność kodu** - zadbaj o czytelność i zrozumiałość kodu.
4. **Struktura programu** - zapanuj nad strukturą programu, pamiętając o metrykach **cohesion** (spójność) i **coupling** (zależności).
5. **Testy jednostkowe** - postaraj się wykorzystać w rozwiązaniu testy jednostkowe.

### Kroki do wykonania

1. **Analiza istniejącego kodu** - przeanalizuj kod w `LegacyApp`, szczególnie klasę `UserService` i metodę `AddUser`.
2. **Refaktoryzacja** - wprowadź zmiany w kodzie, aby poprawić jego strukturę, czytelność i testowalność, nie zmieniając jego funkcjonalności.
3. **Testowanie** - upewnij się, że po refaktoryzacji aplikacja `LegacyAppConsumer` nadal działa poprawnie.
4. **Testy jednostkowe** - dodaj testy jednostkowe dla zrefaktoryzowanego kodu, aby zapewnić jego poprawność.

### Podsumowanie

Refaktoryzacja ma na celu poprawę jakości kodu bez zmiany jego funkcjonalności. Pamiętaj, że kod w `LegacyAppConsumer` musi działać bez zmian, a klasa `UserDataAccess` nie może być modyfikowana. Kieruj się zasadami SOLID, dbaj o testowalność i czytelność kodu, oraz wykorzystaj testy jednostkowe do weryfikacji poprawności wprowadzonych zmian.