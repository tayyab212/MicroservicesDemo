# Microservices CQRS Example with MediatR, MassTransit, and RabbitMQ

This is a **small microservices example** demonstrating:

- **CQRS** (Command Query Responsibility Segregation) with **MediatR**  
- **Message-based communication** using **MassTransit** and **RabbitMQ**  
- **Shared contracts** for decoupling microservices  

The solution contains **three projects**:

1. **Shared.Contracts** – shared message types (`OrderMessage`)  
2. **OrderService** – publishes orders using MediatR commands and MassTransit  
3. **NotificationService** – consumes order messages from RabbitMQ and prints them  

---

## Folder Structure

Solution/
│
├─ Shared.Contracts/
│ └─ Messages/
│ └─ OrderMessage.cs
│
├─ OrderService/
│ ├─ Commands/
│ │ ├─ PlaceOrderCommand.cs
│ │ └─ PlaceOrderHandler.cs
│ └─ Program.cs
│
└─ NotificationService/
├─ Consumers/
│ └─ OrderConsumer.cs
└─ Program.cs


---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- [Docker](https://www.docker.com/) (for RabbitMQ)  

---

## 1️⃣ Start RabbitMQ

Run RabbitMQ with management UI using Docker (PowerShell):

```powershell
docker run -d `
  --hostname rabbitmq `
  --name rabbitmq `
  -p 5672:5672 `
  -p 15672:15672 `
  rabbitmq:3-management

Access dashboard: http://localhost:15672
Username/password: guest / guest

2️⃣ Run OrderService
cd OrderService
dotnet run

3️⃣
Run NotificationService
cd NotificationService
dotnet run

Expected output:
[NotificationService] Order received: Id=1, Product=Laptop
[NotificationService] Order received: Id=2, Product=Phone

