using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected Player Target { get; set; }

    public void Enter(Player target)
    {
        if (!enabled)
        {
            Target = target;
            enabled = true;
            foreach (Transition t in _transitions)
            {
                t.enabled = true;
                t.Init(Target);
            }
        }
    }

    public void Exit()
    {
        if (enabled)
        {
            foreach (Transition t in _transitions)
                t.enabled = false;

            enabled = false;
        }
    }

    public State GetNextState()
    {
        foreach (Transition t in _transitions)
        {
            if (t.NeedTransit)
                return t.TargetState;
        }

        return null;
    }


}
