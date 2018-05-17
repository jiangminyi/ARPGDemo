using ARPGDemo.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    ///  伤害生命
    /// </summary>
    public class DamageImpactEffect : IImpactEffect
    {
        private SkillData deployerData;
        private SkillDeployer deployer;
        public void Excute(SkillDeployer deployer)
        {
            this.deployer = deployer;
            deployerData = deployer.CurrentSkillData;
            deployer.StartCoroutine(RepeatDamage());
        }

        private IEnumerator RepeatDamage()
        {
            float atkTime = 0;
            do
            {
                OnceDamage();
                yield return new WaitForSeconds(deployerData.atkInterval);
                atkTime += deployerData.atkInterval;
                deployer.CalculateTargets();
            } while (atkTime < deployerData.durationTime);
        }

        private void OnceDamage()
        {
            for (int i = 0; i < deployerData.attackTargets.Length; i++) {
                deployerData.attackTargets[i].GetComponent<CharacterStatus>().Damage(deployerData.owner.GetComponent<CharacterStatus>().baseATK * deployerData.atkRatio);
            }
            //单次伤害
            //遍历被攻击的目标 data.attackTargets,调用受伤方法。
            //
        }
    }
}

