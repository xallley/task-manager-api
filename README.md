# Task Manager API

A personal productivity tool that allows users to manage tasks with priorities, deadlines, and completion status. Built with ASP.NET Core 8 Web API.

## Features

- User authentication with JWT
- Task management (CRUD operations)
- Task prioritization (Low, Medium, High)
- Deadline tracking
- Task completion status
- Filtering and sorting capabilities

## Tech Stack

- ASP.NET Core 8 Web API
- Entity Framework Core with SQLite
- JWT Authentication
- AutoMapper for DTO mapping
- Swagger/OpenAPI for API documentation

## Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or VS Code
- SQLite (included with EF Core)

## Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/xallley/task-manager-api.git
   cd task-manager-api
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Run the application:
   ```bash
   dotnet run --project TaskManager.API
   ```

4. Access the Swagger UI at:
   ```
   http://localhost:5230/swagger
   ```

## API Endpoints

### Authentication
- POST `/api/auth/register` - Register new user
- POST `/api/auth/login` - Login and get JWT token

### Tasks
- GET `/api/tasks` - Get all tasks (with optional filters)
- GET `/api/tasks/{id}` - Get specific task
- POST `/api/tasks` - Create new task
- PUT `/api/tasks/{id}` - Update task
- PUT `/api/tasks/{id}/complete` - Mark task as completed
- DELETE `/api/tasks/{id}` - Delete task

## Testing the API

1. Register a new user:
   ```bash
   curl -X POST "http://localhost:5230/api/auth/register" \
     -H "Content-Type: application/json" \
     -d '{"email": "test@example.com", "password": "Test123!", "fullName": "Test User"}'
   ```

2. Login to get JWT token:
   ```bash
   curl -X POST "http://localhost:5230/api/auth/login" \
     -H "Content-Type: application/json" \
     -d '{"email": "test@example.com", "password": "Test123!"}'
   ```

3. Create a task (replace {token} with your JWT token):
   ```bash
   curl -X POST "http://localhost:5230/api/tasks" \
     -H "Authorization: Bearer {token}" \
     -H "Content-Type: application/json" \
     -d '{"title": "My Task", "description": "Task description", "priority": 1, "deadline": "2024-05-20T00:00:00Z"}'
   ```

## Project Structure

```
TaskManager.API/
├── Controllers/         # API endpoints
├── Data/               # Database context and configurations
├── DTOs/               # Data transfer objects
├── Models/             # Domain models
├── Services/           # Business logic and services
└── Mapping/            # AutoMapper profiles
```

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details. 
