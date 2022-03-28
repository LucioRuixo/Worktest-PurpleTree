using UnityEngine;

namespace Worktest_PurpleTree.Gameplay.Physics
{
    public class Physics : MonoBehaviour
    {
        [Header("Object Physical Properties")]
        [SerializeField] bool gravity = true;

        public bool Gravity { get { return gravity; } }

        PhysicsManager physicsManager;

        Vector2 velocity = Vector2.zero;

        void Awake() => physicsManager = PhysicsManager.Instance;

        void FixedUpdate()
        {
            if (gravity) ApplyGravity();

            if (velocity != Vector2.zero) Translate();
        }

        void ApplyGravity() => velocity += physicsManager.GravityDirection * physicsManager.GravityAcceleration * Time.fixedDeltaTime;

        void Translate()
        {
            Vector2 position = transform.position;
            position += velocity;
            transform.position = position;
        }
    }
}