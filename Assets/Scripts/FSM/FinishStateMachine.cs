using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public interface IStateMachine
    {
        void Init(params IState[] states);
        void Update();
    }

    public interface IStateMachineSwither
    {
        void Switch(AIState state);
    }

    public class FinishStateMachine : IStateMachineSwither, IStateMachine
    {
        private Dictionary<AIState, IState> _states = new Dictionary<AIState, IState>();

        private IState _current;

        public void Init(params IState[] states)
        {
            foreach(var state in states)
            {
                _states[state.State] = state;
            }
        }

        

        public void Switch(AIState state)
        {
            _current?.Exit();
            _current = _states[state];
            _current.Enter();
        }

        public void Update()
        {
            _current.Stay();
        }
    }

}
