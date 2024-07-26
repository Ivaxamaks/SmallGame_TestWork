# Test Work for position Unity Developer

![Game Demo](Assets\Readme\Gif_Preview)

## Overview

My Awesome Game is a 2D shooter game where the player must defeat waves of enemies. The player can move within a bounded area and shoot automatically at the nearest enemy. The game is built using Unity and features a simple yet engaging gameplay mechanic.

## UML Diagram

Below is the UML diagram representing the main structure and relationships of the game components:

![UML Diagram](Assets\Readme\UML.png)

## Technologies and Design Patterns Used

- **VContainer**: Dependency Injection framework for Unity.
- **EventBus**: Messaging system for decoupled communication between game components.
- **Object Pool**: Efficiently reuse objects like bullets and enemies to optimize performance.
- **Factory Pattern**: Used for creating enemy instances.
- **Finite State Machine (FSM)**: Manages the game's states (BootstrapState, GameRunningState, EndGameState).
- **Model-View-Controller (MVC)**: Architecture pattern to separate concerns within the game.

## Description of the Task

On the screen in portrait orientation, enemies move from top to bottom along three lanes. The player moves within the lower part of the playfield and shoots at enemies to prevent them from reaching the finish line.

### Character Logic

- The character is located at the bottom of the screen, controlled by the player using WASD keys.
- The character's movement is limited by a rectangular area. Left, right, and bottom edges are the screen boundaries, while the top is the finish line.
- The character automatically shoots at the nearest enemy within its range.

### Enemy Logic

- Enemies spawn randomly at one of three spawn points at the top, with a set timeout.
- Enemies move straight down at a set speed.
- When crossing the finish line, the enemy reduces the player's health by 1 and disappears.
- When hit by the character, the enemy's health is reduced by the character's damage amount.
- When the enemy's health reaches 0, it dies.

### Win and Lose Conditions

- The player wins when all enemies are killed.
- The player loses when their health reaches 0.

### Interface

- A health counter is displayed at the top of the screen.
- Upon defeat, a window with the title "Defeat" and a "Restart" button appears, starting the game anew when clicked.
- Upon victory, a window with the title "Victory" and a "Restart" button appears, starting the game anew when clicked.

### Settings

- The number of enemies to defeat for victory (range int); chosen randomly from the range [min, max] at each game start.
- Timeout for enemy spawn (range float); the next enemy spawns after a random number of seconds from the range [min, max].
- Enemy movement speed (range float); each new enemy's speed is randomly chosen from the range [min, max].
- Enemy health (int).
- Character's shooting range (float).
- Character's shooting rate (float).
- Character's bullet damage (int).
- Bullet speed (float).

## Technical Requirements

- Use Unity 2022.2.17f1 strictly.
- The project should be strictly 2D.
- Implement enemy spawning using the "factory" pattern.
- Scene orientation - vertical.
- Completion time: less than 1 week.
- Actual time spent: 16 hours.
- The completed assignment should be uploaded to GitHub.


