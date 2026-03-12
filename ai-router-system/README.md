# AI Router System (.NET 10 Microservices)

Production-oriented Router + Specialists architecture for travel use-cases.

## Services
- Gateway API: external entrypoint (`POST /ai/query`)
- Router Service: intent classification + specialist dispatch
- Flight Agent Service
- Hotel Agent Service
- Itinerary Agent Service
- Restaurant Agent Service

## Shared Libraries
- Contracts: cross-service DTOs
- Models: domain constants + tool abstractions
- LLM: reusable OpenAI client and configuration

## Flow
Client -> Gateway -> Router -> Specialist Agent -> OpenAI -> Response

## Run
1. Set `OPENAI_API_KEY`
2. `cd ai-router-system/docker`
3. `docker compose up --build`
4. Call `POST http://localhost:8080/ai/query`

## Extending with a New Specialist
1. Add new service project.
2. Implement `IAgentTool` instances for domain tools.
3. Configure system prompt in the service's `AgentService`.
4. Add route in `Router.Service` intent mapping + appsettings.
5. Register in `docker-compose.yml`.
