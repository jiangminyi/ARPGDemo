using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xx
{
	/// <summary>
	///  
	/// </summary>
	public class Gun : MonoBehaviour 
	{
        private GameObject bulletPrefab;
        private void Awake()
        {
            bulletPrefab = ResourceManager.Load<GameObject>("Sphere");
        }

        public void Fire()
        {
            //Instantiate(bulletPrefab, transform.position, transform.rotation);
            GameObjectPool.Instance.CreateObject("Sphere",bulletPrefab, transform.position,transform.rotation);

        }

        //测试
        private void OnGUI()
        {
            if (GUILayout.Button("发射"))
            {
                Fire();
            }
        }
    }
}
