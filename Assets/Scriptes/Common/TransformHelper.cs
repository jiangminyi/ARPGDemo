using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
扩展方法：
       在不修改类(Transform)代码情况下，为其添加新方法。
作用：
        使静态方法的调用"像"实例方法。
语法：
       扩展方法必须在非泛型的静态类中定义
       使用this关键字修饰第一个参数（被扩展类型）
       建议将扩展方法定义在某一命名空间下
*/

namespace Common
{
    //ArrayHelper    JsonHelper  AnimationEventBehaviour
    /// <summary>
    ///  变化组件助手类：封装与变换组件相关常用功能。
    /// </summary>
    public static class TransformHelper 
	{ 
        /// <summary>
        /// 未知层级查找后代物体
        /// </summary>
        /// <param name="currentTF"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        public static Transform FindChildByName(this Transform currentTF, string childName)
        {
            //在子物体中查找
            Transform childTF = currentTF.Find(childName);
            if (childTF != null) return childTF;
            //交给儿子
            for (int i = 0; i < currentTF.childCount; i++)
            {
                //currentTF.GetChild(i)
                childTF = FindChildByName(currentTF.GetChild(i), childName);
                if (childTF != null) return childTF;
            }
            return null;
        }

        /// <summary>
        /// 缓动注视方向旋转
        /// </summary>
        /// <param name="currentTF"></param>
        /// <param name="dir"></param>
        /// <param name="rotateSpeed"></param>
        public static void LookAtDirection(this Transform currentTF, Vector3 dir, float rotateSpeed)
        {
            if (dir == Vector3.zero) return;
            Quaternion targetDir = Quaternion.LookRotation(dir);
            currentTF.rotation = Quaternion.Lerp(currentTF.rotation, targetDir, Time.deltaTime * rotateSpeed);
        }
     
        /// <summary>
        /// 缓动注视位置旋转
        /// </summary>
        /// <param name="currentTF"></param>
        /// <param name="pos"></param>
        /// <param name="rotateSpeed"></param>
        public static void LookAtPosition(this Transform currentTF, Vector3 pos, float rotateSpeed)
        {
            Vector3 targetDir = pos - currentTF.position;
            currentTF.LookAtDirection(targetDir, rotateSpeed);
        }
    }
}
