# Redis and Kafka Infrastructure Setup Guide

This document provides developer guidelines for running, configuring, and verifying **Redis** and **Apache Kafka** services within the CleanArchitecture solution.

---

## 🚀 Quick Start with Docker Compose

To start all infrastructure services (PostgreSQL, Redis, Zookeeper, Kafka) locally, run:

```bash
docker compose up -d
```

To stop all services:

```bash
docker compose down
```

---

## 🔴 Redis Service Configuration

- **Image**: `redis:7-alpine`
- **Default Host**: `localhost` (or `redis` inside Docker containers)
- **Port**: `6379`
- **Health Check**: `redis-cli ping`
- **Connection String**: `"ConnectionStrings:Redis": "localhost:6379"`

### Environment Variables
In `docker-compose.override.yml`:
```yaml
ConnectionStrings__Redis=redis:6379
```

### Direct CLI Verification
You can test the Redis connection using `redis-cli` within the Docker container:

```bash
docker exec -it redis redis-cli ping
# Expected output: PONG
```

---

## 🟢 Kafka Service Configuration

- **Broker Image**: `confluentinc/cp-kafka:7.6.0`
- **Zookeeper Image**: `confluentinc/cp-zookeeper:7.6.0`
- **Host Listeners**:
  - `localhost:9092` (For host applications & local testing)
  - `kafka:29092` (For inter-container communications inside Docker network)
- **Health Check**: `kafka-topics --bootstrap-server localhost:9092 --list`
- **Configuration Section**:
```json
"Kafka": {
  "BootstrapServers": "localhost:9092",
  "GroupId": "cleanarchitecture-group"
}
```

### Environment Variables
In `docker-compose.override.yml`:
```yaml
Kafka__BootstrapServers=kafka:29092
```

### Direct CLI Verification & Topic Operations
To create and list topics directly using Kafka CLI inside the container:

```bash
# Create a test topic
docker exec -it kafka kafka-topics --create --topic test-topic --bootstrap-server localhost:9092 --partitions 1 --replication-factor 1

# List topics
docker exec -it kafka kafka-topics --list --bootstrap-server localhost:9092
```

---

## 🧪 Application Integration

Both Redis caching and Kafka messaging are registered in `CleanArchitecture.Infrastructure`:

```csharp
// DependencyInjection.cs
services.AddCaching(configuration);
services.AddMessaging(configuration);
```

- **Caching**: Access `IDistributedCache` in application services.
- **Messaging**: Inject `ProducerConfig` / `ConsumerConfig` or custom messaging abstractions.
