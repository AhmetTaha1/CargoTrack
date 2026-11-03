# CargoTrack

A cargo tracking REST API built with ASP.NET Core.

> 🚧 **Work in progress.** The project is under active development. See the [roadmap](#roadmap) for what's done and what's next.

## Features

- Create shipments with a server-generated tracking number (`TR` + 6 digits) and initial status `Alındı`
- List all shipments
- Get a shipment by tracking number
- Search shipments by receiver city (case-insensitive)
- Update shipment details
- Cancel (delete) shipments

### Business Rules

- The tracking number and initial status are always assigned by the server; client-sent values are ignored.
- The tracking number never changes after creation.
- Delivered shipments (`Teslim Edildi`) cannot be updated → `409 Conflict`
- Only shipments in `Alındı` status can be cancelled → `409 Conflict`

## Tech Stack

**Current**
- .NET 10 / ASP.NET Core Web API (controller-based)
- OpenAPI document generation
- Scalar API reference UI

**Planned**
- Entity Framework Core + PostgreSQL
- JWT authentication and role-based authorization
- Redis, RabbitMQ, SignalR
- Docker / Docker Compose

## API Endpoints

| Method | Endpoint | Description | Responses |
|---|---|---|---|
| `POST` | `/api/shipments` | Creates a shipment | `201 Created` + `Location` header |
| `GET` | `/api/shipments` | Returns all shipments | `200 OK` |
| `GET` | `/api/shipments/{trackingNumber}` | Returns a shipment by tracking number | `200 OK`, `404 Not Found` |
| `GET` | `/api/shipments/search?city={city}` | Returns shipments whose receiver city matches the given city | `200 OK`, `400 Bad Request` |
| `PUT` | `/api/shipments/{trackingNumber}` | Updates sender city, receiver city and status | `204 No Content`, `404 Not Found`, `409 Conflict` |
| `DELETE` | `/api/shipments/{trackingNumber}` | Cancels a shipment | `204 No Content`, `404 Not Found`, `409 Conflict` |

## Getting Started

**Prerequisites:** [.NET 10 SDK](https://dotnet.microsoft.com/download)

```bash
git clone https://github.com/AhmetTaha1/CargoTrack.git
cd CargoTrack
dotnet run --project CargoTrack
```

The API runs at `http://localhost:5009`. In the Development environment:

- Interactive API reference (Scalar): `http://localhost:5009/scalar`
- OpenAPI document: `http://localhost:5009/openapi/v1.json`

## Roadmap

- [x] Shipment query endpoints (list, get by tracking number, search)
- [x] Create, update and delete shipments with proper HTTP status codes
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
