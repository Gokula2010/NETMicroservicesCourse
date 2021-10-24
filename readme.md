md NETMicroservicesCourse

cde NETMicroservicesCourse 

dotnet new webapi -n PlatformService

cd PlatformService

## Install Packages
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore.SqlServer



## docker build
docker build -t gokula2010/platformservice .