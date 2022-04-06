using System;
using UnityEngine;
using Worktest_PurpleTree.Spawning;
using Worktest_PurpleTree.Utility;
using Worktest_PurpleTree.Utility.Coroutines;

namespace Worktest_PurpleTree.VFX
{
    public class VFXManager : PersistentMonoBehaviourSingleton<VFXManager>
    {
        [Header("References")]
        [SerializeField] VFXAnimationSpawner animationSpawner;

        #region Lerp
        void SetPosition(Transform transform, Vector2 position) => transform.position = position;

        void SetAnchoredPosition(RectTransform transform, Vector2 position) => transform.anchoredPosition = position;

        public void LerpPosition(Transform transform, Vector2 a, Vector2 b, float duration, Action onLerpEnd)
        {
            Action<Vector2> onLerpLoop = (Vector2 lerpPosition) => SetPosition(transform, lerpPosition);

            CoroutineManager.Instance.Lerp(a, b, duration, onLerpLoop, onLerpEnd);
        }

        public void LerpPosition(RectTransform transform, Vector2 a, Vector2 b, float duration, Action onLerpEnd)
        {
            Action<Vector2> onLerpLoop = (Vector2 lerpPosition) => SetAnchoredPosition(transform, lerpPosition);

            CoroutineManager.Instance.Lerp(a, b, duration, onLerpLoop, onLerpEnd);
        }
        #endregion

        #region Animation
        public VFXAnimation SpawnAnimation(GameObject prefab, RuntimeAnimatorController controller)
        {
            animationSpawner.Prefab = prefab;

            return animationSpawner.Spawn(controller);
        }

        public VFXAnimation SpawnAnimation(GameObject prefab, Vector2 position, RuntimeAnimatorController controller)
        {
            animationSpawner.Prefab = prefab;

            return animationSpawner.Spawn(position, controller);
        }
        #endregion
    }
}