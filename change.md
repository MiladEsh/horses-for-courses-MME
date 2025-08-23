# Responding to Change
> A.k.a.: Hoe goed kan ik mijn eigen code lezen/aanpassen ?

Er is net een fusie doorgevoerd tussen verschillende *cursus verstrekkers*.
De hoofd aandeelhouder heeft een analyse laten uitvoeren en vastgesteld dat al deze verstrekkers
een online administratie module hebben die gelijkaardige functionaliteiten aanbiedt.

## Service Contract
Om deze verschillende sites te *stream linen* is er gekozen voor één enkele frontend, die de Web-APi's van
de verstrekkers zal aanspreken.
Er is wel een kleine inspanning nodig van de verstrekkers zelf om de interface *compliant* te maken
met de frontend van de hoofd aandeelhouder.

De [specificatie](./interface-definition.md), oftewel het *service contract*, is aan de technische diensten van de verstrekkers bezorgd.

## Technical Guidelines
De verschillende cursus verstrekkers worden gevraagd om rekening te houden met de technische specificaties van de hoofd aandeelhouder en deze te implementeren.

1. - De [invarianten](../1.TheStables/readme.md) zoals geïmplementeerd in het domein, dienen door minstens één test gedocumenteerd te worden (meerdere liefst om *edge-cases* te verduidelijken).
   - Een markdown document dient te worden opgeleverd aan de hoofd aandeelhouder via een bestand in de root van de *solution*: `domain-invariants.md`. Daarin voor elke *invariant*:
     - een korte beschrijving 
     - link(s) naar de test code
   

2. In de Web Api laag is het niet toegelaten om ook maar enige logica, met uitzondering van eventuele null-checks, terug te vinden.
Dus geen loops, if's, switches, regex, ...

2. De `GET` requests halen de entiteiten op uit je storage service (bvb repository) en geven deze aan een specifieke mapper class.
Loops om bvb een lijst van Dto objecten om te zetten naar een domein entiteit, horen hier te zitten.


