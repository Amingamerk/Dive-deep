# Valg vi har taget undervejs

Bookings blev flyttet til API'et under tidspres. Her er de steder hvor vi har valgt den hurtige løsning, så vi kan tage dem op igen bagefter.

- `BookingValidationResult` ligger i `DiveDeep.Lib`, selvom Lib ellers kun indeholder DTO'er. Den er svaret på validering begge veje, og en kopi i hvert projekt ville bare skulle holdes ens.
- Validering der fejler kommer tilbage som `Ok` med `IsSuccessful = false` i stedet for `BadRequest`. Så kan web appen stadig læse `Key` og hænge fejlen på det rigtige felt i formen.
- `ValidateDates` findes både i API'et og i web appens `BookingRules`. API'et er det tjek der gælder, kopien er kun så kunden får beskeden med det samme.
- Admin filtrerer bookinger i web appen på en liste der allerede er hentet. Det burde være query parametre på `GET api/bookings`, så databasen filtrerer og en mobil app får de samme filtre.
- `CreatedBundleBookingId` hænger på valideringssvaret, fordi web appen skal bruge id'et til bekræftelsessiden og kun API'et kender det.
- Pakker ligger in-memory i begge projekter. Id'erne skal passe sammen, ellers validerer API'et op mod en anden pakke end den kunden ser.
- Ledighed bliver tjekket og gemt i to trin. To kunder kan nå at booke det samme produkt samtidig. Det skal løses i databasen med en constraint eller en transaktion.

## Brugerens id (indtil Krav 8)

Bookinger og kurv skal vide hvem brugeren er, men API'et har ikke login endnu. Indtil da sender web appen brugerens id med til API'et, og API'et stoler på det. Det er et kendt hul, som Krav 8 lukker.

- Id'et sendes i body, ikke i route'en. Så skal der ændres mindre når Krav 8 kommer.
- Al kommunikation med API'et går gennem `"DiveDeepAPI"` clienten, så login senere kan sættes på ét sted.
