using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPGDemo.Character;
using Common;

namespace ARPGDemo.Skill
{
    /// <summary>
    ///  技能管理器：负责技能释放前的逻辑处理。
    /// </summary>
    public class CharacterSkillManager : MonoBehaviour
    {
        //技能列表
        public SkillData[] skills;
        private CharacterStatus status;


        private void Start()
        {
            status = GetComponent<CharacterStatus>();
            for (int i = 0; i < skills.Length; i++)
            { 
                InitSkillData(skills[i]);
            }
        }

        /// <summary>
        /// 初始化技能
        /// </summary>
        /// <param name="data"></param>
        private void InitSkillData(SkillData data)
        {
            //data.skillPrefab = Resources.Load<GameObject>("Skill/" + data.prefabName);
            data.skillPrefab = ResourceManager.Load<GameObject>(data.prefabName);
            data.owner = gameObject;
            //data.prefabName --> data.skillPrefab
            //data.owner
        }

        //准备技能
        //--判断条件:法力    冷却
        public SkillData PrepareSkill(int id)
        {
            SkillData data = null;
            //for (int i = 0; i < skills.Length; i++)
            //{
            //    if (skills[i].skillID == id) {
            //        data = skills[i];
            //        break;
            //    } 
            //}
            data = skills.Find(s => s.skillID == id);
            if (data != null && data.coolRemain <= 0 && data.costSP <= status.SP)
                return data;
            return null;
        }

        //释放技能
        public void GenerateSkill(SkillData data)
        {
            GameObject skillG0 =  Instantiate(data.skillPrefab,transform.position,transform.rotation);
            Destroy(skillG0, data.durationTime);
            //开启冷却
            StartCoroutine(CoolTimeDown(data));
        }

        //开启冷却
        private IEnumerator CoolTimeDown(SkillData data)
        {
            data.coolRemain = data.coolTime;
            while (data.coolRemain>0) {
                yield return new WaitForSeconds(1);
                data.coolRemain--;
            }
            //每秒自减
        }

    }
}
