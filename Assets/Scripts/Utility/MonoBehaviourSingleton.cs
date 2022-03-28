using UnityEngine;

namespace Worktest_PurpleTree.Utility
{
	public class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
	{
		static T instance;

		public static T Instance { get { return instance; } }

		public virtual void Awake()
		{
			if (!instance)
			{
				instance = this as T;
				DontDestroyOnLoad(this);
			}
			else Destroy(gameObject);
		}
	}
}