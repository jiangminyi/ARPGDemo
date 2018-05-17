using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Common{
	/// <summary>
	/// 配置文件读取器
	/// </summary>
	public class ConfigurationReader : MonoBehaviour 
	{

        public static string GetConfigFile(string fileName)
        {
            string configPath;
            //如果是编译器或者PC平台
#if UNITY_EDITOR || UNITY_STANDALONE
            configPath = "file://" + Application.dataPath + "/StreamingAssets/" + fileName;

            //否则如果是iphone手机平台
#elif UNITY_IPHONE
                  configPath ="file://"+ Application.dataPath + "/Raw/"+ fileName;  
    
            //否则是安卓手机平台
#elif UNITY_ANDROID
                configPath ="jar:file://"+ Application.dataPath + "!/assets/"+ fileName;    
#endif
            WWW www = new WWW(configPath);
            while (true)
            {
                if (www.isDone)
                    return www.text;
            }
        }

        public static void ReaderFile(string content,Action<string> handle)
        {
            using (StringReader reader = new StringReader(content))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //string[] keyValue = line.Split('=');
                    //map.Add(keyValue[0], keyValue[1]);
                   
                    handle(line);
                }
            }
        }
    }
}

