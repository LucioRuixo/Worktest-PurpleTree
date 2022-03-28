namespace Worktest_PurpleTree.Utility.Coroutines
{
    public class Coroutine
    {
        int id;
        UnityEngine.Coroutine routine;

        public int ID { get { return id; } }
        public UnityEngine.Coroutine Routine { get { return routine; } }

        public Coroutine(UnityEngine.Coroutine routine, int id)
        {
            this.id = id;
            this.routine = routine;
        }
    }
}