:: Must be inside WebApp folder
cd WebApp

:: Create Web Controllers
:: dotnet aspnet-codegenerator controller -name AssortmentLevelsController -actions -m App.Domain.AssortmentLevel -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

pause

:: dotnet aspnet-codegenerator controller -name CompaniesController -actions -m App.Domain.Company -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

pause

:: dotnet aspnet-codegenerator controller -name CustomersController -actions -m App.Domain.Customer -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

pause

:: dotnet aspnet-codegenerator controller -name CustomerInCompaniesController -actions -m App.Domain.CustomerInCompany -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

pause

:: dotnet aspnet-codegenerator controller -name DivisionsController -actions -m App.Domain.Division -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

pause

:: dotnet aspnet-codegenerator controller -name GroupsController -actions -m App.Domain.Group -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

pause

:: dotnet aspnet-codegenerator controller -name HistoriesController -actions -m App.Domain.History -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

pause

dotnet aspnet-codegenerator controller -name PersonnelsController -actions -m App.Domain.Personnel -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

pause

dotnet aspnet-codegenerator controller -name PointOfSalesController -actions -m App.Domain.PointOfSale -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

pause

dotnet aspnet-codegenerator controller -name PointOfSaleInGroupsController -actions -m App.Domain.PointOfSaleInGroup -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

pause

dotnet aspnet-codegenerator controller -name ProductsController -actions -m App.Domain.Product -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

pause

dotnet aspnet-codegenerator controller -name ProductInAssortmentsController -actions -m App.Domain.ProductInAssortment -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

pause

dotnet aspnet-codegenerator controller -name ProductInHistoriesController -actions -m App.Domain.ProductInHistory -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

pause

dotnet aspnet-codegenerator controller -name ProductInPointOfSalesController -actions -m App.Domain.ProductInPointOfSale -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

pause

dotnet aspnet-codegenerator controller -name ProductInTablesController -actions -m App.Domain.ProductInTable -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

pause

dotnet aspnet-codegenerator controller -name TablesController -actions -m App.Domain.Table -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

pause