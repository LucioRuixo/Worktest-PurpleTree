using UnityEngine;
using Worktest_PurpleTree.Utility.Coroutines;

namespace Worktest_PurpleTree.Gameplay
{
    public class Rock : Projectile
    {
        [SerializeField] string floorTag;

        [Header("Rock Properties")]
        [SerializeField] float groundLifeTime = 1f;

        bool despawning = false;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (!despawning && collision.CompareTag(floorTag))
            {
                despawning = true;
                CoroutineManager.Instance.WaitForSeconds(groundLifeTime, () => Destroy(gameObject));
            }
        }
    }
}