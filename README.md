# Project Scourge

## A First-Person Horde Survival Shooter Inspired by Call of Duty Zombies, DOOM, and Left 4 Dead

![Gameplay Screenshot](placeholder) // Not implemented yet

## Overview

Project Scourge is a first-person horde survival shooter developed in Unity with the goal of combining the round-based survival progression and resource management of **Call of Duty Zombies** with the aggressive combat philosophy and encounter design of **DOOM**.

The core design goal is to create a game where players transition from a vulnerable survivor struggling against overwhelming enemies into a powerful combatant through skillful resource management, weapon mastery, and strategic decision-making.

Rather than relying solely on increasing enemy health and quantity, this project aims to create meaningful difficulty through:

- Combat puzzles that require adapting to different enemy threats.
- Environmental interactions that reward creativity.
- Resource decisions that influence survival.
- Enemy encounters designed around movement, positioning, and player skill.

The intended player experience is:

> "A desperate struggle for survival that evolves into a carefully earned power fantasy."

---

# Development Goals

This project serves as both a playable game prototype and an exploration of professional gameplay programming practices.

Major development goals include:

- Building modular and maintainable gameplay systems.
- Creating reusable enemy, weapon, and interaction frameworks.
- Designing engaging FPS combat loops.
- Learning Unity's gameplay architecture and optimization workflows.
- Developing systems that can scale from a prototype into a complete survival experience.

---

# Current Features

## Player Controller

Implemented:

- First-person camera system.
- WASD movement.
- Camera-relative movement.
- Sprinting.
- Jumping.
- Custom gravity system.
- Unity Input System integration.

---

## Combat System

Implemented:

- Raycast-based hitscan shooting.
- Crosshair UI.
- Damage system.
- Reusable health component.
- Debug weapon visualization.

The damage architecture is designed around reusable components so that future objects such as enemies, environmental hazards, and interactive objects can share the same damage framework.

---

## Zombie AI System

Implemented:

- Zombie enemy prefab.
- NavMesh-based navigation.
- AI state machine architecture.
- Chase behavior.
- Attack behavior.
- Death behavior.
- Animation integration.
- Sound effects.

Current AI states:

- Idle
- Chasing
- Attacking
- Dead

---

## Horde Survival System

Implemented:

- Round-based zombie spawning.
- Wave progression.
- Zombie spawn management.
- Active zombie limits.
- Difficulty scaling.

Current difficulty scaling affects:

- Zombie quantity.
- Zombie health.
- Zombie movement speed.

---

## User Interface

Implemented:

- Crosshair UI.
- Round counter UI.
- Screen scaling support.

---

# Development Milestones

## Version 0.1.0 — FPS Foundation

Completed:

- First-person controller.
- Player movement.
- Camera controls.
- Unity Input System implementation.

---

## Version 0.2.0 — Combat and Enemy Prototype

Completed:

- Raycast shooting.
- Damage system.
- Health system.
- Zombie enemy prototype.
- NavMesh AI.
- Enemy animations and audio.

---

## Version 0.3.0 — Horde Survival Foundation

Completed:

- Wave manager.
- Zombie spawning.
- Round progression.
- Difficulty scaling.
- Round UI.

The project now contains the fundamental gameplay loop:

- Player
- Combat
- Zombie AI
- Wave Survival
- Increasing Difficulty


---

# Upcoming Development Roadmap

## Version 0.4.0 — FPS Gameplay Expansion

Planned:

- Reload system.
- Ammunition system.
- Ammo UI.
- Weapon recoil.
- Weapon effects.
- Player health.
- Damage feedback.
- Game over flow.

---

## Version 0.5.0 — Weapon and Progression Systems

Planned:

- Weapon framework.
- Multiple weapon types.
- Weapon switching.
- Weapon pickups.
- Currency system.
- Purchasable upgrades.
- Powerups.

---

## Version 0.6.0 — Combat Sandbox

Planned:

- Environmental hazards.
- Interactive map elements.
- Combat puzzles.
- Arena encounters.
- Resource-driven gameplay mechanics.

---

## Version 0.7.0 — Enemy Expansion

Planned:

- Additional zombie types.
- Special enemies.
- Improved AI behaviors.
- Boss encounters.

---

## Version 1.0.0 — Vertical Slice

Target:

A polished playable demo featuring:

- Complete survival loop.
- Multiple weapons.
- Enemy variety.
- Progression systems.
- Environmental gameplay.
- Optimized performance.

---

# Technical Architecture

The project follows a modular component-based architecture using Unity's component system.

Major systems:

Player
├── Input System
├── Movement
├── Camera
└── Health

Combat
├── Weapon System
├── Raycasting
└── Damage Framework

Enemies
├── AI State Machine
├── NavMesh Navigation
├── Animation
└── Health

Game Flow
├── Wave Manager
├── Zombie Spawner
└── Difficulty Scaling

---

# Technologies Used

## Engine

- Unity 3D

## Programming

- C#
- Object-Oriented Programming
- Component-Based Architecture

## Unity Systems

- New Input System
- CharacterController
- NavMesh
- Animator
- Audio System
- UI Canvas System
- Physics Raycasting
- Coroutines

## Development Tools

- Git / GitHub
- Unity Profiler
- Rider / Visual Studio

---

# Installation and Usage

## Requirements

- Unity version: 6.5
- Supported platform: Windows

  
---

## Running the Project

1. Clone the repository: git clone [repository-url]
2. Open Unity Hub.
3. Select: Add Project
4. Choose the cloned project folder.
5. Open the main gameplay scene: Assets/Scenes/DevScene
6. Press: Play


---

# Controls

| Action | Input |
|-|-|
| Move | WASD |
| Look | Mouse |
| Jump | Space |
| Sprint | Left Shift |
| Fire | Left Mouse |

---

# Development Practices

This project uses:

- Feature-based development.
- Version-controlled milestones.
- Modular gameplay systems.
- Incremental prototyping.
- Performance profiling.

Each milestone is developed as a collection of focused features before expanding into larger gameplay systems.

---

# Future Vision

The long-term goal is to create a survival shooter where player mastery determines success.

The game should encourage players to:

- Learn enemy behaviors.
- Manage limited resources.
- Use the environment creatively.
- Take calculated risks.
- Earn increasingly powerful tools.

The final experience should capture the tension of surviving impossible odds while rewarding players with the satisfaction of becoming the threat themselves.

---

# Developer

Rohaan Mansoor

UCF Computer Science Student | Gameplay Programming Portfolio Project
