using ARPGDemo.Character;
using ARPGDemo.Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI.FSM
{
    /// <summary>
    ///  基础状态机
    /// </summary>
    public class BaseFSM : MonoBehaviour
    {
        #region 脚本生命周期
        private void Start()
        {
            InitComponent();
            ConfigFSM();
            InitDefaultState();
        }

        private void Update()
        {
            currentState.Reason(this);
            currentState.Action(this);
            FindTarget();
        }
        #endregion

        #region 状态机自身成员
        //状态列表
        private List<FSMState> states;

        //读取配置文件 反射创建对象
        //配置状态机
        private void ConfigFSM()
        {
            states = new List<FSMState>();
            IdleState idle = new IdleState();
            states.Add(idle);
            idle.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
            idle.AddMap(FSMTriggerID.SawTarget, FSMStateID.Pursuit);

            DeadState dead = new DeadState();
            states.Add(dead);

            PursuitState pursuit = new PursuitState();
            states.Add(pursuit);
        }

        //当前状态
        private FSMState currentState;

        [Tooltip("默认状态编号")]
        public FSMStateID defaultStateID;
        private FSMState defaultState;
        private void InitDefaultState()
        {
            defaultState = states.Find(s => s.StateID == defaultStateID);
            currentState = defaultState;
            currentState.EnterState(this);
        }

        //切换状态（状态类调用）
        public void ChangeState(FSMStateID stateID)
        {
            FSMState targetState;
            //如果需要切换的是默认状态 则
            //FSMStateID.Default
            if (stateID == FSMStateID.Default)
                targetState = defaultState;
            else
                targetState = states.Find(s => s.StateID == stateID);

            //退出之前状态
            currentState.ExitState(this);
            //切换状态
            currentState = targetState;
            //进入当前状态
            currentState.EnterState(this);
        }
        #endregion

        #region 为条件和状态提供的成员
        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public CharacterStatus chStatus;
        [Tooltip("目标标签")]
        public string[] targetTags = { "Player" };
        [Tooltip("搜索距离")]
        public float findDistance = 10;
        [Tooltip("跑步速度")]
        public float runSpeed = 5;
        //[HideInInspector]
        public Transform targetTF;
        private NavMeshAgent navAgent;

        private void InitComponent()
        {
            anim = GetComponentInChildren<Animator>();
            chStatus = GetComponent<CharacterStatus>();
            navAgent = GetComponent<NavMeshAgent>();
        }

        //查找目标方法(由Update调用)
        private void FindTarget()
        {
            SkillData data = new SkillData()
            {
                attackTargetTags = targetTags,
                attackDistance = findDistance,
                attackAngle = 360,
                attackType = SkillAttackType.Single
            };
            Transform[] array = new SectorSelector().SelectTarget(transform, data);
            targetTF = array.Length == 0 ? null : array[0];
        }

        //供追逐、巡逻状态调用
        public void MoveToTarget(Vector3 pos, float speed, float stopDistance)
        {
            //通过寻路组件运动
            navAgent.SetDestination(pos);
            navAgent.speed = speed;
            navAgent.stoppingDistance = stopDistance;
        }
        #endregion

    }
}

