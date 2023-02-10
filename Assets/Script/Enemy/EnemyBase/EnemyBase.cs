using System;
using System.Collections;
using System.Collections.Generic;
using Script.Base;
using Script.Caracter.Enemy;
using Script.Enemy;
using Script.Interface;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour, IAttackable
{
    
    
    public static List<EnemyBase> enemyBases = new List<EnemyBase>();

    private int lifeMax = 800;
    private int currentLife = 100;

    [SerializeField] private int numberOfEnemyReady;
    [SerializeField] private int numberOfChampiReady;

    [Range(1, 100)] public int speedGenerationOfScarab = 10;
    [Range(1, 100)] public int speedGenerationOfChampi = 10;
    [Range(1, 100)] public int speedOfLaunch = 10;


    Unity.Mathematics.Random random;

    public ScarabEnemy scarabScarabEnemy;
    public ChampiEnemy ChampiEnemy;

    // Start is called before the first frame update
    void Start()
    {
        EnemyBase.enemyBases.Add(this);

        random = new Unity.Mathematics.Random();
        random.InitState((uint)(PlayTime.random.NextInt(1, 100) + PlayTime.totalTime));
        currentLife = lifeMax;
        StartCoroutine(SumonEnemy());
    }


    public void Damage(int damage)
    {
        currentLife -= damage;
        if (currentLife <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        StopCoroutine(SumonEnemy());

        EnemyBase.enemyBases.Remove(this);
        if(EnemyBase.enemyBases.Count == 0)
        {
            PlayTime.instance.EndGameWin();
        }

        Destroy(gameObject);
    }


    //coroutine daugmentation de la lumiere
    private IEnumerator SumonEnemy()
    {
        while (true)
        {
            if (random.NextInt(0, 100) < speedGenerationOfScarab + PlayTime.totalTime/4)
            {
                numberOfEnemyReady++;
            }

            if (random.NextInt(0, 100) < speedGenerationOfChampi + PlayTime.totalTime/4)
            {
                numberOfChampiReady++;
            }

            if (random.NextInt(0, 100) < speedOfLaunch + PlayTime.totalTime/10)
            {
                SumonEnemyReady();
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void SumonEnemyReady()
    {
        for (int i = 0; i < numberOfEnemyReady; i++)
        {
            ScarabEnemy scarabEnemyIa = Instantiate(scarabScarabEnemy, transform.position, Quaternion.identity);
            scarabEnemyIa.transform.parent = transform;
        }

        for (int i = 0; i < numberOfChampiReady; i++)
        {
            ChampiEnemy enemyIA = Instantiate(ChampiEnemy, transform.position, Quaternion.identity);
            enemyIA.transform.parent = transform;
        }

        numberOfEnemyReady = 0;
        numberOfChampiReady = 0;
    }
}