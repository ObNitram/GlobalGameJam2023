using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foreuse : MonoBehaviour
{

    public float speed = 1f;

    private void Update()
    {
        Vector3 pos = transform.position;
        pos.y -= speed*Time.deltaTime;
        transform.position = pos;
    }
}
