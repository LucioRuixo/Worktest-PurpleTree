using UnityEngine;

namespace Worktest_PurpleTree.Utility
{
	public class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
	{
		static T instance;

		public static T Instance { get { return instance; } }

		public virtual void Awake()
		{
			if (instance == null) instance = this as T;
			else Destroy(gameObject);
		}

		void OnDestroy() { if (instance == this as T) instance = null; }
	}
}