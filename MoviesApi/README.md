# MoviesApi

**PROG8555 - Microsoft Web Technologies**  
**Assignment 4**  
**Student Name:** Bo Yang  
**Student ID:** 9086117  

## Description
MoviesApi is a robust, RESTful Web API built with **.NET 10**, designed to manage a catalog of movies. This API serves as the backend for the Assignment 4 project, transitioning a traditional MVC application into a fully decoupled API architecture offering database-level pagination.

## Technologies Used
- **ASP.NET Core Web API (.NET 10)**
- **Entity Framework Core (EF Core)** -> `Microsoft.EntityFrameworkCore.Sqlite`
- **SQLite Database**
- **Swashbuckle.AspNetCore (Swagger)** for detailed API documentation and testing

## Key Features
1. **RESTful Architecture:** Implements strict adherence to standard HTTP verbs (`GET`, `POST`, `PUT`, `DELETE`).
2. **Database-level Pagination:** Leverages EF Core's `Skip()` and `Take()` at the repository pattern base to query data efficiently, preventing memory overhead.
3. **Data Transfer Objects (DTOs):** Uses `MovieCreateDto`, `MovieUpdateDto`, `MovieReadDto` and `PagedResult<T>` with Model State Validation to secure and trim payload intake against unwanted injections.
4. **Repository Pattern:** Fully abstracted `IMovieRepository` implementation decouples data connections from controllers.
5. **Extensive Swagger UI:** Complete with XML summaries (`<summary>`, `<param>`) detailing standard responses securely.

## Endpoints Summary

| HTTP Method | Route | Purpose |
|-------------|-------|---------|
| `GET` | `/api/movies` | Retrieves a paginated list of all active movies. Example: `?pageNumber=1&pageSize=5`. |
| `GET` | `/api/movies/{id}` | Locates and surfaces the details of a single unique movie. |
| `POST` | `/api/movies` | Generates a new movie entry ensuring strict payload validation. |
| `PUT` | `/api/movies/{id}` | Performs a complete update on an existing movie payload. |
| `DELETE` | `/api/movies/{id}` | Completely removes a movie reference from the SQLite database. |

## Quick Start

1. Open your terminal at the root directory (`/MoviesApi`).
2. To spin up the backend server, execute:
   ```bash
   dotnet run
   ```
3. A browser should open or you can manually navigate to:  
   `http://localhost:5154/swagger`
