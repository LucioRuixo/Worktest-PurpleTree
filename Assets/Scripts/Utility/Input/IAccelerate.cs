using UnityEngine;

namespace Worktest_PurpleTree.Utility.Input
{
    public interface IAccelerate : IInput
    {
        void Accelerate(float acceleration, Vector2 direction);
    }
}