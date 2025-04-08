[![Monogame][Monogame-icon]][Monogame-url]
# Jump

A 2D platformer built using the Monogame framework, it features both a level editor for creating custom levels, and a level browser for interacting with levels.

## Description

The game is a level-based puzzle platformer where the goal is to reach the exit door, but only after collecting the key. Inspired by the Portal series, gameplay revolves around interacting with different surface types that affect movement. For example, ice reduces friction, sticky blocks prevent jumping, and bouncy blocks launch the player when fallen on from a height.

There are seven object types in total:

- Basic surface (no effect)
- Ice, sticky, and bouncy surfaces
- Spawn point flag (green)
- Key (required to open the door)
- Door (level exit)

All objects can be placed using the built-in level editor, which includes a simple interface for creating custom levels. Objects are selected from a toolbar, placed with left-click, and removed with right-click. Designed levels can be named and saved, then accessed via the level browser. The browser allows levels to be searched for, renamed, edited, deleted, or played.

## Images

### Gameplay
![Level Gameplay][level-gameplay-image]

### Editor
![Level Editor][level-editor-image]

### Browser
![Level Browser][level-browser-image]

[Monogame-url]: https://monogame.net/
[Monogame-icon]: https://img.shields.io/badge/monogame-grey?style=for-the-badge&logo=monogame
[level-browser-image]: Images/Browser.png
[level-editor-image]: Images/Editor.png
[level-gameplay-image]: Images/Gameplay.png
