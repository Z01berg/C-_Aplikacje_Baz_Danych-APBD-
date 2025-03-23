# Ćwiczenia 5

## Tworzenie aplikacji REST API z bazą danych SQL Server

W trakcie niniejszych ćwiczeń do wykonania jest prosta aplikacja REST API, która umożliwia wykonanie operacji pozwalających na modyfikowanie danych w bazie SQL Server. Razem z zadaniem załączony jest skrypt pozwalający na stworzenie tabelki `Animals` i wypełnienie jej danymi. Komunikacja z bazą danych powinna odbywać się poprzez klasy `SqlConnection` i `SqlCommand`.

### Dane serwera

- **Serwer bazy danych:** `confidential`

### Zadania do wykonania

1. **Dodaj kontroler `Animals`**:
   - Kontroler będzie odpowiadał za obsługę żądań związanych z operacjami na tabeli `Animals`.

2. **Dodaj metodę/endpoint pozwalający na uzyskanie listy zwierząt**:
   - Końcówka powinna reagować na żądanie typu HTTP GET wysłane na adres `/api/animals`.
   - Końcówka powinna pozwolić na przyjęcie parametru w query string, który określa sortowanie. Parametr nazywa się `orderBy`. Przykład: `/api/animals?orderBy=name`.
   - Parametr `orderBy` przyjmuje następujące wartości: `name`, `description`, `category`, `area`. Możemy sortować wyłącznie po jednej kolumnie. Sortowanie jest zawsze w kierunku „ascending”.
   - Domyślne sortowanie (kiedy w żądaniu nie zostanie przekazany parametr w query string) powinno odbywać się po kolumnie `name`.

3. **Dodaj metodę/endpoint pozwalający na dodanie nowego zwierzęcia**:
   - Metoda powinna odpowiadać na żądanie HTTP POST wysłane na adres `/api/animals`.
   - Metoda powinna przyjmować dane w postaci JSON.

4. **Dodaj metodę/endpoint pozwalający na aktualizację danych konkretnego zwierzęcia**:
   - Metoda powinna odpowiadać na żądanie HTTP PUT wysłane na adres `/api/animals/{idAnimal}`.
   - Metoda przyjmuje dane w postaci JSON.
   - Zakładamy, że klucze główne nie ulegają modyfikacji (kolumna `IdAnimal`).

5. **Dodaj metodę/endpoint do usuwania danych na temat konkretnego zwierzęcia**:
   - Metoda powinna odpowiadać na żądanie HTTP DELETE wysłane na adres `/api/animals/{idAnimal}`.

### Wymagania dodatkowe

1. **Poprawne kody HTTP** - pamiętaj o zwracaniu odpowiednich kodów HTTP w zależności od wyniku operacji (np. 200 OK, 201 Created, 400 Bad Request, 404 Not Found).
2. **Dependency Injection** - postaraj się skorzystać z wbudowanego mechanizmu Dependency Injection.
3. **Walidacja danych** - dbaj o walidację danych przesyłanych w żądaniach.
4. **Nazewnictwo i styl** - zadbaj o odpowiednie nazewnictwo metod, zmiennych oraz styl kodu.

### Przykładowe końcówki

- **GET /api/animals** - pobierz listę zwierząt z opcjonalnym sortowaniem.
- **POST /api/animals** - dodaj nowe zwierzę.
- **PUT /api/animals/{idAnimal}** - zaktualizuj dane konkretnego zwierzęcia.
- **DELETE /api/animals/{idAnimal}** - usuń dane konkretnego zwierzęcia.

### Podsumowanie

Celem ćwiczenia jest stworzenie aplikacji REST API, która umożliwia zarządzanie danymi w tabeli `Animals` w bazie danych SQL Server. Aplikacja powinna obsługiwać operacje CRUD (Create, Read, Update, Delete) oraz umożliwiać sortowanie danych. Pamiętaj o poprawnym użyciu kodów HTTP, walidacji danych oraz zasadach Dependency Injection.