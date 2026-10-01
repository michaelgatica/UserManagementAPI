# UserManagementAPI

This project is a back-end API for **TechHive Solutions**. It provides CRUD endpoints for managing users and includes validation, debugging improvements, and middleware for logging, authentication, and centralized error handling.

## Project Features

- ASP.NET Core Web API using minimal API endpoints
- CRUD endpoints for users: `GET`, `POST`, `PUT`, and `DELETE`
- Input validation for user data
- Graceful handling for non-existent users
- Centralized error handling middleware
- Token-based authentication middleware
- Request and response logging middleware
- Request examples in `REQUEST_EXAMPLES.http`

## Run the Project

```bash
cd UserManagementAPI
dotnet restore
dotnet run
```

The included launch profile uses `http://localhost:5096`.

## Authentication

All `/api` endpoints require a token. Use either `X-API-Key: techhive-demo-token` or `Authorization: Bearer techhive-demo-token`.

## Middleware Order

The middleware is configured in this order:

1. `ErrorHandlingMiddleware`
2. `TokenAuthenticationMiddleware`
3. `RequestResponseLoggingMiddleware`

This order ensures unexpected errors are caught first, protected endpoints require a valid token, and successful authenticated requests are logged.
