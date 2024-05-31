# RocketFin

Brief project description or purpose.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
  - [Running the .NET Project](#running-the-net-project)
  - [Running the Angular Project](#running-the-angular-project)

## Prerequisites

List the prerequisites that users should have installed before attempting to run the project. This might include:

- [.NET SDK](https://dotnet.microsoft.com/download/dotnet)
- [Node.js](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)

## Getting Started

Provide step-by-step instructions on how to set up and run both the .NET and Angular projects.

### Running the .NET Project

1. Clone the repository to your local machine:

   ```bash
   git clone https://github.com/JohnsonSnow/RocketFin.git

2. Navigate to the .NET project directory:
   ```bash
   cd RocketFin
   
3. Setup Docker-Compose as the application startup project in Visual Studio :

4. Set up your database
   ```bash
   dotnet ef database update

5. Run the .NET application:
   ```bash
   dotnet run

6. Run the .NET Application on Docker From your visual studio.

6. Open a web browser and navigate to url to access the .NET application.


### Running the Angular Project

1. Navigate to the Angular project directory:
   ```bash
   cd eHealthcareApp/eHealthcare.UI

2. Install Angular dependencies:
   ```bash
   npm install

3. Start the Angular development server:
   ```bash
   npm start

4. Update the environment variable (baseUrl) on the environment.ts to point to the api port.

5. Open a web browser and navigate to http://localhost:4200 to access the Angular application.
