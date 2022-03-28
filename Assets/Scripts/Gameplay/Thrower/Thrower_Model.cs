using UnityEngine;

namespace Worktest_PurpleTree.Gameplay
{
    public class Thrower_Model : MonoBehaviour
    {
        [Header("Thrower Properties")]
        [SerializeField] MinMax<float> launchInterval = new MinMax<float>(1f, 2f);

        public MinMax<float> LaunchInterval { get { return launchInterval; } }
    }
}