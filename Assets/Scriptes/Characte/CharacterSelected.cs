using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character{
    /// <summary>
    ///  角色选择器
    /// </summary>
    public class CharacterSelected : MonoBehaviour 
	{
        private GameObject selectedGO;
        private float hideTime;
        [Tooltip("调整激活的时间")]
        public float showTime=3;


        private void Start()
        {
            selectedGO = transform.FindChildByName("selected").gameObject;
        }
        //设置选择器激活状态
        public void SetSelectedActive(bool state) {
            selectedGO.SetActive(state);
            if (state) {
                hideTime = Time.time + showTime;
                enabled = true;
            }

        }

        private void Update()
        {
            if (hideTime < Time.time) {
                selectedGO.SetActive(false);
                enabled = false;
            }
        }
    }
}

