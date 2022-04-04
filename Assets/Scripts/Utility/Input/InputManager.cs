namespace Worktest_PurpleTree.Utility.Input
{
    public enum Axes
    {
        Horizontal,
        Vertical
    }

    public class InputManager : PersistentMonoBehaviourSingleton<InputManager>
    {
        public float GetAxis(Axes axis) => UnityEngine.Input.GetAxis(axis.ToString());

        public float GetAxisRaw(Axes axis) => UnityEngine.Input.GetAxisRaw(axis.ToString());
    }
}