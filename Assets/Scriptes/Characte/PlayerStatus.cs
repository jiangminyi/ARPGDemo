using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{
	/// <summary>
	/// 角色状态类
	/// </summary>
	public class PlayerStatus : CharacterStatus
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
            if (values < 1f) values = 1;
            HP -= values;
            HP -= values;
            if (HP <= 0)
            {
                Dead();
            }
        }

        public override void Dead()
        {
            animator.SetBool(playerStatus.animParams.death, true);
            inputController.enabled = false;
        }
    }
}

