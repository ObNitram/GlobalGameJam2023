using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "New scarabScarabEnemy", menuName = "ScriptableObject/SpriteAnimator")]

    public class SC_SpriteAnimator : ScriptableObject
    {
        public Sprite[] spritesIdle;
        public Sprite[] spritesIdleBack;
        public Sprite[] spritesWalk;
        public Sprite[] spritesWalkBack;
        
        [Range(0,1)]
        public float frameRate;
    }
}

