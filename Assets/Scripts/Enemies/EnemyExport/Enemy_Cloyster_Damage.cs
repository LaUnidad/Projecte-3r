using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Cloyster_Damage : MonoBehaviour
{

    Animator m_animator;

    public float m_KnockBackForce = 5f;
    

    // Start is called before the first frame update
    void Start()
    {
        m_animator = transform.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            m_animator.SetTrigger("Collision");

            Debug.Log("Hit");
            //GameManager.Instance.m_player.GetComponent<ImpactReciever>().AddImpact(KnockbakDirection(), m_KnockBackForce);
            transform.GetComponent<Enemy_Cloyster>().HitPlayer();
            other.gameObject.GetComponent<HippiCharacterController>().PlayerTakeDamage(20);

            /*
            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized;
            other.gameObject.GetComponent<HippieMovement>().KnockBack(hitDirection, 3f);
            */

            //other.gameObject.GetComponent<HippieMovement>().KnockbackV2(16f, .2f);
        }
    }

    Vector3 KnockbakDirection()
    {

        return GameManager.Instance.m_player.transform.position + Vector3.up * 0.9f - transform.position;
    }

}
