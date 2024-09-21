## About
Repository contains applications for obtaining information from popular web APIs:
1. [The Star Wars API](https://swapi.dev/)
2. [IPinfo](https://ipinfo.io/developers)
3. [HP-API](https://hp-api.onrender.com/)

## Quick Start
To run the applications, you need to have PostgreSQL installed on your system. It should be available on port 5432 and has a user 'postgres', which has the password 'postgres'.

In addition, the following databases should be created in the system: starwars, ipinfo, harrypotter. You can create it using the following command:
```sql
CREATE DATABASE starwars;
CREATE DATABASE ipinfo;
CREATE DATABASE harrypotter;
```

You also need to have `wasm-tools` installed on your system. You can install it using the following command:
```
dotnet workload install wasm-tools
```
