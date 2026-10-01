# Middleware Implementation Report

`RequestResponseLoggingMiddleware` logs incoming HTTP requests and outgoing HTTP responses. It records the request method, path, and response status code.

`TokenAuthenticationMiddleware` protects all `/api` endpoints. Requests must include either `X-API-Key: techhive-demo-token` or `Authorization: Bearer techhive-demo-token`. Invalid or missing tokens return a `401 Unauthorized` response.

`ErrorHandlingMiddleware` catches unhandled exceptions and returns a standardized JSON response. This prevents the API from exposing raw errors in an inconsistent way.

The middleware is configured in `Program.cs` in this order:

```csharp
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<TokenAuthenticationMiddleware>();
app.UseMiddleware<RequestResponseLoggingMiddleware>();
```
