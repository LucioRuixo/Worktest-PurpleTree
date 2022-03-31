using UnityEngine;
using Worktest_PurpleTree.Utility;

namespace Worktest_PurpleTree.Gameplay.Physics
{
    public class Physics : MonoBehaviour
    {
        #region Physical Properties
        [Header("Physical Properties")]
        [SerializeField] bool kinematic = false;
        [SerializeField] float mass = 1f;
        [SerializeField] float verticalReboundFactor = 0.75f;

        public bool Kinematic { get { return kinematic; } }
        public float Mass { get { return mass; } }
        public float VerticalReboundFactor { get { return verticalReboundFactor; } }
        public Vector2 Velocity { get { return velocity; } }
        #endregion

        #region Gravity Properties
        [Header("Gravity")]
        [SerializeField] bool gravity = true;
        [SerializeField] bool localGravity = false;
        [SerializeField] float localGravityAcceleration = 9.8f;
        [SerializeField] Vector2 localGravityDirection = Vector2.down;

        public bool Gravity { get { return gravity; } }
        public bool LocalGravity { get { return localGravity; } }
        public float LocalGravityAcceleration { get { return localGravityAcceleration; } }
        public Vector2 LocalGravityDirection { get { return localGravityDirection; } }

        public float GlobalGravityAcceleration { get { return physicsManager ? physicsManager.GravityAcceleration : 0f; } }
        public Vector2 GlobalGravityDirection { get { return physicsManager ? physicsManager.GravityDirection : Vector2.zero; } }

        public float GravityAcceleration { get { return localGravity ? localGravityAcceleration : GlobalGravityAcceleration; } }
        public Vector2 GravityDirection { get { return localGravity ? localGravityDirection.normalized : GlobalGravityDirection; } }
        #endregion

        PhysicsManager physicsManager;
        Vector2 velocity = Vector2.zero;

        void Awake() => physicsManager = PhysicsManager.Instance;

        void FixedUpdate()
        {
            if (Kinematic) return;

            if (Gravity) ApplyGravity();
            if (velocity != Vector2.zero) Translate();

            if (transform.position.y < physicsManager.YLimit) Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (kinematic) return;

            Bounce(GetCollisionNormal(collision), collision.GetComponent<Physics>());
        }

        void Translate()
        {
            Vector2 position = transform.position;
            position += velocity;
            transform.position = position;
        }

        #region Collision
        Vector2 GetCollisionNormal(Collider2D collision)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (collision.transform.position - transform.position).normalized);
            return hit.collider ? hit.normal : Vector2.zero;
        }

        void Bounce(Vector2 normal, Physics surfacePhysics)
        {
            velocity = Vector2.Reflect(velocity, normal);

            if (surfacePhysics) velocity = Math.ScaleVectorOnAxis(velocity, normal, surfacePhysics.VerticalReboundFactor);
        }
        #endregion

        #region Forces
        void ApplyAcceleration(float acceleration, Vector2 direction)
        {
            velocity += acceleration * direction.normalized * Time.fixedDeltaTime;

            if (velocity.magnitude > physicsManager.MaxSpeed) velocity = Math.ScaleVectorToLength(velocity, physicsManager.MaxSpeed);
        }

        void ApplyForce(Vector2 force) => ApplyAcceleration(Math.Acceleration(force.magnitude, Mass), force);

        void ApplyGravity() => ApplyAcceleration(GravityAcceleration, GravityDirection);
        #endregion

        public void Accelerate(float acceleration, Vector2 direction) => ApplyAcceleration(acceleration, direction);

        public void Impulse(Vector2 force) => ApplyForce(force);
    }
}