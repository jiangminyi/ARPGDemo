using ARPGDemo.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common{
	/// <summary>
	///  游戏对象池(缓存)
	/// </summary>
	public class GameObjectPool : MonoSingleton<GameObjectPool> 
	{
        //1、池
        Dictionary<string,List<GameObject>> cache;

        public override void Init()
        {
            base.Init();
            if (cache == null) {
                cache = new Dictionary<string, List<GameObject>>();
            }

        }
        /*
         先判断该字典中有没有这个种类的对象池
         1、假设有，判断该种类对象池中还有没有缓存这样一个对象。假设有初始化然后返回。假设没有，往该种类对象池里面放一个对象，回调自身方法
         2、假设没有，新生成一个这样种类的对象池，并往对象池里面放一个对象，回调自身。
         */

        //方法执行在栈中。
        //值类型存储数据。
        //引用类型存储地址,数据在堆中。

        

        //2、创建对象
        public GameObject NewCreateObject(string type, GameObject prefab, Vector3 postion,Quaternion dir) {
            if (cache.ContainsKey(type)) {
                //获取该种类可以使用的游戏对象（禁用）
                //如果找到了则使用（启用、设置位置、旋转）
                if (cache[type].Count > 0)
                {
                    GameObject go = cache[type][0];
                    cache[type].RemoveAt(0);
                    go.transform.position = postion;
                    go.transform.rotation = dir;
                    go.SetActive(true);
                    foreach (var item in go.GetComponents<IResetable>())
                    {
                        item.OnReset();
                    }
                    return go;
                }
                else {
                    GameObject go = Instantiate(prefab);
                    go.SetActive(false);
                    cache[type].Add(go);
                    return NewCreateObject(type, prefab, postion, dir);
                }
            }
            else
            {
                GameObject go = Instantiate(prefab);
                go.SetActive(false);
                cache.Add(type, new List<GameObject>() { go });
                return NewCreateObject(type, prefab, postion, dir);
                //如果没有找到则创建游戏对象，加入池中
            }
        }
        public  GameObject CreateObject(string type, GameObject prefab, Vector3 postion,Quaternion direction)
        {
            //获取该种类可以使用的游戏对象（禁用）
            GameObject instance = FindUsableObject(type);

            //如果没有找到则创建游戏对象，加入池中
            if (instance == null)
                instance = AddObject(type, prefab);
           
            //使用游戏对象
            UseObject(postion, direction, instance);
            return instance;
        }

        private static void UseObject(Vector3 postion, Quaternion direction, GameObject instance)
        {
            instance.transform.position = postion;
            instance.transform.rotation = direction;
            instance.SetActive(true);
            foreach (var item in instance.GetComponents<IResetable>())
            {
                item.OnReset();
            }

        }

        private GameObject AddObject(string type, GameObject prefab)
        {
            GameObject instance = Instantiate(prefab);
            if (!cache.ContainsKey(type))
                cache.Add(type, new List<GameObject>() { });
            cache[type].Add(instance);
            return instance;
        }

        private GameObject FindUsableObject(string type)
        {
            GameObject instance = null;
            if (cache.ContainsKey(type))
                instance = cache[type].Find(go => !go.activeInHierarchy);
            return instance;
        }

        //3、回收对象
        //禁用
        public void NewCollectObject(GameObject go) {
            go.SetActive(false);
            string key = go.transform.name.Replace("(Clone)", "");
            if (cache.ContainsKey(key))
                cache[key].Add(go);
        }

        public void CollectObjectSeconds(GameObject go, float delay=0) {
            StartCoroutine(DelayCollectObject(go,delay));
        }
        private void CollectObject(GameObject go)
        {
            go.SetActive(false);
        }

        private IEnumerator DelayCollectObject(GameObject go,float delay) {
            yield return new WaitForSeconds(delay);
            CollectObject(go);
        }

        //清空
        //--清空某一个类别 
        public void Clear(string type) {
            for (int i = 0; i < cache[type].Count; i++)
            {
                //删除具体的游戏对象
                Destroy(cache[type][i]);
            }
            //移除字典子路
            cache.Remove(type);
        }

        public void ClearAll() {
            List<string> keys = new List<string>(cache.Keys);
            foreach (var item in keys)
            {
                Clear(item);
            }
            
        }
        
    }
    public interface IResetable
    {
        void OnReset();
    }
}

