using UnityEngine;
using Worktest_PurpleTree.VFX;

namespace Worktest_PurpleTree.Spawning
{
    public class VFXAnimationSpawner : Spawner
    {
        GameObject prefab;

        new public GameObject Prefab
        {
            set
            {
                if (!value.TryGetComponent(out VFXAnimation vfxAnimation))
                {
                    Debug.LogError("VFX animation prefab is not valid: prefab does not have a VFXAnimator component");
                    return;
                }

                prefab = value;
            }

            get { return prefab; }
        }

        #region Overrides
        protected override void Awake() { if (defaultPrefab) Prefab = defaultPrefab; }

        public override GameObject Spawn() => Instantiate(Prefab, defaultParent.position, Quaternion.identity, defaultParent);

        public override GameObject Spawn(Vector2 position) => Instantiate(Prefab, position, Quaternion.identity, defaultParent);

        public override GameObject Spawn(Vector2 position, float rotation) => Instantiate(Prefab, position, Quaternion.Euler(0f, 0f, rotation), defaultParent);

        public override GameObject Spawn(Vector2 position, Transform parent) => Instantiate(Prefab, position, Quaternion.identity, parent);

        public override GameObject Spawn(Vector2 position, float rotation, Transform parent) => Instantiate(Prefab, position, Quaternion.Euler(0f, 0f, rotation), parent);
        #endregion

        public VFXAnimation Spawn(RuntimeAnimatorController controller)
        {
            VFXAnimation animation = Spawn().GetComponent<VFXAnimation>();
            animation.Animator.runtimeAnimatorController = controller;

            return animation;
        }

        public VFXAnimation Spawn(Vector2 position, RuntimeAnimatorController controller)
        {
            VFXAnimation animation = Spawn(position).GetComponent<VFXAnimation>();
            animation.Animator.runtimeAnimatorController = controller;

            return animation;
        }
    }
}