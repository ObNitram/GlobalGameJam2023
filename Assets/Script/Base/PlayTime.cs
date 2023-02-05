using System.Collections;
using NavMeshPlus.Components;
using Script.Ship.ShipTileMap;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = Unity.Mathematics.Random;
using static System.DateTime;

namespace Script.Base
{
    public class PlayTime : MonoBehaviour
    {
    
        public static float totalTime;
        public static Random random;
        
        public static float timeDuration;

        [SerializeField] private GameObject tilemap;
        [SerializeField] private TilemapGenerator _tilemapGenerator;
        [SerializeField] private NavMeshSurface _navigationDrawer;
        [SerializeField] private GameObject player;
        [SerializeField] private Light2D light2D;
        
        [SerializeField] private GameObject enemy;
        [SerializeField] private GlobalEnemyIA _globalEnemyIA;
        
        private void Start()
        {
            random = new Random();
            random.InitState((uint) Now.Ticks);

            totalTime = 0;
            light2D.intensity = 0;
            player.SetActive(false);
            enemy.SetActive(false);
            
            tilemap.SetActive(true);
            
            //_tilemapGenerator.GenerateTilemap();
            
            _navigationDrawer.BuildNavMesh();

            
            
            player.SetActive(true);
            
            enemy.SetActive(true);
            _globalEnemyIA.Init();

            StartCoroutine(ActivateLight());
        
        }
    
        private void Update()
        {
            totalTime += Time.deltaTime;
        }
        
        
        //coroutine daugmentation de la lumiere
        private IEnumerator ActivateLight()
        {
            while (light2D.intensity < 1)
            {
                light2D.intensity += 0.04f;
                yield return new WaitForSeconds(0.08f);
            }
        }
        
        
        public void EndGame()
        {
            _globalEnemyIA.enabled = false;
            player.SetActive(false);
            enemy.SetActive(false);
            StartCoroutine(DesactivateLight());

            
    }
        
        private IEnumerator DesactivateLight()
        {
            while (light2D.intensity > 0)
            {
                light2D.intensity -= 0.04f;
                yield return new WaitForSeconds(0.08f);
            }
        }
        
    }
}
