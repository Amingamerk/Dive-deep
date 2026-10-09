# Guide: flyt forretningslogik fra web app til web API

Krav 7 siger at al forretningslogik skal ligge i API'et, så en mobil app også kan bruge det. Web appen (DiveDeep) skal ende med kun at vise sider og kalde API'et (DiveDeep.API) over HTTP, på samme måde som vi allerede kalder vejr-API'et.

Products er lavet først og bruges som eksempel hele vejen igennem. Når du er i tvivl, så kig på hvordan Products er gjort.

## Hvordan det hænger sammen

- **DiveDeep.API** ejer databasen. Den har entiteterne (Models), DbContext, repositories og migrations. Det er kun API'et der laver migrations fra nu af.
- **DiveDeep.Lib** er det fælles "sprog" mellem de to projekter. Her ligger kun DTO'er og enums, alle i namespace `DiveDeep.Lib.Models`.
- **DiveDeep** (web appen) har controllere, views, viewmodels og en HTTP service per ressource, der kalder API'et. Den skal ikke kende til databasen.

Login (Identity) bliver i web appen indtil vi har haft klassen om sikkerhed. Det er Krav 8.

## Før du starter

1. Sæt solution til at starte både DiveDeep og DiveDeep.API, ellers kan web appen ikke få fat i API'et. Højreklik på solution og vælg Configure Startup Projects, og vælg Multiple startup projects.
   https://learn.microsoft.com/en-us/visualstudio/ide/how-to-set-multiple-startup-projects
2. API'et kører på `https://localhost:7109`, og Scalar åbner på `/scalar/v1`. Her kan du teste dine endpoints uden at røre web appen.
3. Vælg én ressource ad gangen (fx Bundles eller Bookings) og gør den helt færdig, før du tager den næste. Så kan projektet køre hele tiden.

## Trin for trin

Eksemplet bruger "Booking", men det er det samme for alle ressourcer.

### 1. Find ud af hvad web appen faktisk bruger

Søg efter fx `IBookingRepository` og `BookingService` i web appen og skriv ned hvilke metoder controllerne kalder. Det er dem der skal have et endpoint i API'et. Metoder ingen bruger behøver ikke flyttes nu.

Spørg også for hver metode: er det forretningslogik (validering, overlap, regler) eller er det bare at hente data? Forretningslogik skal over i API'et, også selvom den i dag ligger i en service i web appen, som `BookingService`.

### 2. Tjek API-siden af databasen

Entiteter og repositories er allerede kopieret over i `DiveDeep.API/Models` og `DiveDeep.API/Persistence`, og repositories derovre er async.

- Er repository registreret i `DiveDeep.API/Program.cs`? Lige nu er det kun `ProductRepository`.
- Skal der flyttes en service med logik (fx `BookingService`)? Så skal den også over i API'et, gøres async og registreres.

### 3. Lav DTO'en i DiveDeep.Lib

Læg den i `DiveDeep.Lib/Models`. Se `ProductDto.cs` som eksempel.

- Tag kun det med som klienten har brug for.
- Ingen entiteter inde i en DTO. Peger den på noget andet, så brug en anden DTO eller bare id'et.
- Hvis du laver en ny enum, så læg den i `Enums.cs` med samme `JsonConverter` attribut som de andre, så den sendes som tekst.

### 4. Lav controlleren i API'et

Opret en controller i `DiveDeep.API/Controllers`. Kopier opsætningen fra `ProductsController.cs` (`[ApiController]` og route `api/[controller]`).

- Én action per ting web appen skal bruge.
- Konverter fra entitet til DTO før du returnerer. Products er mappet i hånden. Til resten bruger vi Mapster med `Adapt<T>()`, som vi gjorde i Pr07_WebAPI (`BooksController.cs`). Mapster skal installeres i DiveDeep.API, det ligger kun i web appen nu.
- Returner `Ok(...)`, `NotFound()` eller `BadRequest(...)` alt efter hvad der skete.

Test hver action i Scalar før du går videre.

### 5. Lav HTTP service interfacet i web appen

1. Kopier interfacet fra `DiveDeep/Persistence` (fx `IBookingRepository`) ind i `DiveDeep/Services/HttpServices`.
2. Omdøb det til `IBookingHttpService` og ret namespace.
3. Skift entiteter ud med DTO'er og gør metoderne async (`Task<...>`).
4. Udkommenter de metoder der ikke bruges endnu, eller som var forretningslogik der nu ligger i API'et. Så er det nemt at se hvad der mangler.

Se `IProductHttpService.cs`.

### 6. Lav HTTP service klassen

Opret `BookingHttpService` ved siden af interfacet. Se `ProductHttpService.cs`.

- Brug `IHttpClientFactory` og den navngivne client `"DiveDeepAPI"`. Den er sat op i web appens `Program.cs`.
- Hvis API'et svarer med en fejl eller 404, returnerer vi `null`. Det har vi aftalt, så alle controllere kan håndtere det ens.
- Brug `ReadFromJsonAsync<T>()` til at læse svaret.

### 7. Registrer servicen

Tilføj `AddScoped<IBookingHttpService, BookingHttpService>()` i `DiveDeep/Program.cs`. Lad den gamle repository-registrering blive, indtil ingen controller bruger den længere.

### 8. Opdater controllerne i web appen

- Skift det injicerede repository ud med HTTP servicen.
- Actions bliver `async Task<IActionResult>`, og alle kald får `await`.
- Entiteter bliver til DTO'er. Husk også `@model` i viewet.
- Tjek for `null`, og returner fx `NotFound()` eller vis en besked.

Se `DiveDeep/Controllers/ProductsController.cs`.

### 9. Ryd op

Når ingen controller bruger det gamle repository, så slet det fra web appen og fjern registreringen i `Program.cs`. Til sidst skal `Persistence` mappen og entiteterne i `Models` kunne slettes helt fra web appen.

## Faldgruber vi allerede er løbet ind i

- **URL'en i HTTP servicen.** Base address slutter på `/api/`, så URL'en skal starte med controllerens navn og ingen skråstreg foran, fx `products/5`. Med en `/` foran forsvinder `/api/` fra adressen.
- **`await` før LINQ.** `await service.Get().GroupBy(...)` virker ikke, fordi `GroupBy` så bliver kaldt på en `Task`. Gem resultatet i en variabel først, og lav LINQ på den.
- **Datoer i URL'en.** Brug formatet `yyyy-MM-dd`, så slipper du for problemer med klokkeslæt og `/` i datoen.

## Brugerens id (indtil Krav 8)

Bookinger og kurv skal vide hvem brugeren er, men API'et har ikke login endnu. Indtil da sender web appen brugerens id med til API'et, og API'et stoler på det. Det er et kendt hul, som Krav 8 lukker.

- Send id'et i body eller query string, ikke i route'en. Så skal der ændres mindre når Krav 8 kommer.
- Al kommunikation med API'et går gennem `"DiveDeepAPI"` clienten, så login senere kan sættes på ét sted.

## Spørgsmål vi skal blive enige om

- Kurven ligger i session i web appen. Skal den blive der, eller skal den over i API'et? Hvad ville en mobil app have brug for?
- Bundles ligger i et in-memory repository. Skal de i databasen nu, eller kan de blive i API'ets hukommelse?
- Admin-delen (opret, ret og slet produkter, upload af billeder) skal også bruge API'et. Hvem tager den?

## Links

- HTTP kald med IHttpClientFactory (se "Named clients"): https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests
- Returtyper i API controllere (`Ok`, `NotFound` osv.): https://learn.microsoft.com/en-us/aspnet/core/web-api/action-return-types
- async og await: https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/
- Mapster: https://github.com/MapsterMapper/Mapster
