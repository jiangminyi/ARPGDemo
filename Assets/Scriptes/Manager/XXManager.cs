using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Common
{
	/// <summary>
	/// 某某管理器，单例
	/// </summary>
	public class XXManager : MonoSingleton<XXManager>
    {
        public override void Init()
        {
            base.Init();
            print("初始化");
        }

        private void Fun1() {
            print("用于管理器测试");
        }


	}
}

