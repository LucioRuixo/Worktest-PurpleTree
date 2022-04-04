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

        public int WaitForSeconds(float waitFor, UnityAction onWaitEnd, bool loop = false) => StartCoroutine(CWaitForSeconds(waitFor, onWaitEnd, loop));
        public int WaitForSeconds(MinMax<float> waitFor, UnityAction onWaitEnd, bool loop = false) => StartCoroutine(CWaitForSeconds(waitFor, onWaitEnd, loop));

        #region Coroutines
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
    }
}