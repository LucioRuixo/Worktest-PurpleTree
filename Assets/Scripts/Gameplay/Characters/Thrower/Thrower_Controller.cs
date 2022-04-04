using UnityEngine;
using Worktest_PurpleTree.Gameplay.Spawning;
using Worktest_PurpleTree.Utility.Coroutines;

namespace Worktest_PurpleTree.Gameplay
{
    public class Thrower_Controller : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Thrower_Model model;
        [SerializeField] Thrower_View view;
        [Space]
        [SerializeField] ProjectileSpawner projectileSpawner;

        public Spawner ProjectileSpawner { get { return projectileSpawner; } }

        CoroutineManager coroutineManager;

        bool throwing = false;
        int throwCoroutineID = -1;

        void Awake() => coroutineManager = CoroutineManager.Instance;

        void Start() => StartThrowing();

        void StartThrowing()
        {
            if (throwing) return;

            throwCoroutineID = coroutineManager.WaitForSeconds(model.ThrowInterval, StartThrow, true);
            throwing = true;
        }

        void StopThrowing()
        {
            if (!throwing) return;

            coroutineManager.StopCoroutine(throwCoroutineID);
            throwCoroutineID = -1;
            throwing = false;
        }

        void StartThrow() => view.StartThrow();

        public void ThrowProjectile()
        {
            float angle = Random.Range(-(model.ThrowConeRadius / 2f), model.ThrowConeRadius / 2f);
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Vector2 force = (rotation * model.ThrowConeDirection) * model.ThrowForce;

            projectileSpawner.SpawnWithImpulse(force).GetComponent<Projectile>();
        }
    }
}