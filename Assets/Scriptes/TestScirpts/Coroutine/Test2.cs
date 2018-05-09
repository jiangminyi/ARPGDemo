using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xxx.xx{
    /// <summary>
    /// adf
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
        public float times = 2;
        private float x;
        private float y;

        public  Color endColor;
        private IEnumerator Fun3()
        {
            for (x=0; x<1;x+=Time.deltaTime/times) {
                y = curve.Evaluate(x);
                material.color = Color.Lerp(material.color, endColor,y);
                yield return null;
            }
        }
    }
}

