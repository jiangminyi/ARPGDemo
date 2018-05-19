using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.FSM
{
    public enum FSMTriggerID
    {
        /// <summary>
        /// 没有生命
        /// </summary>
        NoHealth, 
        /// <summary>
        /// 目标被击杀
        /// </summary>
        KilledTarget,
        /// <summary>
        /// 发现目标
        /// </summary>
        LoseTarget,
        /// <summary>
        /// 完成巡逻
        /// </summary>
        CompletePathfinding,
        /// <summary>
        /// 发现目标
        /// </summary>
        SawTarget
    }
}
