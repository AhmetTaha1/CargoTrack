# CargoTrack

A cargo tracking REST API built with ASP.NET Core.

> 🚧 **Work in progress.** The project is under active development. See the [roadmap](#roadmap) for what's done and what's next.

## Features

- List all shipments
- Get a shipment by tracking number
- Search shipments by receiver city (case-insensitive)

## Tech Stack

**Current**
- .NET 10 / ASP.NET Core Web API (controller-based)
- OpenAPI document generation

**Planned**
- Entity Framework Core + PostgreSQL
- JWT authentication and role-based authorization
- Redis, RabbitMQ, SignalR
- Docker / Docker Compose

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/shipments` | Returns all shipments |
| `GET` | `/api/shipments/{trackingNumber}` | Returns the shipment with the given tracking number, or `404 Not Found` |
| `GET` | `/api/shipments/search?city={city}` | Returns shipments whose receiver city matches the given city |

## Getting Started

**Prerequisites:** [.NET 10 SDK](https://dotnet.microsoft.com/download)

```bash
git clone https://github.com/AhmetTaha1/CargoTrack.git
cd CargoTrack
dotnet run --project CargoTrack
```

The API runs at `http://localhost:5009`. In the Development environment, the OpenAPI document is available at `http://localhost:5009/openapi/v1.json`.

## Roadmap

- [x] Shipment query endpoints (list, get by tracking number, search)
- [ ] Create, update and delete shipments with proper HTTP status codes
- [ ] DTOs and request validation
- [ ] Persistence with Entity Framework Core and PostgreSQL
- [ ] Service layer and layered architecture
- [ ] Global error handling and logging
- [ ] JWT authentication with roles (Customer, Courier, Branch Manager, Admin)
- [ ] Shipment status workflow with business rules
- [ ] Pagination, filtering and sorting
- [ ] Redis caching for tracking queries
- [ ] RabbitMQ events on shipment status changes
- [ ] Real-time tracking updates with SignalR
- [ ] LLM-powered customer assistant using tool calling
- [ ] Unit tests (xUnit)
- [ ] Docker Compose setup
- [ ] CI with GitHub Actions

## Development Workflow

This project follows **Git Flow**: features are developed on `feature/*` branches, merged into `develop` through pull requests, and released to `main`.
