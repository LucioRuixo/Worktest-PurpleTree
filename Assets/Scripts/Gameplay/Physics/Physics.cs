using UnityEngine;
using Worktest_PurpleTree.Utility.Math;

namespace Worktest_PurpleTree.Gameplay.Physics
{
    public class Physics : MonoBehaviour
    {
        #region Physical Properties
        [Header("Physical Properties")]
        [SerializeField] bool kinematic = false;
        [SerializeField] float mass = 1f;
        [SerializeField] float verticalReboundFactor = 0.75f;
        [SerializeField] float frictionFactor = 0.5f;

        public bool Kinematic { get { return kinematic; } }
        public float Mass { get { return mass; } }
        public float VerticalReboundFactor { get { return verticalReboundFactor; } }
        public float FrictionFactor { get { return frictionFactor; } }
        public Vector2 Velocity { set; get; } = Vector2.zero;
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

        public bool ShouldBounce { set; get; } = true;
        
        PhysicsManager physicsManager;
        SurfaceCollisionHandler surfaceCollisionHandler;

        void Awake()
        {
            physicsManager = PhysicsManager.Instance;
            surfaceCollisionHandler = new SurfaceCollisionHandler(transform, GetComponent<Collider2D>(), this);
        }

        void FixedUpdate()
        {
            if (kinematic) return;

            if (Gravity) ApplyGravity();
            if (surfaceCollisionHandler.State == PhysicalState.OnSurface)
            {
                ApplyNormal();

                if (surfaceCollisionHandler.Surface.hasPhysics) ApplyFriction();
            }

            if (Velocity != Vector2.zero) Translate();
            if (transform.position.y < physicsManager.YLimit) Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (kinematic) return;

            if (ShouldBounce)
            {
                Vector2 collisionNormal = surfaceCollisionHandler.GetCollisionNormal(collision);
                Physics collisionPhysics = collision.GetComponent<Physics>();
                Bounce(collisionNormal, collisionPhysics);
            }
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            if (kinematic) return;

            surfaceCollisionHandler.HandleCollisionStay(collision);
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (kinematic) return;

            surfaceCollisionHandler.HandleCollisionExit(collision);
        }

        #region Movement
        void Translate()
        {
            Vector2 position = transform.position;
            position += Velocity * Time.deltaTime;
            transform.position = position;
        }

        void Bounce(Vector2 normal, Physics surfacePhysics)
        {
            Velocity = Vector2.Reflect(Velocity, normal);

            if (surfacePhysics) Velocity = VectorMath.ScaleVectorOnAxis(Velocity, normal, surfacePhysics.VerticalReboundFactor);
        }
        #endregion

        #region Forces
        void ApplyAcceleration(float acceleration, Vector2 direction)
        {
            if (acceleration == 0f || direction == Vector2.zero) return;

            Velocity += acceleration * direction.normalized * Time.fixedDeltaTime;
            if (Velocity.magnitude > physicsManager.MaxSpeed) Velocity = VectorMath.ScaleVectorToLength(Velocity, physicsManager.MaxSpeed);
        }

        void ApplyForce(Vector2 force) => ApplyAcceleration(ForceMath.Acceleration(force.magnitude, Mass), force);

        void ApplyGravity() => ApplyAcceleration(GravityAcceleration, GravityDirection);

        void ApplyNormal() => ApplyAcceleration(GravityAcceleration, surfaceCollisionHandler.Surface.normal);

        void ApplyFriction() => ApplyAcceleration(Velocity.magnitude * surfaceCollisionHandler.Surface.frictionFactor, -Velocity);
        #endregion

        public void Accelerate(float acceleration, Vector2 direction) => ApplyAcceleration(acceleration, direction);

        public void Impulse(Vector2 force) => ApplyForce(force);
    }
}