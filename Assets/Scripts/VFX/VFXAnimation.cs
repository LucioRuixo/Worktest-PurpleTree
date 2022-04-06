using UnityEngine;

namespace Worktest_PurpleTree.VFX
{
    public class VFXAnimation : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Animator animator;

        public Animator Animator { get { return animator; } }

        public void Despawn() => Destroy(gameObject);
    }
}