# Equipment Trcker API

A RREST API for tracking equipment abd it maintenance status, built with ASP.NET Core 8 to practice C# and .NET

## Tech Stack

- C# / ASP.NET Core 8 Web API (controllers)
- Entity Framework Core with SQLite
- xUnit for unit tests
- Swagger / OpenAPI for interactive docs

#Design

- **Repository patter:** the controller depends on the `IEquipmentRepository` interface, not a concrete class.
- **Dependency injection:** `Program.cs` registers which implementation to use. Storage was swapped from in-memory list to EF Core with SQLite by changing one line, with no controller changes.
- **Validation:** data annotations (`[Required]`, `[StringLength]`) retunr 400 automatically.

# Run it

```
dotnet run --project EquipmentTracker.Api
```

Then open `/swagger` at the URL shown.

## Enpoints

| Method | Route                          | Result                                      |
| ------ | ------------------------------ | ------------------------------------------- |
| GET    | /api/equipment?status={status} | 200, all items or filtered by status        |
| GET    | /api/equipment/{id}            | 200, or 404 if not found                    |
| POST   | /api/equipment                 | 201 with Location header, or 400 if invalid |
| PUT    | /api/equipment/{id}            | 204, 400 if ids don't match, or 404         |
| DELETE | /api/equipment/{id}            | 204, or 404                                 |

## Tests

```
dotnet test
```
