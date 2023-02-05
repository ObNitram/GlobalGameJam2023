using UnityEngine;
using UnityEngine.Serialization;

namespace SO
{
    [CreateAssetMenu(fileName = "New scarabScarabEnemy", menuName = "ScriptableObject/scarabScarabEnemy")]
    public class EnemySO : ScriptableObject
    {
        [Min(0)] public int maxLife;
        public Sprite sprite;
        
        public float spokeCollider;
        public float rangeAttack;
        public float cooldownAttack;
        public int attackDamage;
        
        [Range(1,10)] public float speed;
        
        SC_SpriteAnimator _spriteAnimator;
        
        
        

    }
}
