using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    
    // Start is called before the first frame update
    public void initBullet(WeaponSO weaponSO)
    {
        //_rigidbody2D.velocity = transform.right * weaponSO.speed;
        _rigidbody2D.AddForce(transform.right * weaponSO.speed, ForceMode2D.Impulse);
        Destroy(gameObject, weaponSO.range);
    }
    
    
    
}
