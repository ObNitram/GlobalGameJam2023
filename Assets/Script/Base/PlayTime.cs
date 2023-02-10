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
        public static PlayTime instance;

        public static float totalTime;
        public static Random random;

        public static float timeDuration;

        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private GameObject tilemap;
        [SerializeField] private TilemapGenerator _tilemapGenerator;
        [SerializeField] private NavMeshSurface _navigationDrawer;
        [SerializeField] private GridShadowCastersGenerator _gridShadowCastersGenerator;

        [SerializeField] private GameObject player;
        [SerializeField] private Light2D light2D;

        [SerializeField] private GameObject enemy;
        [SerializeField] private GlobalEnemyIA _globalEnemyIA;
        [SerializeField] private GameObject blackScreen;

        [SerializeField] private GameObject _winScreen;
        [SerializeField] private GameObject _loseScreen;

        private void Start()
        {
            PlayTime.instance = this;
            random = new Random();
            random.InitState((uint)Now.Ticks);
            totalTime = 0;
            light2D.intensity = 0;


            player.SetActive(false);
            enemy.SetActive(false);

            _tilemapGenerator.GenerateTilemap();
            _navigationDrawer.BuildNavMesh();
            StartCoroutine(LunchGame());
        }

        private IEnumerator LunchGame()
        {
            yield return new WaitForSeconds(3f);
            player.SetActive(true);
            enemy.SetActive(true);
            _globalEnemyIA.Init();

            _gridShadowCastersGenerator.Generate();
            blackScreen.SetActive(false);
            loadingScreen.SetActive(false);
            StartCoroutine(ActivateLight());
        }

        private IEnumerator ActivateLight()
        {
            while (light2D.intensity < 1)
            {
                light2D.intensity += 0.04f;
                yield return new WaitForSeconds(0.08f);
            }

            _gridShadowCastersGenerator.Generate();
        }


        public void EndGameLose()
        {
            Debug.Log("Lose");
            _loseScreen.SetActive(true);
            StartCoroutine(DesactivateLight());

        }

        public void EndGameWin()
        {
            Debug.Log("Win");
            _winScreen.SetActive(true);
            StartCoroutine(DesactivateLight());

        }
        
        

        private IEnumerator DesactivateLight()
        {
            while (light2D.intensity > 0)
            {
                light2D.intensity -= 0.04f;
                yield return new WaitForSeconds(0.08f);
            }
            blackScreen.SetActive(true);
            player.SetActive(false);
            enemy.SetActive(false);
            tilemap.SetActive(false);
        }

        private void Update()
        {
            totalTime += Time.deltaTime;
        }
    }
}