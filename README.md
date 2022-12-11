# Json.DIffer
A .NET Core 6 API to compare json encoded files

# Flows:
- Upload the left file
- Upload the right file
- Compare the uploaded files

# Requirements to run the application local
- .NET Core 6
- SQL Server
- Optional: Docker (you can run the docker compose)

IF you don't want to use Docker, you should execute the script: https://github.com/lLeafaRafaelL/Json.DIffer/blob/main/src/Json.Differ.Database/CreateDatabase/db-init.sql to create the databases.
The miragtion will be running with the aplication.

# Postman Collections:
- Theres also a postman collection to help you in your tests: https://github.com/lLeafaRafaelL/Json.DIffer/tree/main/PostmanCollections

# Improvements:
- Implement log service
- Implement a event source flow
- Use a sqlite in memory database to run the integration tests
- Implement file property difference's line number
- Upload the json file data on cloud service like amazon s3 and only storage the file's url on database
