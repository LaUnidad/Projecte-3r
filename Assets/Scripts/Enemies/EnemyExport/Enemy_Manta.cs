using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manta : MonoBehaviour
{
    public State m_CurrentState = State.INITIAL;
    public List<Transform> m_PatrolPoints;

    public float m_RotationalDamp = 0.4f;
    public float m_RotationalDampPatrol = 0.4f;
    public float m_RotationalDampAlert = 1f;
    public float m_Speed = 4f;
    public float m_SpeedPatrol = 4f;
    public float m_SpeedChase = 9f;
    public float m_ReachRadius;
    public float m_MinDistanceToAlert = 19f;
    public float m_MinDistanceToReplace = 25f;
    public float m_DistanceToOutOfRange = 25f;
    public float m_Knockback = 16f;
    public LayerMask m_CollisionLayerMask;
    public List<GameObject> m_AbsorbableItems;
    public float m_MinHeight;

    private float m_CurrentTime;
    private Transform m_Target;
    private Vector3 m_initialPosition;
    private Animator m_Animator;
    private int m_CurrentPatrolPositionId = -1;
    public float m_ConeAngle = 15.0f;
    private bool m_hasPath = false;
    private bool m_isAlive = true;



    public enum State
    {
        INITIAL,
        PATROL,
        ALERT,
        CHASE,
        REPLACE,
        DIE
    }

    // Start is called before the first frame update
    void Start()
    {
        m_initialPosition = transform.position;
        //m_Animator = GetComponent<Animator>();
        //GameManager.Instance.AddRestartGameElement(this);
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentTime += Time.deltaTime;


        if (m_AbsorbableItems.Count <= 0 && m_isAlive)
        {
            ChangeState(State.DIE);
            m_isAlive = false;
        }

        //IN STATE
        switch (m_CurrentState)
        {
            case State.INITIAL:

                ChangeState(State.PATROL);

                break;
            case State.PATROL:

                if (!m_hasPath) MoveToNextPatrolPosition();

                if (Vector3.Distance(transform.position, m_Target.transform.position) <= m_ReachRadius)
                {
                    m_hasPath = false;
                }

                Turn();
                Move();

                if (HearsPlayer() && GameManager.Instance.m_player.transform.position.y > m_MinHeight)
                {
                    ChangeState(State.ALERT);
                }

                break;
            case State.ALERT:

                Turn();


                if (SeesPlayer() && m_CurrentTime >= 1.5f)
                {
                    //_Animator.SetBool("Alert", true);
                    ChangeState(State.CHASE);
                }

                break;
            case State.CHASE:
                Move();

                if (Vector3.Distance(transform.position, m_Target.position) <= m_ReachRadius)
                {
                    ChangeState(State.REPLACE);
                    StartCoroutine(DamagePlayer(m_Knockback, 0.2f));
                }

                if (!SeesPlayer())
                {
                    ChangeState(State.REPLACE);
                }

                break;

            case State.REPLACE:
                Turn();
                Move();

                if (Vector3.Distance(transform.position, m_Target.transform.position) <= m_ReachRadius)
                {
                    ChangeState(State.PATROL);
                }

                break;
            case State.DIE:

                if (m_CurrentTime > 1)
                {
                    //DIE
                    gameObject.SetActive(false);
                }
                break;
        }

        if (GameManager.Instance.m_player.transform.position.y <= m_MinHeight && Vector3.Distance(GameManager.Instance.m_player.transform.position, transform.position) <= m_MinDistanceToAlert)
        {
            ChangeState(State.REPLACE);
            //return;
        }

    }

    void ChangeState(State l_newState)
    {
        //ON EXIT STATE
        switch (m_CurrentState)
        {
               
            case State.PATROL:

                break;
            case State.ALERT:
                break;
            case State.CHASE:
                break;
            case State.REPLACE:
                break;
            case State.DIE:
                break;
        }

        //ON ENTER STATE
        switch (l_newState)
        {
                
            case State.PATROL:
                m_CurrentTime = 0f;
                m_hasPath = false;
                m_RotationalDamp = m_RotationalDampPatrol;
                m_Speed = m_SpeedPatrol;
                CheckDie();
                //Animation Patrol
                break;
            case State.ALERT:
                m_CurrentTime = 0f;
                m_Target = GameManager.Instance.m_player.transform;
                m_RotationalDamp = m_RotationalDampAlert;
                CheckDie();
                break;
            case State.CHASE:
                m_Target = GameManager.Instance.m_player.transform;
                m_CurrentTime = 0f;
                m_RotationalDamp = m_RotationalDampAlert;
                m_Speed = m_SpeedChase;
                CheckDie();
                break;
            case State.REPLACE:
                m_CurrentTime = 0f;
                m_Target = (GetNearestPoint());
                m_RotationalDamp = m_RotationalDampAlert;
                CheckDie();
                break;
            case State.DIE:
                m_CurrentTime = 0f;
                break;
        }

        m_CurrentState = l_newState;

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

    void MoveToNextPatrolPosition()
    {
        ++m_CurrentPatrolPositionId;
        if (m_CurrentPatrolPositionId >= m_PatrolPoints.Count)
            m_CurrentPatrolPositionId = 0;
        m_Target = m_PatrolPoints[m_CurrentPatrolPositionId].transform;
        m_hasPath = true;
    }

    bool SeesPlayer()
    {
        //Nomenclatura player al GameManager m_player > m_Player;
        Vector3 l_Direction = (GameManager.Instance.m_player.transform.position + Vector3.up * 0.8f) - transform.position;
        Ray l_Ray = new Ray(transform.position, l_Direction);

        float l_Distance = l_Direction.magnitude;
        l_Direction /= l_Distance;

        bool l_Collides = Physics.Raycast(l_Ray, l_Distance, m_CollisionLayerMask.value);
        float l_DotAngle = Vector3.Dot(l_Direction, transform.forward);

        Debug.DrawRay(transform.position, l_Direction * l_Distance, l_Collides ? Color.red : Color.yellow);
        
        if (m_CurrentState == State.ALERT) return !l_Collides && l_DotAngle > Mathf.Cos(m_ConeAngle * 0.5f * Mathf.Deg2Rad);
        else if (m_CurrentState == State.CHASE) return !l_Collides && l_DotAngle > Mathf.Cos((m_ConeAngle + 130) * 0.5f * Mathf.Deg2Rad);

        return false;
    }

    Transform GetNearestPoint ()
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in m_PatrolPoints)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    void Turn()
    {
        Vector3 l_pos = m_Target.position - transform.position;
        Quaternion l_rotation = Quaternion.LookRotation(l_pos);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, l_rotation, m_RotationalDamp * Time.deltaTime);
        
    }

    void Move()
    {
         transform.position += transform.forward * m_Speed * Time.deltaTime;
       
    }

    void CheckDie()
    {
        foreach (GameObject item in m_AbsorbableItems)
        {
            if (item == null)
            {
                m_AbsorbableItems.Remove(item);
            }
        }
    }

    IEnumerator DamagePlayer(float sumPos, float inTime)
    {
        var from = GameManager.Instance.m_player.transform.position;
        var to = GameManager.Instance.m_player.transform.position + (-sumPos * GameManager.Instance.m_player.transform.forward.normalized);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            GameManager.Instance.m_player.transform.position = Vector3.Lerp(from, to, t);
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_ReachRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, m_MinDistanceToAlert);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_MinDistanceToReplace);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_DistanceToOutOfRange);


    }

}
