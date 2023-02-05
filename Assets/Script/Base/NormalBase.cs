using System;
using System.Collections;
using System.Collections.Generic;
using Script.Base;
using UnityEngine;

public class NormalBase : MonoBehaviour
{
    private Resource resource;
    public int genaratedRessource = 10;
    
    
    public BaseState baseState;

    
    public enum BaseState
    {
        growing,
        full,
    }
    
    
    private void Start()
    {
        resource = Resource.Instance;
    }
    
    private void Update()
    {
        switch (baseState)
        {
            case BaseState.growing:
                Growing();
                break;
            case BaseState.full:
                break;
        }
 
    }

    private void Growing()
    {
        
    }
    
    
    private void OnFull()
    {
        
        baseState = BaseState.full;
    }
}
