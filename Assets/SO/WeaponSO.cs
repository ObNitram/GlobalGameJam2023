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
      public float distanceDeVue;
      [Range(1,10)]
      public int numberOfBullets;

      [Range(0,1)]
      public float idlAiming;
      [Range(0,1)]
      public float walkAiming;

      [Min(0)]
      public int penetration;
      
      
      public Sprite sprite;
   
   }
}

