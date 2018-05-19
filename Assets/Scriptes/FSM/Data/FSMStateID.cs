namespace AI.FSM
{
    /// <summary>
    /// 状态编号
    /// </summary>
    public enum FSMStateID
    {
        /// <summary>
        /// 不存在该状态
        /// </summary>
        None,
        /// <summary>
        /// 默认
        /// </summary>
        Default,
        /// <summary>
        /// 死亡
        /// </summary>
        Dead,
        /// <summary>
        /// 闲置
        /// </summary>
        Idle, 
        /// <summary>
        /// 攻击
        /// </summary>
        Attacking,
        /// <summary>
        /// 寻路
        /// </summary>
        Pathfinding,
        /// <summary>
        /// 追赶
        /// </summary>
        Pursuit
    }
}
