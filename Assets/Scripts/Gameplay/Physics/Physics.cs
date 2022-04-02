using System.Collections.Generic;
using UnityEngine;
using Worktest_PurpleTree.Utility;

namespace Worktest_PurpleTree.Gameplay.Physics
{
    public class Physics : MonoBehaviour
    {
        #region Enums & Structs
        enum State
        {
            InAir,
            OnSurface
        }

        struct Surface
        {
            public Vector2 normal;

            public float frictionFactor;
            public bool hasPhysics;

            public Surface(Vector2 normal, float frictionFactor)
            {
                this.normal = normal;

                this.frictionFactor = frictionFactor;
                hasPhysics = true;
            }

            public Surface(Vector2 normal)
            {
                this.normal = normal;

                frictionFactor = -1f;
                hasPhysics = false;
            }

            public void Reset()
            {
                normal = Vector2.zero;

                frictionFactor = -1f;
                hasPhysics = false;
            }
        }
        #endregion

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

        State state = State.InAir;
        Surface surface;

        new Collider2D collider;
        Dictionary<Collider2D, int> activeCollisions = new Dictionary<Collider2D, int>(); // collider, frames in collision

        PhysicsManager physicsManager;

        Vector2 velocity = Vector2.zero;

        void Awake()
        {
            collider = GetComponent<Collider2D>();
            physicsManager = PhysicsManager.Instance;
        }

        void FixedUpdate()
        {
            if (kinematic) return;

            if (velocity != Vector2.zero) Translate();
            if (transform.position.y < physicsManager.YLimit) Destroy(gameObject);

            if (Gravity) ApplyGravity();
            if (state == State.OnSurface)
            {
                ApplyNormal();

                if (surface.hasPhysics) ApplyFriction();
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (kinematic) return;

            GetCollisionInfo(collision, out Vector2 point, out Vector2 normal);
            Bounce(normal, collision.GetComponent<Physics>());
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            if (kinematic) return;

            ProcessCollisionStay(collision);
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (kinematic) return;

            ProcessCollisionExit(collision);
        }

        void Translate()
        {
            Vector2 position = transform.position;
            position += velocity;
            transform.position = position;
        }

        #region Collision
        void GetCollisionInfo(Collider2D collision, out Vector2 point, out Vector2 normal)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (collision.transform.position - transform.position).normalized);

            point = hit.point;
            normal = hit.normal;
        }

        void ProcessCollisionStay(Collider2D collision)
        {
            if (!activeCollisions.ContainsKey(collision)) activeCollisions.Add(collision, 1);
            else activeCollisions[collision]++;

            if (activeCollisions[collision] >= physicsManager.CollisionStayThreshold)
            {
                GetCollisionInfo(collision, out Vector2 collisionPoint, out Vector2 collisionNormal);
                Physics collisionPhysics = collision.GetComponent<Physics>();

                Bounce(collisionNormal, collisionPhysics);

                state = State.OnSurface;
                if (collisionPhysics) surface = new Surface(collisionNormal, collisionPhysics.frictionFactor);
                else surface = new Surface(collisionNormal);
            }
        }

        void ProcessCollisionExit(Collider2D collision)
        {
            if (activeCollisions.ContainsKey(collision)) activeCollisions.Remove(collision);

            if (activeCollisions.Count == 0)
            {
                state = State.InAir;
                surface.Reset();
            }
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
            if (acceleration == 0f || direction == Vector2.zero) return;

            velocity += acceleration * direction.normalized * Time.fixedDeltaTime;
            if (velocity.magnitude > physicsManager.MaxSpeed) velocity = Math.ScaleVectorToLength(velocity, physicsManager.MaxSpeed);
        }

        void ApplyForce(Vector2 force) => ApplyAcceleration(Math.Acceleration(force.magnitude, Mass), force);

        void ApplyGravity() => ApplyAcceleration(GravityAcceleration, GravityDirection);

        void ApplyNormal() => ApplyAcceleration(GravityAcceleration, surface.normal);

        void ApplyFriction() => ApplyAcceleration(velocity.magnitude * surface.frictionFactor, -velocity);
        #endregion

        public void Accelerate(float acceleration, Vector2 direction) => ApplyAcceleration(acceleration, direction);

        public void Impulse(Vector2 force) => ApplyForce(force);
    }
}