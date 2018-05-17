using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{
	/// <summary>
	/// 动画参数类
	/// </summary>
    [System.Serializable]//可序列化，可以在Unity编辑器中显示属性
	public class CharacterAnimationParamter
    {
        public string run = "run";
        public string death = "death";
        public string idle = "idle";
        public string attack1 = "attack1";
        public string attack2 = "attack2";
        public string attack3 = "attack3";
        public string walk = "walk";

    }
}

