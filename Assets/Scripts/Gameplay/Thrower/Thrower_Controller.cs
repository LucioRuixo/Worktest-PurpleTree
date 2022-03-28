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

        bool throwing = true;
        int throwCoroutineID = -1;

        void Start() => throwCoroutineID = CoroutineManager.Instance.WaitForSeconds(model.LaunchInterval, SpawnProjectile, true);

        void SpawnProjectile()
        {
            Vector2 position = projectileContainer.transform.position;
            Instantiate(projectilePrefab, position, Quaternion.identity, projectileContainer);
        }
    }
}