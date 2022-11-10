# LifeInsuranceRetirement

## How to setup database

Prerequisite: make sure you are able to connect with your database successfully via connection string and your user rights or roles to create a database.
Open command prompt and run `dotnet tool install --global dotnet-ef` to install dotnet -ef globally.
Navigate to LifeInsuranceRetirement\LifeInsuranceRetirement.Data via command prompt.
run `dotnet ef database update -s ..\LifeInsuranceRetirement\LifeInsuranceRetirement.csproj` to run the migrations. 

## Setup user secrets

Open project `LifeInsuranceRetirement.sln` in Visual Studio and right click both project:
* LifeInsuranceRetirement.Api
* LifeInsuranceRetirement
Select Manage User Secrets and insert/edit your connection string example:
`{
  "ConnectionStrings": {
    "LifeInsuranceRetirementDb": "Data Source=YourDB Connection;Initial Catalog=LifeInsuranceRetirement;User ID=YourID;Password=YourPassword;TrustServerCertificate=True"
  }
}`

## Run the project

Rebuild the solution and run the project LifeInsuranceRetirement.Api if you want to run together with Angular App. Or run the project LifeInsuranceRetirement if you want to run ASP .net app.
