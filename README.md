# Arcade Hub

A desktop arcade platform built in C# with MonoGame — five classic games (Space Invaders, Tetris, Snake, Pac-Man, and Icy Tower) running on a single shared game engine, with a dashboard, persistent leaderboard, and Facebook login.

## Highlights

- **Custom reusable game engine** (`Infrastructure`) — a general-purpose MonoGame framework with its own screen stack, component hierarchy, sprite/animation system, and input/sound/collision managers, kept fully decoupled from any one game.
- **Five playable games** built on top of that engine, sharing rendering, input, audio, and collision code instead of duplicating it per game.
- **Stack-based screen manager** supporting modal and overlay screens (pause menus, settings, transitions) without each game needing its own navigation logic.
- **Composable sprite animation system** — animators (cell/sheet, fade, blink, pulse, shrink, rotate, waypoints, sequential) can be chained together via a `CompositeAnimator`.
- **Persistent leaderboard** backed by SQLite (`Microsoft.Data.Sqlite`), with per-player name entry and score/level tracking.
- **Facebook Login** integration with an accompanying privacy policy and data-deletion flow.
- Single-player and local two-player modes, configurable sound/volume settings, and keyboard-driven menus throughout.

## Games

| Game | Description |
|---|---|
| **Space Invaders** | Classic shoot-'em-up with 4 levels, a roaming mothership, 3 degradable barriers, and single/two-player modes with shared lives. |
| **Tetris** | Falling-block puzzle with a standard piece bag and line clearing. |
| **Snake** | Grow-and-avoid classic with food spawning. |
| **Pac-Man** | Maze chase with ghost AI. |
| **Icy Tower** | Platformer-style vertical climbing game. |

## Architecture

The solution is split into two projects:

- **Infrastructure** — a reusable game framework (compiled to a DLL). Contains the base `Game` classes, the four core singleton managers (`InputManager`, `CollisionsManager`, `SoundsManager`, `ScreensMananger`), and the full object model (screens, sprites, animators).
- **ArcadeHub** — the game executable. References `Infrastructure` and contains all game-specific logic: the dashboard/game picker, each game's screens and entities, and the `ArcadeManager` that wires everything together.

```
Program.cs → ArcadeManager (BaseGame) → ScreensMananger → GamePickerScreen → individual game screens
```

### Object model

```
IGameComponent
└── GameComponent (MonoGame)
    └── RegisteredComponent        (auto-registers with Game.Services)
        └── GameService
    └── DrawableGameComponent
        └── LoadableDrawableComponent    (manages ContentManager)
            └── Component2D              (Position, Rotation, Scale)
                └── Sprite                (texture, color, animations)
                    └── game sprites (Ship, Enemy, Bullet, Barrier, Ghost, Tetromino, ...)
        └── CompositeDrawableComponent<T>
            └── GameScreen
                └── MenuScreen           (keyboard-navigable menu list)
                └── concrete screens (WelcomeScreen, PlayScreen, GameOverScreen, LeaderboardScreen, ...)
```

Each game (Tetris, Snake, Pac-Man, Icy Tower) lives in its own folder under `ArcadeHub/`, built from the same base sprite/screen/animator primitives as Space Invaders.

## Tech Stack

- **C# / .NET 8**
- **MonoGame** (DesktopGL)
- **SQLite** via `Microsoft.Data.Sqlite` for score persistence
- Facebook OAuth for social login

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MonoGame SDK](https://www.monogame.net/downloads/)

### Run

```bash
git clone https://github.com/Avital-Fine/arcade-hub.git
cd arcade-hub
dotnet run --project ArcadeHub
```

### Build

```bash
dotnet build                  # build everything
dotnet build Infrastructure   # build just the framework
dotnet build ArcadeHub        # build just the game
```

## Controls

### Gameplay (Space Invaders)

| Action | Player 1 | Player 2 |
|---|---|---|
| Move Left | ← | A |
| Move Right | → | D |
| Shoot | Space | K |
| Pause | P | P |
| Mute Sound | M | M |

### Menus (all games)

| Action | Keys |
|---|---|
| Navigate | ↑ / ↓ |
| Select | Enter |
| Back / Exit | Escape |

## Project Structure

```
arcade-hub/
├── ArcadeHub/              # Game executable
│   ├── Screens/            # Dashboard, menus, shared play-flow screens
│   ├── Managers/           # ArcadeManager, PlayersManager
│   ├── PacMan/ Snake/ Tetris/ IcyTower/   # Per-game logic
│   ├── Sprites/            # Space Invaders sprites
│   └── Content/            # Game assets
├── Infrastructure/         # Reusable game framework
│   ├── Managers/           # Input, Sounds, Collisions, Scores
│   ├── ObjectModel/        # Screens, Sprites, Animators
│   └── ServiceInterfaces/
├── ArcadeHub.sln
└── README.md
```

## Data & Privacy

Score data is stored locally in a SQLite database (`scores.db`). See [PRIVACY_POLICY.md](PRIVACY_POLICY.md) and [DATA_DELETION.md](DATA_DELETION.md) for details on the Facebook Login integration.
