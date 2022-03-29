namespace Worktest_PurpleTree.Utility.Physics
{
    public static class PhysicsMath
    {
        public static float ForceMagnitude(float mass, float acceleration) => mass * acceleration;

        public static float Mass(float forceMagnitude, float acceleration) => forceMagnitude / acceleration;

        public static float Acceleration(float forceMagnitude, float mass) => forceMagnitude / mass;
    }
}