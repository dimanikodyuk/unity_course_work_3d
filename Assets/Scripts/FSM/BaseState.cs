using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public enum AIState
    {
        Idle,
        EnemySearch,
        Attack,

    }
    public interface IState
    {
        AIState State { get;  }
        void Enter();
        void Stay();
        void Exit();
    }

    public abstract class BaseState : IState
    {
        public abstract AIState State { get; }

        protected IStateMachineSwither _stateMachineSwitcher;

        public BaseState(IStateMachineSwither stateMachineSwither, AISharedData sharedData)
        {
            _stateMachineSwitcher = stateMachineSwither;
            _sharedData = sharedData;
        }

        protected AISharedData _sharedData;

        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
            
        }

        public abstract void Stay();
        
    }

}