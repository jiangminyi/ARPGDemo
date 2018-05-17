using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
	/// <summary>
	/// 攻击选择器接口
	/// </summary>
	public interface IAttackSelector {
        Transform[] SelectTarget(Transform skillTF,SkillData data);
	}
}

