using ARPGDemo.Character;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill{
	/// <summary>
	/// 角色技能系统类：技能系统外观类，提供更简单的释放技能方法
	/// </summary>
   [RequireComponent(typeof(CharacterSkillManager))]
	public class CharacterSkillSystem : MonoBehaviour 
	{
        private Transform targetTF;
        private CharacterSkillManager skillManager;
        private SkillData data;
        private Animator anim;
        private MeshRenderer meshRenderer;
        private float noAcitviceTime;
        private List<SkillData> dataCache;

        private void OnEnable()
        {
            GetComponentInChildren<AnimationEventBehaviour>().attackHandler += DeploySkill;
        }

        private void OnDisable()
        {
            GetComponentInChildren<AnimationEventBehaviour>().attackHandler -= DeploySkill;
        }

        private void DeploySkill()
        {
            if (data!=null)
            {
                skillManager.GenerateSkill(data);
                data = null;
                ///dataCache.RemoveAt(0);
            }
        }

        private void Start()
        {
            skillManager = GetComponent<CharacterSkillManager>();
            anim = GetComponentInChildren<Animator>();
            dataCache = new List<SkillData>();
        }

        public void AttackUseSkill(int skillID,bool isBatter = false) {
            if (isBatter && data != null)
                skillID = data.nextBatterId;


            data = skillManager.PrepareSkill(skillID);
            if (data != null) {
                if (data.attackType == SkillAttackType.Single) {
                    Transform[] tran= PitchOnTargetOnSkillData(data);
                    if (tran[0]!=null) {
                        transform.LookAt(tran[0]);
                        tran[0].GetComponent<CharacterSelected>().SetSelectedActive(true);

                        //2. 如果单攻 选中目标，间隔3秒自动取消。
                        //3.如果选中A目标，再选中B目标时，先取消选中A。
                        //取消之前的物体
                        SetSelectedFX(targetTF, false);
                        //选中当前的物体
                        SetSelectedFX(tran[0], true);
                        targetTF = tran[0];

                        //ActivateTarger(tran[0]);
                    }
                }
                anim.SetBool(data.animationName, true);
            }     
        }

        private void ActivateTarger(Transform tran) {
            transform.LookAt(tran);
            if (meshRenderer != null) 
                if (meshRenderer != tran.FindChildByName("selected").GetComponent<MeshRenderer>()) 
                    meshRenderer.enabled = false;
            
            meshRenderer = tran.FindChildByName("selected").GetComponent<MeshRenderer>();
            meshRenderer.enabled = true;
            noAcitviceTime = Time.time + 3;
        }


        private void Update()
        {
            if (noAcitviceTime <= Time.time) {
                if (meshRenderer!=null) {
                    meshRenderer.enabled = false;
                }
            }
        }

        private Transform[] PitchOnTargetOnSkillData(SkillData data) {
            //List<Transform> list = new List<Transform>();
            //for (int i = 0; i < data.attackTargetTags.Length; i++)
            //{
            //    GameObject[] tempGO = GameObject.FindGameObjectsWithTag(data.attackTargetTags[i]);
            //    Transform[] tempTF = tempGO.Select(g => g.transform);
            //    list.AddRange(tempTF);
            //}
            //Transform[] allTarget = list.ToArray();
            //allTarget = allTarget.FindAll(t =>
            //     Vector3.Distance(t.position, transform.position) <= data.attackDistance &&
            //     t.GetComponent<CharacterStatus>().HP > 0
            //);
            //return allTarget.GetMin(t => Vector3.Distance(t.position, transform.position));
            SectorSelector selector = SkillDeployerConfigFactory.CreateObject<SectorSelector>("ARPGDemo.Skill.SectorSelector");
            return selector.SelectTarget(transform, data);
        }

        private Transform FindTarget()
        {
            Transform[] targets = new SectorSelector().SelectTarget(transform, data);
            return targets.Length == 0 ? null : targets[0];
        }

        private void SetSelectedFX(Transform tf, bool state)
        {
            if (tf == null) return;
            var selected = tf.GetComponent<CharacterSelected>();
            if (selected == null) return;
            selected.SetSelectedActive(state);
        }            


    }
}

