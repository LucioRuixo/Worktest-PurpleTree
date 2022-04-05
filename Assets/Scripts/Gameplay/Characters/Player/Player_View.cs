using UnityEngine;
using Worktest_PurpleTree.Gameplay.Spawning;

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
        [SerializeField] Spawner dustSpawner;

        [Header("Visual Parameters")]
        [SerializeField] float dustXOffset;

        bool moving = false;

        public bool Moving
        {
            set
            {
                moving = value;

                animator.SetBool(BMoving, value);
                if (moving && controller.XMovement > 0f) dustSpawner.Spawn(new Vector2(transform.position.x + dustXOffset, transform.position.y));
            }

            get { return moving; }
        }
    }
}