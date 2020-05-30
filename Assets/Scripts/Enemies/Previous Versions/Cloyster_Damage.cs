using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloyster_Damage : MonoBehaviour
{
    private float m_timer;
    private float m_timerTotal;

    public float m_knockback = 5;
    public float m_damage = 20f;

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
            StartCoroutine(DamagePlayer(m_knockback, 0.2f));
            GameManager.Instance.m_player.GetComponent<HippiCharacterController>().PlayerReciveDamage(m_damage);
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
}
