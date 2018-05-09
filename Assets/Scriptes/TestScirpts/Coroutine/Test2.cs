using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xxx.xx{
    /// <summary>
    /// 
    /// </summary>
    public class Test2 : MonoBehaviour
    {

        private Material material;
        private void OnGUI()
        {
            if (GUILayout.Button("按钮1"))
            {
                StartCoroutine(Fun2());
            }

            if (GUILayout.Button("按钮2"))
            {
                StartCoroutine(Fun3());
            }

        }
        private void Start()
        {
            material = GetComponent<MeshRenderer>().material;

        }

        private IEnumerator Fun2() {
            while (material.color.a >= 0.01f) {
                material.color -= new Color(0, 0, 0, 0.005f);
                yield return new WaitForFixedUpdate();
            }
        }

        public AnimationCurve curve;
        public float speedTimes ;
        private float x;


        public  Color endColor =Color.clear;
        private IEnumerator Fun3()
        {
            Color beginColor = material.color;
            for (x=0; x<1;x+=Time.deltaTime/ 15) {
                print(Time.deltaTime);
                float y = curve.Evaluate(x);
                print("x:" + x);
                print("y:" + y);
                material.color = Color.Lerp(beginColor, endColor,y);
                yield return null;
            }
        }
    }
}

