# Ćwiczenia 6

## Tworzenie aplikacji dla zarządzania magazynem

W trakcie tego ćwiczenia ponownie użyjemy klas `SqlConnection` i `SqlCommand`. Tym razem logika interakcji z bazą danych będzie bardziej skomplikowana. Tworzymy aplikację dla firmy zarządzającej magazynem. Poniżej przedstawiono strukturę bazy danych, której używamy. W pliku `create.sql` znajdziesz skrypt, który tworzy tabele i wypełnia je danymi.

### Struktura bazy danych

#### Tabela `Product`
- `IdProduct` (int, PK)
- `Name` (nvarchar(200))
- `Description` (nvarchar(200))
- `Price` (numeric(25,2))

#### Tabela `Order`
- `IdOrder` (int, PK)
- `IdProduct` (int, FK)
- `Amount` (int)
- `CreatedAt` (datetime)
- `FulfilledAt` (datetime, nullable)

#### Tabela `Product_Warehouse`
- `IdProductWarehouse` (int, PK)
- `IdWarehouse` (int, FK)
- `IdProduct` (int, FK)
- `IdOrder` (int, FK)
- `Amount` (int)
- `Price` (numeric(25,2))
- `CreatedAt` (datetime)

#### Tabela `Warehouse`
- `IdWarehouse` (int, PK)
- `Name` (nvarchar(200))
- `Address` (nvarchar(200))

### Zadanie 1

Utwórz `WarehouseController` i endpoint, który będzie akceptować dane w następującym formacie:

```json
{
    "IdProduct": 1,
    "IdWarehouse": 2,
    "Amount": 20,
    "CreatedAt": "2012-04-23T18:25:43.511Z"
}Wszystkie pola są wymagane. Następnie zaimplementuj następujący scenariusz:
```

1. **Sprawdź istnienie produktu i magazynu**:
   - Sprawdź, czy produkt o podanym `IdProduct` istnieje.
   - Sprawdź, czy magazyn o podanym `IdWarehouse` istnieje.
   - Wartość `Amount` przekazana w żądaniu powinna być większa niż 0.
2. **Sprawdź istnienie zamówienia**:
   - Możemy dodać produkt do magazynu tylko wtedy, gdy istnieje zamówienie zakupu produktu w tabeli `Order`.
   - Sprawdź, czy w tabeli `Order` istnieje rekord z `IdProduct` i `Amount`, które odpowiadają naszemu żądaniu.
   - Data utworzenia zamówienia (`CreatedAt`) powinna być wcześniejsza niż data utworzenia w żądaniu.
3. **Sprawdź, czy zamówienie zostało zrealizowane**:
   - Sprawdź, czy nie ma wiersza z danym `IdOrder` w tabeli `Product_Warehouse`.
4. **Aktualizuj zamówienie**:
   - Zaktualizuj kolumnę `FulfilledAt` zamówienia na aktualną datę i godzinę (UPDATE).
5. **Wstaw rekord do tabeli `Product_Warehouse`**:
   - Wstaw rekord do tabeli `Product_Warehouse`.
   - Kolumna `Price` powinna odpowiadać cenie produktu pomnożonej przez kolumnę `Amount` z naszego zamówienia.
   - Wstaw wartość `CreatedAt` zgodnie z aktualnym czasem (INSERT).
6. **Zwróć wartość klucza głównego**:
   - W wyniku operacji zwróć wartość klucza głównego wygenerowanego dla rekordu wstawionego do tabeli `Product_Warehouse`.

### Zadanie 2*

Dodaj drugi endpoint do `WarehouseController` i zaimplementuj tę samą logikę za pomocą procedury składowanej. W pliku `proc.sql` znajdziesz procedurę składowaną przygotowaną przez administratora bazy  danych. Sprawdź poprawność procedury i wykonaj ją z poziomu endpointu.

### Pamiętaj

- W przypadku jakiegokolwiek błędu zwróć odpowiedni kod HTTP (np. 400 Bad Request, 404 Not Found, 500 Internal Server Error).
- Dbaj o walidację danych i odpowiednie nazewnictwo metod oraz zmiennych.

### Podsumowanie

Celem ćwiczenia jest stworzenie aplikacji, która umożliwia zarządzanie  produktami w magazynie poprzez interakcję z bazą danych. Aplikacja  powinna obsługiwać dodawanie produktów do magazynu, sprawdzanie zamówień oraz aktualizację danych w bazie. Dodatkowo, w drugim zadaniu należy  zaimplementować tę samą logikę za pomocą procedury składowanej.