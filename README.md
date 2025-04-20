# PCTO QubicaAMF - Book Order Backend API

## Panoramica

Questo repository contiene il codice sorgente per il backend di un sistema di gestione di una libreria online, sviluppato come progetto durante il Percorso per le Competenze Trasversali e per l'Orientamento (PCTO) presso QubicaAMF.

Il sistema è costruito interamente in **C#** utilizzando il framework **.NET 8** e implementa **API RESTful** per gestire le seguenti funzionalità:

*   **Gestione Utenti:** Registrazione e login sicuro tramite **JSON Web Tokens (JWT)**.
*   **Catalogo Libri:** Visualizzazione, ricerca (per titolo/autore) e aggiunta di libri.
*   **Gestione Ordini:** Creazione di ordini da parte degli utenti e visualizzazione dello storico ordini personali.
*   **Sicurezza:** Protezione degli endpoint API tramite autenticazione e autorizzazione basata su JWT.
*   **Testing:** Presenza di **Unit Test** per validare la logica di business e/o i controller.
*   **CI/CD:** Pipeline di Integrazione Continua e Continuous Delivery configurata tramite **GitHub Actions** per build e test automatici.

Questo progetto dimostra competenze nello sviluppo backend, progettazione di API REST, implementazione di meccanismi di autenticazione, scrittura di test automatici e configurazione di pipeline CI/CD.

## Tecnologie Utilizzate

*   **Linguaggio:** C#
*   **Framework:** ASP.NET Core 8
*   **Database:** SQL Server
*   **Autenticazione/Autorizzazione:** JWT (JSON Web Tokens)
*   **Testing:** NUnit
*   **CI/CD:** GitHub Actions
*   **IDE:** Visual Studio 
*   **Version Control:** Git, GitHub

## Funzionalità Principali

*   **Autenticazione Utente:**
    *   `POST /register`: Registrazione di un nuovo utente.
    *   `POST /login`: Login utente e rilascio di un token JWT.
*   **Gestione Libri (Endpoints protetti):**
    *   `GET /books`: Ottiene l'elenco di tutti i libri.
    *   `GET /books/filter`: Cerca libri per titolo o autore.
    *   `POST /books`: Aggiunge un nuovo libro al catalogo.
*   **Gestione Ordini (Endpoints protetti):**
    *   `POST /orders`: Crea un nuovo ordine per l'utente autenticato.
    *   `GET /orders`: Ottiene lo storico ordini dell'utente autenticato.

*Nota: Gli endpoint esatti potrebbero variare.*

## Come Iniziare (Getting Started)

### Prerequisiti

*   [.NET SDK 8](https://dotnet.microsoft.com/download)
*   SQL Server
*   Un IDE come Visual Studio, VS Code o Rider.
*   Git

### Installazione ed Esecuzione

1.  **Clona il repository:**

2.  **Configura la connessione al database:**
    *   Modifica il file `appsettings.Development.json` (o `appsettings.json`) con la stringa di connessione corretta per il tuo ambiente di sviluppo.
    *   Applica le migrazioni al database.

3.  **Avvia il progetto:**
    *  apri la soluzione `FinalProject.sln` in Visual Studio e premi F5.

L'API sarà disponibile all'indirizzo specificato nel profilo di lancio.
