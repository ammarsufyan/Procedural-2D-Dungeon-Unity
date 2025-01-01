# Procedural 2D Dungeon in Unity
Using random walk and binary space partitioning to create a 2D procedural dungeon in Unity

## How to:
- Clone the repository
- Open Unity
- Go to assets -> import package -> custom package

![image](https://raw.githubusercontent.com/ammarsufyan/procedural-2d-dungeon-unity/main/Screenshot/ImportPackage.png)

- Import the 00_StarterProjectAssets.unitypackage
### Generators
- Select the generator 
- RandomWalkDungeonGenerator -> Generate only random room without corridor
- CorridorFirstDungeonGenerator -> Generate random room and corridor
- RoomFirstDungeonGenerator -> Generate random room (random walk) and corridor using binary space partition

![image](https://raw.githubusercontent.com/ammarsufyan/procedural-2d-dungeon-unity/main/Screenshot/SelectTheGenerator.png)

- Edit the generator parameter (ex. RoomFirstDungeonGenerator)

![image](https://raw.githubusercontent.com/ammarsufyan/procedural-2d-dungeon-unity/main/Screenshot/EditTheParametersGenerator.png)


### Create a New Parameters (ex. BigDungeon, SmallDungeon, etc)
- Right click Project in Data Folders -> Create -> PCG -> RandomWalkData

![image](https://raw.githubusercontent.com/ammarsufyan/procedural-2d-dungeon-unity/main/Screenshot/CreateParameters.png)

- After the file created, you can change the file name

![image](https://raw.githubusercontent.com/ammarsufyan/procedural-2d-dungeon-unity/main/Screenshot/CreateParameters_3.png)

- Finally, edit the parameters and select randomly each iteration to make a different start position

![image](https://raw.githubusercontent.com/ammarsufyan/procedural-2d-dungeon-unity/main/Screenshot/CreateParameters_2.png)


## Tools:
- Unity 2020.3.25f1 Personal License
- Visual Studio or Visual Studio Code

## Assets:
- https://pixel-poem.itch.io/dungeon-assetpuck

## Results:
### Random Walk Room:
![image](https://raw.githubusercontent.com/ammarsufyan/procedural-2d-dungeon-unity/main/Screenshot/RandomWalk.png)

### Random Walk Room + Corridor:
![image](https://raw.githubusercontent.com/ammarsufyan/procedural-2d-dungeon-unity/main/Screenshot/RandomWalkCorridors.png)

### Binary Space Partitioning:
![image](https://raw.githubusercontent.com/ammarsufyan/procedural-2d-dungeon-unity/main/Screenshot/BinarySpacePartitioning.png)

### Binary Space Partitioning + Random Walk (rooms):
![image](https://raw.githubusercontent.com/ammarsufyan/procedural-2d-dungeon-unity/main/Screenshot/BinarySpacePartitioningAndRandomWalk.png)

## Credit:
Thanks to Sunny Valley Studio (https://www.youtube.com/c/SunnyValleyStudio)
