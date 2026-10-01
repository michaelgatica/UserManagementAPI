# API Test Plan

## CRUD Endpoints

- `GET /api/users` should return a list of users.
- `GET /api/users/{id}` should return one user or `404 Not Found`.
- `POST /api/users` should create a valid user or return validation errors.
- `PUT /api/users/{id}` should update an existing user or return `404 Not Found`.
- `DELETE /api/users/{id}` should remove an existing user or return `404 Not Found`.

## Validation Tests

Submit missing names, empty department/role values, invalid email addresses, and duplicate email addresses. The API should return `400 Bad Request` with validation messages.

## Middleware Tests

- Send a request without `X-API-Key`; expect `401 Unauthorized`.
- Send a request with `X-API-Key: techhive-demo-token`; expect success.
- Request `/api/testing/throw`; expect a standardized `500 Internal Server Error` JSON response.
- Check console logs for request and response logging.
