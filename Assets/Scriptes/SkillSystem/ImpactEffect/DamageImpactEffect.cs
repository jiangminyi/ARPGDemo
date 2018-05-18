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
        public void Excute(SkillDeployer deployer)
        {
            deployer.StartCoroutine(RepeatDamage(deployer));
        }

        private IEnumerator RepeatDamage(SkillDeployer deployer)
        {
            float atkTime = 0;
            do
            {
                OnceDamage(deployer);
                yield return new WaitForSeconds(deployer.CurrentSkillData.atkInterval-Time.deltaTime);
                atkTime += (deployer.CurrentSkillData.atkInterval - Time.deltaTime);
                deployer.CalculateTargets();
            } while (atkTime < deployer.CurrentSkillData.durationTime);
        }

        private void OnceDamage(SkillDeployer deployer)
        {
            for (int i = 0; i < deployer.CurrentSkillData.attackTargets.Length; i++) {
                deployer.CurrentSkillData.attackTargets[i].GetComponent<CharacterStatus>().Damage(deployer.CurrentSkillData.owner.GetComponent<CharacterStatus>().baseATK * deployer.CurrentSkillData.atkRatio);
            }
            //单次伤害
            //遍历被攻击的目标 data.attackTargets,调用受伤方法。
            //
        }
    }
}

