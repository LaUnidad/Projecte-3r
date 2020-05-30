using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public State m_CurrentState = State.INITIAL;
    public List<Transform> m_PatrolPositions;
    public float m_MinDistanceToAlert = 5.0f;
    public LayerMask m_CollisionLayerMask;
    public float m_MinDistanceToAttack = 3.0f;
    public float m_MaxDistanceToAttack = 7.0f;
    public float m_MaxDistanceToPatrol = 5.0f;
    public float m_rotationSpeed = 30.0f;
    public float m_ConeAngle = 60.0f;
    public float m_LerpAttackRotation = 0.6f;

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
        HIT_PLAYER
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

                m_CurrentAlertRotation += m_rotationSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, transform.rotation.y + m_CurrentAlertRotation, 0);

                if (SeesPlayer())
                {
                    //_Animator.SetBool("Alert", true);
                    ChangeState(State.CHASE);
                }
                else if (m_CurrentAlertRotation >= 360 || m_CurrentTime > 5f)
                {

                }

                break;
            case State.CHASE:
                m_NavMeshAgent.SetDestination(m_playerPos);

                break;
            case State.WAIT_TO_ATTACK:
                break;
            case State.HIT_PLAYER:
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
                break;
            case State.CHASE:
                break;
            case State.WAIT_TO_ATTACK:
                break;
            case State.HIT_PLAYER:
                break;
        }

        //ON ENTER STATE
        switch (m_CurrentState)
        {
            case State.PATROL:

                m_CurrentTime = 0f;
                //m_Animator.SetBool("Walk", true);

                break;
            case State.ALERT:

                m_CurrentTime = 0f;
                m_CurrentAlertRotation = 0.0f;
                m_StartAlertRotation = transform.localRotation.y;

                break;
            case State.CHASE:

                m_CurrentTime = 0f;
                //Nomenclatura player al GameManager m_player > m_Player;
                m_playerPos = GameManager.Instance.m_player.transform.position;
                m_NavMeshAgent.ResetPath();

                break;
            case State.WAIT_TO_ATTACK:

                m_CurrentTime = 0f;

                break;
            case State.HIT_PLAYER:

                m_CurrentTime = 0f;

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
    public float GetSqrDistanceXZToPosition(Vector3 a)
    {
        Vector3 distancia = a - transform.position;
        distancia.y = 0;

        float dist = distancia.sqrMagnitude;

        return dist;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, m_MinDistanceToAlert);
    }
}
