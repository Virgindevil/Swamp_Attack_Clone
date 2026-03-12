using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _transitRange;
    [SerializeField] private float _rangeSpread;

    private void Start()
    {
        _transitRange += Random.Range(-_rangeSpread, _rangeSpread);
    }

    private void Update()
    {
        if (Target)
            if (Vector2.Distance(transform.position, Target.transform.position) < _transitRange)
                NeedTransit = true;
    }
}
