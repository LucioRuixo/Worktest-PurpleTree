using UnityEngine;

namespace Worktest_PurpleTree.Gameplay
{
    public class Player_Model : MonoBehaviour
    {
        [Header("Player Properties")]
        [SerializeField] float speed = 1f;

        public float Speed { get { return speed; } }
    }
}