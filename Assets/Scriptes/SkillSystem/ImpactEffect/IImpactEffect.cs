using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill{
	/// <summary>
	/// 影响效果接口
	/// </summary>
	public interface IImpactEffect
	{
        void Excute(SkillDeployer deployer);
	}
}

