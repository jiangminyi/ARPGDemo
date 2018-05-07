using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{
    /// <summary>
    /// 马达
    /// </summary>
    public class CharacterMotor : MonoBehaviour
    {

        [Tooltip("旋转速度")]
        
        public float moveSpeed = 8F;
        public float gravity = 20.0F;
        private CharacterController characterController;
        private Vector3 moveDirection  =Vector3.zero;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
        }
        //旋转
        //Z轴指向该放下
        public void LookAtTarget(Vector3 dir)
        {
            if (dir == Vector3.zero) return;
                Quaternion targetDir = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetDir, 0.8f);
         }

        //向前移动
        public void Movement(Vector3 dir)
        {
            LookAtTarget(dir);
            moveDirection = dir;
            moveDirection *= moveSpeed;
            moveDirection.y -= gravity * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime);
            //向前移动
            //CharacterController
        }
	}
}

