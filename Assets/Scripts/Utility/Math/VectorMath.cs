using UnityEngine;

namespace Worktest_PurpleTree.Utility.Math
{
    public static class VectorMath
    {
        public static Vector2 RotateVector(Vector2 vector, float euler) => Quaternion.Euler(0f, 0f, euler) * vector;

        public static Vector2 ScaleVectorToLength(Vector2 vector, float lenth) => (lenth / vector.magnitude) * vector;

        public static Vector2 ScaleVectorOnAxis(Vector2 vector, Vector2 axis, float scalar)
        {
            float axisToUpAngle = Vector2.Angle(axis, Vector2.up);
            if (axis.x < 0f) axisToUpAngle *= -1f;

            Vector2 scaledVector = vector;
            if (axisToUpAngle != 0f) scaledVector = Quaternion.Euler(0f, 0f, axisToUpAngle) * scaledVector;
            scaledVector.y *= scalar;
            if (axisToUpAngle != 0f) scaledVector = Quaternion.Euler(0f, 0f, -axisToUpAngle) * scaledVector;

            return scaledVector;
        }

        public static void GetConeSides(Vector2 direction, float radius, out Vector2 side1, out Vector2 side2)
        {
            side1 = RotateVector(direction, -(radius / 2f));
            side2 = RotateVector(direction, radius / 2f);
        }
    }
}