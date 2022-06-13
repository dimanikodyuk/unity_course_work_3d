using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private AISharedData _aiSharedData;
    [SerializeField] private AIState _firstState;

    private FinishStateMachine _finiteStateMachine;
    // Start is called before the first frame update
    void Start()
    {
        PrepareStateMachine();
        PrepareAIStates();
        SwitchToFirstState();
    }

    // Update is called once per frame
    void Update()
    {
        _finiteStateMachine.Update();
    }

    private void PrepareStateMachine()
    {
        _finiteStateMachine = new FinishStateMachine();
    }

    private void PrepareAIStates()
    {
        var idleState = new IdleState(_finiteStateMachine, _aiSharedData);
        var enemySearchState = new TargetSearchState(_finiteStateMachine, _aiSharedData);
        var attackState = new AttackState(_finiteStateMachine, _aiSharedData);
        _finiteStateMachine.Init(idleState, enemySearchState, attackState);
    }

    private void SwitchToFirstState()
    {
        _finiteStateMachine.Switch(_firstState);
    }


}
