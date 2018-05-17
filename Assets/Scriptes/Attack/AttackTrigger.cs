using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPGDemo.Character;

namespace ARPGDemo.Attack
{
	/// <summary>
	/// 
	/// </summary>
	public class AttackTrigger : MonoBehaviour 
	{
        private CharacterStatus targetstatus;
        private CharacterStatus sellStatus;

        private void OnTriggerEnter(Collider other)
        {
            targetstatus = GetTargerStatus(other);
            sellStatus = GetSellStatus();

        }

        //获取目标者身上的状态
        private CharacterStatus GetTargerStatus(Collider other) {
            return other.gameObject.GetComponent<CharacterStatus>();
        }

        //获取自者身上的状态
        private CharacterStatus GetSellStatus()
        {
            return FindMostGameObject(gameObject).GetComponent<CharacterStatus>();
        }

        //找出最高级的父类
        private GameObject FindMostGameObject(GameObject obj) {
            if (obj.transform.parent)
            {
                obj = obj.transform.parent.gameObject;
                return FindMostGameObject(obj);
            }
            else {
                return obj;
            }
        }
    }
}

