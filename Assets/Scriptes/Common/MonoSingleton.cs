using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Common{
	/// <summary>
	/// 
	/// </summary>
	public class MonoSingleton<T> : MonoBehaviour where T: MonoSingleton<T>
    {
        private static T instance;
        private static T Instance {
            get
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        new GameObject("MonoSingleton of " + typeof(T).Name).AddComponent<T>();
                    }
                    else
                    {
                        instance.Init();
                    }
                    return instance;
                }
        }

        protected void Awake()
        {
            if (instance == null) {
                instance = this as T;
                Init();
            }
        }

        //让子类初始化
        public virtual void Init() {
        }
    }
}

