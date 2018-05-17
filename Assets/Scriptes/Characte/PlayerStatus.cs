using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{
    /// <summary>
    ///  玩家状态类
    /// </summary>
    public class PlayerStatus : CharacterStatus
    {
        //隐藏方法：子类具有与父类相同签名的方法

        //子类脚本生命周期隐藏了父类脚本生命周期
        //protected override void Start()
        //{
        //    base.Start();
        //    print("初始化子类组件");
        //}

        protected new void Start()
        {
            base.Start();
            print("初始化子类组件");
        }

        /// <summary>
        /// 死亡
        /// </summary>
        public override void Death()
        {
            base.Death();
            print("游戏结束");
        }
    }
}

