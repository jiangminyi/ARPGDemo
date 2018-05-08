using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{

    /// <summary>
    /// 角色输入控制器
    /// </summary>
    public class CharacterInputController : MonoBehaviour
    {
        private ETCJoystick joystick;
        private CharacterMotor moter;
        private PlayerStatus playersStatus;
        private Animator animator;
        private ETCButton[] etcButtons;
        private BoxCollider sword;

        //查找角色
        private void Awake()
        {
            //整个项目中只有这样一个类型可以这么找
            joystick = FindObjectOfType<ETCJoystick>();
            moter = GetComponent<CharacterMotor>();
            playersStatus = GetComponent<PlayerStatus>();
            animator = GetComponentInChildren<Animator>();
            etcButtons = FindObjectsOfType<ETCButton>();
            sword = GameObject.Find("sword").GetComponent<BoxCollider>();
        }

        private void Start()
        {
            sword.enabled = false;
        }

        private void Update()
        {
            IfAttackSetCollision();
        }
        //注册事件
        private void OnEnable()
        {
            joystick.onMove.AddListener(OnJoystickMove);
            joystick.onMoveStart.AddListener(OnJoystickMoveStart);
            joystick.onMoveEnd.AddListener(OnJoystickMoveEnd);
            for (int i = 0; i < etcButtons.Length; i++) {
                etcButtons[i].onDown.AddListener(OnSkillButtonDown);
                etcButtons[i].onUp.AddListener(OnSkillButtonUp);
            }
        }


        //注销事件
        private void OnDisable()
        {
            joystick.onMove.RemoveListener(OnJoystickMove);
            joystick.onMoveStart.RemoveListener(OnJoystickMoveStart);
            joystick.onMoveEnd.RemoveListener(OnJoystickMoveEnd);

        }


        //事件处理逻辑
        //当拖动遥感时调用
        private void OnJoystickMove(Vector2 arg0)
        {
            if (!OnAttackStatus()) return;
            if (arg0.sqrMagnitude > 0.3f)
            {
                animator.SetBool(playersStatus.animParams.walk, true);
                animator.SetBool(playersStatus.animParams.run, true);
            }
            else
            {
                animator.SetBool(playersStatus.animParams.walk, true);
                animator.SetBool(playersStatus.animParams.run, false);
            }
            moter.Movement(new Vector3(arg0.x, 0, arg0.y));
        }

        private void OnJoystickMoveStart()
        {
            animator.SetBool(playersStatus.animParams.walk, true);

        }

        private void OnJoystickMoveEnd()
        {
            animator.SetBool(playersStatus.animParams.walk, false);
            animator.SetBool(playersStatus.animParams.run, false);
            //取消跑步动画
        }
        private void OnSkillButtonDown(string arg0)
        {
            switch (arg0)
            {
                case "BaseButton":
                    Attack1();
                    break;
                case "SkillButton01":
                    Attack2();
                    break;
                case "SkillButton02":
                    Attack3();
                    break;
            }
        }

        private void OnSkillButtonUp()
        {
            animator.SetBool(playersStatus.animParams.attack1, false);
            animator.SetBool(playersStatus.animParams.attack2, false);
            animator.SetBool(playersStatus.animParams.attack3, false);
        }

        /// <summary>
        /// 判断人物是否在攻击状态
        /// </summary>
        /// <returns></returns>
        private bool OnAttackStatus() {
            return !animator.GetCurrentAnimatorStateInfo(0).IsName("swordStrike1") &&
                !animator.GetCurrentAnimatorStateInfo(0).IsName("swordStrike2") &&
                !animator.GetCurrentAnimatorStateInfo(0).IsName("swordStrike3");
        }

        //播放攻击动画
        private void Attack1() {
            animator.SetBool(playersStatus.animParams.attack1, true);
        }

        private void Attack2()
        {
            animator.SetBool(playersStatus.animParams.attack2, true);
        }
        private void Attack3()
        {
            animator.SetBool(playersStatus.animParams.attack3, true);
        }

        //当在播放攻击动画时，激活word组件
        private void IfAttackSetCollision() {
            if (OnAttackStatus())
            {
                sword.enabled = false;
            }
            else
            {
                sword.enabled = true;
            }
        }
    }
}
