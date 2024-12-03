# NET Backend Bootcamp
.NET Backend Bootcamp

## Why we need Modular Monoliths
- Traditionally, monolithic architectures were the standard.
- All components were earlier  in single, indivisible unit and everything was centralized.
- It simplified initial deployment and development.
- As apps grew in size and complexity, challenges multiplied.
- Updating one part of application meant deploying the whole app
- Scaling could not be done individually.
- So it led to rise of microservices.
- They are independently deployable services and communicate via lightweight mechanisms
- They are easier to update and scale.
- Distributed nature of microservices led to complexities in managing network latency, data consistency and fault tolerance.
- It complicated the development environment and operational overhead.
- So modular monolith architecture emerged as a bridge between monoliths and microservices. 
- Offers the agility of microservices.
- MMs breaks down a traditional monolith into well defined loosely coupled modules that are easier to develop and maintain
- Each module is designed around a business domain, but run in the same application process.
- Simplify data management and transactional integrity.
- Start with MM and gradually evolve to microservices.
- ![alt text](image.png)
- Secure APIs with Keycloak using Open ID Connect and Bearer Tokens
- Implement outbox pattern for reliable messaging between modules.
- ![alt text](image-1.png)
- ![alt text](image-2.png)
- ![alt text](image-3.png)
- ![alt text](image-4.png)
- ![alt text](image-5.png)
- ![alt text](image-6.png)
- ![alt text](image-7.png)
- ![alt text](image-8.png)
- ![alt text](image-9.png)
- ![alt text](image-11.png)

## Characteristics of Monolithic Architecture
- ![alt text](image-12.png)
- What is wrong with traditional monolithic architecture
- Scalability is a big problem, scale everything or none at all
- Leads to inefficient resource utilization
- Becomes complex with time.
- Tight coupling...change in one part may affect other parts of the application.
- Makes overall development process slower 
- Expensive to maintain and difficult to add new features.
- Single point of failure and rollback is difficult and risky
- Deploying updates requires downtime.
- Leads to technology lock-in
- ![alt text](image-13.png)
- Leads to Fear Cycle.
- Most of time is spent in maintaining old application
- Responding to new business requirements is slow
- We accumulate technical debt and code quality is compromised.
- Fixing bugs is tough.
- Requires extensive testing.
- Traditional monoliths lead to a big ball of mud.

### Microservice architectural style is an approach to developing a single application as a suite of small services each running in its own process and communicating with lightweight mechanisms often an HTTP or gRPC API

- Microservice architecture is cloud native.
- Microservices have distribution tax.
- Lot of moving parts compared to monolithic application
- Prone to network latency and failures.
- Need robust retry and fallback mechanisms.
- Traditional ACID transactions are hard to implement in a distributed system
- Ensuring data consistency across multiple services is difficult.
- Eventual consistency and distributed transactions are often necessary but add complexity.
- May require data duplications
- Need to setup CI/CD for each microservice.
- Monitoring and logging is difficult.
- Need to invest in Devops automation
- Each protocol for communication has its own trade offs.
- Need consul, eureka for service discovery.
- Handling failures is difficult. Need circuit breakers.
- Difficult to setup integration tests.
- Debugging distributed systems is tough. Need Jaeger and Zipkin for distributed tracing. 

## Microservices Antipattern: Distributed Monolith.
- Here a microservices based system retains the drawbacks of a traditional monolithic architecture.
- Here services are highly dependent on each other. Change in one service require changes in another service.
- Here we need synchronized deployments. Negates benefits of microservices.
- Often we have shared databases which makes it difficult to change database schema without affecting all the dependent services.
- Makes database migrations challenging.
- Frequent synchronous calls may lead to bottlenecks.

## Why distributed monoliths happen?
- Poorly defined service boundaries.
- Lack of knowledge of domain driven design
- Sharing database schema across services.
- Migrating from monolithic application to microservices without changing code.
- ![alt text](image-14.png)
- ![alt text](image-15.png)

## Modular Monolith Architecture
- Bridge between Traditional Monolithic and Microservice Architectures.
- In MM architecture, application is developed as a single cohesive unit but is internally divided into well-defined, loosely coupled modules
- Maintain the simplicity of monolith but also benefits of modularity associated with microservices.
- Breaks up the code into independent modules and each module encapsulates their own features
- We will build and deploy a single app 
- But **modules represent bounded contexts** of our application domain and group features of domain contexts.
- App is divided into several modules and **each module encapsulates specific business domain.**
- **Communication between modules is through specific well defined interfaces**, reducing dependencies and enhancing maintainability.
- App is **deployed as a single unit**.
- **Communication is through direct method calls** or event mechanisms within the same application process.
- **segment database by schemas** or tables specific to each module.
- ![alt text](image-16.png)
- Each module is a separate class library project.


## Resurgence of Modular Monolith.
- Middle Path between monolithic and microservices
- Not a new concept
- Do not want complexity of microservices.
- Want benefits of monolithic architecture with agility of microservices.
- Each module is designed a specific business domain.

## Advantages over microservices
- Eliminates complexity of communication
- Possible to achieve ACID 
- Data can be consistent
- We can have targeted scaling of specific modules
- Avoid complexity of service discovery, network latency and inter-service communication.
  
## Donot start any project with microservices.
- Every project starts off as a monolith till the need for microservices arises.
- Need to define bounded contexts.
- Martin Fowler advocates monolith first architecture.

## Starting Point for Microservices: Modular Monolith
- Companies can start with modular monolith and later spin off some modules as full fledged microservices.
- Can incrementally refactor parts of monolith into microservices.
- Need efficient resource utilization.
- Transitioning from monolith to microservices can be risky and complex.
- Avoid Big bang approach
- Gradually breakdown monolith into microservices.
- ![alt text](image-17.png)
- Need smaller manageable changes by focusing on one module at a time.
- ![alt text](image-18.png)
  

## Shopify has modular monolithic architecture.
- Built on ruby on rails 
- 2.8 million lines of code
- 500,000 commits.
- Engineering team made their monoliths even more modular by creating smaller, independent units of code called components.
- Shopify uses multi-layered testing approach.
- Used canary testing where changes are first deployed to a small subset of production instances.
- Deployment pipeline involves building images, deploying to canaries, monitoring metrics and rolling out changes across the global fleet.


## Bounded context pattern
- Central pattern in DDD. Helps in identifying boundaries within which a particular domain model applies.
- What are the main functionalities?
- How do different parts of the system interact?
- What are key entities and processes?
- ![alt text](image-19.png)

## Setting up the Project
- ![alt text](image-20.png)
- launchSettings.json file contains the various profiles for running the application.
- what is launchsettings.json ?
- launchSettings.json is a configuration file used in .NET projects, particularly for ASP.NET Core applications. It contains settings related to how the application should be launched during development.
- Profiles: Different configurations for launching the application, like IIS Express, Project, or custom profiles.
- Environment Variables: Specific environment variables that should be set when the application is run.
- Application URL: The URL at which the application will be accessible when launched.

## External vs Internal Architecture:
- ![alt text](image-21.png)
- Internal Architectural Styles can be:
- N-Layer Architecture
- Vertical Slice Architecture
- Clean Architecture.

## Vertical Slice Architecture
- Introduced by Jimmy Bogard
- Competes with Traditional Layered architecture/onion/clean architecture.
- Aims to organize code around specific features or use cases rather than technical concerns
- A feature is implemented across all layers from UI to database
- Divides application into distinct features or functionalities.
- In contrast to traditional layered architecture where application is divided horizontally.
- ![alt text](image-22.png)
- Application into features based slices.
- Each slice is self contained and independent.
- Reduced dependencies between different parts of application
- Each slice is its own mini application from start to finish
- Supports scalability and maintainability.
- New features can be added as a separate slice
- Allows for improved testing and deployment
- Aligns well with Agile and Devops practices.
- Simplifies refactoring and upgrades.
- Problem is there might be duplication of code across slices.
- Keeping the slices independent is tough.

## Clean Architecture
- Introduced by Uncle Bob.
- Separate concerns and create systems independent of frameworks, UI and databases.
- ![alt text](image-23.png)
- System is more adaptable to change.
- Not tightly coupled with one framework.
- Business rules can be tested without UI or database
- UI can change easily without changing rest of the system
- Business rules is independent of database
- Business rules dont know anything about the outside world.
- Has entity layer(domain layer also called core) 
- -->Application Layer(Application specific business rules) 
- -->Interface Adapters Layer or Infrastructure Layer(Maps data from db model to domain entities.)
- -->Frameworks and Driver Layer(Contains tools like DB, Web framework)
- Systems with clean architecture are more modular, adaptable, testable and maintainable.

## Vertical Slice vs Clean Architecture.
- VSA is built around feature slices which are independent.
- CA focuses on separation of concerns and dependency rules 
- In VSA, we deliver complete features with each feature potentially touching all layers of the stack 
- CA is more structured approach where business logic is different from external concerns
- VSA well suited for agile teams working on complex apps and lot of features
- CA is ideal for large scale application where long term maintenance is paramount. 
- If priority is to develop features quickly and independently go for VSA 
- If teams need to be agile and deliver features in short cycles go for VSA 
- In microservices also , where each service is responsible for distinct feature go for VSA 


## Shared Kernel Pattern in DDD 
- Small subset of domain model is shared between multiple bounded contexts or modules 
- Includes common data structures, utility classes or services that multiple parts of the system need to use.
- Shared kernel ensures consistency and reduces duplication while maintaining clear boundaries between different modules.
- ![alt text](image-24.png)
- Only minimal, necessary part of domain model are shared, keeping rest of module implementations decoupled.
- By using shared project we can centralize all common code and cross cutting concerns into a single location.
- This includes common nuget objects, entities and value objects.

## Dependencies Path
- ![alt text](image-25.png)


