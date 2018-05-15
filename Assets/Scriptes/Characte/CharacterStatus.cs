using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{
	/// <summary>
	/// 状态类
	/// </summary>
	public abstract class CharacterStatus : MonoBehaviour 
	{
        private Animator animator;
        [Tooltip("血量")]
        public float HP;
        [Tooltip("最大血量")]
        public float maxHP;
        [Tooltip("魔法值")]
        public float SP;
        [Tooltip("最大魔法值")]
        public float maxSP;
        [Tooltip("基础攻击力")]
        public float baseATK;
        [Tooltip("防御")]
        public float defence;
        [Tooltip("攻击间隔")]
        public float attackInterval;
        [Tooltip("攻击距离")]
        public float attackDistance;

        //被攻击
        public abstract void OnDemage(float values);

        //死亡
        public abstract void Dead(); 
	}
}

