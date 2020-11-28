using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AI_STATE
{
    OFF = 0,
    PATROL = 1,
    AGGRO = 2,
    ATTACK = 3
}

public class EnemyAI : MonoBehaviour
{
    // CONSTS ////////////////////////////////////////////////////////////////

    protected readonly Color[] gizmoColors = {
        Color.black,
        Color.blue,
        Color.yellow,
        Color.red
    };

    //////////////////////////////////////////////////////////////////////////

    protected AI_STATE _state = AI_STATE.PATROL;
    protected AI_STATE _lateState = AI_STATE.OFF;

    protected delegate void OnStateDelegate();
    protected OnStateDelegate[] onState;
    protected OnStateDelegate[] onStateEnter;
    protected OnStateDelegate[] onStateExit;

    //--HIDDEN-REFERENCES---------------------------------------------------//

    protected Transform _transform;
    protected Transform _player;

    //////////////////////////////////////////////////////////////////////////

    protected virtual void Awake()
    {
        _transform = transform;
        _player = GameObject.FindWithTag("Player").transform;

        onState = new OnStateDelegate[4];
        onState[(int)AI_STATE.OFF] = OnStateOff;
        onState[(int)AI_STATE.PATROL] = OnStatePatrol;
        onState[(int)AI_STATE.AGGRO] = OnStateAggro;
        onState[(int)AI_STATE.ATTACK] = OnStateAttack;

        onStateEnter = new OnStateDelegate[4];
        onStateEnter[(int)AI_STATE.OFF] = OnStateOffEnter;
        onStateEnter[(int)AI_STATE.PATROL] = OnStatePatrolEnter;
        onStateEnter[(int)AI_STATE.AGGRO] = OnStateAggroEnter;
        onStateEnter[(int)AI_STATE.ATTACK] = OnStateAttackEnter;

        onStateExit = new OnStateDelegate[4];
        onStateExit[(int)AI_STATE.OFF] = OnStateOffExit;
        onStateExit[(int)AI_STATE.PATROL] = OnStatePatrolExit;
        onStateExit[(int)AI_STATE.AGGRO] = OnStateAggroExit;
        onStateExit[(int)AI_STATE.ATTACK] = OnStateAttackExit;
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        onState[(int)_state]();

        if (_state != _lateState)
        {
            onStateEnter[(int)_state]();
            onStateExit[(int)_lateState]();
        }

        _lateState = _state;
    }

    //----------------------------------------------------------------------//

    protected virtual void OnStateOff()
    {
    }

    protected virtual void OnStateOffEnter()
    {
    }

    protected virtual void OnStateOffExit()
    {
    }

    protected virtual void OnStatePatrol()
    {
    }

    protected virtual void OnStatePatrolEnter()
    {
    }

    protected virtual void OnStatePatrolExit()
    {
    }

    protected virtual void OnStateAggro()
    {
    }

    protected virtual void OnStateAggroEnter()
    {
    }

    protected virtual void OnStateAggroExit()
    {
    }

    protected virtual void OnStateAttack()
    {
    }

    protected virtual void OnStateAttackEnter()
    {
    }

    protected virtual void OnStateAttackExit()
    {
    }

    //----------------------------------------------------------------------//

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColors[(int)_state];
        Gizmos.DrawSphere(transform.position, 1f);
    }

    //////////////////////////////////////////////////////////////////////////
}