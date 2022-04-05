using System;
using Worktest_PurpleTree.Utility;

namespace Worktest_PurpleTree
{
    public class SceneManager : PersistentMonoBehaviourSingleton<SceneManager>
    {
        #region Scenes
        public const string MainMenu = "Main Menu";
        public const string Gameplay = "Gameplay";
        #endregion

        public static event Action OnSceneChange;

        public void ChangeScene(string scene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);

            OnSceneChange?.Invoke();
        }
    }
}