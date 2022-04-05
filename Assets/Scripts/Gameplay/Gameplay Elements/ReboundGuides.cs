using System.Collections.Generic;
using UnityEngine;
using Worktest_PurpleTree.Gameplay.Spawning;
using Worktest_PurpleTree.Utility.Coroutines;

namespace Worktest_PurpleTree.Gameplay
{
    public class ReboundGuides : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Spawner guideSpawner;

        [Header("Rebound Guide Parameters")]
        [SerializeField] int maxGuides = 5;

        List<GameObject> guides = new List<GameObject>();

        void OnEnable() => Rock.OnReboundProjected += SpawnGuide;

        void OnDisable() => Rock.OnReboundProjected -= SpawnGuide;

        void SpawnGuide(float reboundX, float timeToLand)
        {
            if (guides.Count >= maxGuides) return;

            GameObject guide = guideSpawner.Spawn(new Vector2(reboundX, guideSpawner.DefaultParent.position.y));
            guides.Add(guide);

            CoroutineManager.Instance.WaitForSeconds(timeToLand, () => DespawnGuide(guide));
        }

        void DespawnGuide(GameObject guide)
        {
            guides.Remove(guide);
            Destroy(guide);
        }
    }
}