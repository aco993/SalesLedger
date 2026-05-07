SalesLedger is a professional portfolio-grade ASP.NET Core MVC application for sales and transaction management.  
It is designed as both:

- a presentable .NET portfolio project for junior to mid-level employer discussions
- a realistic operational foundation for a future Data Warehouse / Business Intelligence master thesis

The project models sales from multiple sellers, mixed data sources, reusable products, optional customers, and import tracking for future Excel or scanned-record ingestion.

## Screenshots

Screenshot placeholders can be added here after local UI capture:

- `docs/screenshots/dashboard.png`
- `docs/screenshots/transactions.png`
- `docs/screenshots/reports.png`

## Features

- Dashboard with total sales, transaction volume, seller breakdown, monthly trend, top products, and recent transactions
- Transaction management with create, edit, delete, details, filtering, and validation
- Seller management with linked transaction context
- Product management with reusable catalog records and active/inactive state
- Customer management with lightweight, GDPR-friendly fields
- Import log module for future Excel/OCR ingestion workflows
- Reporting page with seller, monthly, and product analytics
- SQL Server persistence with Entity Framework Core migrations and seed data
- Layered architecture with separated domain, application, infrastructure, and web concerns

## Technology Stack

- C#
- .NET 8
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server / LocalDB for local development
- Bootstrap 5
- Chart.js

## Architecture Overview

The solution is structured into four projects:

- `SalesLedger.Domain`
  Contains entities, enums, and core domain concepts.
- `SalesLedger.Application`
  Contains service interfaces, DTOs, and view models used by the UI.
- `SalesLedger.Infrastructure`
  Contains `AppDbContext`, EF Core configurations, migrations, seed data, and service implementations.
- `SalesLedger.Web`
  Contains MVC controllers, Razor views, static assets, configuration, and composition root startup code.

This structure keeps the controllers thin, makes business logic easier to test later, and gives the project a clean shape for portfolio review.

## Database Model Overview

Operational entities currently include:

- `Seller`
- `Product`
- `Customer`
- `Transaction`
- `ImportLog`

Supporting enums:

- `PaymentType`
- `TransactionSourceType`
- `ImportStatus`

Relationships:

- one seller to many transactions
- one product to many transactions
- one customer to many transactions, optionally
- one seller to many import logs, optionally

## Data Warehouse / BI Perspective

The application is intentionally designed so the operational model can later feed a small analytical model:

- `Transaction` can become the source for `FactSales`
- `Seller` can feed `DimSeller`
- `Product` can feed `DimProduct`
- `Customer` can feed `DimCustomer`
- `TransactionDate` can feed `DimDate`

Suggested thesis evolution:

1. Add staging tables for Excel and OCR ingestion.
2. Introduce ETL flows to standardize source records.
3. Build star-schema dimensions and a sales fact table.
4. Add Power BI or SSAS style analytical reporting.
5. Compare operational reporting vs analytical reporting use cases.

## How To Run Locally

### Prerequisites

- .NET 8 SDK
- SQL Server LocalDB or SQL Server Express

### Steps

1. Restore dependencies:

```powershell
dotnet restore .\SalesLedger.sln
```

2. Apply migrations:

```powershell
dotnet ef database update --project .\src\SalesLedger.Infrastructure\SalesLedger.Infrastructure.csproj --startup-project .\src\SalesLedger.Web\SalesLedger.Web.csproj
```

3. Start the application:

```powershell
dotnet run --project .\src\SalesLedger.Web\SalesLedger.Web.csproj
```

4. Open the local URL shown in the console.

The application auto-seeds realistic sample data on first startup so the dashboard and management pages are immediately presentable.

## Seed Data

The starter dataset includes:

- multiple sellers
- reusable products across categories
- customers with lightweight contact metadata
- multi-month transaction history
- import logs with successful, warning, and pending states

This helps the application look realistic immediately during demos, portfolio reviews, or screenshots.

## Future Improvements

- full Excel import implementation with mapping profiles
- staged OCR pipeline for handwritten or scanned records
- export to CSV / Excel for filtered datasets
- authentication and role-based access
- pagination for larger transaction sets
- integration tests for services and controllers
- Power BI dashboard on top of the operational database or warehouse

## Portfolio Note

This repository is intentionally positioned as a serious portfolio and master-thesis-oriented project.  
The goal is to demonstrate:

- professional ASP.NET Core application structure
- practical EF Core and SQL Server usage
- clean separation of concerns
- business-oriented UX instead of a classroom demo
- awareness of how operational systems evolve into BI-ready architectures
=======
SalesLedger is a professional portfolio-grade ASP.NET Core MVC application for sales and transaction management.  
>>>>>>> 46a65ed54fed8b662d9585fd8c591c829656ae90
