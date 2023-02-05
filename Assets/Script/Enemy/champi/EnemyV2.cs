using UnityEngine;

namespace Script.Caracter.Enemy
{
    public class EnemyV2 : MonoBehaviour
    {
        
        public enum EnemyState
        {
            GoToPlayer,
            Attack,
        }
        
        public Vector2 _targetLook;
        public Vector2 _targetWay;
        
        
        

    }
}