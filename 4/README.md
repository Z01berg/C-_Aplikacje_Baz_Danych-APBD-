# Ćwiczenia 4

## Tworzenie aplikacji REST API dla schroniska weterynaryjnego

Stwórz aplikację REST API, która pozwoli nam zarządzać danymi zwierząt w bazie danych schroniska dla kliniki weterynaryjnej.

### Opis zwierzęcia

Zwierzę opisywane jest przez następujące pola:

- `id`
- `imię`
- `kategoria` (np. pies, kot itp.)
- `masa`
- `kolor sierści`

### Wymagania funkcjonalne

Chcielibyśmy mieć możliwość:

1. Pobierania listy zwierząt.
2. Pobierania konkretnego zwierzęcia po `id`.
3. Dodawania nowego zwierzęcia.
4. Edycji istniejącego zwierzęcia.
5. Usuwania zwierzęcia.

### Zarządzanie wizytami

Ponadto chcielibyśmy zapisywać informacje na temat wizyt zwierzęcia:

1. Pobierania listy wizyt powiązanych z danym zwierzęciem.
2. Dodawania nowych wizyt.

#### Opis wizyty

Wizyta obejmuje następujące informacje:

- `data wizyty`
- `zwierzę`
- `opis wizyty`
- `cena wizyty`

### Kroki do wykonania

1. **Przygotuj aplikację REST API** z odpowiednimi końcówkami HTTP:
   - `GET` - pobieranie danych
   - `POST` - dodawanie nowych danych
   - `PUT` - edycja istniejących danych
   - `DELETE` - usuwanie danych

2. **Struktura końcówek** powinna być zgodna z zasadami projektowania końcówek REST.

3. **Baza danych** - jako bazę danych przygotuj statyczną kolekcję z obiektami (można użyć listy w pamięci).

4. **Podejście do implementacji**:
   - Możesz wykorzystać podejście **MinimalAPI**.
   - Możesz również skorzystać z wersji API wykorzystującej klasy kontrolerów.

5. **Testowanie aplikacji** - przetestuj przygotowaną aplikację z pomocą narzędzia **Postman**.

### Przykładowe końcówki

#### Zwierzęta

- `GET /api/animals` - pobierz listę wszystkich zwierząt.
- `GET /api/animals/{id}` - pobierz konkretne zwierzę po `id`.
- `POST /api/animals` - dodaj nowe zwierzę.
- `PUT /api/animals/{id}` - edytuj istniejące zwierzę.
- `DELETE /api/animals/{id}` - usuń zwierzę.

#### Wizyty

- `GET /api/animals/{id}/visits` - pobierz listę wizyt dla danego zwierzęcia.
- `POST /api/animals/{id}/visits` - dodaj nową wizytę dla danego zwierzęcia.

### Podsumowanie

Celem ćwiczenia jest stworzenie aplikacji REST API, która pozwala na zarządzanie danymi zwierząt oraz ich wizytami w schronisku weterynaryjnym. Aplikacja powinna być zgodna z zasadami REST, a jej funkcjonalność powinna być przetestowana za pomocą narzędzia Postman.