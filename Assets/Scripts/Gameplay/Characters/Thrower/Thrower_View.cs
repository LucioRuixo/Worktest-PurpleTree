using UnityEngine;

namespace Worktest_PurpleTree.Gameplay
{
    public class Thrower_View : MonoBehaviour
    {
        #region Parameters
        const string TThrow = "Throw";
        #endregion

        [Header("References")]
        [SerializeField] Animator animator;

        public void StartThrow() => animator.SetTrigger(TThrow);
    }
}