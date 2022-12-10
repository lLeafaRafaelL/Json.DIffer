# Json.DIffer
A .NET Core 6 API for compare json encoded files

Flows:
- Upload the left file
- Upload the right file
- Compare the uploaded files

# requirements to run the application local
- .NET Core 6
- SQL Server
- Optional: Docker (you can run the docker compose)

IF you don't want to use Docker, you should execute the script: Json.DIffer\src\Json.Differ.Database\CreateDatabase\db-init.sql to create the database
The miragtion will be running with the aplication

- Postman Collections
Theres also a postman collection to help you in your tests

# Improvements:
- Implement log service
- Implement a event source flow
- Fix integration tests order bug
