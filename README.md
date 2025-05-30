This project is a small application built using a microservices architecture (2 microservices). It manages functionalities such as client insertion, sending notification and device integration.

Project Structure:
- **API**: Exposes public endpoints for communication.
- **Business**: Contains the core business logic for processing operations.
- **ClientHTTP**: Handles HTTP communication between microservices.
- **Repository**: Manages data access and interactions with the database.
- **Shared**: Includes shared components, DTOs, and utilities.

Added **Kafka** for asynchronous communication.

Created 3 Kafka topics:

- "notification-send": Produced by Notification Service and it sent message via whatsapp.
- "client-created": Produced by Registry Service and consumed by Notification Service for caching purpose
- "device-created": Produced by Registry Service.
