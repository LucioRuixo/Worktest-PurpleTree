using System;
using UnityEngine;
using Worktest_PurpleTree.Utility.Input;

namespace Worktest_PurpleTree.Gameplay
{
    public class Player_Controller : MonoBehaviour, IMoveXY
    {
        [Header("References")]
        [SerializeField] Player_Model model;

        public static event Action<GameObject> OnCoinGrabbed;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(GameplayManager.CoinTag)) OnCoinGrabbed?.Invoke(collision.gameObject);
        }

        void Update() => TakeInput();

        void ClampPosition()
        {
            Vector2 position = transform.position;
            position.x = Mathf.Clamp(position.x, model.XLimit.Min, model.XLimit.Max);
            transform.position = position;
        }

        #region IMoveXY
        public void TakeInput()
        {
            float xMovement = InputManager.Instance.GetAxisRaw(Axes.Horizontal);
            if (xMovement != 0f) MoveX(xMovement * model.Speed * Time.deltaTime);

            ClampPosition();
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