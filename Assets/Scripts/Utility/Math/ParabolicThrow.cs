using UnityEngine;

namespace Worktest_PurpleTree.Utility.Math
{
    public class ParabolicThrow : MonoBehaviour
    {
        public static float YFromTime(float yVelocity, float initialY, float gravity, float time) => 0.5f * gravity * (time * time) - yVelocity * time + initialY;

        public static float TimeFromY(float yVelocity, float initialY, float gravity)
        {
            double a = 0.5f * gravity;
            double b = -yVelocity;
            double c = initialY;
            QuadraticFormula(a, b, c, out double root1, out double root2);

            return (float)root1;
        }

        public static float YVelocityFromYAndTime(float initialY, float gravity, float time) => (0.5f * -gravity * (time * time) + initialY) / -time;

        public static int QuadraticFormula(double a, double b, double c, out double root1, out double root2)
        {
            root1 = double.NaN;
            root2 = double.NaN;

            double p, q, D;

            p = b / (2 * a);
            q = c / a;

            D = p * p - q;

            if (D == 0d)
            {
                root1 = -p;
                return 1;
            }
            else if (D < 0d) return 0;
            else
            {
                double sqrt_D = System.Math.Sqrt(D);
                root1 = sqrt_D - p;
                root2 = -sqrt_D - p;

                return 2;
            }
        }
    }
}