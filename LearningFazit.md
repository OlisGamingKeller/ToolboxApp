# Lernfazit

## Aktueller Stand

Mit dem `CalculatorTool` und dem `LeetTool` hast du schon einige wichtige C#-Grundlagen praktisch angewendet. Dabei ging es nicht nur um einzelne Sprachbausteine, sondern schon um den Aufbau eines kleinen, strukturierten Programms.

## Was du schon geuebt hast

- Klassen fuer unterschiedliche Aufgaben anlegen
- Methoden in kleinere, sinnvoll getrennte Einheiten aufteilen
- Konsolenmenues aufbauen und Benutzereingaben verarbeiten
- Eingaben mit `int.TryParse(...)` validieren
- mit `if`, `else if`, `while` und `return` den Programmablauf steuern
- mit `string?` und `null` arbeiten
- Text mit `StringBuilder` und `foreach` Zeichen fuer Zeichen verarbeiten
- `Dictionary<char, char>` fuer Uebersetzungsregeln verwenden
- Logik aus einer grossen Klasse in Hilfsklassen auslagern
- Dateien lesen und speichern
- einfache Fehlerbehandlung mit `try/catch` einsetzen
- Dateiendungen und Pfade pruefen

## CalculatorTool

Das `CalculatorTool` war vor allem gut fuer:

- Zahlen verarbeiten
- Rechenarten unterscheiden
- Eingaben pruefen
- Menueablauf trainieren

## LeetTool

Das `LeetTool` hat darauf aufgebaut und zusaetzlich diese Themen reingebracht:

- Textverarbeitung
- Uebersetzungslogik mit Dictionaries
- Erkennung von Eingabetypen
- Dateieinlesen und Dateispeichern
- Auslagern von Verantwortung in eigene Klassen

Die Aufteilung ist inzwischen deutlich sauberer:

- `LeetTool` steuert Menue, Ablauf und Konsolenausgabe
- `LeetTranslator` enthaelt die Uebersetzungslogik
- `LeetFileService` kuemmert sich um Dateioperationen

## Was daran besonders gut ist

Du lernst nicht nur einzelne Befehle auswendig, sondern schon, wie man Code besser strukturiert. Genau dieses Trennen von Verantwortung ist ein wichtiger Schritt von "es funktioniert irgendwie" hin zu "es ist verstaendlich und ausbaubar".

## Naechste sinnvolle Schritte

- genauere Fehlermeldungen statt nur `null` oder `false`
- Erkennungslogik im `LeetTranslator` weiter verbessern
- weitere Leet-Regeln ergaenzen
- wiederkehrende Menuevalidierung noch weiter vereinfachen
- spaeter vielleicht Tests fuer einzelne Logikteile schreiben
- langfristig eine kleine GUI als naechsten grossen Lernschritt angehen
