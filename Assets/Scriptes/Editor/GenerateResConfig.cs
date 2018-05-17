using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/*
 * Editor 编译器代码: 存放在Editor目录下，只在编译器环境下执行的代码。
 * （程序发布时不打包）
 * StreamingAssets :Unity工程特殊目录之一，在移动端可读取，在PC端可读写。
 * Application.persistentDataPath (持久化路径):Unity工程外特殊目录之一,在移动端可读写，在PC端可读写。
 */
namespace xxx.xx{
	/// <summary>
	/// 配置文件生成器
	/// </summary>
	public class GenerateResConfig : Editor 
	{
        [MenuItem("Tools/Resources/Generate ResConfig File")]
        public static void Genertae() {
            //生成配置文件
            //文件名=路径
            //1、获取Resources目录下的文件
            string[] resourceFiles =  AssetDatabase.FindAssets("t:Prefab t:Sprite t:TextAsset", new string[] { "Assets/Resources" });
            for (int i = 0; i < resourceFiles.Length; i++)
            {
                string assetsPath = AssetDatabase.GUIDToAssetPath(resourceFiles[i]);
                string fileName = Path.GetFileNameWithoutExtension(assetsPath);
                string filePath = assetsPath.Replace("Assets/Resources/", "");
                filePath = filePath.Substring(0,filePath.IndexOf("."));
                resourceFiles[i] = fileName + "=" + filePath;
            }
            //2、生成对应关系
            //3、写入到文件
            File.WriteAllLines("Assets/StreamingAssets/ResMap.txt", resourceFiles);
            AssetDatabase.Refresh();
        }
    }
}

