using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{
	/// <summary>
	/// 角色状态类
	/// </summary>
	public class EnemyStatus : Status
    {
        public CharacterAnimationParamater animParams;
        private CharacterInputController inputController;
        private PlayerStatus playerStatus;
        private Animator animator;

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            playerStatus = GetComponent<PlayerStatus>();
            inputController = GetComponent<CharacterInputController>();
        }

        public override void OnDemage(float values)
        {
            values -= defence;
            if (values < 1) values = 1;
            HP -= values;
        }

        public override void Dead()
        {
            throw new NotImplementedException();
        }
    }
}

