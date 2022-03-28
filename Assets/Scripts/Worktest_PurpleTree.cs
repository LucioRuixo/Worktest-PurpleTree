using System;
using UnityEngine;

namespace Worktest_PurpleTree
{
    [Serializable]
    public struct MinMax<T>
    {
        [SerializeField] T min;
        [SerializeField] T max;

        public T Min { get { return min; } }
        public T Max { get { return max; } }

        public MinMax(T min, T max)
        {
            this.min = min;
            this.max = max;
        }
    }
}