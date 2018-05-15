using UnityEngine;
using System;

namespace ARPGDemo.Skill
{
    /// 技能数据
    [Serializable]
    public class SkillData
    {
        ///<summary>技能ID</summary>
        public int skillID;
        ///<summary>技能名称</summary>
        public string name;
        ///<summary>技能描述</summary>
        public string description;
        ///<summary>冷却时间</summary>
        public int coolTime;
        [HideInInspector]
        ///<summary>冷却剩余</summary>
        public int coolRemain;
        ///<summary>魔法消耗</summary>
        public int costSP;
        ///<summary>攻击距离</summary>
        public float attackDistance;
        ///<summary>攻击角度</summary> 
        public float attackAngle;
        ///<summary>攻击目标tags</summary> 
        public string[] attackTargetTags = { "Enemy" };
        ///<summary>攻击目标对象数组</summary>
        [HideInInspector]//在编译中隐藏
        public Transform[] attackTargets;
        ///<summary>技能影响类型</summary> 
        public string[] impactType = { "CostSP", "Damage" };
        ///<summary>连击的下一个技能编号</summary>
        public int nextBatterId;
        ///<summary>伤害比率</summary>
        public float atkRatio;
        ///<summary>持续时间</summary>
        public float durationTime;
        ///<summary>伤害间隔</summary>
        public float atkInterval;
        ///<summary>技能所属</summary>
        [HideInInspector]
        public GameObject owner;
        ///<summary>技能预制件名称</summary>
        public string prefabName;
        ///<summary>预制件对象</summary>
        [HideInInspector]
        public GameObject skillPrefab;
        ///<summary>动画名称</summary> 
        public string animationName;
        ///<summary>受击特效名称</summary>
        public string hitFxName;
        ///<summary>受击特效预制件</summary>
        [HideInInspector]
        public GameObject hitFxPrefab;
        ///<summary>技能等级</summary>
        public int level; 
        ///<summary>攻击类型 单攻，群攻</summary> 
        public SkillAttackType attackType;
        /////<summary>选择类型 扇形(圆形)，矩形</summary>  
        public SelectorType selectorType; 
    }
}
