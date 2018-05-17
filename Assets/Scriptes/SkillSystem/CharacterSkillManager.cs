using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPGDemo.Character;
using Common;
using Newtonsoft.Json;
using System.IO;

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
        private string jsonStr;

        private void OnGUI()
        {
            if (GUILayout.Button("序列化对象"))
            {
                string[] resourceFiles = new string[skills.Length];
                for (int i=0; i < skills.Length; i++) {
                    jsonStr = JsonHelper.ObjectToJson(skills[i]);
                    resourceFiles[i] = jsonStr;
                }
                Debug.Log(resourceFiles[1]);
                File.WriteAllLines("Assets/StreamingAssets/SkillsConfig.txt", resourceFiles);
            }

            if (GUILayout.Button("反序列化"))
            {
                TextAsset jsonobj = ResourceManager.Load<TextAsset>("SkillsConfig");
                ConfigurationReader.ReaderFile(jsonobj.ToString(), LineToObject);
                jsonStr = JsonHelper.ObjectToJson(jsonobj);


                Debug.Log(jsonStr);
                Debug.Log(jsonobj);
                //SkillData ski = new SkillData();
                // ski = JsonConvert.DeserializeObject<SkillData>(jsonStr);
                // jsonStr = JsonHelper.ObjectToJson(skills[0]);
                Debug.Log("1");
            }
        }

        private void LineToObject(string jsonLine)
        {
            Debug.Log(jsonLine);
            SkillData ski = new SkillData();
            ski = JsonConvert.DeserializeObject<SkillData>(jsonStr);

        }


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

        //生成技能
        public void GenerateSkill(SkillData data)
        {
            //GameObject skillG0 =  Instantiate(data.skillPrefab,transform.position,transform.rotation);
            GameObject skillG0 = GameObjectPool.Instance.CreateObject(data.prefabName, data.skillPrefab, transform.position, transform.rotation);
            GameObjectPool.Instance.CollectObjectSeconds(skillG0, data.durationTime);
            //传递技能数据
            SkillDeployer deployer = skillG0.GetComponent<SkillDeployer>();
            //赋值初始化准备技能算法
            deployer.CurrentSkillData = data;
            //开始执行算法
            deployer.DeployerSkill();

            //Destroy(skillG0, data.durationTime);
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
