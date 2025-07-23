# Object Pooling

This project is a Unity demonstration showcasing a robust, high-performance object pooling system and other professional software design patterns. It serves as a practical example of how to build scalable and maintainable game systems by separating concerns and decoupling logic. The primary demo features an automated turret that uses a sophisticated targeting system to engage enemies, all managed through the object pooler and a central object registry.

![Timeline 4](https://github.com/user-attachments/assets/26bb879c-1224-44a7-9460-52d22b924c90)


## Features

 - Object Pooling: Efficiently reuses GameObjects (like bullets and enemies) to minimize garbage collection spikes and improve overall performance.

 - Service Locator Pattern: A central `ActiveObjectRegistry` tracks all active, trackable entities in the scene for high-performance querying, completely avoiding slow Find operations during gameplay.

 - ScriptableObject-based Stats: Enemy statistics (health, speed, color, etc.) are managed via `ScriptableObject` assets. This decouples game data from logic, allowing designers to create and balance new enemy types without writing any code.

 - Interface-Driven Design: Core systems are decoupled using interfaces. For example, `IDamageable` allows any entity to receive damage, and `IPoolable` provides a contract for objects managed by the pooling system.

 - Separation of Concerns: Logic is broken down into modular, single-responsibility classes (`TargetingSystem`, `RotateSelectedPoint`, `HealthSystem`), making the code easier to understand, debug, and extend.

 - Benchmark Scene: Includes a dedicated scene to visually compare the significant performance difference between object pooling and the standard `Instantiate`/`Destroy` methods.

## Core Architectural Concepts

This project was built with several key architectural principles in mind to ensure it is robust, performant, and scalable.


## Data-Driven Design with ScriptableObjects

The `EnemyStats.cs` ScriptableObject allows for creating an unlimited number of enemy variations directly in the Unity Editor. The `StatChanger.cs` service reads these data assets and applies the stats to enemy instances at runtime.


## Installation
1. Clone the repository: 
   ```
   git clone https://github.com/EmreBoztas/enemy-spawner-with-object-pooling 
   ```
2. Open the project in Unity `6000.0.51.f1` or a newer version for optimal compatibility.


## Usage
- Open the project in Unity.  
- Run the game and start testing.



## Performance Test

The key benefit of Object Pooling is the significant reduction in the cost of creating and destroying objects. The benchmark scene was used to measure the average execution time for spawning 2500 entities using both standard and pooled methods.

<img width="906" height="509" alt="430094466-af4a5fbb-584d-4d43-9dca-381bf11ce600" src="https://github.com/user-attachments/assets/f65851f7-a9f6-498d-a4fb-8a3f3d4a8943" />



| Operation (2500 entities) | Standard Method (Instantiate/Destroy) | Object Pooling (Enable/Disable) |
| :--- | :---: | :---: |
| **Spawning Objects** | `~65 ms` | `~14 ms` |
| **Removing Objects** | `~14 ms` | `~15 ms` |
| **Total Time ** | `~79 ms` | `~29 ms` |



As the data shows, when considering the full lifecycle of an object (creation and removal), the Object Pooling pattern is significantly faster (~2.7 times) than the standard method of using `Instantiate` and `Destroy`. This approach virtually eliminates performance spikes during gameplay.




## Test It Online

If you’d like to try it out without downloading, you can test the project directly on  Itch.io:

🔗 https://emreboztas.itch.io/object-pooling
