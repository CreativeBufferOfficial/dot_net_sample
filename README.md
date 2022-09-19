# Introduction 
A Sample Project built using .Net 6 as backend and Microsoft's SQL Server as Database (Dapper is the ORM being used).
- The Backend (API Endpoints) are built using a 3-Layered Architecture as listed below
  - Presentational Layer - using which the user will interact (in this case the user will interact through the Swagger).
  - Business Layer - contains all the business logic of the application.
  - Data Access Layer - will be responsible for all the database operations.
- This project makes use of numerous helpful concepts like AutoMapper, Dependency Injection, Blow Fish Encryption, CORS.
- This project includes the swagger documentation of all the API Endpoints exposed making it simpler for the Front-end Developers getting familiar with the API endpoints and the request pay load.
- This project is built using Single Responsibilty Repository Pattern (means there is a specific repository for each of the table/model concerned).
- This project depends on JWT (Json Web Token) for Authorization of API Endpoints.

# Getting Started
Before running this project on your local maching, one should have the following tools being installed. 
1. Visual Studio 2022
2. Microsoft SQL Server Management Studio (Version 18.10.1 or above).
Once the tools are installed, follow below steps to run the code on your local machine
1. Clone the Project's repo on your local machine.
2. Using the SampleProject.Database project (included with-in the application), Create a database on your machine.
3. Replace the connection string in the appsettings.json file (to run code in Release mode (Ctrl+F5)) and appsettings.Development.json file (to run code in Debug mode (F5)) - file can be found under SampleProject.API project.

# Contribute
For any queries and suggestions, kindly raise a issue. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)
