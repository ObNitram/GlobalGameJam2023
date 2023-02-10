using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tpToPlayer : MonoBehaviour
{
    public Transform targetTransform;
    private void OnEnable()
    {
        var position = targetTransform.position;
        position.z = -1f;
        transform.position = position;
    }
}
