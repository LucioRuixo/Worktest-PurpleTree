using UnityEngine;

namespace Worktest_PurpleTree.Gameplay
{
    public class Projectile : MonoBehaviour
    {
        public Physics.Physics _Physics { private set; get; }

        void Awake() => _Physics = GetComponent<Physics.Physics>();
    }
}