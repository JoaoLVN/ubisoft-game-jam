using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehaviour))]
public class EnemyAIMelee : EnemyAI
{
    //////////////////////////////////////////////////////////////////////////

    [Header("Patrol", order = 0)]
    [Range(0f, 10f)]
    public float patrolDistance;
    public float patrolSpeed;

    [Header("Aggro", order = 1)]
    [Range(0f, 10f)]
    public float aggroRange;
    public float aggroOvertime;
    public float aggroSpeed;

    //--CONTROL-VALUES------------------------------------------------------//

    protected Vector2 _playerOffset;
    protected Vector2 _playerDistance;
    protected Vector2 _playerDirection;
    protected float _overtime;
    protected Vector2 _distanceToPatrol;
    protected Vector2 _initialPatrolPoint;
    protected float _distancePatrolled;

    //--HIDDEN-REFERENCES---------------------------------------------------//

    protected EnemyBehaviour _character;

    //////////////////////////////////////////////////////////////////////////

    protected override void Awake()
    {
        base.Awake();

        _character = GetComponent<EnemyBehaviour>();
    }

    protected override void Update()
    {
        _playerOffset = _player.position - _transform.position;
        _playerDistance = new Vector2(Mathf.Abs(_playerOffset.x), Mathf.Abs(_playerOffset.y));
        _playerDirection = _playerOffset.normalized;

        base.Update();
    }

    //----------------------------------------------------------------------//

    protected override void OnStatePatrolEnter()
    {
        _initialPatrolPoint = _transform.position;
        _distanceToPatrol = Random.insideUnitSphere * patrolDistance;
        _character.MoveSpeed = patrolSpeed;
    }

    protected override void OnStatePatrol()
    {
        // Patrol an area specified in patrolDistance
        Vector2 direction = _distanceToPatrol.normalized;
        _character.Move(direction);

        //TODO: Get a more accurate reading of distance travelled
        _distancePatrolled += _character.MoveSpeed * Time.deltaTime;

        // Check when to turn around during patrol
        if (_distancePatrolled > _distanceToPatrol.magnitude)
        {
            _distanceToPatrol = (_initialPatrolPoint - (Vector2)_transform.position) +
                ((Vector2)Random.insideUnitSphere * patrolDistance);
            _distancePatrolled = 0f;
        }

        // Check if player is in aggro range
        if (_playerDistance.magnitude <= aggroRange)
            _state = AI_STATE.AGGRO;
    }

    protected override void OnStateAggroEnter()
    {
        _character.MoveSpeed = aggroSpeed;
    }

    protected override void OnStateAggro()
    {
        _character.Move(_playerDirection);

        if (_playerDistance.magnitude > aggroRange)
        {
            _overtime += Time.deltaTime;

            if (_overtime >= aggroOvertime)
                _state = AI_STATE.PATROL;
        }
        else
        {
            _overtime = 0f;

            if (_playerDistance.magnitude < _character.Range)
                _state = AI_STATE.ATTACK;
        }
    }

    protected override void OnStateAttack()
    {
        base.OnStateAttack();

        _character.Move(Vector2.zero);
        _character.Attack(_player.GetComponent<Character>());

        if (_playerDistance.magnitude > _character.Range)
            _state = AI_STATE.AGGRO;
    }

    //----------------------------------------------------------------------//

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        Gizmos.color = new Color(1f, 0f, 1f, .25f);
        Gizmos.DrawSphere(transform.position, aggroRange);

        if (_state == AI_STATE.PATROL)
        {
            Gizmos.color = new Color(0f, 1f, 0f, .5f);

            if (_lateState == AI_STATE.OFF)
                Gizmos.DrawSphere(transform.position, patrolDistance);
            else
                Gizmos.DrawSphere(_initialPatrolPoint, patrolDistance);
        }
    }

    //////////////////////////////////////////////////////////////////////////
}