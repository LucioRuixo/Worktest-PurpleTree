using System;
using UnityEngine;
using Worktest_PurpleTree.Gameplay;

namespace Worktest_PurpleTree.UI.Gameplay
{
    public class UIManager_Gameplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Timer timer;
        [SerializeField] ItemCounter rockCounter;
        [SerializeField] ItemCounter coinCounter;
        [SerializeField] GameObject endMenu;

        void OnEnable()
        {
            GameplayManager.OnTimerStart += StartTimer;
            GameplayManager.OnRockScored += SetRockCount;
            GameplayManager.OnCoinGained += SetCoinCount;
        }

        void OnDisable()
        {
            GameplayManager.OnTimerStart -= StartTimer;
            GameplayManager.OnRockScored -= SetRockCount;
            GameplayManager.OnCoinGained -= SetCoinCount;
        }

        void StartTimer(int time, Action onTimerEnd) => timer.StartTimer(time, () => OnTimerEnd(onTimerEnd));

        void OnTimerEnd(Action onTimerEnd)
        {
            onTimerEnd?.Invoke();

            DisplayEndMenu();
        }

        void SetRockCount(int count) => rockCounter.Count = count;

        void SetCoinCount(int count) => coinCounter.Count = count;

        void DisplayEndMenu() => endMenu.SetActive(true);

        public void ReturnToMainMenu() => SceneManager.Instance.ChangeScene(SceneManager.MainMenu);
    }
}