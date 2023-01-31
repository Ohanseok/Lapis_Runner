using System.Collections.Generic;
using UnityEngine;
                            
namespace Bono.StateMachine.ScriptableObjects
{
    public abstract class StateActionSO : DescriptionSMActionBaseSO
    {
        internal StateAction GetAction(StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstance)
        {
            if (createdInstance.TryGetValue(this, out var obj))
                return (StateAction)obj;

            var action = CreateAction();
            createdInstance.Add(this, action);
            action._originSO = this;
            action.Awake(stateMachine);
            return action;
        }

        protected abstract StateAction CreateAction();
    }

    public abstract class StateActionSO<T> : StateActionSO where T : StateAction, new()
    {
        protected override StateAction CreateAction() => new T();
    }
}
