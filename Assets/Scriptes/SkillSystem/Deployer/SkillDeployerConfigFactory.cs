using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 技能释放器配置工厂
    /// </summary>
    public class SkillDeployerConfigFactory
    {
        private static Dictionary<string, object> cache;
        static SkillDeployerConfigFactory(){
            cache = new Dictionary<string, object>();
        }

        public static IAttackSelector CreateAttackSelector(SkillData data)
        {
            //选取对象
            //currentSkillData.selectorType --> IAttackSelector
            //命名规范： ARPGDemo.Skill + SelectorType(枚举类型)Selector
            //Type type = Type.GetType("ARPGDemo.Skill." + data.selectorType + "Selector");
            return CreateObject<IAttackSelector>("ARPGDemo.Skill." + data.selectorType + "Selector");
        }

        public static IImpactEffect[] CreateImpactEffects(SkillData data)
        {
            // 选取对象
            //currentSkillData.impactType  ---> IImpactEffect[]
            IImpactEffect[] effectArr = new IImpactEffect[data.impactType.Length];
            for (int i = 0; i < effectArr.Length; i++)
            {
                //Type type = Type.GetType("ARPGDemo.Skill." + data.impactType[i] + "ImpactEffect");
                effectArr[i] = CreateObject<IImpactEffect>("ARPGDemo.Skill." + data.impactType[i] + "ImpactEffect");
            }
            return effectArr;
        }

        private static T CreateObject<T>(string className) where T: class
        {
            if (!cache.ContainsKey(className)){
                Type type = Type.GetType(className);
                cache.Add(className, Activator.CreateInstance(type));
            }
            return cache[className] as T;
        }
    }
}

