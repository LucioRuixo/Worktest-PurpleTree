using UnityEngine;

namespace Worktest_PurpleTree.Utility
{
    public static class Math
    {
        #region Vectors
        public static Vector2 RotateVector(Vector2 vector, float euler) => Quaternion.Euler(0f, 0f, euler) * vector;

        public static Vector2 ScaleVectorToLength(Vector2 vector, float lenth) => (lenth / vector.magnitude) * vector;

        public static void GetConeSides(Vector2 direction, float radius, out Vector2 side1, out Vector2 side2)
        {
            side1 = RotateVector(direction, -(radius / 2f));
            side2 = RotateVector(direction, radius / 2f);
        }
        #endregion

        #region Forces
        public static float ForceMagnitude(float mass, float acceleration) => mass * acceleration;

        public static float Mass(float forceMagnitude, float acceleration) => forceMagnitude / acceleration;

        public static float Acceleration(float forceMagnitude, float mass) => forceMagnitude / mass;
        #endregion
    }
}