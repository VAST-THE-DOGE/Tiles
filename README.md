> [!IMPORTANT]
> This branch is for the C# version. It is being made and tested on windows.
> Therefore, it is only supported on Windows machines (Windows 10 and 11) with a decent CPU and at least 2GB of ram.
> Tip: If you can run a browser to see this, you can run the game.

# Tiles
![image](https://github.com/user-attachments/assets/7361dca8-24f8-4ea5-b66b-37ebc02e3aa5)
Tiles, a game where you start with nothing and build your own town and manage resources. This is a simulation game I am working on. The game is a 2d pixelated style management simulation where tiles can be upgraded.

# How to Play

**Setup:**
  - Download the latest version.
  - Link: [Go to the latest releases](https://github.com/VAST-THE-DOGE/Tiles/releases)
  - Unzip the folder.
  - Enjoy the game!

**Gameplay:**
  *very wip*
  Normal Gameplay:
      - wip

  Map Creator:
      - wip

# Modding the Game

  **Adding Custom Tiles:**
  If you want to create your own tile for the game.
  
  - open the game folder and navigate to "Data"
  - In the "Data" folder, click on "TileData.json"
  - add a comma"," after the last curly brace "}"
  - Copy and paste the template below in between the last curly brace "}" and the last bracket "]"

  {
    "Name": "tile name here",
    "ResourceChange": [0, 0, 0, 1, 0, 0, -2, 0],
    "Upgrades": [25, -1, -1],
    "Cost1": [0, 0, 0, 0, 0, 0, 500],
    "Cost2": [0, 0, 0, 0, 0, 0, 0],
    "Cost3": [0, 0, 0, 0, 0, 0, 0],
    "NearTileEffects": {"0":[0, 0, 0, 0, 0, 0, 0]},
    "Frames": 1,
    "SoundID": -1,
    "ResearchIDs": []
  }

  - Now edit any fields to create the tile that you want.
  - After you are done, you need to create a skin pack so the tile has an image.
  - *View "Adding Skin Packs"* to learn how to create a pack.
    
  - Please note that you should be careful when updating the game to keep any custom tiles!
  - Tip: Tile IDs are just the index in the Json. Tile 0 is the first tile in the Json.
  - Tip: a "-1" in upgrades and sound ID tell the game that there is no upgrade or sound ID.
  - Tip: ResearchIDs do not work yet.
  - Tip: Frames should be kept at 1 unless the tile has multiple images for an animation.
  
  **Modifying Tile Data:**
  if you want to change the cost of a tile, the income of a tile, or anything else.

  - instructions are wip. view above instructions for info and edit the tile with the correct name. Do not add a new tile.
  
  **Adding Skin Packs:**
  If you want a custom look in the game.
  
  - not implemented
  
  **Adding Music Packs:**
  If you want custom music in the game.
  
  - not implemented

# Feedback and Reporting Bugs

I have not implemented a way to report bugs yet. 
(Discord messages with testers only for now)

# Timeline

***STATUS: IN DEVELOPMENT***

***VERSION: 0.3.0 / Alpha***

1/3/2024

[Done]{start work on the game}

- [x] create the Github page *[DONE]*
- [ ] create the discord server.
- [x] finish planning *[Done]*
- [x] create the file layout *[Done]*
- [x] work on basic menu systems *[Done]*
  (Main menu, Loading system, exiting system)
- [x] create basic game systems *[Done]*
  (game loop, resource system, map system)
- [x] create the basic tiles *[Done]*
  (art and info for land, sea, and shoreline)
- [x] make a default map for testing. *[Done]*
- [x] make a tile edit system. *[DONE]*

~1/26/2024

[Done]{release a barely working testing version for testers to test 0.1.0}

- [x] make art for more basic tiles. *[Done]*
  (mountains, forest, better shorelines, cliffs, and fields)
- [x] make info for all tiles *[Done]*
- [x] add a system for getting resources over time. *[Done]*
- [x] improve/add the time system. *[Done]*
- [x] add a system for saving worlds. *[Done]*
- [x] add a basic upgrade system with new tiles. *[Done]*

~1/30/2024

*[Done]{release a pre alpha build that has finished basic mechanics 0.2.0}*

- [x] ***[Port to C#]***
  
- [x] look for and fix bugs.
- [x] add more tiles for upgrades. *[Done]*
- [x] start working on music for the game.
- [x] add a system for tiles to affect nearby tiles and give bonuses and whatnot.
- [x] fix a bug with displaying the tile names and the resource values to make it look nicer.
- [x] update the resource system to use a special integer data type to allow for the player to have more resources and to print it out easier than a float or a long.
- [x] add a testing version of the map editor.
- [x] try to optimize the game a bit more.

5/26/2024

**[DONE]{release the fully working alpha 0.3.0}**

- [ ] finish the music and add it to the game.
- [ ] add a market to trade gold for resources and resources for gold.
- [ ] add more tiles and upgrades.
- [ ] add an animation system to the tiles.
- [ ] start balancing the game.
- [ ] add a sytem for tile variants (scraped / won't implement).
- [ ] Add a system for having different skin packs.
- [ ] Remake the image system to load in bulk (one image for all tile icons, one image for all menu icons, and so on).
- [ ] create a settings part of the menu
- [ ] create a map generator that uses a seed to make a random map.
- [ ] Remake the GUIs for better resizing/performance and make them resizable.
- [ ] Add tile construction/upgrade timers and a way to disable tiles.

~8/15/2024

**[5%]{release a beta 0.4.0}**

- [ ] find and fix bugs.
- [ ] try to find better ways to make the functions and optimize more.
- [ ] update art for the tile icons and menu icons (maybe, might not update all the icons).
- [ ] create tile animations.
- [ ] more balancing.
- [ ] Update this README to be more informational.
- [ ] create a research system that will unlock upgrades.
- [ ] Remake the map gui system to fix a bug with very large maps that crashes the game and to fix performance issues on large maps.

~4/15/2025

***[0%]{release the full game (maybe on steam?) 1.0.0}***

- [ ] add a weather system.
- [ ] add an event system.
- [ ] add a fire system.

~2025

**[0%]{the environment update 1.1.0}**

- [ ] add an enemy to the game that attacks from the sea

~2025

**[0%]{the Viking update 1.2.0}**

- [ ] add leaders that impact different aspects of the game. These leaders can be elected from an selection of a few leaders. Each leader will have different buffs and traits.
- [ ] Add laws to the game that impact different things.

~2025

**[0%]{the leadership update 1.3.0}**

- [ ] work on the first DLC that adds a variety of worlds that look different with different tiles and map generation.
- [ ] free worlds would include the default with 2 others (tbd).
- [ ] DLC worlds will include 5 new worlds (tbd).

~2026

**[0%]{the worlds update and DLC 1.4.0}**

- [ ] add a system for trading and declaring war on other AI controlled islands.

~2026

**[0%]{the Diplomacy update 1.5.0}**

(tbd)

~2027

**[0%]{the Multiplayer update 2.0.0}**

(tbd)

~2028

**[0%]{END OF DEVELOPMENT}**
