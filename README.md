# Financial Project, .NET API

> **Status: finished learning project, November 2025. Not maintained.** Built to learn ASP.NET Core
> and Entity Framework before starting my Masters. It is here as a record, not as live work. For
> current projects see [Fine Print](https://github.com/giddypergrid/nzfineprint-backend) or
> [NZ Bird Sound](https://github.com/giddypergrid/NZBirdSoundDatabase-AWS).

A REST API for a stock research app. Users register, look up a company, and leave comments on it.

Its React client is [React_Financial_Project](https://github.com/giddypergrid/React_Financial_Project).

```
  UserController      register, login, JWT issue
  StockController     company stock CRUD, portfolio
  CommentController   comments attached to a stock

  Models  ──►  Dtos  ──►  Controllers        Mapster does the mapping
     │
     └──►  EF Core migrations  ──►  SQL Server
```

ASP.NET Core Identity for users, JWT bearer tokens for auth, Entity Framework Core code-first with
migrations, Mapster for model-to-DTO mapping, Swagger for the API surface.

---

.NET 9, ASP.NET Core, Entity Framework Core, SQL Server, JWT, Swashbuckle.
