# Juhan Microservices Project

## Overview
EShop-Juhan is a microservices-based e-commerce platform built with .NET 8. The application demonstrates modern cloud-native patterns and practices, implementing a distributed system architecture with multiple independent services.

## Architecture
The solution consists of the following microservices:

### Catalog Service
- Manages product catalog and inventory
- Built with ASP.NET Core Web API
- Uses PostgreSQL for data persistence
- Endpoints:
  - Product management
  - Category management
  - Inventory tracking

### Basket Service
- Handles shopping cart operations
- Built with ASP.NET Core Web API
- Uses PostgreSQL for data persistence
- Redis for distributed caching
- Features:
  - Cart management
  - Price calculation
  - Temporary storage

### Discount Service
- Manages product discounts and promotions
- Implemented as gRPC service
- Features:
  - Discount calculation
  - Promotion management
  - Coupon validation

### User Service
- Handles user management and authentication
- Features:
  - User registration
  - JWT authentication
  - Profile management
- Inter-service communication via:
  - HTTP REST APIs
  - RabbitMQ for event-driven updates

## Technical Stack
- **.NET 8**: Core framework
- **PostgreSQL**: Primary database for Catalog and Basket services
- **Redis**: Distributed caching
- **SQL Server**: Database for User service
- **RabbitMQ**: Message broker for event-driven communication
- **Docker**: Containerization
- **JWT**: Authentication
- **gRPC**: High-performance RPC framework
- **CQRS Pattern**: Command and Query Responsibility Segregation
- **MediatR**: Implementation of mediator pattern

## Prerequisites
- .NET 8 SDK
- Docker Desktop
- Visual Studio 2022 or Visual Studio Code
- PostgreSQL (if running locally)
- SQL Server (if running locally)

## Getting Started

### 1. Clone the Repository
```bash
git clone <repository-url>
cd EShop-Juhan
```

### 2. Configure Environment
Update the following configuration files as needed:
- `appsettings.json` in each service
- Environment variables in `docker-compose.yml`

### 3. Run with Docker
```bash
docker-compose build
docker-compose up
```

### 4. Access Services
- Catalog API: http://localhost:8000
- Basket API: http://localhost:8001
- Discount gRPC: http://localhost:8002
- User Service: http://localhost:5001
- RabbitMQ Management: http://localhost:15672 (guest/guest)
- PostgreSQL:
  - Catalog DB: localhost:5432
  - Basket DB: localhost:5433
- SQL Server: localhost:1433

## Project Structure
```
EShop-Juhan/
├── Services/
│   ├── Catalog/
│   │   └── Catalog.API/
│   ├── Basket/
│   │   └── Basket.API/
│   └── Discount/
│       └── Discount.Grpc/
├── UserService/
│   ├── Controllers/
│   ├── Commands/
│   ├── Queries/
│   └── Models/
└── docker-compose.yml
```

## Development Guidelines

### API Versioning
All APIs are versioned using URL versioning:
```
/api/v1/[controller]
```

### Authentication
- JWT tokens are used for authentication
- Token validity: 1 hour
- Required for protected endpoints
- Token format:
  ```json
  {
    "sub": "user_id",
    "email": "user@example.com",
    "exp": 1234567890
  }
  ```

### Event Communication
RabbitMQ events follow this naming convention:
- User events: `user.{event_name}`
- Product events: `product.{event_name}`
- Order events: `order.{event_name}`

### Database Migrations
Run migrations for each service:
```bash
dotnet ef database update --project Services/Catalog/Catalog.API
dotnet ef database update --project Services/Basket/Basket.API
dotnet ef database update --project UserService
```

## Testing
Run tests for all services:
```bash
dotnet test
```

Individual service tests:
```bash
dotnet test Services/Catalog/Catalog.Tests
dotnet test Services/Basket/Basket.Tests
dotnet test Services/Discount/Discount.Tests
dotnet test UserService.Tests
```

## Deployment
The application is containerized and can be deployed to any container orchestration platform. Sample Kubernetes manifests are provided in the `k8s` directory.

### Production Considerations
- Configure proper secrets management
- Set up monitoring and logging
- Configure SSL/TLS
- Implement rate limiting
- Set up proper backup strategies
- Configure health checks

## Contributing
1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License
This project is licensed under the MIT License - see the LICENSE file for details.

## Support
For support, please open an issue in the project repository or contact the development team.
