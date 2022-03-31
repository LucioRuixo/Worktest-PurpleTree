using UnityEngine;
using Worktest_PurpleTree.Utility.Input;

namespace Worktest_PurpleTree.Gameplay
{
    public class Player_Controller : MonoBehaviour, IMoveXY
    {
        [Header("References")]
        [SerializeField] Player_Model model;

        //void Update() => TakeInput();
        //Debug
        void Update()
        {
            TakeInput();

            //Vector2 normal = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
            //Debug.Log((Vector3)normal);
            //float angle = Vector2.Angle(normal, Vector2.up);
            ////Debug.Log("ANGLE: " + angle);
            //Quaternion rotation = normal.x >= 0f ? Quaternion.Euler(0f, 0f, angle) : Quaternion.Euler(0f, 0f, -angle);
            //Debug.DrawLine(Vector2.zero, normal, Color.red);
            //Debug.DrawLine(Vector2.zero, rotation * normal, Color.blue);
        }

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