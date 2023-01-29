using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D.velocity = transform.right * speed;
        Destroy(gameObject, 5f);
    }
    
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        //Destroy(gameObject);
    }

   
}
