using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{
    /// <summary>
    ///  敌人状态类
    /// </summary>
    public class EnemyStatus : CharacterStatus
    {
        /// <summary>
        /// 死亡
        /// </summary>
        public override void Death()
        {
            base.Death();

            Destroy(gameObject, 10);
        }
    }
}

