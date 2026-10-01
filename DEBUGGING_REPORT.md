# Debugging and Optimization Report

## Issues Identified

The original API design could have experienced the following issues:

1. Users could be added with missing names, invalid emails, or empty departments and roles.
2. Requests for non-existent users could result in unclear responses.
3. Unhandled exceptions could crash the request pipeline or return inconsistent error output.
4. The user list endpoint could become harder to use as data grows.

## Fixes Implemented

Validation was added in `Validation/UserValidation.cs`. The API now checks required fields, email format, length limits, and duplicate emails. The `GET`, `PUT`, and `DELETE` endpoints now check whether the requested user exists and return clear `404 Not Found` responses. `ErrorHandlingMiddleware` catches unexpected exceptions and returns a consistent JSON response with a trace ID. The `GET /api/users` endpoint supports optional `search` and `activeOnly` query parameters.
