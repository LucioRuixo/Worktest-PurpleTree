using UnityEngine;
using Worktest_PurpleTree.VFX;

namespace Worktest_PurpleTree.Gameplay
{
    public class Player_View : MonoBehaviour
    {
        #region Parameters
        const string BMoving = "Moving";
        #endregion

        [Header("References")]
        [SerializeField] Player_Controller controller;
        [Space]
        [SerializeField] Animator animator;
        [SerializeField] Physics.Physics physics;

        [Header("Dust Animation")]
        [SerializeField] GameObject dustPrefab;
        [SerializeField] Vector2 dustOffset;
        [SerializeField] RuntimeAnimatorController dustController;

        bool moving = false;

        public bool Moving
        {
            set
            {
                moving = value;

                animator.SetBool(BMoving, value);

                bool spawnDust = moving && controller.XMovement > 0f && physics.Velocity.x >= 0f;
                if (spawnDust)
                {
                    Vector2 dustPosition = new Vector2(transform.position.x + dustOffset.x, transform.position.y + dustOffset.y);
                    VFXManager.Instance.SpawnAnimation(dustPrefab, dustPosition, dustController);
                }
            }

            get { return moving; }
        }
    }
}