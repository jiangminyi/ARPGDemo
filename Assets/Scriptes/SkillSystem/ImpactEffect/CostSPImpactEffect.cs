using ARPGDemo.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 
    /// </summary>
    public class CostSPImpactEffect : IImpactEffect
    {
        public void Excute(SkillDeployer deployer)
        {
            deployer.CurrentSkillData.owner.GetComponent<CharacterStatus>().SP -= deployer.CurrentSkillData.costSP;
        }
    }
}

