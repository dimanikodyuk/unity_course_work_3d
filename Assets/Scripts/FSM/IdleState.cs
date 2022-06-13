using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{

    public class IdleState : BaseState
    {

        public IdleState(IStateMachineSwither stateMachineSwither, AISharedData sharedData) : base(stateMachineSwither, sharedData)
        {

        }

        public override AIState State => AIState.Idle;

        public override void Stay()
        {
            if (!_sharedData.Navigation.IsAnyEnemyDetected)
            {
                //Debug.Log($"_sharedData.Navigation.IsAnyEnemyDetected {_sharedData.Navigation.IsAnyEnemyDetected}");
                _stateMachineSwitcher.Switch(AIState.EnemySearch);
            }
            else
            {
                _stateMachineSwitcher.Switch(AIState.Attack);
            }
        }

    }

}
