using System.Collections.Generic;
using UnityEngine;

namespace Worktest_PurpleTree.Gameplay.Physics
{
    #region Enums & Structs
    public enum PhysicalState
    {
        InAir,
        OnSurface
    }

    public struct Surface
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

    public class CollisionHandler
    {
        Transform transform;
        Collider2D collider;

        public PhysicalState State { private set; get; } = PhysicalState.InAir;
        public Surface Surface { private set; get; }

        Dictionary<Collider2D, int> activeCollisions = new Dictionary<Collider2D, int>(); // collider, frames in collision

        public CollisionHandler(Transform transform, Collider2D collider)
        {
            this.transform = transform;
            this.collider = collider;
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
                if (collisionPhysics) Surface = new Surface(collisionNormal, collisionPhysics.FrictionFactor);
                else Surface = new Surface(collisionNormal);
            }
        }

        public void HandleCollisionExit(Collider2D collision)
        {
            if (activeCollisions.ContainsKey(collision)) activeCollisions.Remove(collision);

            if (activeCollisions.Count == 0)
            {
                State = PhysicalState.InAir;
                Surface.Reset();
            }
        }

        public Vector2 GetCollisionNormal(Collider2D collision)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (collision.transform.position - transform.position).normalized);

            return hit.normal;
        }
    }
}