# ApotekHjartat.Api

## Generella antaganden och tankar
Jag har egentligen bäst koll på hur jag skulle sätta upp data för ett lagersystem. Jag valde dock att utgå från att detta är ett e-com
DB som även nås av ett admingränssnitt, trots att jag inte har lika bra koll/erfarenhet av det. Detta p ga att jag annars hade säkerligen råkat läcka info om hur vårt data och affärslogik på mitt nuvarande företag är uppsatt.
Hade det varit ett lagersystem så hade jag dock ex tagit hänsyn till narkotikaklassade produkter, kylvara, delat upp ordrarna i paket (då ex kyl skickas separat), etc.

Då jag utgick från att det är ett admingränssnitt som kommer ropa på alla get metoder, så ser jag till att alla receptlagda varor hamnar i en receptpåse, då innehållet är klassificierad information som inte vemsomhelst får se.

## Hur man kommer igång
Om man startar projektet så kommer en lokal DB skapas. Man kan då använda swagger för att testa alla endpoints genom att skriva in /swagger efter projektets URL.
Tänk på att du behöver ladda ner sql server Developer version och SSMS om du inte redan har det.
Samt att man har satt en miljövariabel till namnet ASPNETCORE_ENVIRONMENT och värdet Developemnent.

https://www.microsoft.com/sv-se/sql-server/sql-server-downloads?rtc=1


Man kan annars även använda sig av integrationtesterna :)

## Reflektioner och sånt jag skulle göra om jag hade mer tid
### Return a list with all orders
Det är generellt bad practise att köra en getAll() om inte man med säkerhet vet att det aldrig kommer vara fler än ett x antal st.
Konsekvensen kan vara att man får en timeout error, kraschar APIet eller att DBn går på knäna. Är DBn kopplad till hemsidan dessutom, så riskerar man att sega ner hemsidan och förlora kunder p ga det. Företaget vill självklart öka försäljningen, så jag valde att paginera responsen.

### Delete all orders
Ganska stor säkerhetsrisk. Ordrar är kärnan av en e-coms data. Konsekvensen är att man skulle göra sig av med försäljningsdata och/eller aktiva ordrar.
Kan också vara så att en kund vill öppna ett ärende om en gammal order, eller ha koll på sina gamla ordrar.
Hade jag utgått från att det är ett lagerdb så kan man göra en synk och rensa på inaktiva ordrar varje natt.
Annars, i det här fallet, kom jag bara att tänka på när kunder vill att man ska ta bort dem enl. GDPR. Så jag gjorde en endpoint där man kan ta bort kunddatan på ordern, såvida den inte är aktiv. 

### Om jag hade haft mer tid:

- lagt till loggning
- tillämpat permissions på alla endpoints
- kopplat ordrarna till kunder, fick utgå från att det är gästbeställningar.
- sett till så att det kan skapas fler receptpåsar än en, då man kan ex köpa läkemedel för sitt barn eller husdjur.
  Jag vart osäker på om ni också använder maxx, isf hade jag grupperat de
  baserat på maxxorderid.
- migrationerna hade varit bättre, hade inte tid för att reverta när jag insåg att något var tokigt
- namngivning hade varit bättre och mer konsekvent
- utökat med mer relevant data man kan få när en kund lägger en order
