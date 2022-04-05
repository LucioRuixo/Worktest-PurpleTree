using UnityEngine;

namespace Worktest_PurpleTree.Gameplay
{
    public class Player_Model : MonoBehaviour
    {
        [Header("Player Properties")]
        [SerializeField] float acceleration = 1f;
        [SerializeField] float frictionFactor = 2f;
        [SerializeField] float maxSpeed = 5f;
        [Space]
        [SerializeField] MinMax<float> xLimit;

        public float Acceleration { get { return acceleration; } }
        public float FrictionFactor { get { return frictionFactor; } }
        public float MaxSpeed { get { return maxSpeed; } }
        public MinMax<float> XLimit { get { return xLimit; } }
    }
}