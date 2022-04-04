using UnityEngine;
using Worktest_PurpleTree.Gameplay;

namespace Worktest_PurpleTree.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Timer timer;
        [SerializeField] ItemCounter rockCounter;
        [SerializeField] ItemCounter coinCounter;

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

        void StartTimer(int time) => timer.StartTimer(time, null);

        void SetRockCount(int count) => rockCounter.Count = count;

        void SetCoinCount(int count) => coinCounter.Count = count;
    }
}