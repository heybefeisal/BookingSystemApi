# BookingSystem API

A cloud-native ASP.NET Core Web API for managing bookings, built using Clean Architecture principles and deployed to Azure Kubernetes Service (AKS).

---

# Features

* Create bookings
* Accept or decline bookings
* Retrieve bookings by date
* Swagger/OpenAPI documentation
* SQL Server persistence
* AutoMapper DTO mapping
* Structured logging with Serilog
* Docker containerization
* Kubernetes deployment with AKS
* CI/CD with GitHub Actions

---

# Tech Stack

* ASP.NET Core 8
* Entity Framework Core
* SQL Server
* Docker
* Kubernetes (AKS)
* Azure Container Registry (ACR)
* GitHub Actions
* Serilog
* AutoMapper
* xUnit + Moq

---

# Architecture

```text
Client
  ↓
Controllers
  ↓
Services
  ↓
Repositories
  ↓
SQL Server
```

---

# Running Locally

## Restore packages

```bash
dotnet restore
```

## Run application

```bash
dotnet run --project BookingSystemApi
```

## Swagger

```text
https://localhost:xxxx/swagger
```

---

# Docker

## Build image

```bash
docker build -t bookingsystemapi:local .
```

## Run container

```bash
docker run -d -p 8080:8080 bookingsystemapi:local
```

---

# Kubernetes Deployment

Kubernetes manifests are located in:

```text
k8s/
```

Deploy resources:

```bash
kubectl apply -f k8s/
```

Check pods:

```bash
kubectl get pods
```

Check services:

```bash
kubectl get services
```

---

# CI/CD

GitHub Actions automatically:

* builds the application
* runs tests
* builds Docker images
* pushes images to Azure Container Registry
* deploys to AKS

Workflow file:

```text
.github/workflows/deploy.yml
```

---

# API Endpoints

| Method | Endpoint                     |
| ------ | ---------------------------- |
| GET    | `/api/bookings`              |
| GET    | `/api/bookings/{id}`         |
| POST   | `/api/bookings`              |
| PUT    | `/api/bookings/{id}/accept`  |
| PUT    | `/api/bookings/{id}/decline` |
| DELETE | `/api/bookings/{id}`         |

---

# Cloud Infrastructure

```text
GitHub Actions
    ↓
Azure Container Registry
    ↓
Azure Kubernetes Service
    ↓
BookingSystem API
    ↓
SQL Server
```

---

# Future Improvements

* Azure SQL Database
* Helm charts
* Terraform
* Monitoring/Observability
* Azure Key Vault
* HTTPS + Ingress

---

# Author

Built as a backend/cloud engineering portfolio project using .NET, Docker, Kubernetes, Azure, and GitHub Actions.


