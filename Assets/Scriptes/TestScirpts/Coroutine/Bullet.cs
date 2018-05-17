using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace xx
{
	/// <summary>
	///  
	/// </summary>
	public class Bullet : MonoBehaviour ,IResetable
    {
        private Vector3 targetPos;
        //public void CalculateTargetPoint()
        //{
        //    //将自身坐标 转换为 世界坐标
        //    targetPos = transform.TransformPoint(0, 0, 50);
        //}

        public void OnReset()
        {
            targetPos = transform.TransformPoint(0, 0, 50);
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 50);
            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                //Destroy(gameObject);
                GameObjectPool.Instance.CollectObjectSeconds(gameObject);
            }
        }
    }
}
