:: Must be inside WebApp folder
cd WebApp

:: Create API Controllers
dotnet aspnet-codegenerator controller -name ImagesController -actions -m App.Domain.Image -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ItemsController -actions -m App.Domain.Item -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name StorageLevelsController -actions -m App.Domain.StorageLevel -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

pause