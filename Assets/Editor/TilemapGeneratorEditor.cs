
using Script.Ship.ShipTileMap;
using UnityEngine;
using UnityEditor;


namespace Editor
{
    [CustomEditor(typeof(TilemapGenerator))]
    public class TilemapGeneratorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            TilemapGenerator tilemapGenerator = (TilemapGenerator) target;
            if (GUILayout.Button("Generate"))
            {
                tilemapGenerator.GenerateTilemap();
            }
        }
    }
}