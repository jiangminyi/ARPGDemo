using System;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    /*C#的扩展方法：在不修改代码的情况下，为其增加新的功能
     * 但是还不会改变微软的数组类，为他增加新方法。
     * 
     * 三要素：
     * 1.扩展方法所在的类必须是静态类
     * 2.在第一个参数上，使用this关键字修饰被扩展的类型
     * 3.在另一个命名空间下
     * 
     * 作用：让调用者方便调用该方法
     * （就好像是在调用自身的类型方法一样）
     * 语法：被扩展类型.方法名；
     */

        // Linq  

    /// <summary>
    /// 数组助手类，主要就是对数组的一些改造和操作；
    /// 提供一些数组常用的功能
    /// </summary>
    public static class ArrayHelper
    {
        //7个方法 查找  查找所有满足条件的所有对象
        //排序【升序，降序】最大值，最小值，筛选

        /// <summary>
        /// 查找满足条件的单个元素
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="condition">查找条件</param>
        /// <returns></returns>
        public static T Find<T>
            (this T[] array,Func<T,bool> condition)
        {
            for (int i = 0; i < array.Length; i++)
            {
                //满足条件【调用者指定相应的条件】
                //if(array[i]==5)
                if (condition(array[i]))
                {
                    return array[i];
                }
            }
            //泛型的默认值
            return default(T);
        }
        //查找满足条件的所有数组元素
        //找敌人 血量>79 100敌人
        public static T[] FindAll<T>
            (this T[] array,Func<T,bool> condition)
        {
            //集合存储满足条件的元素
            List<T> list = new List<T>();
            for (int i = 0; i < array.Length; i++)
            {
                //查找的条件
                //if (array[i]>79)
                if(condition(array[i]))
                {
                    list.Add(array[i]);
                }
            }
            //ToArray将集合转换成数组
            return list.ToArray();
        }
        //
        /// <summary>
        /// 最大值
        /// </summary>
        /// <typeparam name="T">代表的数组的类型 </typeparam>
        /// <typeparam name="Q">比较条件的返回值类型 </typeparam>
        /// <param name="array">要比较的数组</param>
        /// <param name="condition">要比较的方法</param>
        /// <returns></returns>
        public static T GetMax<T,Q>
            (this T[] array,Func<T,Q> condition ) 
            where Q:IComparable
        {
            T max = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                //比较的条件
                //if (max <array[i])
                if(condition(max).
                    CompareTo
                    (condition(array[i]))<0)
                {
                    max = array[i];
                }
            }
            return max;
        }
        /// <summary>
        /// 求最小值
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <typeparam name="Q">返回值类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="condition">比较条件</param>
        /// <returns></returns>
        public static T GetMin<T, Q>(this T[] array,Func<T,Q> condition)where Q:IComparable
        {
            T min = array[0];
            //求最大值
            for (int i = 0; i < array.Length; i++)
            {

                if (condition(min).CompareTo(condition(array[i])) > 0)
                {
                    min = array[i];
                }
            }
            return min;
        }
        //升序
        /// <summary>
        /// 升序方法
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <typeparam name="Q">返回值类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="condition">委托类型</param>
        public static void OrderBy<T,Q>
            (this T[] array,Func<T,Q> condition)
            where Q:IComparable
        {
            for (int i = 0; i < array.Length-1; i++)
            {
                for (int j = 0; j < array.Length-1-i; j++)
                {
                    //if (array[j] > array[j + 1])
                    if(condition(array[j]).CompareTo(condition(array[j+1]))>0)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
            
        }
        /// <summary>
        /// 降序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        public static void OrderDescding<T, Q>(this T[] array, Func<T, Q> condition) where Q : IComparable
        {
            for (int i = 0; i < array.Length-1; i++)
            {
                for (int j = 0; j < array.Length-1-i; j++)
                {
                    if (condition(array[j]).CompareTo(condition(array[j + 1])) < 0)
                    {
                        T temp = array[j];
                        array[j] = array[j+1];
                        array[j + 1] = temp;
                    }
                }
            }
        }
        //筛选
        public static Q[] Select<T,Q>(this T[] array,Func<T,Q> condition)
        {
            //存储筛选出来满足条件的元素
            Q[] result = new Q[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                //筛选的条件【满足筛选条件，就将该元素存到result】
                result[i] = condition(array[i]);
            }
            return result;

        }

    }
}
