# Personal AI Assistant

A modular, extensible AI assistant that leverages hosted LLM APIs with a cost-effective local fallback, supports rich conversation context through Retrieval-Augmented Generation (RAG), and is built for extensibility via a plugin architecture. This project uses ASP.NET Core for the backend, Next.js for the frontend, Postgres (with optional [pgvector](https://github.com/pgvector/pgvector)) for persistent storage, and RabbitMQ for decoupled messaging between services. The entire solution is containerized with Docker and can be deployed on Kubernetes.

## Table of Contents

- [Features](#features)
- [Technology Stack](#technology-stack)
- [Architecture Overview](#architecture-overview)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Local Setup](#local-setup)
- [Configuration](#configuration)
- [Deployment](#deployment)
  - [Docker / Docker Compose](#docker--docker-compose)
  - [Kubernetes](#kubernetes)
- [CI/CD](#cicd)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgements](#acknowledgements)

## Features

- **AI Conversation Engine**:  
  Leverages both a hosted LLM API and a local LLM fallback (based on token budgeting) to provide responsive, cost-effective responses.

- **Retrieval-Augmented Generation (RAG)**:  
  Enhances responses by retrieving relevant conversation history stored in Postgres (with vector similarity using pgvector).

- **Plugin Architecture**:  
  Easily extend the assistant with additional capabilities (e.g., file uploads, email integration, notifications, reminders) through a decoupled, event-driven approach.

- **Inter-Service Messaging**:  
  Uses RabbitMQ for asynchronous communication between the core orchestrator and plugin services.

- **Containerized & Scalable**:  
  Dockerized services with Kubernetes deployment manifests for scalable production deployments.

## Technology Stack

- **Backend**: ASP.NET Core (.NET 9)
- **Frontend**: Next.js
- **Database**: Postgres (with optional pgvector extension)
- **Messaging**: RabbitMQ
- **LLM Integration**: Hosted LLM API & Local LLM fallback
- **Containerization**: Docker & Docker Compose
- **Deployment**: Kubernetes

## Architecture Overview

The system is designed with a modular architecture:

- **Core Orchestrator (Backend)**:  
  Manages conversation flows, applies RAG by retrieving context from the database, decides which LLM service to call (hosted vs. local), and publishes events for plugins.

- **Frontend Interface**:  
  A Next.js application that provides a chat interface for user interactions.

- **Persistent Storage**:  
  Postgres is used for storing conversation logs, user sessions, and vector embeddings.

- **Message Queue**:  
  RabbitMQ decouples inter-service communication, allowing plugins to subscribe to events such as `ConversationUpdated`.

- **Plugin Services**:  
  Additional microservices (e.g., for email notifications, file uploads, reminders) subscribe to RabbitMQ and extend the assistant’s functionality.

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (v14+ recommended)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
- [Kubernetes](https://kubernetes.io/) (for production deployment)
- (Optional) [Helm](https://helm.sh/)

### Local Setup

1. **Clone the Repository**

   ```bash
   git clone https://github.com/yourusername/ai-assistant.git
   cd ai-assistant
