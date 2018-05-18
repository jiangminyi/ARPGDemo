using ARPGDemo.Skill;
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
        private CharacterSkillManager playerSkills;
        private CharacterSkillSystem skillSystem;
        //查找角色
        private void Awake()
        {
            //整个项目中只有这样一个类型可以这么找
            joystick = FindObjectOfType<ETCJoystick>();
            moter = GetComponent<CharacterMotor>();
            playersStatus = GetComponent<PlayerStatus>();
            animator = GetComponentInChildren<Animator>();
            etcButtons = FindObjectsOfType<ETCButton>();
            playerSkills = GetComponent<CharacterSkillManager>();
            skillSystem = GetComponent<CharacterSkillSystem>();
        }

        //注册事件
        private void OnEnable()
        {
            joystick.onMove.AddListener(OnJoystickMove);
            joystick.onMoveStart.AddListener(OnJoystickMoveStart);
            joystick.onMoveEnd.AddListener(OnJoystickMoveEnd);
            for (int i = 0; i < etcButtons.Length; i++) {
                if (etcButtons[i].name == "BaseButton") {
                    etcButtons[i].onPressed.AddListener(OnSkillButtonPress);
                }
                else {
                    etcButtons[i].onDown.AddListener(OnSkillButtonDown);
                }
            }
        }


        //注销事件
        private void OnDisable()
        {
            joystick.onMove.RemoveListener(OnJoystickMove);
            joystick.onMoveStart.RemoveListener(OnJoystickMoveStart);
            joystick.onMoveEnd.RemoveListener(OnJoystickMoveEnd);
            for (int i = 0; i < etcButtons.Length; i++)
            {
                if (etcButtons[i].name == "BaseButton")
                    etcButtons[i].onPressed.RemoveListener(OnSkillButtonPress);
                else {
                    etcButtons[i].onDown.RemoveListener(OnSkillButtonDown);
                }
            }

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
            int id = 0;
            switch (arg0)
            {
                //case "BaseButton":
                //    id = 1001;
                //    ///Attack1();
                //    break;
                case "SkillButton01":
                    id = 1002;
                    //Attack2();
                    break;
                case "SkillButton02":
                    id = 1003;
                    //Attack3();
                    break;
            }
            skillSystem.AttackUseSkill(id);

        }

        //private void OnSkillButtonUp()
        //{
        //    animator.SetBool(playersStatus.animParams.attack1, false);
        //    animator.SetBool(playersStatus.animParams.attack2, false);
        //    animator.SetBool(playersStatus.animParams.attack3, false);
        //}

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

        private float prePressTime;

        [Range(1,10)]
        public float minAttackInterval = 1;

        public float maxBatterTime = 3;
        private void OnSkillButtonPress() {
            float interval = Time.time - prePressTime;
            //如果按下间隔过短，则退出。
            if (interval < minAttackInterval) return;
            //如果按下间隔较短，则连击。
            //间隔 = 当前时间  - 上次时间
            bool isBatter = interval < maxBatterTime; 
            skillSystem.AttackUseSkill(1001, isBatter); 
            prePressTime = Time.time;

        }

        //当在播放攻击动画时，激活word组件
        //private void IfAttackSetCollision() {
        //    if (OnAttackStatus())
        //    {
        //        sword.enabled = false;
        //    }
        //    else
        //    {
        //        sword.enabled = true;
        //    }
        //}
    }
}
