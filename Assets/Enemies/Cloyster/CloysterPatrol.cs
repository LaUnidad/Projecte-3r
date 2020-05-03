using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloysterPatrol : MonoBehaviour
{
    public float speed;
    public float startWaitTime;
    private float waitTime;

    public Transform[] moveSpots;
    private int randomSpot;

    Animation anim;
    public AnimationClip cloysterOpenAnimClip;
    bool playAnim = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Wait Time: " + waitTime);

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(moveSpots[randomSpot].position.x, transform.position.y, moveSpots[randomSpot].position.z), speed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (playAnim)
            {
                anim.Play(cloysterOpenAnimClip.name);
                playAnim = false;
            }

            if (waitTime < 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
                playAnim = true;
            }
            else
            {
                waitTime -= Time.deltaTime;
                playAnim = false;
            }
        }
    }
}
