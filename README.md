# Shelf Layout Manager

Shelf Layout Manager is a .NET Core application for managing the layout of shelves in a warehouse. It provides an API for creating, updating, and querying cabinets, rows, and lanes.

## Prerequisites

Before you begin, ensure you have met the following requirements:

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Entity Framework Core Tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet) (for managing migrations and database)
- [PostgreSQL Database](or another database of your choice)
  
## Setup

## Navigate to the project directory:
1. **Clone the repository:**

   ```bash
   git clone 'repo'

   cd ShelfLayoutManager

## Database Configuration:
    Install PostgreSQL DB

    Connection String: Update the database connection string in appsettings.json. By default, it's configured for PostgreSQL.
    "DefaultConnection": "Host=localhost;Port='';Database='';Username=;Password=;"

## Database Migration:

    Create Migrations: dotnet ef migrations add InitialCreate

    Apply Migrations: dotnet ef database update

## Run the Application:

    dotnet build 
    dotnet watch run .

## Test the API
    Open Swagger to test the API : http://localhost:5194/swagger/index.html


########## About the Develeopment ###########

## ShelfManager Project
    The ShelfManager project is a comprehensive solution for managing the layout of shelves, cabinets, rows, lanes, and SKU data.
    This README provides an overview of the project, the technologies used, and the decisions made during development.


## Features
    CRUD operations for cabinets, rows, lanes, and SKUs.
    Comprehensive error handling and data validation.
    Logging for auditing and debugging.

## Technology Stack
   - C# and ASP.NET Core for the backend API.
   - Entity Framework Core for database management.
   - PostgreSQL for data storage.
   - Serilog for logging.
   - Swagger for API documentation.
   - Xunit for unit testing.

## Project Structure

   - Controllers: Contains API endpoints for managing cabinets, rows, lanes, SKUs, and user accounts.
   - Repositories: Manages data access for each entity (cabinet, row, lane, SKU, user).
   - Models: Defines the data structures for the API's input and output.
   - Entities: Entity classes that map to the database tables.
   - Migrations: Database migration scripts.
   - RepositoriesInterface

## API Implementation

    The ShelfManager project offers a traditional RESTful API, which simplifies integration and web frontend compatibility.
    This choice was made to accommodate the needs of the operations team and frontend developers.
    Detailed API documentation can be found in Swagger when the project is running.

## API Choice

    1. Justification
        Compatibility: Traditional APIs are widely adopted and understood in the industry. 

        Simplicity: Traditional APIs are relatively straightforward to design, implement, and consume. 

        Web Frontend Compatibility: The project aims to provide a user-friendly web interface for managing shelf layouts.

    2. Considerations

        Ease of Use

        Web Frontend Integration

        Community Support

     3. Trade-offs

        Performance: Traditional APIs can be less performant compared to gRPC.

        Complexity of Contracts: Traditional APIs require careful design of endpoints and request/response structures.

        Real-time Communication: For applications that require real-time communication or bidirectional streaming

    In conclusion, the choice of a traditional API aligns with the project's objectives, providing a straightforward and widely-supported solution for managing shelf layouts.

## Deployment

    The project can be deployed using platforms such as Docker and Kubernetes on Azure Cloud 
    Ensure that the PostgreSQL database connection is configured properly during deployment.

##Time Breakdown
    Project setup and planning: 3 hours
    Data model and entity creation: 10 hours
    API endpoint development: 15 hours
    Logging implementation: 0.5 hours
    Documentation and README: 1 hours
    Total time spent: 29.5 hours

## Future Work

    - Implement JWT authentication and Authorization
    - Format all the files , to have proper coding standards
    - Implement FrontEnd for this Project
    - Have Gateway and Monitoring 


