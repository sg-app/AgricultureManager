# AgricultureManager

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
![GitHub Release](https://img.shields.io/github/v/release/sg-app/AgricultureManager)
![GitHub Tag](https://img.shields.io/github/v/tag/sg-app/AgricultureManager)



### AgricultureManager is a comprehensive application designed to manage various aspects of agricultural operations. This project includes features for managing harvest documentation, accounting, and more.

## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Pages](#pages)
- [License](#license)

## Features

- **Harvest Documentation**: Generate detailed PDF reports for harvest years.
- **Accounting**: Manage financial transactions and document uploads.

## Installation

[Docker Componse Example](./doc/docker_compose_example.yml)


## Project Structure

The project is structured into several modules, each responsible for different functionalities:

- **AgricultureManager.CoreApp**: Contains core pages and application logic and shared models.
- **AgricultureManager.Module.Pdf**: Handles PDF generation for harvest documentation.
- **AgricultureManager.Module.Accounting**: Manages accounting features including document uploads and financial transactions.

## Pages

- **Schlagdokumentation**: Dokumentation von: Aussaat, Pflanzenschutz, Düngung, Ernte.
- **Module**:
  - **Düngeplanung**: Planung der Düngerausbringung für das Erntejahr. Erstellen von Vorgaben und Planen der einzelnen gaben
  - **Buchhaltung**: 
    - **Buchungen**: Übersicht und laden von Kontobewegungen direkt aus dem Bankkonto. Zum Abrufen der Daten wird die FinTS Schnittstelle der Bank benutzt und die Daten abgerufen.
    Ausserdem können zu jeder Kontobewegung Dokumente geladen werden und die Buchungen Klassifiziert werden.
    - **Kontoauszug**: Hochladen der Kontauszüge. Download von Quartalsdaten für den Steuerberater -> Kontoauszüge und Dokumente zu den Kontobewegungen.
  - **Dokumente**:
    - **Schlagdokumentation**: Pdf der Dokumentation für jeden Schlag.
    - **Düngeplanung**: Ausdruck und zusammenfassung der für die Düngeplanung.
    - **Anbauflächen**: Übersicht über die Kulturen und Flächen für das Erntejahr.
    - **Fruchtfolgen**: Übersicht der einzelnen Schläge und der Fruchfolge der letzten Jahre.
  - **Daten**:
    - **Betrieb**: Betriebsinformation
    - **Stammdaten**: Pflege von allen Stammdaten die in der Applikation benötigt bzw. zur Verfügung gestellt werden sollen.
    - **Erntejahr bearbeiten**: Editor zum planen der Schläge und Nutzungsschläge für das Erntejahr.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE.txt) file for details.
