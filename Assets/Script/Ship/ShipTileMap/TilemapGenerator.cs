using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = Unity.Mathematics.Random;


namespace Script.Ship.ShipTileMap
{
    public class TilemapGenerator : MonoBehaviour
    {
        [SerializeField] private Tilemap _terrainTilemap;
        [SerializeField] private Tilemap _wallTilemap;

        [SerializeField] private Vector2Int _shipSize;

        [SerializeField] private Tile _terrainTile;
        [SerializeField] private Tile _wallTile;
        
        public void GenerateTilemap()
        {
            _terrainTilemap.ClearAllTiles();
            _wallTilemap.ClearAllTiles();

            TilemapData data = new TilemapData(100, 100);
            //data.SpawnRoom();
            
            data.generateWalls(50, 20);
            data.generateWalls(25, 30);
            //data.generateWalls(10, 100);
            for (int x = 0; x < _shipSize.x; x++)
            {
                for (int y = 0; y < _shipSize.y; y++)
                {
                    Vector3Int position = new Vector3Int(x-(_shipSize.x/2), y-(_shipSize.y/2));
                   

                    switch (data.data[x,y])
                    {
                        case 0 :
                            _terrainTilemap.SetTile(position, _terrainTile);
                            break;
                        case 1 :
                            _wallTilemap.SetTile(position, _wallTile);
                            break;
                       

                    }
                    
                    
                }
            }
            
            
            for(int x = 0; x < _shipSize.x; x++)
            {
                Vector3Int position = new Vector3Int(x-(_shipSize.x/2), -(_shipSize.y/2));
                _wallTilemap.SetTile(  position, _wallTile);
                
                position = new Vector3Int(x-(_shipSize.x/2), (_shipSize.y/2));
                _wallTilemap.SetTile(  position, _wallTile);
            }
            
            for(int y = 0; y < _shipSize.y; y++)
            {
                Vector3Int position = new Vector3Int(-(_shipSize.x/2), y-(_shipSize.y/2));
                _wallTilemap.SetTile(  position, _wallTile);
                
                position = new Vector3Int((_shipSize.x/2), y-(_shipSize.y/2));
                _wallTilemap.SetTile(  position, _wallTile);
            }
            
            Debug.Log("Tilemap generated");

        }
    }


    public class TilemapData
    {
        public int[,] data;
        private const int spawnRoomSize = 15;
        private static int _lMapSize = 33;
        private static int _LMapSize = 97;

        private const int FLOOR = 0;
        private const int WALL = 1;
        private const int UNDER_FURNATURE = 2;
        private const int FURNATURE = 3;
        private const int ON_FURNATURE = 4;
        private const int SPAWN_PLATEFORM = 10;
        
        
        private int initX = (_lMapSize - spawnRoomSize) / 2;
        private int initY = ((_LMapSize - spawnRoomSize) / 2) - 1;
        
        
        public TilemapData()
        {
            _lMapSize = 33;
            _LMapSize = 97;
            data = new int[_lMapSize, _LMapSize];

        }
        public TilemapData(int x, int y)
        {
            data = new int[x, y];
            _lMapSize = x;
            _LMapSize = x;

        }

        public void SpawnRoom()
        {
            int centerX = _lMapSize / 2;
            int centerY = _LMapSize / 2;
            
            int doorSize = 5;
            int beginDoorX = initX + (spawnRoomSize - doorSize) / 2;
            int endDoorX = beginDoorX + doorSize;
            
            for (int i = initX; i < initX + spawnRoomSize; i++)
            {
                if (i < beginDoorX || i >= endDoorX)
                {
                    //this.data[i, initY] = WALL;
                    this.data[i, initY + spawnRoomSize] = WALL;
                    this.data[i, initY + 1] = WALL;
                }
                else
                {
                    // here the doors of the spawnRoom 
                    // For this moment, we put only floor
                    this.data[i, initY] = this.data[i, initY + spawnRoomSize] = FLOOR;
                }

                for (int j = initY + 1; j < initY + spawnRoomSize; j++)
                {
                    if ((i >= centerX - 1 && i <= centerX + 1) && (j >= centerY - 1 && j <= centerY + 1)) //is in the spawn space
                    {
                        this.data[i, j] = SPAWN_PLATEFORM;
                        
                    }else if (i == initX || i == initX + spawnRoomSize - 1)
                    {
                        this.data[i, j] = WALL;      //OK
                    }else if (((i == initX + 1 || i == initX + spawnRoomSize - 2) &&
                                (j <= centerY + 2 && j >= centerY - 2)) ||
                              (((i > initX + 1 && i < initX + 3) ||
                               (i < initX + spawnRoomSize - 1 && i > initX + spawnRoomSize - 4)) && (j == centerY + 2 || j == centerY - 2)))
                    {
                        this.data[i, j] = FURNATURE;
                    }
                }
            }
        }

        public void generateWalls(int wallSize, int nbWalls)
        {
            bool bigDoor= true;

            //Random rand = new Random(Convert.ToUInt32(Time.time.ToString()));
            System.Random rand = new System.Random(Time.time.ToString().GetHashCode());
            int randIntX ;
            int randIntY ;
            int nbMaxWall;
            int orientation;
            int count = 0;
            for (int  i = 0;  i < nbWalls;  i++)
            {
                    Debug.Log(rand.Next(1, 7));
                randIntX = rand.Next(1, _lMapSize - 1); 
                randIntY = rand.Next(1, _LMapSize - 1);
                nbMaxWall = rand.Next(1, wallSize);
                
                orientation = rand.Next(1,5);
                count = 0;
                

                if (data[randIntX, randIntY] == 0 && (randIntX > 11 && randIntY > 11) || (randIntY < _LMapSize - 11 && randIntX < _lMapSize - 11))
                {
                    while (randIntX > 0 && randIntY > 0 && randIntX < _lMapSize && randIntY < _LMapSize &&data[randIntX, randIntY] == 0 && count++ < nbMaxWall)
                    {
                        if ((randIntX <= 11 && randIntY <= 11) ||
                              (randIntY >= _LMapSize - 11 && randIntX >= _lMapSize - 11))
                        {
                            orientation = rand.Next(1,5);
                        }
                        switch (orientation)
                        {
                            case 1: //Go up
                                data[randIntX--, randIntY] = WALL;
                                break;
                            case 2: //Go down
                                data[randIntX++, randIntY] = WALL;
                                break;
                            case 3: //Go right
                                data[randIntX, randIntY++] = WALL;
                                break;
                            case 4: //Go left
                                data[randIntX, randIntY--] = WALL; 
                                break;
                        }
                    }

                    //while(data[randIntX, randIntY] == 0 )
                    //data[randIntX, randIntY] = WALL;
                }
            }
            //for (int x = 1; x < _lMapSize - 1; x++)
            //{
            //    for (int y = 1; y < _LMapSize - 1; y++)
            //    {
            //        
            //    }
            //}
        }
    }
}/**/