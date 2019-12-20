# Projecten III - Kolveniershof - API Applicatie

> Deze API bevat de gegevens van alle gebruikers en de verschillende planningen van het Kolveniershof. De API wordt toegepast op een [android](https://github.com/HoGent-Projecten3/projecten3-1920-android-kolveniershof-groep-3) en een [angular](https://github.com/HoGent-Projecten3/projecten3-1920-angular-kolveniershof-groep-3) applicatie.

## Url van de website  

U kunt de swagger pagina van deze api bekijken via [deze](https://kolveniershof3punt2.azurewebsites.net/swagger/Index.html#/) link.
Als u online een api call wilt maken dan begint iedere call met "https://kolveniershof3punt2.azurewebsites.net/api"

## Admin  
Login: jonahdesmet@hotmail.com  
Wachtwoord: password1010  

## Begeleider  
Login: dieterdobbeleer@hotmail.com  
Wachtwoord: password1010  

## Ouder van cliënt 
Login: lisa@gmail.be  
Wachtwoord: password1010  

## Extra settings?
Als u lokaal wilt runnen, moet u de usersecrets aanpassen zoals volgt:
```
 "Tokens": {
    "Key": "dezestringmoeteenvoldoendelangelengtehebben"
  }
```
En zorg ervoor dat de ApplicationDataInitializer als volgt is opgebouwd:
```
_dbContext.Database.EnsureDeleted();
    if (_dbContext.Database.EnsureCreated())
    {
      ///
      /// Data
      ///
    }
```
## Meta
De applicatie is ontworpen door 6 laatstejaars bachelor studenten van HoGent Toegepaste Informatica:
- Bram De Bleecker
- Johanna De Bruycker
- Jonah De Smet
- Kato Duwee
- Robin Vanluchene
- Lucas Vermeulen
