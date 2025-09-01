using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public List<Transform> Waypoints = new List<Transform>();
    [SerializeField] public float ChaseDistance;
    [SerializeField] public Player Player;

    public PatrolState PatrolState = new PatrolState();
    public ChaseState ChaseState = new ChaseState();
    public RetreatState RetreatState = new RetreatState();
    public Animator Animator;

    [HideInInspector] public NavMeshAgent NavMeshAgent;

    private BaseState _currentState;

    public void SwitchState(BaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        _currentState = PatrolState;
        _currentState.EnterState(this);
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (Player != null)
        {
            Player.OnPowerUpStart += StartRetreating;
            Player.OnPowerUpStop += StopRetreating;
        }
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }

    private void StartRetreating()
    {
        SwitchState(RetreatState);
    }

    private void StopRetreating()
    {
        SwitchState(PatrolState);
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    /// Called when the enemy collides with another object.
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the enemy is not in the retreat state
        if (_currentState != RetreatState)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().Dead();
            }
        }
    } 
}
