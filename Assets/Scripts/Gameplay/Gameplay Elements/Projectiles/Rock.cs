using System;
using UnityEngine;
using Worktest_PurpleTree.Utility.Coroutines;
using Worktest_PurpleTree.Utility.Math;

namespace Worktest_PurpleTree.Gameplay
{
    public class Rock : Projectile
    {
        [Header("Rock Properties")]
        [SerializeField] float lastReboundDuration = 1.5f;
        [SerializeField] float groundLifetime = 1f;

        bool lastRebound = false;
        bool despawning = false;

        public static event Action<float, float> OnReboundProjected;

        void Start() => ProjectLanding();

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (despawning) return;

            switch (collision.tag)
            {
                case GameplayManager.BoxTag:
                    OnRebound();
                    break;

                case GameplayManager.FloorTag:
                    BeginDespawn();
                    break;

                case GameplayManager.GoalTag:
                    Despawn();
                    break;

                default: break;
            }
        }

        void OnRebound()
        {
            if (!lastRebound) ProjectLanding();
            else BounceToGoal();
        }

        void ProjectLanding()
        {
            float timeToLand = ParabolicThrow.TimeFromY(_Physics.Velocity.y, 0f, _Physics.GravityAcceleration);
            float projectedMovement = _Physics.Velocity.x * timeToLand;
            float projectedX = transform.position.x + projectedMovement;

            if (projectedX > GameplayManager.Instance.LastReboundXThreshold)
            {
                lastRebound = true;
                _Physics.ShouldBounce = false;
            }

            OnReboundProjected?.Invoke(projectedX, timeToLand);
        }

        void BounceToGoal()
        {
            Vector2 goalPosition = GameplayManager.Instance.GoalPosition;
            float xVelocity = (goalPosition.x - transform.position.x) / lastReboundDuration;
            float yVelocity = ParabolicThrow.YVelocityFromYAndTime(transform.position.y - goalPosition.y, _Physics.GravityAcceleration, lastReboundDuration);

            _Physics.Velocity = new Vector2(xVelocity, yVelocity);
        }

        void BeginDespawn()
        {
            despawning = true;
            CoroutineManager.Instance.WaitForSeconds(groundLifetime, Despawn);
        }

        void Despawn() => Destroy(gameObject);
    }
}