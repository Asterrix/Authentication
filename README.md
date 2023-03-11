# Authentication
An authentication API created in C# that features these options:
- Register
- Login
- Hash user password
- Verify the hashed password
- Generate Jwt token

### Prerequisites
To run this project, you'll need to have the following installed on your machine:
- .NET 7
- SQL Server(optional, needs to be configured to work)

### Getting Started
To get started with this project, follow these steps:
1. Clone the repository to your machine by running this command from your terminal:

    ```git clone https://github.com/Asterrix/Authentication.git```

2. You will need to create EntityFramework migrations if you plan to use the database but before that I recommend configuring SQL settings inside ```DependencyInjection.cs``` file found inside "Infrastructure" assembly.
3. Create EF migrations by running:
``` dotnet ef migrations add "InitialCreate" -p Infrastructure -s Presentation``` command from the "Authentication" folder inside your terminal.
4. Apply the migrations by running ```dotnet ef database update -p Infrastructure -s Presentation``` command from the same "Authentication" folder.
5. Run ```dotnet run --urls "https://localhost:7262" --project Presentation``` command from your terminal.
6. I recommend using Swagger for the ease of use which you can find at `https://localhost:7262/swagger/index.html` address.
7. Now you can send HTTP request to the local server.