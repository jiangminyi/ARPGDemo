using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{
    /// <summary>
    ///  
    /// </summary>
    public abstract class CharacterStatus : MonoBehaviour
    {
        public CharacterAnimationParamter animParams;

        public float attackDistance;

        public float attackInterval;

        public float baseATK;

        [Tooltip("防御力")]
        public float defence;

        public float HP;

        public float maxHP;

        public float maxSP;

        public float SP;

        //protected  virtual void Start()
        //{
        //    print("初始化父类组件"); 
        //}

        protected void Start()
        {
            print("初始化父类组件");
        }

        /// <summary>
        /// 受伤
        /// </summary>
        /// <param name="value"></param>
        public void Damage(float value)
        {
            value -= defence;

            if (value <= 0)
                value = 1;

            HP -= value;

            if (HP <= 0)
            {
                Death();
            }
        }

        /// <summary>
        /// 死亡
        /// </summary>
        public virtual void Death()
        {
            GetComponentInChildren<Animator>().SetBool(animParams.death, true);
        }

    }
}

