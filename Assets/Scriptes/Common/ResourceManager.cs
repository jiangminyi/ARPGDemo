using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Common{
	/// <summary>
	/// 资源管理器
	/// </summary>
	public class ResourceManager 
	{
        private static Dictionary<string, string> map;
        //静态构造函数 :类被加载时执行一次
        static ResourceManager()
        {
            //读取配置文件
            //string -->Dictionary 
            map = new Dictionary<string, string>();
            string configFile = ConfigurationReader.GetConfigFile("ResMap.txt");
            ConfigurationReader.ReaderFile(configFile,BuildMap);
        }

        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="line"></param>
        private static void BuildMap(string line) {
            string[] keyValue = line.Split('=');
            map.Add(keyValue[0], keyValue[1]);
        }

        /// <summary>
        /// 外部调用，返回一个路径
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T Load<T>(string fileName)where T:Object {
            //文件名字 --> 路径
            if (!map.ContainsKey(fileName)) return null;
            string path = map[fileName];
            GameObject go = Resources.Load<GameObject>(path);
            return Resources.Load<T>(path);
        }
	}
}

