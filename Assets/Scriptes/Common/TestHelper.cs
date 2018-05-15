using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xx
{
	/// <summary>
	///  
	/// </summary>
	public class TestHelper : MonoBehaviour 
	{
        private void OnGUI()
        {
            if (GUILayout.Button("查找后代物体"))
            {
                //调用静态方法
                //Transform tf = TransformHelper.FindChildByName(transform, "Cube (5)");
                //调用扩展方法
                Transform tf = transform.FindChildByName("Cube (5)");

                print(tf);
            }
        } 
    }
}
