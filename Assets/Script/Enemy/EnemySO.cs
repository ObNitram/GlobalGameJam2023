using UnityEngine;

namespace Script.Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemy")]
    public class EnemySO : ScriptableObject
    {
        [Min(0)]
        public int maxLife;
        public Sprite sprite;
        public float spokeCollider;
        
        [Range(1,10)] public float speed;
        [Range(0, 10)] public float acceleration;

    }
}
