using System;
using UnityEngine;
using Worktest_PurpleTree.Utility.Input;

namespace Worktest_PurpleTree.Gameplay
{
    public class Player_Controller : MonoBehaviour, IMoveXY
    {
        [Header("References")]
        [SerializeField] Player_Model model;
        [SerializeField] Player_View view;

        bool takeInput = true;

        public static event Action OnCoinGrabbed;

        void OnEnable() => GameplayManager.OnGameEnd += () => takeInput = false;

        void OnDisable() => GameplayManager.OnGameEnd -= () => takeInput = false;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(GameplayManager.CoinTag)) OnCoinGrabbed?.Invoke();
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
            if (!takeInput) return;

            float xMovement = InputManager.Instance.GetAxisRaw(Axes.Horizontal);
            if (xMovement != 0f)
            {
                MoveX(xMovement * model.Speed * Time.deltaTime);
                view.Moving = true;
            }
            else view.Moving = false;

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