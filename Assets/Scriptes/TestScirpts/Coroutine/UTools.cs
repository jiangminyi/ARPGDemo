using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xxx.xx{
	/// <summary>
	/// 
	/// </summary>
	public class UTools : MonoBehaviour 
	{
        private Transform FindChindForName(Transform tran,string objName) {
            if (tran.Find(objName) == null && tran.childCount == 0) {
                return null;
            }
            else if (tran.Find(objName) == null && tran.childCount != 0) {
                for (int i = 0; i < tran.childCount; i++) {

                }
            }
            else {
                return transform.Find(objName);
            }
        }
	}
}

