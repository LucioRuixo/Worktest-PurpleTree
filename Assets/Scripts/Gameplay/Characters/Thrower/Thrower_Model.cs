using UnityEngine;
using Worktest_PurpleTree.Utility.Math;

namespace Worktest_PurpleTree.Gameplay
{
    public class Thrower_Model : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Thrower_Controller controller;

        [Header("Projectile Throwing")]
        [SerializeField] MinMax<float> throwInterval = new MinMax<float>(1f, 2f);
        [SerializeField, Range(100f, 700f)] float throwForce = 400f;
        [Space]
        [SerializeField, Range(0f, 90f)] float throwConeRotation = 45f;
        [SerializeField, Range(1f, 135f)] float throwConeRadius = 45f;
        
        public MinMax<float> ThrowInterval { get { return throwInterval; } }
        public float ThrowForce { get { return throwForce; } }

        public float ThrowConeRotation { get { return throwConeRotation; } }
        public float ThrowConeRadius { get { return throwConeRadius; } }
        public Vector2 ThrowConeDirection { get { return VectorMath.RotateVector(Vector2.right, ThrowConeRotation); } }

        #region Gizmos
        const float ForceLineMultiplier = 0.005f;

        void OnDrawGizmos()
        {
            Vector3 containerPosition = controller.ProjectileSpawner.DefaultParent.position;
            VectorMath.GetConeSides(ThrowConeDirection, throwConeRadius, out Vector2 side1, out Vector2 side2);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(containerPosition, containerPosition + (Vector3)side1 * throwForce * ForceLineMultiplier);
            Gizmos.DrawLine(containerPosition, containerPosition + (Vector3)side2 * throwForce * ForceLineMultiplier);
        }
        #endregion
    }
}