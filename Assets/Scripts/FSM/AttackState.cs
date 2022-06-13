using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FSM
{

    public class AttackState : BaseState
    {
        public AttackState(IStateMachineSwither stateMachineSwither, AISharedData sharedData) : base(stateMachineSwither, sharedData)
        {

        }

        public override void Enter()
        {
            if (!_sharedData.Navigation.IsMoveToTarger)
            {
                _sharedData.Navigation.MoveToTarger(_sharedData.MapHelper.NextEnemySearchPoint());
            }
        }

        public override AIState State => AIState.Attack;

        public override void Stay()
        { 
            if (_sharedData.Navigation.IsAnyEnemyDetected)
            {
                _stateMachineSwitcher.Switch(AIState.Attack);
            }
            else
            {
                _stateMachineSwitcher.Switch(AIState.EnemySearch);
            }
        }
    }

}
