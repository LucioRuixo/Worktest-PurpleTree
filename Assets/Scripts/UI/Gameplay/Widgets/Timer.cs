using System;
using TMPro;
using UnityEngine;
using Worktest_PurpleTree.Utility.Coroutines;

namespace Worktest_PurpleTree.UI.Gameplay
{
    public class Timer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] TMP_Text timeText;

        int time;

        int timerCoroutineID = -1;
        Action onTimerEnd;

        public int Time
        {
            set
            {
                time = value;
                timeText.text = time.ToString("0");
            }

            get { return time; }
        }

        void DecreaseTimer(Action onTimerEnd)
        {
            if (--Time <= 0)
            {
                Time = 0;
                StopTimer();

                onTimerEnd?.Invoke();
            }
        }

        public void StartTimer(int time, Action onTimerEnd)
        {
            StopTimer();

            Time = time;
            this.onTimerEnd = onTimerEnd;

            timerCoroutineID = CoroutineManager.Instance.WaitForSeconds(1f, () => DecreaseTimer(onTimerEnd), true);
        }

        public void PauseTimer()
        {
            if (timerCoroutineID != -1)
            {
                CoroutineManager.Instance.StopCoroutine(timerCoroutineID);
                timerCoroutineID = -1;
            }
        }

        public void ContinueTimer()
        {
            if (timerCoroutineID == -1) StartTimer(time, onTimerEnd);
        }

        public void StopTimer()
        {
            if (timerCoroutineID != -1)
            {
                CoroutineManager.Instance.StopCoroutine(timerCoroutineID);
                timerCoroutineID = -1;
            }
        }
    }
}