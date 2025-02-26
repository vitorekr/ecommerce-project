# **E-commerce Microservices - Kubernetes & AWS**

## 📌 **Visão Geral**
Este projeto é um **sistema de e-commerce baseado em microserviços** rodando em **Kubernetes** com **API Gateway**, autenticação JWT, escalabilidade automática e CI/CD na **AWS**.

✅ **API Gateway seguro com Rate Limiting e JWT**  
✅ **Microserviços escaláveis no Kubernetes**  
✅ **CI/CD automatizado com GitHub Actions e AWS CodePipeline**  
✅ **Monitoramento com Prometheus, Grafana e CloudWatch**  
✅ **Logs centralizados com ELK Stack**  
✅ **Circuit Breaker e Fallback com Polly**  
✅ **Testes de carga para validar desempenho**  

---

## 🚀 **Tecnologias Utilizadas**
- **.NET 8** (ASP.NET Core Web API)
- **Kubernetes (EKS - Amazon Elastic Kubernetes Service)**
- **Docker & AWS ECR** (Repositório de imagens)
- **Ocelot API Gateway**
- **RabbitMQ/Kafka** (Comunicação assíncrona)
- **PostgreSQL** (Banco de dados)
- **Elasticsearch, Logstash, Kibana (ELK Stack)**
- **Prometheus & Grafana** (Monitoramento)
- **AWS CodePipeline & GitHub Actions** (CI/CD)

---

## 🛠️ **Estrutura dos Microserviços**

📁 `CatalogService` - Gerencia produtos  
📁 `OrderService` - Gerencia pedidos  
📁 `PaymentService` - Processa pagamentos  
📁 `APIGateway` - Centraliza requisições e segurança  
📁 `AuthService` - Geração e validação de JWT  
📁 `Monitoring` - Prometheus, Grafana, ELK Stack  

---

## ⚙️ **Configuração Local com Docker**

1️⃣ **Clone o repositório:**
```bash
git clone https://github.com/seu-repositorio/ecommerce-microservices.git
cd ecommerce-microservices
```

2️⃣ **Inicie os serviços:**
```bash
docker-compose up -d --build
```

3️⃣ **Acesse os serviços:**
- API Gateway: [http://localhost:8000](http://localhost:8000)
- Prometheus: [http://localhost:9090](http://localhost:9090)
- Kibana: [http://localhost:5601](http://localhost:5601)

---

## ☁️ **Deploy na AWS (EKS - Kubernetes)**

1️⃣ **Autenticar no AWS CLI:**
```bash
aws configure
```

2️⃣ **Criar Cluster Kubernetes (EKS):**
```bash
eksctl create cluster --name ecommerce-cluster --region us-east-1 --nodegroup-name standard-workers --node-type t3.medium --nodes 3
```

3️⃣ **Deploy dos Microserviços no EKS:**
```bash
kubectl apply -f k8s/
```

4️⃣ **Verificar serviços rodando no Kubernetes:**
```bash
kubectl get pods -n ecommerce
kubectl get services -n ecommerce
```

---

## 🔐 **Autenticação JWT**

1️⃣ **Gerar token de acesso:**
```bash
curl -X POST http://localhost:5003/api/auth/login -H "Content-Type: application/json" -d '{"username":"admin","password":"password"}'
```

2️⃣ **Acessar APIs protegidas:**
```bash
curl -H "Authorization: Bearer SEU_TOKEN_AQUI" http://localhost:8000/api/orders
```

---

## 🛡️ **Rate Limiting e Segurança**

- **Rate Limiting:** 5 requisições a cada 10 segundos por cliente.
- **JWT Role-Based Authorization (RBAC):** Apenas usuários autenticados acessam recursos protegidos.
- **AWS WAF:** Proteção contra DDoS no API Gateway.

---

## 📊 **Monitoramento**

- **Logs em tempo real no Kibana:**
  📌 [http://localhost:5601](http://localhost:5601)

- **Monitoramento com Prometheus:**
  📌 [http://localhost:9090](http://localhost:9090)

- **Dashboards no Grafana:**
  📌 [http://localhost:3000](http://localhost:3000) (Usuário: admin | Senha: admin)

---

## 📈 **Testes de Carga**

1️⃣ **Instalar k6:**
```bash
brew install k6  # MacOS
choco install k6  # Windows
sudo apt install k6  # Linux
```

2️⃣ **Executar teste de carga:**
```bash
k6 run load-test.js
```

---

## 🎯 **Conclusão**
Este projeto entrega um sistema **escalável, seguro e pronto para produção** baseado em **microserviços com Kubernetes**. Ele cobre **CI/CD automatizado, monitoramento avançado, logs centralizados e alta disponibilidade**.

📌 **Contribuições são bem-vindas!** Se quiser melhorar alguma parte, sinta-se à vontade para abrir um PR. 🚀

