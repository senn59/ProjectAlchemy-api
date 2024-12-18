[![Deploy](https://github.com/senn59/ProjectAlchemy-api/actions/workflows/deploy.yml/badge.svg)](https://github.com/senn59/ProjectAlchemy-api/actions/workflows/deploy.yml) [![Tests](https://github.com/senn59/ProjectAlchemy-api/actions/workflows/test.yml/badge.svg)](https://github.com/senn59/ProjectAlchemy-api/actions/workflows/test.yml) [![Codacy Badge](https://app.codacy.com/project/badge/Coverage/cc9e2afe95fb423ab1c8452464c0cc83)](https://app.codacy.com/gh/senn59/ProjectAlchemy-api/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_coverage) [![Codacy Badge](https://app.codacy.com/project/badge/Grade/cc9e2afe95fb423ab1c8452464c0cc83)](https://app.codacy.com/gh/senn59/ProjectAlchemy-api/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
# ProjectAlchemy-api
This is the API for the ProjectAlchemy project, a tool for agile project management. See [ProjectAlchemy-client](https://github.com/senn59/ProjectAlchemy-client) for the frontend.

# Architecture & Technologies

This project is developed using .net core and makes use of the N-tier architecture splitting the code into 3 different solutions. 

- **Web**
    - .net web api project 
    - contains API routes and endpoints
- **Core**
    - class library
    - contains business logic
- **Persistence**
    - class library
    - talks with the database using Entity Framework as an ORM


# Running the project locally
This project can easily be run locally using [Docker](https://www.docker.com/) and [Make](https://www.gnu.org/software/make/) (optional)

## Setup
Before running make sure you copy [ProjectAlchemy.Web/appsettings.EXAMPLE.json](ProjectAlchemy.Web/appsettings.EXAMPLE.json) to `ProjectAlchemy.Web/appsettings.json` and pass a connection string of a valid MySQL database.

## Running
If you do not have Make installed you can check [Makefile](/Makefile) for the corresponding docker commands.

Running the docker container:
```bash
make prod
```
API will be accessible on port 3000.

Shutting down the container:
```bash
make down
```

Running unit tests
```bash
make test
```



