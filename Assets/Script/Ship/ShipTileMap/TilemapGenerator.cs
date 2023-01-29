using UnityEngine;
using UnityEngine.Tilemaps;

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
            for (int x = 0; x < _shipSize.x; x++)
            {
                for (int y = 0; y < _shipSize.y; y++)
                {
                    Vector3Int position = new Vector3Int(x-(_shipSize.x/2), y-(_shipSize.y/2));
                    _terrainTilemap.SetTile(  position, _terrainTile);
                }
            }
            
            
            for(int x = 0; x < _shipSize.x; x++)
            {
                Vector3Int position = new Vector3Int(x-(_shipSize.x/2), -(_shipSize.y/2)-1);
                _wallTilemap.SetTile(  position, _wallTile);
                
                position = new Vector3Int(x-(_shipSize.x/2), (_shipSize.y/2));
                _wallTilemap.SetTile(  position, _wallTile);
            }
            
            for(int y = 0; y < _shipSize.y; y++)
            {
                Vector3Int position = new Vector3Int(-(_shipSize.x/2)-1, y-(_shipSize.y/2));
                _wallTilemap.SetTile(  position, _wallTile);
                
                position = new Vector3Int((_shipSize.x/2), y-(_shipSize.y/2));
                _wallTilemap.SetTile(  position, _wallTile);
            }
            
            Debug.Log("Tilemap generated");
            
        }
    }
}