using System;
using UnityEngine;
using Worktest_PurpleTree.Gameplay.Spawning;
using Worktest_PurpleTree.Utility;

namespace Worktest_PurpleTree.Gameplay
{
    public class GameplayManager : MonoBehaviourSingleton<GameplayManager>
    {
        #region Tags
        public const string PlayerTag = "Player";
        public const string FloorTag = "Floor";
        public const string BoxTag = "Box";
        public const string GoalTag = "Goal";
        public const string RockTag = "Rock";
        public const string CoinTag = "Coin";
        #endregion

        [Header("References")]
        [SerializeField] CoinSpawner coinSpawner;
        [SerializeField] Transform goal;

        [Header("Gameplay Parameters")]
        [SerializeField] int timerDuration = 30;
        [Space]
        [SerializeField] float lastReboundXThreshold;
        [SerializeField] int coinScoreGoal = 3;

        bool playing = true;

        int rockScore = 0;
        int coinScore = 0;
        int coins = 0;

        public float LastReboundXThreshold { get { return lastReboundXThreshold; } }
        public Vector2 GoalPosition { get { return goal.position; } }

        public static event Action<int, Action> OnTimerStart;
        public static event Action<int> OnRockScored;
        public static event Action<int> OnCoinGained;
        public static event Action OnGameEnd;

        void OnEnable()
        {
            Goal.OnPointScored += Score;
            Player_Controller.OnCoinGrabbed += GainCoin;
        }

        void Start() => StartTimer();

        void OnDisable()
        {
            Goal.OnPointScored -= Score;
            Player_Controller.OnCoinGrabbed -= GainCoin;
        }

        void StartTimer() => OnTimerStart?.Invoke(timerDuration, OnTimerEnd);

        void OnTimerEnd()
        {
            playing = false;

            OnGameEnd?.Invoke();
        }

        void Score()
        {
            if (!playing) return;

            ScoreRockPoint();
            ScoreCoinPoint();
        }

        void ScoreRockPoint()
        {
            rockScore++;
            OnRockScored?.Invoke(rockScore);
        }

        void ScoreCoinPoint()
        {
            if (++coinScore >= coinScoreGoal)
            {
                coinScore = 0;
                coinSpawner.SpawnInRange();
            }
        }

        void GainCoin() => OnCoinGained?.Invoke(++coins);

        #region Gizmos
        float lastReboundThresholdLineHeight = 5f;

        void OnDrawGizmosSelected()
        {
            Vector2 lastReboundThresholdLineStart = new Vector2(lastReboundXThreshold, -lastReboundThresholdLineHeight / 2f);
            Vector2 lastReboundThresholdLineEnd = new Vector2(lastReboundXThreshold, lastReboundThresholdLineHeight / 2f);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(lastReboundThresholdLineStart, lastReboundThresholdLineEnd);
        }
        #endregion
    }
}