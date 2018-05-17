
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
	/// <summary>
	/// 技能释放器
	/// </summary>
	public abstract class SkillDeployer : MonoBehaviour 
	{
        private SkillData currentSkillData;
        public SkillData CurrentSkillData
        {
            get
            {
                return currentSkillData;
            }
            set
            {
                //当外部传值进来，调用InitDeployer方法初始化
                currentSkillData = value;
                InitDeployer();
            }
        }

        //攻击选择器（攻击的类型父类）
        private IAttackSelector attackSelector;

        //效果（效果的父类）
        private IImpactEffect[] impactArray;

        //创建算法对象
        private void InitDeployer() {
            //反射创建对象
            attackSelector = SkillDeployerConfigFactory.CreateAttackSelector(currentSkillData);

            //影响对象
            impactArray = SkillDeployerConfigFactory.CreateImpactEffects(currentSkillData);
        }


        //执行算法对象

            //计算目标
        public void CalculateTargets() {
            currentSkillData.attackTargets = attackSelector.SelectTarget(transform, currentSkillData);

        }

        //影响目标
        public void ImpactTargerts() {
            for (int i = 0; i < impactArray.Length; i++)
            {
                impactArray[i].Excute(this);
            }
        }


        //释放技能（由子类实现）
        public abstract void DeployerSkill();

	}
}

