# Ćwiczenia 10

## Dodawanie uwierzytelnienia do aplikacji

W trakcie niniejszych ćwiczeń dodajemy końcówki umożliwiające uwierzytelnienie. Pracujemy na kodzie z poprzednich ćwiczeń.

### 1. Middleware do globalnej obsługi błędów

Zaimplementuj middleware, który zapewni globalną obsługę błędów w aplikacji. Middleware powinien przechwytywać wyjątki i zwracać odpowiednie komunikaty błędów w formacie JSON.

### 2. Rejestracja nowego użytkownika

Zaimplementuj końcówkę umożliwiającą zarejestrowanie nowego użytkownika. Użytkownik podczas rejestracji powinien podać login i hasło.

#### Przykład żądania:

```json
{
    "login": "jan12",
    "password": "asd123"
}
```

### 3. Logowanie użytkownika

Zaimplementuj końcówkę umożliwiającą zalogowanie użytkownika. W wyniku logowania powinniśmy zwrócić **access token** i **refresh token**. Refresh token powinien zostać zapisany w bazie danych.

#### Przykład odpowiedzi:

json

Copy

```
{
    "accessToken": "A320WPSK22d...",
    "refreshToken": "ASD123FDW2..."
}
```

### 4. Odświeżanie tokenu

Zaimplementuj końcówkę umożliwiającą uzyskanie nowego **access tokenu** na podstawie **refresh tokenu**.

#### Przykład żądania:

http

Copy

```
POST /api/refresh
Authorization: Bearer ASD2232KADS...
{
    "refreshToken": "AS2K23..."
}
```

#### Przykład odpowiedzi:

json

Copy

```
{
    "accessToken": "A320WPSK22d...",
    "refreshToken": "ASD123FDW2..."
}
```

### Proces logowania

1. **Klient** wysyła żądanie HTTP POST na endpoint `/api/login` z danymi logowania.
2. **Serwer** zwraca odpowiedź z **access tokenem** i **refresh tokenem**.

http

Copy

```
POST /api/login
{
    "login": "jan12",
    "password": "asd123"
}

200 OK
{
    "accessToken": "A320WPSK22d...",
    "refreshToken": "ASD123FDW2..."
}
```

### Proces odświeżania tokenu

1. **Klient** wysyła żądanie HTTP POST na endpoint `/api/refresh` z **refresh tokenem**.
2. **Serwer** zwraca nowy **access token** i **refresh token**.

http

Copy

```
POST /api/refresh
Authorization: Bearer ASD2232KADS...
{
    "refreshToken": "AS2K23..."
}

200 OK
{
    "accessToken": "A320WPSK22d...",
    "refreshToken": "ASD123FDW2..."
}
```

### Podsumowanie

Celem ćwiczenia jest dodanie funkcjonalności uwierzytelnienia do istniejącej aplikacji. Należy zaimplementować:

1. Middleware do globalnej obsługi błędów.
2. Końcówkę do rejestracji nowego użytkownika.
3. Końcówkę do logowania użytkownika, zwracającą **access token** i **refresh token**.
4. Końcówkę do odświeżania **access tokenu** na podstawie **refresh tokenu**.

Pamiętaj o bezpiecznym przechowywaniu haseł (np. przy użyciu hashowania) oraz o walidacji danych wejściowych.
