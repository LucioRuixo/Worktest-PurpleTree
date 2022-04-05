using UnityEngine;
using Worktest_PurpleTree.Utility.Coroutines;

namespace Worktest_PurpleTree.Gameplay
{
    public class Coin : MonoBehaviour
    {
        [Header("Coin Properties")]
        [SerializeField] float lifetime = 3f;

        int despawnCoroutineID = -1;

        void Start() => despawnCoroutineID = CoroutineManager.Instance.WaitForSeconds(lifetime, Despawn);

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(GameManager.PlayerTag)) Despawn();
        }

        void Despawn()
        {
            CoroutineManager.Instance.StopCoroutine(despawnCoroutineID);
            Destroy(gameObject);
        }
    }
}