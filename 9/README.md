# Ćwiczenia 9

## Tworzenie aplikacji z użyciem Entity Framework Code First

W niniejszym zadaniu przygotuj aplikację w oparciu o podejście **Entity Framework Code First**. Zaimplementuj następujące końcówki:

### 1. Dodawanie nowej recepty

Końcówka pozwalająca na wystawienie nowej recepty. Końcówka powinna przyjmować jako element żądania informacje o pacjencie, recepcie oraz informacje o lekach wystawionych na danej recepcie.

#### Wymagania:
- Jeśli pacjent przekazany w żądaniu nie istnieje, wstawiamy nowego pacjenta do tabeli `Pacjent`.
- Jeśli lek podany na recepcie nie istnieje, zwracamy błąd.
- Recepta może obejmować maksymalnie 10 leków. W przeciwnym wypadku zwracamy błąd.
- Musimy sprawdzić, czy `DueDate >= Date`.

#### Przykład żądania:

```json
{
    "patient": {
        "IdPatient": 1,
        "FirstName": "Jan",
        //...
    },
    "medicaments": [
        {"idMedicament": 1, "Dose": 3, "Description": "Some desc..." }
    ],
    "Date": "2012-01-01",
    "DueDate": "2012-01-01"
}
```

### 2. Wyświetlenie danych pacjenta

Przygotuj  końcówkę pozwalającą na wyświetlenie wszystkich danych na temat  konkretnego pacjenta, wraz z listą recept i leków, które pobrał.  Odpowiedź powinna uwzględniać wszystkie informacje na temat leków i  lekarzy. Dane na temat recept powinny być posortowane po polu `DueDate`.

#### Przykład odpowiedzi:

json

Copy

```
{
    "IdPatient": 1,
    "FirstName": "Jan",
    //...
    "Prescriptions": [
        {
            "IdPrescription": 1,
            "Date": "2012-01-01",
            "DueDate": "2012-01-01",
            "Medicaments": [
                {
                    "IdMedicament": 1,
                    "Name": "AAA",
                    "Dose": 10,
                    "Description": "AAA"
                }
            ],
            "Doctor": {
                "IdDoctor": 1,
                "FirstName": "AAA"
            }
        }
    ]
}
```

### 3. Testy jednostkowe

Spróbuj przygotować testy jednostkowe sprawdzające twoje końcówki.

### Wymagania dodatkowe

- **Nazewnictwo** - upewnij się, że nazewnictwo jest poprawne.
- **Struktura kodu** - kod powinien być podzielony na osobne warstwy i być testowalny.
- **Zasady programowania** - pamiętaj o zasadach **REST**, **SOLID**, **DRY**, **YAGNI**, **KISS**.
- **Blokowanie** - pamiętaj o optymistycznym/pesymistycznym blokowaniu, jeśli jest ono potrzebne.

### Podsumowanie

Celem ćwiczenia jest stworzenie aplikacji, która umożliwia wystawianie nowych recept oraz wyświetlanie danych pacjenta wraz z listą recept i leków.  Aplikacja powinna być oparta na podejściu **Entity Framework Code First** i spełniać wymagania dotyczące walidacji danych, struktury kodu oraz  zasad programowania. Dodatkowo, należy przygotować testy jednostkowe  sprawdzające funkcjonalność końcówek.
