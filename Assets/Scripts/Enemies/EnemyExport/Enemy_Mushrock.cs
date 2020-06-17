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
    public List<GameObject> m_AbsorbableItems;

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
        m_Animator = transform.GetComponent<Animator>();
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

                //Suficient aprop i amb energia
                if (Vector3.Distance(GameManager.Instance.m_player.transform.position, transform.position) <= m_MinDistanceToTwinkle && m_AbsorbableItems.Count > 0)
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

                foreach (GameObject item in m_AbsorbableItems)
                {
                    if (item == null) m_AbsorbableItems.Remove(item);
                }

                if (m_CurrentTime >= m_TimeUp || m_AbsorbableItems.Count <= 0)
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
                m_Animator.SetBool("Idle", false);
                m_Collider.SetActive(false);
                break;
            case State.TWINKLE:
                break;
            case State.GO_UP:

                m_Collider.SetActive(false);

                break;
            case State.GO_DOWN:

                m_Collider.SetActive(false);
                SetActiveEnergy();

                break;
          
        }

        // ON ENTER STATE
        switch (l_newState)
        {
            case State.IDLE:
                m_CurrentTime = 0f;
                m_Animator.SetBool("Idle", true);
                m_Collider.SetActive(false);
                break;
            case State.TWINKLE:
                m_CurrentTime = 0f;
                //_Animator.SetBool("Twinkle", true);
                //So Twinkle
                //SoundManager.Instance.PlayOneShotSound(GameManager.Instance.E1_Twincle, transform);
                break;
            case State.GO_UP:
                m_CurrentTime = 0f;
                SetActiveEnergy();
               // StartCoroutine(Move(new Vector3(0, 3.73f, 0), 0.4f));
                //Play Go Up Animation
                m_Animator.SetTrigger("Go Up");

                //So aixercar
                SoundManager.Instance.PlayOneShotSound(GameManager.Instance.E1_Up, transform);
                break;
            case State.GO_DOWN:
                m_CurrentTime = 0f;
                //StartCoroutine(Move(new Vector3(0, -3.73f, 0), 0.4f));
                //Play Go Up Animation
                //So baixar
                SoundManager.Instance.PlayOneShotSound(GameManager.Instance.E1_Down, transform);
                break;
            
        }

        m_CurrentState = l_newState;

    }

    void SetActiveEnergy()
    {
        foreach (GameObject item in m_AbsorbableItems)
        {
            if (item != null)
            {
                if (item.activeSelf) item.SetActive(false);
                else item.SetActive(true);
            }
            else
            {
                m_AbsorbableItems.Remove(item);
            }
         
        }

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