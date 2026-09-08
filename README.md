# CleanArchitecture.Template

![License](https://img.shields.io/badge/license-MIT-green)
![.NET](https://img.shields.io/badge/dotnet-10.0-blue)

**CleanArchitecture.Template** — A universal .NET project template with Blazor WASM, implemented according to the principles of **Clean Architecture**, ready for use in production.  

---

## 📦 Project structure
```
Domain:
— Business model
— Invariants
— Completely isolated
Application:
— Use cases
— DTO
— Business logic
— Without being tied to infrastructure
Infrastructure:
— EF Core
— Identity
— Redis
— Kafka
— NLog
Blazor WASM App:
— UI / Endpoints
— Minimum logic
Migrations:
— Clean migrations
— No "Pollution" of Infrastructure
Jobs:
— Background tasks
— Isolated from API
Tests:
— Architectural tests (Checks that layers don't have unnecessary dependencies. NetArchTest is used)
— Unit tests for Application, Domain, and Infrastructure (XUnit)
— Playwright tests for the web
```

To use the template you just need to install from Nuget:
```bash
dotnet new install CleanArchitecture.Starter
```
and create a project:
```bash
dotnet new cleanarchitecture -n MyApp
```

---

## 🛠 Infrastructure (Redis & Kafka) Setup

Detailed documentation for starting and testing Redis and Kafka services using Docker Compose is available in [REDIS_KAFKA_SETUP.md](content/CleanArchitecture/docs/REDIS_KAFKA_SETUP.md).

Telegram channel for project communications: https://t.me/CleanArchitecture_Template
