# Outboxpattern

This project has been developed using technologies such as the Outbox pattern, CQRS, MediatR, Clean Architecture, Entity Framework, Dapper, RabbitMQ, Mass-Transit andQuartz.

## Tools
- RabbitMq
- PostgreSQL
- Docker

## Technologies
- MediatR
- Rebus
- Mapster
- Npgsql
- EntityFrameworkCore
- Dapper
- Quartz

## Design Patterns
- CQRS Pattern
- Mediatr Pattern
- Unitofwork Pattern
- Repository Base Pattern

The outbox pattern, utilized in the development of microservices architecture, has been addressed. Emphasizing data integrity in distributed architectures, the approach involves first recording the data in the processing database to prevent data loss. Subsequently, a queuing process(using RabbitMQ) is implemented by reading the data from the database. The data in these queues is then processed by the respective consumers.

Thank you for your interest in the Outboxpattern project! If you have any feedback or suggestions, we would love to hear from you. Happy coding!
