using PaymentService.MessageBus;
using PaymentService.Events;
using Prometheus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]))
        };
    });

var app = builder.Build();

var eventBus = new EventBusRabbitMQ();
eventBus.Consume("order_created_queue", (orderEvent) =>
{
    Console.WriteLine($"[Pagamento Recebido] Pedido ID: {orderEvent.OrderId}, Cliente: {orderEvent.CustomerName}, Valor: {orderEvent.TotalPrice}");
});

app.UseMetricServer();
app.UseHttpMetrics();

// Iniciar consumidor RabbitMQ
var consumer = new OrderCreatedEventConsumer();
Task.Run(() => consumer.StartListening());

app.Run("http://0.0.0.0:5002");
