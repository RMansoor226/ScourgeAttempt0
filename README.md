# Project Scourge
### Unity 6 | C# | FPS Horde Shooter

> A survival-focused FPS horde shooter combining the round-based survival
> loop of Call of Duty Zombies with DOOM-inspired combat and future
> combat-puzzle encounter design.

![🎮 Download v0.4.0]() 

![🎥 Watch Demo](https://youtu.be/Tz_PT50aXgc)

![Project Scourge Gameplay](path/to/screenshot.png)

## Technical Highlights

### State-Based Enemy AI

Zombie behavior is implemented using an explicit state machine separating
Idle, Chase, Attack, and Death behavior. This provides a foundation for
introducing additional enemy archetypes and behaviors without coupling
all enemy logic into a single monolithic system.

### Object Pooling

Zombie entities use object pooling to reduce repeated runtime
instantiation and destruction during large horde encounters.

### Data-Driven Weapons

Weapon configuration is separated from runtime behavior using
ScriptableObjects, allowing new weapons to be created and tuned without
duplicating weapon logic.

### Performance Profiling

The project has been profiled during horde gameplay to establish CPU,
GPU, memory, AI, spawning, and object-lifecycle performance baselines.

## Current Release

### `v0.4.0 — Basic FPS Horde Shooter Prototype`

**v0.4.0 establishes the first fully functional FPS horde-shooter prototype.**

The project has progressed from individual Unity gameplay experiments into a cohesive playable experience containing the fundamental systems required for the core FPS gameplay loop.

The player can:

- Move and look around in first person.
- Sprint and jump.
- Shoot enemies using raycast-based weapons.
- Manage ammunition and reload weapons.
- Fight progressively stronger zombie waves.
- Receive visual, audio, and camera feedback when damaged.
- Pause the game and configure settings.
- Survive multiple rounds of increasingly difficult enemies.
- Reach a functional game-over state and restart the game.

This release establishes the **technical foundation** for the project's future combat sandbox, progression systems, enemy variety, and encounter design.

---

## Gameplay Vision

Project Scourge is designed around three major inspirations:

### Call of Duty Zombies — Survival & Progression

The core gameplay loop takes inspiration from the traditional round-based survival structure:

- Increasing enemy difficulty.
- Limited ammunition and resources.
- Escalating waves.
- Player progression.
- Increasing pressure over time.
- Risk/reward decisions.

The objective is to create the feeling of starting a round under pressure with limited resources and gradually building enough power and capability to survive increasingly difficult encounters.

### DOOM — Aggressive Combat & Combat Puzzles

The project aims to eventually move beyond passive survival.

Combat should encourage players to actively engage with enemies and the environment rather than simply retreating until enemies can be safely eliminated.

Future combat encounters will emphasize:

- Aggressive movement.
- Weapon specialization.
- Enemy prioritization.
- Resource management.
- Exploiting enemy weaknesses.
- Maintaining combat momentum.
- Positioning.
- Environmental opportunities.
- Deliberate weapon and resource selection.

The long-term objective is to create **combat puzzles** where the player must determine how to approach an encounter rather than simply overpowering it.

### Left 4 Dead — Enemy Sandbox

Enemy variety and interactions will eventually create situations where the player must adapt to changing combinations of threats.

Future enemy design will explore:

- Specialized enemy behaviors.
- Enemy roles.
- Different threat priorities.
- Enemy combinations.
- Spatial pressure.
- Dynamic encounter conditions.

The goal is to create encounters where the behavior and composition of enemies meaningfully influence how the player approaches combat.

---

# Core Design Philosophy

The central progression philosophy of Project Scourge is:

> **Struggle → Adapt → Build Power → Master the Sandbox**

The early game should make the player feel vulnerable.

Resources are limited, enemies are dangerous, and mistakes are costly.

As the player progresses, they should gradually gain access to better weapons, upgrades, powerups, and resources.

However, increasing player power should not eliminate the need for skill.

Instead, the player's growing power should create increasingly complex decisions:

- Which weapon should be used?
- Which enemy should be prioritized?
- When should ammunition be spent?
- When should a resource be saved?
- Which upgrade provides the greatest benefit?
- Should the player maintain distance or engage aggressively?
- How can the environment be used?
- How can enemy behaviors be manipulated?

The intended result is a progression from **survival** into a hard-earned **power fantasy** where the player feels powerful because they have learned how to play the system rather than simply because their numerical statistics increased.

---

# Current Features

## Player

- First-person camera
- WASD movement
- Camera-relative movement
- Sprinting
- Jumping
- Custom gravity
- Ground detection
- Mouse look
- Cursor locking
- Configurable mouse sensitivity

## Weapon & Combat

- Raycast-based shooting
- Fire input
- Fire-rate handling
- Damage system
- Health system
- Weapon prefabs
- Weapon ScriptableObjects
- Finite magazine ammunition
- Reserve ammunition
- Reload system
- Ammo counter UI
- Weapon recoil
- Muzzle flash
- Surface hit/impact effects
- Gunshot audio
- Dry-fire audio
- Reload audio

Weapon configuration is designed around reusable weapon assets and ScriptableObjects so that future weapon types can be introduced without rebuilding the underlying weapon architecture.

## Zombie AI

- Reusable zombie prefab
- NavMesh navigation
- NavMeshAgent
- Player targeting
- State-machine-based behavior
- Idle state
- Chase state
- Attack state
- Death state
- Attack cooldown
- Player damage
- Zombie attack audio
- Zombie death audio
- Zombie growl audio
- Zombie animation integration

The zombie behavior architecture separates major behavioral states so that future enemy types and behaviors can be expanded without requiring the entire AI system to be rewritten.

## Horde & Wave System

- Zombie spawn points
- Randomized spawning
- Zombie spawning limits
- Wave Manager
- Wave progression
- Wave completion detection
- Increasing zombie population
- Increasing zombie health
- Increasing zombie movement speed
- Round UI

The wave system provides the foundation for the project's long-term survival gameplay loop.

## Player Feedback

- Player health
- Health bar
- Damage vignette
- Damage flash
- Damage flinch
- Damage audio
- Death audio
- Death state
- Game-over screen
- Restart functionality
- Quit functionality

The damage feedback system combines multiple forms of feedback to communicate player state without relying solely on the health bar.

## Game Management & UI

- Pause menu
- Resume functionality
- Game-over screen
- Restart functionality
- Quit functionality
- Settings navigation
- Audio settings
- Video settings
- Resolution selection
- Fullscreen/windowed mode
- Mouse sensitivity
- AudioMixer integration
- Centralized AudioManager

## Audio

- Centralized AudioManager
- Music audio source
- SFX audio source
- UI audio source
- Ambient audio source
- AudioMixer
- Master volume control
- Music volume control
- SFX volume control
- UI volume control
- Ambient volume control
- Background music
- Round-start music
- Round-end music
- Zombie growls
- Player footsteps
- Weapon reload audio

## Performance & Scalability

- Zombie object pooling
- Unity Profiler testing
- CPU profiling
- GPU profiling
- Memory investigation
- AI performance investigation
- Spawning performance investigation
- Object lifecycle optimization

Performance profiling was performed on the horde gameplay systems to establish a baseline for future scalability work.

---

# Technical Architecture

Project Scourge is being developed using a modular, component-oriented architecture designed to keep gameplay systems independently testable and extensible.

A simplified representation of the current architecture is:

```text
                         Game Systems
                              │
              ┌───────────────┼────────────────┐
              │               │                │
              ▼               ▼                ▼
        Game Manager     Audio Manager    Settings Manager
              │               │                │
              │               ▼                ▼
              │          Audio Mixer       Settings UI
              │
              ▼
        Wave Manager
              │
              ▼
       Zombie Spawner
              │
              ▼
          Zombie
      ┌───────┼────────┐
      │       │        │
      ▼       ▼        ▼
     AI     Health    Audio
      │
      ▼
 State Machine
 ┌────┼─────┬─────┐
 ▼    ▼     ▼     ▼
Idle Chase Attack Dead


Player
 ├── Movement
 ├── Camera
 ├── Weapon Controller
 ├── Health
 ├── Damage Feedback
 └── Input

Weapon
 ├── Weapon Controller
 ├── Ammunition
 ├── Reload
 ├── Recoil
 └── Effects

```

The architecture follows several principles:

- **Single responsibility** where practical.
- **Component-based gameplay systems.**
- **Reusable prefabs.**
- **ScriptableObjects for configurable data.**
- **Interfaces for shared gameplay behavior.**
- **Explicit state-based enemy behavior.**
- **Centralized management of global systems.**
- **Separation of configuration from runtime behavior.**
- **Incremental development through feature branches and milestones.**

The architecture will continue to evolve as the project becomes more complex.

---

# Technologies & Tools

## Engine

- Unity
- Unity 6
- Unity Input System
- Unity UI
- NavMesh
- NavMeshAgent
- Animator
- Particle Systems
- AudioMixer
- Unity Profiler

## Programming

- C#
- Object-Oriented Programming
- Interfaces
- Component-based architecture
- State machines
- Coroutines
- ScriptableObjects
- Event-driven communication

## Development & Version Control

- Git
- GitHub
- GitHub Issues
- GitHub Milestones
- GitHub Releases
- Feature branching
- Release branches
- GitHub Actions
- Git LFS

---

# Development Process

Project Scourge is developed incrementally through small, testable gameplay systems.

Rather than attempting to build the entire game simultaneously, development is divided into feature tickets with:

- Defined objectives
- Descriptions
- Acceptance criteria
- Dedicated branches
- Testing
- Integration
- Milestones
- Versioned releases

The development process is intentionally similar to a professional software development workflow.

The project began with the foundational player controller and progressively introduced combat, enemy AI, wave management, player feedback, weapon systems, game-state management, audio, scalability, and performance profiling.

---

# Development Milestones

| Release | Milestone | Status |
|---|---|---|
| `v0.1.0` | Core FPS Foundation | ✅ Complete |
| `v0.2.0` | Zombie AI & Combat Foundation | ✅ Complete |
| `v0.3.0` | Horde & Wave System | ✅ Complete |
| `v0.4.0` | Basic FPS Horde Shooter Prototype | ✅ Complete |
| `v0.5.0` | Expanded Weapon Mechanics & Sandbox | Planned |
| `v0.6.0` | Player Economy | Planned |
| `v0.7.0` | Powerups & Upgrades | Planned |
| `v0.8.0` | Enemy Variety & Difficulty | Planned |
| `v0.9.0` | Combat Sandbox & Encounter Design | Planned |
| `v1.0.0` | Polished Vertical Slice | Planned |

---

# Future Roadmap

## v0.5.0 — Expanded Weapon Mechanics & Sandbox

The next major phase will expand the player's combat options.

Planned systems include:

- Multiple weapon types
- Weapon specialization
- Weapon-specific strengths and weaknesses
- Expanded weapon interactions
- More meaningful weapon tradeoffs
- Expanded combat sandbox

The goal is to make weapon selection an important part of moment-to-moment decision making.

---

## v0.6.0 — Player Economy

The project will introduce resource and economic systems that give the player meaningful long-term decisions.

Planned systems include:

- Currency/resource systems
- Purchasing systems
- Resource management
- Strategic spending
- Risk/reward decisions

The economy will be designed to reinforce the survival loop rather than simply act as a progression menu.

---

## v0.7.0 — Powerups & Upgrades

The player will gain additional methods of increasing their combat capability.

Planned systems include:

- Temporary powerups
- Permanent upgrades
- Player progression
- Upgrade synergies
- Expanded player power curve

These systems will begin creating the transition from early-game survival toward the intended late-game power fantasy.

---

## v0.8.0 — Enemy Variety & Difficulty

The zombie sandbox will be expanded beyond a single basic enemy archetype.

Planned systems include:

- Additional enemy archetypes
- Specialized enemy behaviors
- Different enemy roles
- More complex enemy combinations
- Increased encounter complexity
- More varied difficulty scaling

The objective is to make enemy composition an important factor in combat decision-making.

---

## v0.9.0 — Combat Sandbox & Encounter Design

This phase represents a major shift in the project's design direction.

The wave system will increasingly be used as a framework for constructing **designed combat encounters** rather than simply spawning larger groups of enemies.

Combat encounters will explore:

- DOOM-inspired aggressive combat
- Combat puzzles
- Enemy prioritization
- Resource pressure
- Weapon/enemy interactions
- Environmental opportunities
- Arena positioning
- Enemy combinations
- Player movement and momentum
- Dynamic combat scenarios

The goal is to create encounters where the player must **solve the combat situation** rather than simply survive a numerical increase in enemy difficulty.

---

## v1.0.0 — Polished Vertical Slice

The eventual `v1.0.0` release will consolidate the project's major systems into a polished vertical slice demonstrating the complete gameplay vision.

The intended experience is:

> **A survival-focused FPS horde shooter that combines the round-based progression and resource pressure of Call of Duty Zombies with the aggressive combat, combat puzzles, and player-driven sandbox of DOOM and the dynamic enemy encounters of Left 4 Dead.**

The vertical slice will prioritize:

- Cohesive gameplay
- Strong combat feel
- Meaningful player decisions
- Enemy variety
- Encounter design
- Resource management
- Progression
- Visual presentation
- Audio presentation
- Performance
- Overall polish

---

# Performance

Performance is treated as a first-class engineering concern rather than a final-stage optimization task.

The project has already undergone profiling of:

- CPU frame time
- GPU frame time
- Memory usage
- Garbage collection
- Zombie AI
- Zombie spawning
- Combat systems
- Object lifecycle

Object pooling has been implemented to reduce repeated runtime instantiation and destruction of zombie entities.

The current prototype establishes a performance baseline that will be used when future systems increase the complexity and size of combat encounters.

Detailed profiling methodology and results will be documented as the project continues to scale.

---

# Known Limitations

`v0.4.0` is a technical and gameplay prototype rather than a finished commercial game.

Current limitations include:

- Limited weapon variety.
- Limited enemy variety.
- Basic environment and encounter design.
- Placeholder/early-stage visual assets.
- Limited player progression.
- No persistent economy or long-term progression system.
- No advanced powerup system.
- Basic zombie behaviors compared to the planned enemy sandbox.
- Limited environmental interaction.
- Combat encounters are primarily wave-based rather than fully designed combat puzzles.
- Some UI/settings categories remain relatively basic.
- Audio and visual effects are functional but not yet at final production quality.
- No final art direction or comprehensive visual polish.
- No final balancing pass for the complete gameplay experience.

These limitations are intentional.

**v0.4.0 establishes the engineering foundation. Future releases will increasingly focus on gameplay depth, player choice, encounter design, and production quality.**

---

# Controls

| Action | Input |
|---|---|
| Move | `WASD` |
| Look | `Mouse` |
| Sprint | `Left Shift` |
| Jump | `Space` |
| Fire | `Mouse 1` |
| Reload | `R` |
| Pause | `Escape` |

Additional controls may be introduced as new gameplay systems are implemented.

---

# How to Play

1. Download the latest playable build from the GitHub Releases page.
2. Extract the game files.
3. Launch the executable.
4. Enter the game and begin surviving zombie waves.
5. Manage ammunition and reloads while fighting.
6. Survive increasingly difficult waves.
7. Use the pause menu to access settings or exit the game.

The current release is primarily intended as a **playable development prototype and portfolio demonstration**.

---

# Repository Structure

The project follows a conventional Unity project structure.

```text
Assets/
├── Audio/
├── Materials/
├── Prefabs/
├── Scenes/
├── Scripts/
│   ├── Enemies/
│   ├── Managers/
│   ├── Player/
│   ├── UI/
│   └── Weapons/
├── Settings/
└── ...
```

Gameplay systems are organized by responsibility to make the project easier to navigate and maintain.

---

# Project Development History

The project began as an exercise in learning Unity 3D and progressively evolved into a larger personal game-development project.

Early development focused on learning Unity fundamentals:

1. First-person movement
2. Camera control
3. Shooting
4. Damage
5. Enemy creation
6. Navigation
7. Enemy state machines
8. Wave management

The project then transitioned toward production-oriented development:

9. Difficulty scaling
10. Weapon architecture
11. Reload and ammunition systems
12. Recoil
13. Weapon effects
14. Player damage feedback
15. Game-state management
16. Audio architecture
17. Settings
18. Object pooling
19. Performance profiling

This progression allows the project to serve two purposes:

- Develop practical Unity and game-programming skills.
- Build a substantial portfolio project demonstrating software architecture, gameplay programming, debugging, optimization, and game-design thinking.

---

# Development Philosophy

Project Scourge follows several development principles:

## Build Systems Before Content

Core systems are implemented and tested before large amounts of content are created.

## Prefer Reusable Architecture

Systems should be designed so that future weapons, enemies, abilities, and encounters can reuse existing infrastructure.

## Test Before Expanding

New systems are tested independently and during integrated gameplay before additional complexity is introduced.

## Profile Rather Than Assume

Performance decisions should be guided by profiling and measurements rather than assumptions about bottlenecks.

## Separate Engineering From Design

A technically functional system is not automatically a good gameplay system.

As the project progresses, technical implementation will increasingly be evaluated according to the gameplay experience it produces.

## Build Toward a Vertical Slice

The ultimate objective is not to create an enormous quantity of unfinished features.

The objective is to create a **small but highly representative slice of the final game** that demonstrates the project's technical and design direction.

---

# Portfolio Goals

Project Scourge is intended to demonstrate practical experience in:

- Gameplay programming
- Unity development
- C# programming
- Object-oriented design
- Component-based architecture
- AI programming
- State machines
- Navigation systems
- Weapon systems
- UI development
- Audio systems
- Performance profiling
- Optimization
- Debugging
- Version control
- Agile development practices
- Game-system design

The project is being developed as a long-term portfolio piece with the goal of demonstrating not only the ability to implement gameplay systems, but also the ability to **design, organize, test, debug, profile, and iterate on a complete interactive software project.**

---

# Current Status

**Current Release:** **`v0.4.0`** **— Basic FPS Horde Shooter Prototype**

**Development Status: Active**

The foundational FPS prototype is complete.

Future development will shift increasingly toward:

- Expanding the weapon sandbox
- Building player progression
- Introducing powerups and upgrades
- Creating enemy variety
- Designing combat encounters
- Developing combat puzzles
- Improving the game's visual and audio identity
- Balancing the player power curve
- Increasing environmental interaction
- Creating a polished vertical slice

---

# License

This project is currently a personal portfolio and development project.

Unless otherwise specified, project assets and source code should not be redistributed or reused without permission.
```
