using System;
using UnityEngine;

namespace Worktest_PurpleTree.Gameplay
{
    public class Goal : MonoBehaviour
    {
        public static event Action OnPointScored;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(GameplayManager.RockTag)) OnPointScored?.Invoke();
        }
    }
}