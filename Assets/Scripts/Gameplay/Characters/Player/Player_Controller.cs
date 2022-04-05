using System;
using UnityEngine;
using Worktest_PurpleTree.Utility.Input;
using Worktest_PurpleTree.Utility.Math;

namespace Worktest_PurpleTree.Gameplay
{
    public class Player_Controller : MonoBehaviour, IAccelerate
    {
        [Header("References")]
        [SerializeField] Player_Model model;
        [SerializeField] Player_View view;
        [Space]
        [SerializeField] Physics.Physics physics;

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

            if (position.x < model.XLimit.Min || position.x > model.XLimit.Max)
            {
                physics.Velocity = Vector2.zero;
                position.x = Mathf.Clamp(position.x, model.XLimit.Min, model.XLimit.Max);
            }

            transform.position = position;
        }

        #region IAccelerate
        public void TakeInput()
        {
            if (!takeInput) return;

            float xMovement = InputManager.Instance.GetAxisRaw(Axes.Horizontal);
            if (xMovement != 0f)
            {
                Accelerate(xMovement * model.Acceleration * Time.deltaTime, Vector2.right);
                view.Moving = true;
            }
            else
            {
                Accelerate(model.Acceleration * model.FrictionFactor * Time.deltaTime, -physics.Velocity);
                view.Moving = false;
            }

            ClampPosition();
        }

        public void Accelerate(float acceleration, Vector2 direction)
        {
            physics.Accelerate(acceleration, direction);
            if (physics.Velocity.magnitude > model.MaxSpeed) physics.Velocity = VectorMath.ScaleVectorToLength(physics.Velocity, model.MaxSpeed);
        }

        //public void MoveX(float xMovement)
        //{
        //    Vector3 position = transform.position;
        //    position.x += xMovement;
        //    transform.position = position;
        //}

        //public void MoveY(float yMovement) => Debug.LogWarning("Player does not move in Y axis.");
        #endregion
    }
}