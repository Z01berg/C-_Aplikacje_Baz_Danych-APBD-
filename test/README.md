# Test 6 - Wersja 6

## Struktura bazy danych

### Tabela `Patient`
- `IdPatient` (int, PK)
- `FirstName` (nvarchar(100))
- `LastName` (nvarchar(100))
- `Birthdate` (date)

### Tabela `Visit`
- `IdVisit` (int, PK)
- `Date` (datetime)
- `IdPatient` (int, FK)
- `IdDoctor` (int, FK)
- `Price` (money)

### Tabela `Doctor`
- `IdDoctor` (int, PK)
- `FirstName` (nvarchar(100))
- `LastName` (nvarchar(100))
- `Specialization` (nvarchar(100), nullable)
- `PriceForVisit` (money)

## Zadanie 1: Pobranie danych pacjenta wraz z wizytami

Przygotuj końcówkę, która zwróci dane pacjenta wraz z informacjami na temat przypisanych do niego wizyt.

### Przykład odpowiedzi:

```json
{
    "firstName": "Jan",
    "lastName": "Kowalski",
    "birthdate": "1980-01-01", // format daty może być inny, np. datetime
    "totalAmountMoneySpent": "1200 zł",
    "numberOfVisit": 10,
    "visits": [
        {
            "IdVisit": 1,
            "Doctor": "John Doe",
            "Date": "2024-03-12 12:30",
            "Price": "100 zł"
        },
        // ...
    ]
}
```

### Wymagania:

- Kolumna `totalAmountMoneySpent` zwraca sumę wydanych przez pacjenta pieniędzy.
- Kolumna `numberOfVisit` zwraca liczbę wizyt przypisanych do pacjenta.

## Zadanie 2: Wstawienie nowej wizyty

Zaimplementuj końcówkę, która pozwala na wstawienie nowej wizyty.

### Wymagania:

1. Końcówka powinna przyjmować następujące parametry:
   - `IdPatient`
   - `IdDoctor`
   - `Date`
2. Przed wstawieniem wizyty należy sprawdzić:
   - Czy pacjent istnieje.
   - Czy doktor istnieje.
   - Czy pacjent nie ma już umówionej innej wizyty w danym terminie (sprawdź, czy w bazie istnieje jakakolwiek wizyta z `Date > Now`).
   - Czy wybrany lekarz jest dostępny w danym terminie na podstawie tabeli `Schedule`. Jeśli doktor nie jest dostępny, zwróć odpowiedni kod błędu.
3. Wstaw rekord do tabeli `Visit`.
4. Zwróć wygenerowaną wartość `IdVisit`.
5. Pamiętaj o zwracaniu odpowiednich kodów błędów (np. 400 Bad Request, 404 Not Found).

### Dodatkowe informacje:

- **Zasady REST** - pamiętaj o zasadach projektowania aplikacji typu REST.
- **Dobre praktyki** - dbaj o architekturę aplikacji, oddzielenie logiki biznesowej, prezentacji i infrastruktury.
- **Kompilacja** - upewnij się, że aplikacja się kompiluje.
- **Kody HTTP** - pamiętaj o zwracaniu odpowiednich kodów HTTP i obsłudze błędów.
- **Priorytety** - w pierwszej kolejności ważne jest działanie aplikacji, w drugiej kolejności jakość kodu.

### Podsumowanie

Celem testu jest zaimplementowanie dwóch końcówek:

1. **Pobranie danych pacjenta wraz z wizytami** - zwraca informacje o pacjencie, sumę wydanych pieniędzy oraz listę wizyt.
2. **Wstawienie nowej wizyty** - umożliwia dodanie nowej wizyty po wcześniejszym sprawdzeniu dostępności pacjenta i lekarza.

Pamiętaj o zasadach REST, dobrych praktykach programistycznych oraz odpowiedniej obsłudze błędów.
