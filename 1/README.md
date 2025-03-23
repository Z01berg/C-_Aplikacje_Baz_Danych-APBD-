# Ćwiczenia 1

## Zadanie 1 - Założenie konta na GitHub

Załóż konto na platformie GitHub, wykorzystując przy tym szkolny adres email. Jeśli masz już konto, dodaj szkolny adres do istniejącego konta w ustawieniach.

W domu warto wygenerować klucz SSH i wykorzystywać go podczas uwierzytelniania.

[Instrukcja generowania klucza SSH](https://docs.github.com/en/authentication/connecting-to-github-with-ssh)

## Zadanie 2 - Konfiguracja Git'a

1. Przy pierwszym użyciu może zaistnieć potrzeba konfiguracji Git'a.
2. Minimalny zestaw ustawień, który warto skonfigurować, to nazwa użytkownika i email.

```bash
git config --global user.name "sxxxx"
git config --global user.email "sxxxx@pjwstk.edu.pl"
```

## Zadanie 2 - Nowe repozytorium

1. Stwórz nowe repozytorium na GitHub.
2. Sklonuj powstałe repozytorium na pulpit.
3. Umieść odpowiedni plik `.gitignore` w repozytorium. Można wybrać jeden z predefiniowanych plików dla Visual Studio.
4. [Przykładowe pliki .gitignore](https://github.com/github/gitignore)
5. Dodaj do repozytorium nową aplikację konsolową .NET. Wykonaj commit o nazwie  "Initial project" i wykonaj push do repozytorium online.
6. Wykonaj 3 kolejne commity. W każdym commicie wprowadź modyfikację w kodzie. Dla każdego z commitów podaj nazwę "Modyfikacja 1", "Modyfikacja 2",  "Modyfikacja 3".
7. Wszystkie commity powinny być widoczne online na GitHub.

Możesz również najpierw stworzyć repozytorium lokalnie, a później podpiąć je do repozytorium na GitHub.

## Zadanie 3 - Nowe zadanie

Załóżmy, że otrzymałeś nowe zadanie. Musisz stworzyć statyczną metodę, która  przyjmuje tablicę int'ów i zwraca wyliczoną średnią.

1. Stwórz osobny branch o nazwie `feature-average`.
2. Umieść na tym branchu commity, które implementują wymagania. Możesz umieścić jeden lub dowolną większą liczbę commitów.
3. Zmerguj powstały branch z główną gałęzią `main`. Jaki domyślny rodzaj merge'a zostanie wykonany przez Git'a?
4. Sprawdź, jak wygląda obecnie historia repozytorium poprzez komendę:

bash

Copy

```
git log --oneline --graph
```

1. Zakładając, że branch `main` nie miał żadnych dodatkowych commitów, Git powinien skorzystać z metody "fast-forward".

## Zadanie 4 - Rebase

W kolejnym zadaniu mamy dodać statyczną metodę, która przyjmuje tablicę int'ów i zwraca maksymalną wartość.

1. Stwórz nowy branch `feature-max`.
2. Zaimplementuj opisaną funkcjonalność, dodając commity na branch.
3. Na koniec wykonaj merge swojej gałęzi do gałęzi `main`. Tym razem spróbuj wykonać merge, wykorzystując komendę `rebase` (warto skorzystać z flagi "interactive").
4. Z pomocą komendy `git log` sprawdź, jak wygląda historia repozytorium.
5. Wszystkie zmiany powinny zostać wypełnione do repozytorium online.

## Zadanie 5 - Konflikt

W tym zadaniu zasymulujemy powstanie konfliktu.

1. Stwórz nową gałąź `feature-new`.
2. Będąc na nowo powstałej gałęzi, spróbuj zmodyfikować pętlę odpowiadającą za  wyliczenie średniej. Możesz np. zmienić nazwę zmiennej wykorzystywanej w ramach pętli.
3. Wykonaj commit na gałęzi `feature-new`.
4. Przełącz się na gałąź `main` i wykonaj inną modyfikację tej samej pętli. Możesz np. zmienić nazwę zmiennej na jeszcze inną metodę.
5. Wykonaj commit na gałęzi `main`.
6. W ten sposób obie gałęzie różnią się między sobą. Dodatkowo  modyfikowaliśmy ten sam kod na obu gałęziach. Taka sytuacja powinna  doprowadzić do konfliktu.
7. Spróbuj wykonać merge swojej gałęzi z gałęzią `main`. Rozwiąż konflikt. Wykonaj push zmian na GitHub.
8. Na koniec sprawdź historię swojego repozytorium poprzez komendę:

```
git log
```