using ARPGDemo.Character;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill{
    /// <summary>
    /// 扇形选区
    /// </summary>
    public class SectorSelector : IAttackSelector
    {
        private List<Transform> targets;
        private float lastDistance;
        //public Transform[] SelectTarget(Transform skillTF, SkillData data)
        //{
        //    targets = new List<Transform>();
        //    //1、根据攻击目标标签，查找所有目标变换组件
        //    //data/attackTargetTags
        //    for (int i = 0; i < data.attackTargetTags.Length; i++) {
        //        GameObject[] objs =  GameObject.FindGameObjectsWithTag(data.attackTargetTags[i]);
        //        for (int j = 0; j < objs.Length; j++) {
        //            //2、遍历目标，判断是否在攻击范围内。
        //            //距离
        //            float targetsDistance = Vector3.Distance(skillTF.position, objs[j].transform.position);
        //            //夹角
        //            float targetAngle = Vector3.Angle(skillTF.forward,objs[j].transform.position - skillTF.position);
        //            //3、判断是否活的+-+
        //            float HP = objs[j].GetComponent<CharacterStatus>().HP;

        //            //4、如果群攻返回结果，如果单攻，返回最近目标。
        //            if (targetsDistance <= data.attackDistance && targetAngle * 2 <= data.attackAngle && HP > 0) {
        //                if (data.attackType == SkillAttackType.Group)
        //                {
        //                    targets.Add(objs[j].transform);
        //                }
        //                else if (data.attackType == SkillAttackType.Single) {
        //                    if (targets.Count == 0)
        //                    {
        //                        lastDistance = targetsDistance;
        //                        targets.Add(objs[j].transform);
        //                    }
        //                    else {
        //                        if (lastDistance > targetsDistance) {
        //                            targets.RemoveAt(0);
        //                            lastDistance = targetsDistance;
        //                            targets.Add(objs[j].transform);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return targets.ToArray();
        //}

        public Transform[] SelectTarget(Transform skillTF, SkillData data)
        {
            //1. 根据攻击目标标签，查找所有目标变换组件。
            //data.attackTargetTags  string[]  
            Transform[] allTarget = GetAllTarget(data);

            //3. 判断是否活的。
            allTarget = allTarget.FindAll(t =>
                 Vector3.Distance(t.position, skillTF.position) <= data.attackDistance &&
                 Vector3.Angle(skillTF.forward, t.position - skillTF.position) <= data.attackAngle / 2 &&
                 t.GetComponent<CharacterStatus>().HP > 0
            );

            //4. 如果群攻,返回结果；
            if (data.attackType == SkillAttackType.Group|| allTarget.Length ==0)
                return allTarget;
            //如果单攻，返回最近目标。
            Transform min = allTarget.GetMin(t => Vector3.Distance(t.position, skillTF.position));
            return new Transform[] { min };
        }




        private Transform[] GetAllTarget(SkillData data)
        {
            List<Transform> list = new List<Transform>();
            for (int i = 0; i < data.attackTargetTags.Length; i++)
            {
                GameObject[] tempGO = GameObject.FindGameObjectsWithTag(data.attackTargetTags[i]);
                // GameObject[]   -->  Transform[]
                Transform[] tempTF = tempGO.Select(g => g.transform);
                list.AddRange(tempTF);
            }
            return list.ToArray();
        }

    }
}

