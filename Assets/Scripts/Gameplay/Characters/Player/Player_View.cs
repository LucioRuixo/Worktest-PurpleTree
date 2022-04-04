using UnityEngine;

namespace Worktest_PurpleTree.Gameplay
{
    public class Player_View : MonoBehaviour
    {
        #region Parameters
        const string BMoving = "Moving";
        #endregion

        [Header("References")]
        [SerializeField] Animator animator;

        public bool Moving { set { animator.SetBool(BMoving, value); } }
    }
}