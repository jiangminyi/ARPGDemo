using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{
	/// <summary>
	/// 角色状态类
	/// </summary>
	public class PlayerStatus : MonoBehaviour 
	{
        public CharacterAnimationParamater animParams;
        private CharacterInputController inputController;
        private PlayerStatus playerStatus;
        private Animator animator;
        private float hp=100;
        private float defence=1;

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            playerStatus = GetComponent<PlayerStatus>();
            inputController = GetComponent<CharacterInputController>();
        }
        public void OnDemage(float values) {
            values -= defence;
            if (values < 1f) values = 1;
            hp -= values;
            hp -= values;
            if (hp <= 0) {
                PlayerDead();
            }
            print("收到伤害，还有" + hp + "点生命值");
        }

        public void PlayerDead() {
            print("gameover");
            animator.SetBool(playerStatus.animParams.death,true);
            print("assssssssss");
            inputController.enabled = false;
        }
	}
}

