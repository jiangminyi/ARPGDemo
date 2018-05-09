using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xxx.xx{
	/// <summary>
	/// 
	/// </summary>
	public class Coroutine01 : MonoBehaviour 
	{
        private IEnumerator iterator;
        private void OnGUI()
        {
            if (GUILayout.Button("按钮1"))
            {
                 iterator=  Fun1();
            }

            if (GUILayout.Button("按钮2"))
            {
                iterator.MoveNext();
                print(iterator.Current);
            }

            if (GUILayout.Button("按钮3"))
            {
                StartCoroutine(iterator);
            }
        }


        private IEnumerator Fun1() {
            for (int i = 0; i < 5; i++)
            {
                print(i +"--"+Time.deltaTime);

                yield return new WaitForSeconds(1);//等待一个渲染帧
            }
        }
	}
}

