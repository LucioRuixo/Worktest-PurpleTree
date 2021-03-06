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

        public float XMovement { private set; get; }

        public static event Action<Coin> OnCoinGrabbed;

        void OnEnable() => GameManager.OnGameEnd += OnGameEnd;

        void OnDisable() => GameManager.OnGameEnd -= OnGameEnd;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(GameManager.CoinTag)) OnCoinGrabbed?.Invoke(collision.GetComponent<Coin>());
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

        void OnGameEnd()
        {
            physics.Velocity = Vector2.zero;
            takeInput = false;
        }

        #region IAccelerate
        public void TakeInput()
        {
            if (!takeInput) return;

            XMovement = InputManager.Instance.GetAxisRaw(Axes.Horizontal);
            if (XMovement != 0f)
            {
                Accelerate(XMovement * model.Acceleration * Time.deltaTime, Vector2.right);
                if (!view.Moving) view.Moving = true;
            }
            else
            {
                Accelerate(model.Acceleration * model.FrictionFactor * Time.deltaTime, -physics.Velocity);
                if (view.Moving) view.Moving = false;
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