:: Drop database
dotnet ef database drop --project App.DAL.EF --startup-project WebApp

:: Remove migrations
dotnet ef migrations remove --project App.DAL.EF --startup-project WebApp

:: Create migrations
dotnet ef migrations add InitialMigration --project App.DAL.EF --startup-project WebApp

:: Update database
dotnet ef database update --project App.DAL.EF --startup-project WebApp

pause