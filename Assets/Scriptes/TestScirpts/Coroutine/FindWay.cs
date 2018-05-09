using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xxx.xx{
	/// <summary>
	/// 
	/// </summary>
   
	public class FindWay : MonoBehaviour 
	{
        public Transform[] postions;
        public float times = 10;
        private float y;
        public AnimationCurve curve;
        private delegate IEnumerator PathfiningEvent();

        private void OnGUI()
        {
            if (GUILayout.Button("按钮1"))
            {
                StartCoroutine(CirculationMenthod(Pathfining));
            }


        }

        private IEnumerator SphereMoveMenthod(Vector3 targetPostion) {
            Vector3 beginPostion = transform.position;
                for (float x = 0; x < 1; x += Time.deltaTime/times)
                {
                    y = curve.Evaluate(x);
                    transform.LookAt(targetPostion);
                    transform.position = Vector3.Lerp(beginPostion, targetPostion, y);
                    yield return null;
                }
        }

        private IEnumerator Pathfining() {
            for (int i = 0; i < postions.Length; i++)
            {
                yield return StartCoroutine(SphereMoveMenthod(postions[i].position));

            }
        }

        private IEnumerator CirculationMenthod(PathfiningEvent pathfiningEvent) {
            while (true) {
                yield return StartCoroutine(pathfiningEvent());
            }
        }

    }
}

