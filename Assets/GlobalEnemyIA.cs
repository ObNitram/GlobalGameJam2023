using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEnemyIA : MonoBehaviour
{
    
    [SerializeField] private EnemyBase enemyBase;

    

    public void Init()
    {
        EnemyBase enemyBase1 = Instantiate(enemyBase,new Vector3(-45,45,0),Quaternion.identity);
        enemyBase1.transform.parent = transform;
        
        EnemyBase enemyBase2 = Instantiate(enemyBase,new Vector3(45,-45,0),Quaternion.identity);
        enemyBase2.transform.parent = transform;


    }
}
