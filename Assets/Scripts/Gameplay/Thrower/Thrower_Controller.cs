using UnityEngine;
using Worktest_PurpleTree.Utility.Coroutines;

namespace Worktest_PurpleTree.Gameplay
{
    public class Thrower_Controller : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Thrower_Model model;

        [Header("Projectile Spawning")]
        [SerializeField] GameObject projectilePrefab;
        [SerializeField] Transform projectileContainer;

        public Transform ProjectileContainer { get { return projectileContainer; } }

        CoroutineManager coroutineManager;

        bool throwing = false;
        int throwCoroutineID = -1;

        void Awake() => coroutineManager = CoroutineManager.Instance;

        void Start() => StartThrowing();

        void StartThrowing()
        {
            if (throwing) return;

            throwCoroutineID = coroutineManager.WaitForSeconds(model.ThrowInterval, SpawnProjectile, true);
            throwing = true;
        }

        void StopThrowing()
        {
            if (!throwing) return;

            coroutineManager.StopCoroutine(throwCoroutineID);
            throwCoroutineID = -1;
            throwing = false;
        }

        void SpawnProjectile()
        {
            Vector2 position = projectileContainer.transform.position;
            Projectile projectile = Instantiate(projectilePrefab, position, Quaternion.identity, projectileContainer).GetComponent<Projectile>();

            float angle = Random.Range(-(model.ThrowConeRadius / 2f), model.ThrowConeRadius / 2f);
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            projectile._Physics.Impulse((rotation * model.ThrowConeDirection) * model.ThrowForce);
        }
    }
}