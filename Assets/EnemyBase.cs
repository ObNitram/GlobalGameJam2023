using System;
using System.Collections;
using System.Collections.Generic;
using Script.Caracter.Enemy;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [HideInInspector]
    public Enemy[] enemys;

    private void Awake()
    {
        enemys = GetComponentsInChildren<Enemy>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
