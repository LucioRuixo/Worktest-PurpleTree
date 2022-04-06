using UnityEngine;

namespace Worktest_PurpleTree.Spawning
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] protected GameObject defaultPrefab;
        [SerializeField] protected Transform defaultParent;

        public GameObject Prefab { set; get; }
        public Transform DefaultParent { set { defaultParent = value; } get { return defaultParent; } }

        protected virtual void Awake() { if (defaultPrefab) Prefab = defaultPrefab; }

        public virtual GameObject Spawn() => Instantiate(Prefab, defaultParent.position, Quaternion.identity, defaultParent);

        public virtual GameObject Spawn(Vector2 position) => Instantiate(Prefab, position, Quaternion.identity, defaultParent);

        public virtual GameObject Spawn(Vector2 position, float rotation) => Instantiate(Prefab, position, Quaternion.Euler(0f, 0f, rotation), defaultParent);

        public virtual GameObject Spawn(Vector2 position, Transform parent) => Instantiate(Prefab, position, Quaternion.identity, parent);

        public virtual GameObject Spawn(Vector2 position, float rotation, Transform parent) => Instantiate(Prefab, position, Quaternion.Euler(0f, 0f, rotation), parent);
    }
}