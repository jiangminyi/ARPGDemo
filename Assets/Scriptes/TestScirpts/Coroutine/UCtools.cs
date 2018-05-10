using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCtools : MonoBehaviour {

    // Use this for initialization
    public Transform[] transforms;
    public string objName;

    private void OnGUI()
    {
        if (GUILayout.Button("测试")) {
            Transform ta = FindChildForName( objName, transforms);
            if (ta)
            {
                print(ta.name);
            }
            else {
                print("找不到");
            }
        }

        if (GUILayout.Button("测试2"))
        {
            Finddd(transforms);

        }
    }


    private void Finddd( Transform[] transforms) {
        Transform[] ddfdf= transforms[0].GetComponentsInChildren<Transform>();

        print(ddfdf.Length);

    }

    private static Transform FindChildForName(string objName,params Transform[] trans) {
        Transform tran2 =null;

        //遍历传进来的Transform数组，查找每个Transform的所有子类是否有 objName
        for (int i = 0; i < trans.Length; i++) {
            if (trans[i].Find(objName))
            {
                tran2=trans[i].Find(objName);
            }
        }
        if (tran2) {
            return tran2;
        }

        //如果没有则把该层的子类全部遍历出来放进一个数组里面回调自身方法，如果该层都没有子类则退出循环return null

        List<Transform> tranList = new List<Transform>();
        for (int i = 0; i < trans.Length; i++)
        {
            if (trans[i].childCount != 0) {
                for (int j = 0; j < trans[i].childCount; j++) {
                    tranList.Add(trans[i].GetChild(j));
                }
            }
        }

        if (tranList.Count != 0)
        {
            return FindChildForName( objName, tranList.ToArray());
        }
        else {
            return null;
        }

    }

    
}
