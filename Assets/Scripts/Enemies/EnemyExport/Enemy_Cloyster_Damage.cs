using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Cloyster_Damage : MonoBehaviour
{
    public float m_KnockBackForce = 5f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Hit");
            //GameManager.Instance.m_player.GetComponent<ImpactReciever>().AddImpact(KnockbakDirection(), m_KnockBackForce);
            transform.parent.transform.parent.GetComponent<Enemy_Cloyster>().HitPlayer();
            other.gameObject.GetComponent<HippiCharacterController>().PlayerReciveDamage(20);

            /*
            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized;
            other.gameObject.GetComponent<HippieMovement>().KnockBack(hitDirection, 3f);
            */
        }
    }

    Vector3 KnockbakDirection()
    {

        return GameManager.Instance.m_player.transform.position + Vector3.up * 0.9f - transform.position;
    }

}
