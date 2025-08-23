# Horses for Courses (The Stables)

## Setup

* Repository aanmaken op GitHub : `horses-for-courses`. Invite Mark Meyers.
* Clone lokaal
* Voeg een relevante `.gitignore` toe.
* Voeg een DotNet Solution toe (`HorsesForCourses`) met daarin:
  * een Class Library (`HorsesForCourses.Core`)
  * een xUnit Test Project (`HorsesForCourses.Tests`).
* Voeg een github workflow toe.

## Initieel Domein

Gebruik Engelse namen voor Klassen, Properties, Fields, Variabelen, etc ...

### Opleidingen

Een opleiding:
* Heeft een *naam*.
* wordt ingepland over een bepaalde periode met een start- en einddatum.
* heeft vaste lesmomenten, bijvoorbeeld op maandag en woensdag van 10u tot 12u.
* heeft enkel les op weekdagen (maandag t.e.m. vrijdag).
* plant lessen uitsluitend binnen de kantooruren (tussen 9u00 en 17u00).
* moet in totaal minstens één uur duren.
* vereist een lijst van coach-competenties.
* wordt begeleid door exact één coach.
* is ongeldig zolang er geen lesmomenten zijn toegevoegd.
* kan pas een coach toegewezen krijgen nadat de opleiding als geldig en definitief is bevestigd.
* laat geen wijzigingen aan het lesrooster meer toe zodra een coach is toegewezen.

### Coaches

Een coach:

* beschikt over een lijst van vaardigheden (competenties).
* is slechts geschikt voor opleidingen waarvoor hij of zij alle vereiste competenties bezit.
* kan niet worden toegewezen aan overlappende opleidingen en moet dus beschikbaar zijn op de ingeplande momenten.

## Interactie met het Domein 

Om de interactie met het domein mogelijk te maken via een (toekomstige) Web API, voorzie je de volgende gefaseerde operaties:

* **Coach registreren:** maak een coach aan met naam en e-mailadres.
* **Coach Competenties toevoegen/verwijderen**
* **Cursus aanmaken:** maak een lege cursus aan met naam en periode.
* **Cursus Competenties toevoegen/verwijderen**
* **Cursus Lesmomenten toevoegen/verwijderen** (dag, beginuur, einduur).
* **Cursus bevestigen:** markeer de cursus als geldig en definitief, mits hij aan alle voorwaarden voldoet (inclusief minstens één lesmoment).
* **Coach toewijzen:** wijs een coach toe, enkel indien de cursus bevestigd is en de coach geschikt én beschikbaar is.


> Denk bij elk van deze stappen aan geldige foutmeldingen, domeinvalidaties en het garanderen van een consistente toestand.

*Zie ook:* [Course Entry](../course-edit-mockup.svg)