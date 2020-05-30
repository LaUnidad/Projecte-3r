using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mushrock : MonoBehaviour
{

    public State m_CurrentState = State.INITIAL;
    public float m_MinDistanceToTwinkle = 8f;
    public float m_MinDistanceToReActivate = 14f;
    public float m_StartHitTime;
    public float m_EndHitTime;
    public float m_Knockback = 16f;
    public float m_TimeUp = 4f;
    public float m_TimeDown = 0.3f;
    public GameObject m_Collider;
    public GameObject[] m_AbsorbableItems;

    private Animator m_Animator;
    private float m_CurrentTime;
    

    public enum State
    {
        INITIAL,
        IDLE,
        TWINKLE,
        GO_UP,
        GO_DOWN,
        HIT_PLAYER
    }

    // Start is called before the first frame update
    void Start()
    {
        //m_Animator = GetComponent<Animator>();
        //GameManager.Instance.AddRestartGameElement(this);
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentTime += Time.deltaTime;

        //Si no li quede energia baixe
        /*
        if (m_AbsorbableItems.Length == 0)
        {
            ChangeState(State.GO_DOWN);
        }
        */

        switch (m_CurrentState)
        {
            case State.INITIAL:
                ChangeState(State.IDLE);
                break;
            case State.IDLE:

                if (Vector3.Distance(GameManager.Instance.m_player.transform.position, transform.position) <= m_MinDistanceToTwinkle)
                {
                    ChangeState(State.TWINKLE);

                }

                break;
            case State.TWINKLE:

                if (m_CurrentTime >= 0.5f)
                {

                    ChangeState(State.GO_UP);

                }
                break;
            case State.GO_UP:

                //Falte Animació
                //AnimatorClipInfo[] l_AnimationInfo = m_Animator.GetCurrentAnimatorClipInfo(0);
                //float l_AnimNormalizedTime = GetCurrentAnimatorTime(m_Animator);
                //bool l_Hit = l_AnimNormalizedTime > m_StartHitTime && l_AnimNormalizedTime < m_EndHitTime;
                //m_Collider.SetActive(l_Hit);

                //Sense animació
                m_Collider.SetActive(true);
                if (m_CurrentTime >= m_TimeUp)
                {
                    ChangeState(State.GO_DOWN);
                }
           

                break;
            case State.GO_DOWN:
                //Falte Animació
                //AnimatorClipInfo[] l_AnimationInfo = m_Animator.GetCurrentAnimatorClipInfo(0);
                //float l_AnimNormalizedTime = GetCurrentAnimatorTime(m_Animator);
                //bool l_Hit = l_AnimNormalizedTime > m_StartHitTime && l_AnimNormalizedTime < m_EndHitTime;
                //m_Collider.SetActive(l_Hit);

                //Sense animació
                m_Collider.SetActive(true);
                if (m_CurrentTime >= m_TimeDown)
                {
                    ChangeState(State.IDLE);
                }

                break;

        }
    }

    void ChangeState(State l_newState)
    {
        // ON EXIT STATE
        switch (m_CurrentState)
        {
            case State.IDLE:
                break;
            case State.TWINKLE:
                break;
            case State.GO_UP:

                m_Collider.SetActive(false);

                break;
            case State.GO_DOWN:

                m_Collider.SetActive(false);

                break;
          
        }

        // ON ENTER STATE
        switch (l_newState)
        {
            case State.IDLE:
                m_CurrentTime = 0f;
                break;
            case State.TWINKLE:
                m_CurrentTime = 0f;
                //_Animator.SetBool("Twinkle", true);
                //So Twinkle
                break;
            case State.GO_UP:
                m_CurrentTime = 0f;
                StartCoroutine(Move(new Vector3(0, 2.73f, 0), 0.4f));
                //Play Go Up Animation
                //So aixercar
                break;
            case State.GO_DOWN:
                m_CurrentTime = 0f;
                StartCoroutine(Move(new Vector3(0, -2.73f, 0), 0.4f));
                //Play Go Up Animation
                //So baixar
                break;
            
        }

        m_CurrentState = l_newState;

    }

    public void HitPlayer()
    {
        StartCoroutine(DamagePlayer(m_Knockback, 0.2f));
    }

    private float CanReActivateDistance()
    {
        return Vector3.Magnitude(GameManager.Instance.transform.position - transform.position);
    }

    IEnumerator Move(Vector3 sumPos, float inTime)
    {
        var from = transform.position;
        var to = transform.position + sumPos;
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.position = Vector3.Lerp(from, to, t);
            yield return null;
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

    public float GetCurrentAnimatorTime(Animator targetAnim, int layer = 0)
    {
        AnimatorStateInfo animState = targetAnim.GetCurrentAnimatorStateInfo(layer);
        float currentTime = animState.normalizedTime % 1;
        return currentTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, m_MinDistanceToTwinkle);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_MinDistanceToReActivate);
    }

}