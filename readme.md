# Follow-Up: Case Study Overview

My goal for this case study was to create a solid and flexible foundation for the Everyday Game. Throughout development, I followed best practices as much as possible, focusing on clean architecture, modularity, and maintainability.

## Architectural Overview

### Service Locator Pattern
I used the Service Locator pattern to manage dependencies. While it has known drawbacks, I believe it suits this case well by keeping service access simple and centralized.

### Interfaces and Modularity
Interfaces were used extensively across the project to promote loose coupling and make components easier to extend or replace. This approach ensures the system can adapt to new requirements without major refactoring.

### Context Separation
- A global context registers shared services (e.g., player progress, loaders, progression system, window manager)
- Each scene has its own local context with scene-specific services

### Service Installation
Services are easy to register through installers, making it straightforward to swap implementations simply by replacing the installer.

### Foundation Classes
I created base classes for services and MonoBehaviours to standardize and streamline service initialization and lifecycle handling.

### Window Manager
A basic window manager was implemented, supporting a window stack using prefab-based UI loading. While loading windows as separate scenes would be more scalable, I chose prefabs for simplicity in this phase.

### Game Flow
When a user starts a level, the data is loaded from a ScriptableObject, and the appropriate game logic is launched.

### Game Support Architecture
I created base classes to support multiple game types, including `GameService`, `LevelData`, and `GoalsService`, making it easier to expand beyond the initial game.

### Adherence to SOLID Principles
I aimed to keep all systems simple, decoupled, and compliant with SOLID design principles.

## Additional Work

I developed several services that were not yet used in the prototype, including:
- A pooling system
- An asynchronous scene loading system with task-based loading under the hood

## Final Note

As mentioned earlier, I spent approximately 20 hours on this case study. While I didn't have time to cover every requirement, I managed to build the Color Sort game with a strong and extendable architecture that provides a solid base for future game types.

---

## AI Usage

For this project, I used Cursor IDE integrated with Claude 4, which I found to be the best fit after evaluating several models. Claude provided the most consistent and high-quality outputs for my development needs.

## Configuration and Guidelines

To ensure effective use of AI, I configured Cursor with a clear set of development guidelines, including:

- Defining the AI's role (e.g., experienced senior developer)
- Specifying years of experience
- Enforcing clean, maintainable, and well-structured code
- Following industry best practices
- Applying SOLID principles and other architectural patterns

## My Workflow with AI

From experience, I've learned that without clear direction, AI-generated code can become disorganized or overly generic. To prevent this, I follow a structured workflow:

1. **Define the goal**: Clearly explain the logic and functionality I want to implement
2. **Break down the problem**: Ask the AI to split the logic into manageable, logical chunks
3. **Iterative development**: Implement each chunk one at a time, refining as needed
4. **Code review**: Manually review the generated code, correct any bugs or inconsistencies, and clarify intent where necessary

A critical part of this process is explicitly defining expectations—instructing the AI on how the code should be written and what practices or patterns to follow. This significantly improves both the quality and maintainability of the generated code.