using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushrock_Beta : MonoBehaviour
{

    public float m_near = 7.5f;
    public float m_tooNear = 5.5f;
    public float m_detectionDistance;
    public float m_damage = 20f;

    private bool m_isUp = false;
    private float m_timerTotal = 4f;
    private float m_timer = 4f;
    private float m_timerKnockback = 0.3f;
    private bool m_canDamage = false;
    private float m_knockback = 4f;
    private bool m_canKnockback = true;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        

        if (Vector3.Distance(GameManager.Instance.m_player.transform.position, transform.position) <= m_near && !m_isUp && m_canDamage)
        {
            StartCoroutine(FlyMe(new Vector3(0, 3.38f, 0), 0.4f));
            m_isUp = true;
            m_canDamage = false;

        }

        if (m_isUp) m_timerKnockback -= Time.deltaTime;

        if (Vector3.Distance(GameManager.Instance.m_player.transform.position, transform.position) <= m_tooNear && m_isUp && m_canKnockback && m_timerKnockback >= 0)
        {
            StartCoroutine(DamagePlayer(m_knockback, 0.2f));
            GameManager.Instance.m_player.GetComponent<HippiCharacterController>().PlayerReciveDamage(m_damage);
            //Debug.Log("lol");
            m_canKnockback = false;
        }

        if (m_isUp)
        {
            m_timer -= Time.deltaTime;

            if (m_timer < 0)
            {
                m_timer = m_timerTotal;
                StartCoroutine(FlyMe(new Vector3(0, -3.38f, 0), 0.4f));
                if (Vector3.Distance(GameManager.Instance.m_player.transform.position, transform.position) <= 4.5f)
                {
                    StartCoroutine(DamagePlayer(m_knockback, 0.2f));
                    GameManager.Instance.m_player.GetComponent<HippiCharacterController>().PlayerReciveDamage(m_damage);
                    
                }
                m_isUp = false;
            }
        }

    }

    IEnumerator FlyMe(Vector3 sumPos, float inTime)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_canDamage = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_canDamage = false;
            m_canKnockback = true;
            m_timerKnockback = 0.3f;
        }
    }
}
