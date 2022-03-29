using UnityEngine;
using Worktest_PurpleTree.Utility.Physics;

namespace Worktest_PurpleTree.Gameplay.Physics
{
    public class Physics : MonoBehaviour
    {
        #region Physical Properties
        public float Mass { set; get; } = 1f;
        public Vector2 Velocity { get { return velocity; } }
        #endregion

        #region Gravity Properties
        public bool Gravity { set; get; } = true;
        public bool LocalGravity { set; get; } = false;

        public float LocalGravityAcceleration { set; get; } = 9.8f;
        public Vector2 LocalGravityDirection { set; get; } = Vector2.down;

        public float GlobalGravityAcceleration { get { return physicsManager ? physicsManager.GravityAcceleration : 0f; } }
        public Vector2 GlobalGravityDirection { get { return physicsManager ? physicsManager.GravityDirection : Vector2.zero; } }

        public float GravityAcceleration { get { return LocalGravity ? LocalGravityAcceleration : GlobalGravityAcceleration; } }
        public Vector2 GravityDirection { get { return LocalGravity ? LocalGravityDirection : GlobalGravityDirection; } }
        #endregion

        PhysicsManager physicsManager;
        Vector2 velocity = Vector2.zero;

        void Awake() => physicsManager = PhysicsManager.Instance;

        void FixedUpdate()
        {
            if (Gravity) ApplyGravity();

            if (velocity != Vector2.zero) Translate();
        }

        void ApplyAcceleration(float acceleration, Vector2 direction) => velocity += acceleration * direction * Time.fixedDeltaTime;

        void ApplyForce(Vector2 force) => ApplyAcceleration(PhysicsMath.Acceleration(force.magnitude, Mass), force);

        void ApplyGravity() => ApplyAcceleration(GravityAcceleration, GravityDirection);

        void Translate()
        {
            Vector2 position = transform.position;
            position += velocity;
            transform.position = position;
        }

        public void Accelerate(float acceleration, Vector2 direction) => ApplyAcceleration(acceleration, direction);

        public void Impulse(Vector2 force) => ApplyForce(force);
    }
}