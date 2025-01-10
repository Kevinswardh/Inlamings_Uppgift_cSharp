# C# Projektet - Kontaktlistan 
## Beskrivning
Detta projekt är en kontaktapplikation utvecklad i C# som en del av en skoluppgift. Applikationen har utvecklats i flera versioner, inklusive en konsolapplikation, en WPF-applikation, och en WPF MVVM-version. Målet är att ge praktisk erfarenhet av att utveckla skalbara och strukturerade applikationer med hjälp av designmönster och bästa praxis.

## Funktionalitet

- Skapa, uppdatera och ta bort användare.
- Skapa, uppdatera och ta bort kontakter.
- Hantera favoritkontakter.
- Autentisering med inloggnings- och registreringssidor.

## Teknologier och ramverk

- **Språk:** C#
- **GUI:** WPF
- **Arkitektur:** MVVM (Model-View-ViewModel)
- **Verktyg:** Visual Studio, CommunityToolkit.Mvvm
- **Testning:** xUnit, Moq

## Projektstruktur

### CoreFiles
- **Models:** Innehåller datamodeller som `User` och `Contact`.
- **Interfaces:** Definierar grundläggande operationer som CRUD.
- **Databases:** Hanterar datalagring via JSON.

### Logic
- **Services:** Hanterar affärslogik och kommunicerar med repositories.
- **Repositories:** Hanterar datalagring och datahämtning.

### WPF_Mvvm_Version
- **Views:** Innehåller XAML-filer för användargränssnittet.
- **ViewModels:** Hanterar logik och databindning för respektive vy.
- **Helpers:** Hjälpklasser för konvertering och annan UI-funktionalitet.

## Testning
Projektet inkluderar enhetstester och integrationstester som täcker:
- Grundläggande CRUD-operationer.
- Autentisering och validering.
- Interaktion mellan services och repositories.

## Framtida Utveckling

- Implementera en backend-server för att hantera databasoperationer via API:er.
- Utveckla applikationen i .NET MAUI för stöd på flera plattformar.
- Förbättra UI-designen med responsiv layout.
- Utöka applikationen med fler avancerade funktioner som realtidsuppdateringar.

---

## <span style="color:red">Notis om MAUI-versionerna</span>
De nuvarande MAUI-versionerna är under utveckling och är inte fullständiga. Funktionalitet och design kan saknas eller vara ofullständig i dessa versioner. Arbetet med MAUI är en del av framtida utvecklingsplaner för projektet.
