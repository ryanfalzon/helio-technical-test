# Running Web API Project
Before attempting to run the Web API project, make sure you create a `local.settings.json` file in the web API project. The content of this file should adhere to the following structure:

```json
{
    "ConnectionStrings": {
        "PhoneBookDbContext": "<DatabaseConnectionString>"
    }
}
```

If you are connecting to a local SQL server, the connection string will look something like:
```
Server=localhost\\SQLEXPRESS;Initial Catalog=PhoneBook;Integrated Security=True;Connect Timeout=30;
```

# Creating Database
To re-create the database, you can either run the SQL script found in the `Misc` folder of this repository, or you can run the `Patch` `Migrations` endpoint found in the Web API project. Both options will yield the same result.