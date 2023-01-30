using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObject/Weapon")]
public class WeaponSO : ScriptableObject
{
   
   [Range(0,60)]
   public float speed;
   public int damage;
   public float fireRate;
   [Range (0,10)]
   public float range;
   [Range(0,90)]
   public float precision;
   
}

