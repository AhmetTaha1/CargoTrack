# CargoTrack

A cargo tracking REST API built with ASP.NET Core.

> 🚧 **Work in progress.** The project is under active development. See the [roadmap](#roadmap) for what's done and what's next.

## Features

- Create shipments with a server-generated tracking number (`TR` + 6 digits) and initial status `Received`
- List all shipments
- Get a shipment by tracking number
- Search shipments by receiver city (case-insensitive)
- Update shipment details (sender and receiver city)
- Update shipment status through a dedicated endpoint
- Cancel (delete) shipments

### Business Rules

- The tracking number and initial status are always assigned by the server; clients cannot send them (request DTOs).
- The tracking number never changes after creation and is unique (enforced by a unique database index).
- Status follows a fixed lifecycle: `Received` → `InTransit` → `OutForDelivery` → `Delivered`
- Status can only move forward; going back or setting the same status → `409 Conflict`. Skipping steps is allowed (e.g. a courier marks a shipment `Delivered` directly).
- Status can only be changed via `PATCH .../status`, so the lifecycle rule cannot be bypassed.
- Delivered shipments cannot be updated → `409 Conflict`
- Only shipments in `Received` status can be cancelled → `409 Conflict`
- Requests are validated (required fields, length limits, valid status values) → `400 Bad Request`

## Tech Stack

**Current**
- .NET 10 / ASP.NET Core Web API (controller-based)
- Clean Architecture (Domain / Application / Infrastructure / API), service layer, repository pattern, dependency injection
- Entity Framework Core 10 (code-first, migrations)
- PostgreSQL
- OpenAPI document generation
- Scalar API reference UI

**Planned**
- JWT authentication and role-based authorization
- Redis, RabbitMQ, SignalR
- React frontend
- Docker / Docker Compose

## Project Structure

```
CargoTrack.Domain           Entities and enums (Shipment, ShipmentStatus). No dependencies.
CargoTrack.Application      Business logic: services, DTOs, exceptions, repository interfaces
CargoTrack.Infrastructure   Data access: EF Core DbContext, migrations, repository implementations
CargoTrack                  ASP.NET Core API: controllers, DI setup (Program.cs), configuration
```

Dependencies point inward: `API → Application → Domain`, `Infrastructure → Application`. The Application layer never references EF Core or PostgreSQL directly; it depends on repository interfaces that Infrastructure implements.

## API Endpoints

| Method | Endpoint | Description | Responses |
|---|---|---|---|
| `POST` | `/api/shipments` | Creates a shipment | `201 Created` + `Location` header, `400 Bad Request` |
| `GET` | `/api/shipments` | Returns all shipments | `200 OK` |
| `GET` | `/api/shipments/{trackingNumber}` | Returns a shipment by tracking number | `200 OK`, `404 Not Found` |
| `GET` | `/api/shipments/search?city={city}` | Returns shipments whose receiver city matches the given city | `200 OK`, `400 Bad Request` |
| `PUT` | `/api/shipments/{trackingNumber}` | Updates sender and receiver city | `204 No Content`, `400 Bad Request`, `404 Not Found`, `409 Conflict` |
| `PATCH` | `/api/shipments/{trackingNumber}/status` | Moves the shipment forward in its lifecycle | `204 No Content`, `400 Bad Request`, `404 Not Found`, `409 Conflict` |
| `DELETE` | `/api/shipments/{trackingNumber}` | Cancels a shipment | `204 No Content`, `404 Not Found`, `409 Conflict` |

## Getting Started

**Prerequisites:** [.NET 10 SDK](https://dotnet.microsoft.com/download), [PostgreSQL](https://www.postgresql.org/download/), and the EF Core CLI (`dotnet tool install --global dotnet-ef`)

```bash
git clone https://github.com/AhmetTaha1/CargoTrack.git
cd CargoTrack
```

Set your database connection in `CargoTrack/appsettings.Development.json` (`ConnectionStrings:DefaultConnection`), then create the database and run the API:

```bash
dotnet ef database update --project CargoTrack.Infrastructure --startup-project CargoTrack
dotnet run --project CargoTrack
```

The API runs at `http://localhost:5009`. In the Development environment:

- Interactive API reference (Scalar): `http://localhost:5009/scalar`
- OpenAPI document: `http://localhost:5009/openapi/v1.json`

## Roadmap

- [x] Shipment query endpoints (list, get by tracking number, search)
- [x] Create, update and delete shipments with proper HTTP status codes
- [x] DTOs and request validation
- [x] Persistence with Entity Framework Core and PostgreSQL
- [x] Service layer and Clean Architecture (layered projects, repository pattern)
- [ ] Global error handling and logging
- [ ] JWT authentication with roles (Customer, Courier, Branch Manager, Admin)
- [x] Shipment status workflow with business rules
- [ ] Pagination, filtering and sorting
- [ ] Redis caching for tracking queries
- [ ] RabbitMQ events on shipment status changes
- [ ] Real-time tracking updates with SignalR
- [ ] Background service to detect stalled shipments
- [ ] LLM-powered customer assistant using tool calling
- [ ] Unit tests (xUnit)
- [ ] React frontend (tracking page, branch panel, courier view, assistant chat)
- [ ] Docker Compose setup (API, frontend, PostgreSQL, Redis, RabbitMQ)
- [ ] CI with GitHub Actions

## Development Workflow

This project follows **Git Flow**: features are developed on `feature/*` branches, merged into `develop` through pull requests, and released to `main`.
