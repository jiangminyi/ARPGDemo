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
        private Status targetstatus;
        private Status sellStatus;

        private void OnTriggerEnter(Collider other)
        {
            targetstatus = GetTargerStatus(other);
            sellStatus = GetSellStatus();
            print(targetstatus);
            print(sellStatus);
            if (targetstatus && sellStatus) targetstatus.OnDemage(sellStatus.baseATK);
        }

        //获取目标者身上的状态
        private Status GetTargerStatus(Collider other) {
            return other.gameObject.GetComponent<Status>();
        }

        //获取自者身上的状态
        private Status GetSellStatus()
        {
            return FindMostGameObject(gameObject).GetComponent<Status>();
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

