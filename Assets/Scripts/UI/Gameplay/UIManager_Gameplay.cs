using System;
using UnityEngine;
using Worktest_PurpleTree.Gameplay;
using Worktest_PurpleTree.Spawning;
using Worktest_PurpleTree.VFX;

namespace Worktest_PurpleTree.UI.Gameplay
{
    public class UIManager_Gameplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] RectTransform canvas;
        [Space]
        [SerializeField] Timer timer;
        [Space]
        [SerializeField] ItemCounter rockCounter;
        [SerializeField] ItemCounter coinCounter;
        [SerializeField] Spawner coinSpawner;
        [Space]
        [SerializeField] GameObject endMenu;

        [Header("Coin Animation Parameters")]
        [SerializeField] float coinAnimationDuration = 0.5f;
        RectTransform animatedCoin;
        int coinCount = 0;

        void OnEnable()
        {
            GameManager.OnTimerStart += StartTimer;
            GameManager.OnRockScored += SetRockCount;
            GameManager.OnCoinGained += PlayCoinAnimation;
        }

        void OnDisable()
        {
            GameManager.OnTimerStart -= StartTimer;
            GameManager.OnRockScored -= SetRockCount;
            GameManager.OnCoinGained -= PlayCoinAnimation;
        }

        Vector2 GetCanvasPosition(Vector2 position)
        {
            position.x -= canvas.sizeDelta.x / 2f;
            position.y -= canvas.sizeDelta.y / 2f;
            return position;
        }

        #region Timer
        void StartTimer(int time, Action onTimerEnd) => timer.StartTimer(time, () => OnTimerEnd(onTimerEnd));

        void OnTimerEnd(Action onTimerEnd)
        {
            onTimerEnd?.Invoke();

            DisplayEndMenu();
        }
        #endregion

        #region Counters
        void SetRockCount(int count) => rockCounter.Count = count;

        void PlayCoinAnimation(int count, Transform coinWorldTransform)
        {
            coinCount = count;

            Vector2 coinScreenPosition = Camera.main.WorldToScreenPoint(coinWorldTransform.position);
            Vector2 coinPosition = GetCanvasPosition(coinScreenPosition);

            Vector2 counterScreenPosition = coinCounter.GetComponent<RectTransform>().position;
            Vector2 counterPosition = GetCanvasPosition(counterScreenPosition);

            animatedCoin = coinSpawner.Spawn().GetComponent<RectTransform>();
            animatedCoin.anchoredPosition = coinPosition;

            VFXManager.Instance.LerpPosition(animatedCoin, coinPosition, counterPosition, coinAnimationDuration, OnCoinAnimationEnd);
        }

        void SetAnimatedCoinPosition(Vector2 position) => animatedCoin.anchoredPosition = position;

        void OnCoinAnimationEnd()
        {
            Destroy(animatedCoin.gameObject);
            SetCoinCount(coinCount);
        }

        void SetCoinCount(int count) => coinCounter.Count = count;
        #endregion

        #region Menues
        void DisplayEndMenu() => endMenu.SetActive(true);

        public void ReturnToMainMenu() => SceneManager.Instance.ChangeScene(SceneManager.MainMenu);
        #endregion
    }
}