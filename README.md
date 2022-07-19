<p align="center">
  <img alt="Track Roll title image" src="https://img.itch.zone/aW1nLzk1NDQwMDguZ2lm/315x250%23c/nr27fd.gif">
</p>

# Track Roll

**Track Roll** was developed in a 48-hour period during the GMTK Game Jam 2022 for the theme: "Roll of the Dice".

It is a puzzle game in which the player controls a cube which they roll along a track to reach a goal. The colored faces of the cube must always match the colored tiles along the track, or they must restart the level. Neutral cube-faces and neutral tiles are always safe.

You can play the game on its itch.io page <a href="https://arkfuldodger.itch.io/track-roller">here</a>.

The personal goal set for development of this game was to create something in Unity3D that was modular and extensible, while also delivering a fun/complete game experience by the jam deadline.

The finished game-jam version contains 13 levels, and utilized the following Unity features in its  development:

- **Singleton Managers:** Each level (Unity Scene) contains the same Game Manager, Event Manager, and Level Manager, which implement the singleton pattern and persist from scene to scene
- All game events are handled by the Event Manager, with any script that acts on an event subscribing to it through the Event Manager Instance
- The Level Manager handles all scene transitions, including a Loading Overlay with a Loading bar

- **Public Enums:** The various color types used are assigned through the use of a globally defined TileType enum, to ensure all references to specific types are handled the same way, and can be globally updated as needed

- **Scriptable Objects:** Color Schemes and Cube Maps are both defined on through scriptable objects.
 - Color Schemes associate each of the TileTypes defined in the public enum with the Unity Material applied to those objects.
 - Cube Side Maps assign a TileType (and associated color) to each of the cube's six sides. Instances of this enum are then assigned per level
 
- **Prefabs:** prefabs are implemented for the game's Managers, as well as track tiles (with variants of different colors), the player, the player's cube, and other elements which are utilized more than once and need to be updated from a single location

- **The Player:** The player does not actually use a Unity3D cube primative, but rather implements six separate Quad meshes for each side, so the sides can independently have colors assigned to them and handle unique collision/trigger detection

- **Input:** The game uses Unity's Input System, so the players Input asset can be changed/updated as needed
 - A C# script is generated for the input, so the player controller can programatically subscribe to the input events and handle them through code
 
 
 Game by Noah Reece
 Sound Engineering by David Magadan
