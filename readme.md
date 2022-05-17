md NETMicroservicesCourse

cd NETMicroservicesCourse 

dotnet new webapi -n PlatformService

cd PlatformService

## Install Packages
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.Console



## docker build
docker build -t gokula2010/platformservice .


## K8s
kubectl apply -f .\k8s\platforms.yaml 

kubectl delete -f .\k8s\platforms.yaml or
kubectl delete deployment [name of the deployemnt that configured in deployment yaml file]
kubectl delete deployment platform-deployment


kubectl get deployments

kubectl get pods



NodePort (service) can be used to connect the the pod that running the container.

NodePort - can be composed in part for deployment yaml file or can be added in a separate file. 

Kubectl get service => will list all the services. and it will proivde the port 32276 that can accessed from outside world.
ex:
platformservice-service   NodePort    10.101.205.184   <none>        80:32267/TCP   28s
http://localhost:32267/api/platforms

# Create CommandService in root for the this folder

dotnet new webapi -n CommandService

cd CommandService


## Install Packages
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore.SqlServer



#RabbitMQ

- A Message Broker - it accepts and forwards messages
- Messages are sent by Producers (or Publishers)
- Messages are received by Consumers (or Subscribers)
- Messages are stored on Queues (essentially a message buffer)
- Exchanges can be used to add "routing" functionality
- Uses Advanced Message Queuing Protocol (AMQP)

##Exahanges 
- Direct Exchanges
- Fanout Exchange
- Topic Exchange
- Header Exchange

dotnet add package RabbitMQ.Client


#Dependnecy Injection

##Singleton
Created first time requested, subsequent requests use the same instance

##Scoped
Same within a reqeust but created for every new request

##Transient
New instance provided everytime, never the same/reused
