using UnityEngine;

namespace Script.Weapon
{
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
      [Range(1,10)]
      public int numberOfBullets;
   
      public Sprite sprite;
   
   }
}

