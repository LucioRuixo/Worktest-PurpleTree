using UnityEngine;
using Worktest_PurpleTree.Utility;

namespace Worktest_PurpleTree.Gameplay.Physics
{
    public class PhysicsManager : MonoBehaviourSingleton<PhysicsManager>
    {
        [Header("Gravity")]
        [SerializeField] float gravityAcceleration = 9.8f;
        [SerializeField] Vector2 gravityDirection = Vector2.down;

        public float GravityAcceleration { get { return gravityAcceleration; } }
        public Vector2 GravityDirection { get { return gravityDirection; } }
    }
}