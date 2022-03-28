using UnityEngine;
using Worktest_PurpleTree.Utility.Input;

namespace Worktest_PurpleTree.Gameplay
{
    public class Player_Controller : MonoBehaviour, IMoveXY
    {
        [Header("References")]
        [SerializeField] Player_Model model;

        void Update() => TakeInput();

        #region IMoveXY
        public void TakeInput()
        {
            float xMovement = InputManager.Instance.GetAxisRaw(Axes.Horizontal);
            if (xMovement != 0f) MoveX(xMovement * model.Speed * Time.deltaTime);
        }

        public void MoveX(float xMovement)
        {
            Vector3 position = transform.position;
            position.x += xMovement;
            transform.position = position;
        }

        public void MoveY(float yMovement) => Debug.LogWarning("Player does not move in Y axis.");
        #endregion
    }
}