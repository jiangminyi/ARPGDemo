using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 动画事件行为
/// </summary>
public class AnimationEventBehaviour : MonoBehaviour {
    //策划:为动画添加事件，指向OnAttack,OnCancelAnim
    //程序:在脚本中播放动画，注册attackHandler事件
    public event Action attackHandler;
    //声明事件
    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
    /// <summary>
    /// Unity引擎调用
    /// </summary>
    /// <param name="animParam"></param>
	private void OnCancelAnim(string animParam)
    {
        anim.SetBool(animParam, false);
    }
    /// <summary>
    /// Unity 引擎调用的
    /// </summary>
    private void  OnAttack()
    {
        if (attackHandler!=null)
        {
            attackHandler();//引发事件
        }
    }
}
