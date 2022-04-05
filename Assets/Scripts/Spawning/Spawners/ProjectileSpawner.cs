using UnityEngine;
using Worktest_PurpleTree.Gameplay;

namespace Worktest_PurpleTree.Spawning
{
    public class ProjectileSpawner : Spawner
    {
        public Projectile SpawnWithImpulse(Vector2 force)
        {
            if (Spawn().TryGetComponent(out Projectile projectile))
            {
                projectile._Physics.Impulse(force);
            
                return projectile;
            }
            else
            {
                Debug.LogError("Can not spawn projectile: prefab does not have a Projectile component. Returning null.");
            
                return null;
            }
        }
    }
}