# Miller Time API

## Overview
Miller Time API is a RESTful web API built targeting .NET 6, Entity Framework, and SQL Server. It serves as the backend for the Full Stack project of Miller Time, following a Microservice Architecture. The API is hosted on an Azure Web App, with separate production and local databases. It includes a timer-triggered "KeepWarm" function to prevent the API from going idle by calling a Health Checkpoint in the Health Controller. The API is tested using XUnit, ensuring high code quality and reliability throughout the development process.

The API works alongside the **[Miller Time UI / Frontend](https://github.com/wes-mitchell/miller-time-ui)**, a React-based frontend built with TypeScript that interacts with the API to provide a complete solution for the project.

## Tech Stack
- .NET 6
- Entity Framework
- SQL Server
- Azure Web App
- Microservice Architecture
- Azure Functions (Timer Trigger)
- XUnit
- GitHub Actions
- Docker

## Local Development: Seed Data & Docker
In the local development environment, the project utilizes Docker to create a containerized setup. The process includes:

- Docker automatically builds the container.
- The SQL Server database is seeded with realistic data for local testing.
- Developers can test the API locally with real-world data, ensuring consistency and functionality before deploying.

## Steps to Run Locally

1. **Clone the Repository**
   - Clone the project repository to your local machine:
     ```bash
     git clone git@github.com:wes-mitchell/Miller_Time_API.git
     ```

2. **Download Docker**
   - Ensure Docker is installed on your machine. You can download Docker [here](https://www.docker.com/products/docker-desktop).

3. **Run the Application**
   - In Visual Studio or your IDE of choice, set the Startup Item to **MillerTime.API** to ensure the correct project is running:
     - Right-click on the solution in Solution Explorer.
     - Select Set Startup Projects.
     - Choose **MillerTime.API** as the startup project.

   - When you run the application, Docker will automatically build and start the container. The SQL Server instance will be running inside the container, and the database will be seeded with data.

   **Note:** On the first startup, the Docker container may fail to start properly due to the initialization of SQL Server. If this happens, simply restart the project. You can also run the database container manually in Docker if you prefer.

4. **Access the API**
   - Once the API is running, you can access it at `http://localhost:5000` or the appropriate port specified in your setup. Additionally, you can use **Swagger** to test the available API endpoints by navigating to `http://localhost:5000/swagger`.

5. **Automatically Seeded Database**
   - The SQL Server database within the container will automatically be seeded with realistic data for testing at startup.

6. **Login and Connect to the SQL Server Instance**
   - You can connect to the SQL Server instance running in Docker using SQL Server Management Studio (SSMS) or any other database client.
   - Use the following connection details:
     - Server: `localhost,1433`
     - Username: `SA`
     - Password: `!mill3rLoc@l`
   - This allows you to inspect or query the database locally.

## Deployment
The project is configured for continuous deployment through **GitHub Actions** to both the Web API and Functions solution on Azure.
