using UnityEngine;

namespace Worktest_PurpleTree.Gameplay.Spawning
{
    public class CoinSpawner : Spawner
    {
        [Header("References")]
        [SerializeField] Transform player;

        [Header("Coin Spawn Properties")]
        [SerializeField] MinMax<float> xRange;
        [SerializeField] float spawnY;
        [Space]
        [SerializeField] float playerZoneWidth = 2f;

        public GameObject SpawnInRange()
        {
            float x;
            do { x = Random.Range(xRange.Min, xRange.Max); }
            while (x > player.position.x - playerZoneWidth / 2f && x < player.position.x + playerZoneWidth / 2f);

            return Spawn(new Vector2(x, spawnY));
        }

        #region Gizmos
        float xRangeLineHeight = 1f;
        float playerZoneLineHeight = 0.5f;

        void OnDrawGizmosSelected()
        {
            Vector2 leftXRangeStart = new Vector2(xRange.Min, spawnY - xRangeLineHeight / 2f);
            Vector2 leftXRangeEnd = new Vector2(xRange.Min, spawnY + xRangeLineHeight / 2f);
            Vector2 rigthXRangeStart = new Vector2(xRange.Max, spawnY - xRangeLineHeight / 2f);
            Vector2 rigthXRangeEnd = new Vector2(xRange.Max, spawnY + xRangeLineHeight / 2f);

            Vector2 leftPlayerZoneStart = new Vector2(player.position.x - playerZoneWidth / 2f, spawnY - playerZoneLineHeight / 2f);
            Vector2 leftPlayerZoneEnd = new Vector2(player.position.x - playerZoneWidth / 2f, spawnY + playerZoneLineHeight / 2f);
            Vector2 rigthPlayerZoneStart = new Vector2(player.position.x + playerZoneWidth / 2f, spawnY - playerZoneLineHeight / 2f);
            Vector2 rigthPlayerZoneEnd = new Vector2(player.position.x + playerZoneWidth / 2f, spawnY + playerZoneLineHeight / 2f);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(leftXRangeStart, leftXRangeEnd);
            Gizmos.DrawLine(rigthXRangeStart, rigthXRangeEnd);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(leftPlayerZoneStart, leftPlayerZoneEnd);
            Gizmos.DrawLine(rigthPlayerZoneStart, rigthPlayerZoneEnd);
        }
        #endregion
    }
}