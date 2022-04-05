using System.Collections.Generic;
using UnityEngine;
using Worktest_PurpleTree.Utility.Math;

namespace Worktest_PurpleTree.Gameplay.Physics
{
    public enum PhysicalState
    {
        InAir,
        OnSurface
    }

    public class SurfaceCollisionHandler
    {
        Transform transform;
        Collider2D collider;
        Physics physics;

        public PhysicalState State { private set; get; } = PhysicalState.InAir;
        public Vector2 SurfaceNormal { private set; get; }

        Dictionary<Collider2D, int> activeCollisions = new Dictionary<Collider2D, int>(); // collider, frames in collision

        public SurfaceCollisionHandler(Transform transform, Collider2D collider, Physics physics)
        {
            this.transform = transform;
            this.collider = collider;
            this.physics = physics;
        }

        public void HandleCollisionStay(Collider2D collision)
        {
            if (!activeCollisions.ContainsKey(collision)) activeCollisions.Add(collision, 1);
            else activeCollisions[collision]++;

            if (activeCollisions[collision] >= PhysicsManager.Instance.CollisionStayThreshold)
            {
                Vector2 collisionNormal = GetCollisionNormal(collision);
                Physics collisionPhysics = collision.GetComponent<Physics>();

                State = PhysicalState.OnSurface;
                SurfaceNormal = collisionNormal;

                physics.Velocity = VectorMath.ScaleVectorOnAxis(physics.Velocity, SurfaceNormal, 0f);
            }
        }

        public void HandleCollisionExit(Collider2D collision)
        {
            if (activeCollisions.ContainsKey(collision)) activeCollisions.Remove(collision);

            if (activeCollisions.Count == 0)
            {
                State = PhysicalState.InAir;
                SurfaceNormal = Vector2.zero;
            }
        }

        public Vector2 GetCollisionNormal(Collider2D collision)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (collision.transform.position - transform.position).normalized);

            return hit.normal;
        }
    }
}