using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Worktest_PurpleTree.Utility.Coroutines
{
    public class CoroutineManager : PersistentMonoBehaviourSingleton<CoroutineManager>
    {
        int lastID = -1;

        Dictionary<int, Coroutine> activeCoroutines = new Dictionary<int, Coroutine>();

        void OnEnable() => SceneManager.OnSceneChange += StopAllCoroutines;
        
        void OnDisable() => SceneManager.OnSceneChange -= StopAllCoroutines;

        new int StartCoroutine(IEnumerator routine)
        {
            Coroutine coroutine = new Coroutine(base.StartCoroutine(routine), ++lastID);
            activeCoroutines.Add(coroutine.ID, coroutine);

            return coroutine.ID;
        }

        public void StopCoroutine(int id)
        {
            StopCoroutine(activeCoroutines[id].Routine);
            activeCoroutines.Remove(id);
        }

        new public void StopAllCoroutines()
        {
            foreach (int id in activeCoroutines.Keys) StopCoroutine(id);
        }

        public int WaitForSeconds(float waitFor, UnityAction onWaitEnd, bool loop = false) => StartCoroutine(CWaitForSeconds(waitFor, onWaitEnd, loop));

        public int WaitForSeconds(MinMax<float> waitFor, UnityAction onWaitEnd, bool loop = false) => StartCoroutine(CWaitForSeconds(waitFor, onWaitEnd, loop));

        public int Lerp(Vector2 a, Vector2 b, float duration, UnityAction<Vector2> onLerpLoop, UnityAction onLerpEnd) => StartCoroutine(CLerp(a, b, duration, onLerpLoop, onLerpEnd));

        #region Coroutines
        #region Wait For Seconds
        IEnumerator CWaitForSeconds(float waitFor, UnityAction onWaitEnd, bool loop)
        {
            do
            {
                yield return new WaitForSeconds(waitFor);

                onWaitEnd();
            }
            while (loop);
        }

        IEnumerator CWaitForSeconds(MinMax<float> waitFor, UnityAction onWaitEnd, bool loop)
        {
            do
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(waitFor.Min, waitFor.Max));

                onWaitEnd();
            }
            while (loop);
        }
        #endregion

        #region Lerp
        IEnumerator CLerp(Vector2 a, Vector2 b, float duration, UnityAction<Vector2> onLerpLoop, UnityAction onLerpEnd)
        {
            Vector2 value = a;
            float time = 0f;

            while (value != b)
            {
                time += Time.deltaTime;
                value = Vector2.Lerp(a, b, time / duration);

                yield return null;

                onLerpLoop?.Invoke(value);
            }

            onLerpEnd?.Invoke();
        }
        #endregion
        #endregion
    }
}