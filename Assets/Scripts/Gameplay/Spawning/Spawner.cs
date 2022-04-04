using UnityEngine;

namespace Worktest_PurpleTree.Gameplay.Spawning
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] GameObject prefab;
        [SerializeField] Transform defaultParent;

        public GameObject Prefab { set { prefab = value; } get { return prefab; } }
        public Transform DefaultParent { set { defaultParent = value; } get { return defaultParent; } }

        public GameObject Spawn() => Instantiate(prefab, defaultParent.position, Quaternion.identity, defaultParent);

        public GameObject Spawn(Vector2 position) => Instantiate(prefab, position, Quaternion.identity, defaultParent);

        public GameObject Spawn(Vector2 position, float rotation) => Instantiate(prefab, position, Quaternion.Euler(0f, 0f, rotation), defaultParent);

        public GameObject Spawn(Vector2 position, Transform parent) => Instantiate(prefab, position, Quaternion.identity, parent);

        public GameObject Spawn(Vector2 position, float rotation, Transform parent) => Instantiate(prefab, position, Quaternion.Euler(0f, 0f, rotation), parent);
    }
}