using UnityEngine;

namespace Worktest_PurpleTree.UI.MainMenu
{
    public class UIManager_MainMenu : MonoBehaviour
    {
        public void Play() => SceneManager.Instance.ChangeScene(SceneManager.Gameplay);

        public void Quit() => Application.Quit();
    }
}