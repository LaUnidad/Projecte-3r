using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Cloyster : MonoBehaviour
{
    [Header("Information")]
    public State m_CurrentState = State.INITIAL;
    public List<Transform> m_PatrolPositions;
    public LayerMask m_CollisionLayerMask;

    [Header("Attributes")]
    public float m_Speed = 20f;
    public float m_ChaseSpeed = 9f;
    public float m_MinDistanceToAlert = 19f;
    public float m_DistanceToOutOfRange = 25f;
    public float m_MinDistanceToAttack = 3.0f;
    public float m_MaxDistanceToAttack = 7.0f;
    public float m_MaxDistanceToPatrol = 5.0f;
    public float m_MaxTimeToWaitAlert = 5.0f;
    public float m_MaxTimeToWaitToAttack = 6f;
    public float m_MaxTimeToAbsorb = 4f;
    public float m_rotationSpeed = 65.0f;
    public float m_ConeAngle = 15.0f;
    public float m_LerpAttackRotation = 0.6f;
    public GameObject[] m_AbsorbableItems;

    private int m_CurrentPatrolPositionId = -1;
    private float m_CurrentTime;
    private float m_StartAlertRotation = 0.0f;
    private float m_CurrentAlertRotation = 0.0f;
    private Vector3 m_playerPos;
    private Vector3 m_initialPosition;
    private Animator m_Animator;
    private NavMeshAgent m_NavMeshAgent;

    public enum State
    {
        INITIAL,
        PATROL,
        ALERT,
        CHASE,
        WAIT_TO_ATTACK,
        DIE
    }

    // Start is called before the first frame update
    void Start()
    {

        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_initialPosition = transform.position;
        //m_Animator = GetComponent<Animator>();
        //GameManager.Instance.AddRestartGameElement(this);
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentTime += Time.deltaTime;

        //Si no li quede energia mor
        /*
        if (m_AbsorbableItems.Length == 0)
        {
            ChangeState(State.DIE);
        }
        */

        // IN STATE
        switch (m_CurrentState)
        {
            case State.INITIAL:
                ChangeState(State.PATROL);
                break;
            case State.PATROL:

                if (!m_NavMeshAgent.hasPath && m_NavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
                    MoveToNextPatrolPosition();

                if (HearsPlayer())
                {
                    ChangeState(State.ALERT);
                }

                break;
            case State.ALERT:

                RotateTowards(GameManager.Instance.m_player.transform);

                if (SeesPlayer())
                {
                    //_Animator.SetBool("Alert", true);
                    ChangeState(State.CHASE);
                }
                else if (m_CurrentAlertRotation >= 360 || m_CurrentTime > m_MaxTimeToWaitAlert)
                {
                    ChangeState(State.PATROL);
                }

                break;
            case State.CHASE:

                if (!OutOfRange())
                {
                    ChangeState(State.PATROL);
                }
                if (m_CurrentTime >= 0.5f)
                {
                    MoveToPoint(GameManager.Instance.m_player.transform.position);
                }

                if (m_CurrentTime > m_MaxTimeToWaitToAttack)
                {
                    ChangeState(State.WAIT_TO_ATTACK);
                }

                break;
            case State.WAIT_TO_ATTACK:

                if (m_CurrentTime > m_MaxTimeToAbsorb)
                {
                    ChangeState(State.ALERT);
                }

                break;
               
            case State.DIE:

                if (m_CurrentTime > 5)
                {
                    //DIE
                    gameObject.SetActive(false);
                }
                break;
        }

    }

    void ChangeState(State l_newState)
    {
        // ON EXIT STATE
        switch (m_CurrentState)
        {
            case State.PATROL:

                m_NavMeshAgent.SetDestination(this.transform.position);
                
                break;
            case State.ALERT:

                //m_NavMeshAgent.isStopped = false;

                break;
            case State.CHASE:

                m_NavMeshAgent.SetDestination(this.transform.position);
                m_NavMeshAgent.speed = m_Speed;

                break;
            case State.WAIT_TO_ATTACK:

                //m_Animator.SetTrigger("Close");

                break;
                
            case State.DIE:
                break;
        }

        //ON ENTER STATE
        switch (l_newState)
        {
            case State.PATROL:

                m_CurrentTime = 0f;
                m_NavMeshAgent.isStopped = false;
                //m_Animator.SetBool("Walk", true);

                break;
            case State.ALERT:

                m_CurrentTime = 0f;
                m_CurrentAlertRotation = 0.0f;
                m_StartAlertRotation = transform.rotation.y;

                break;
            case State.CHASE:

                m_CurrentTime = 0f;
                m_NavMeshAgent.speed = m_ChaseSpeed;
                MoveToPoint(GameManager.Instance.m_player.transform.position);

                break;
            case State.WAIT_TO_ATTACK:

                m_CurrentTime = 0f;
                //m_Animator.SetTrigger("Open");

                break;

            case State.DIE:

                m_CurrentTime = 0f;
                //m_Animator.SetTrigger("Die");

                break;

        }

        m_CurrentState = l_newState;
    }

    void MoveToNextPatrolPosition()
    {
        ++m_CurrentPatrolPositionId;
        if (m_CurrentPatrolPositionId >= m_PatrolPositions.Count)
            m_CurrentPatrolPositionId = 0;
        m_NavMeshAgent.SetDestination(m_PatrolPositions[m_CurrentPatrolPositionId].position);
    }

    bool SeesPlayer()
    {
        //Nomenclatura player al GameManager m_player > m_Player;
        Vector3 l_Direction = (GameManager.Instance.m_player.transform.position + Vector3.up * 0.9f) - transform.position;
        Ray l_Ray = new Ray(transform.position, l_Direction);

        float l_Distance = l_Direction.magnitude;
        l_Direction /= l_Distance;

        bool l_Collides = Physics.Raycast(l_Ray, l_Distance, m_CollisionLayerMask.value);
        float l_DotAngle = Vector3.Dot(l_Direction, transform.forward);

        Debug.DrawRay(transform.position, l_Direction * l_Distance, l_Collides ? Color.red : Color.yellow);
        return !l_Collides && l_DotAngle > Mathf.Cos(m_ConeAngle * 0.5f * Mathf.Deg2Rad);
    }

    bool HearsPlayer()
    {
        //Nomenclatura player al GameManager m_player > m_Player;
        return GetSqrDistanceXZToPosition(GameManager.Instance.m_player.transform.position) < (m_MinDistanceToAlert * m_MinDistanceToAlert);
    }

    bool OutOfRange()
    {
        //Nomenclatura player al GameManager m_player > m_Player;
        return GetSqrDistanceXZToPosition(GameManager.Instance.m_player.transform.position) < (m_DistanceToOutOfRange * m_DistanceToOutOfRange);
    }
    public float GetSqrDistanceXZToPosition(Vector3 a)
    {
        Vector3 distancia = a - transform.position;
        distancia.y = 0;

        float dist = distancia.sqrMagnitude;

        return dist;
    }

    private void MoveToPoint (Vector3 destination)
    {
        m_NavMeshAgent.isStopped = false;
        //m_NavMeshAgent.enabled = true;
        m_NavMeshAgent.SetDestination(destination);

    }

    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * m_rotationSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, m_MinDistanceToAlert);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_DistanceToOutOfRange);
    }
}
