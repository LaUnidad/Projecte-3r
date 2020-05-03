using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMushroom : MonoBehaviour
{
    Animation anim;
    public AnimationClip mushroomJumpAnimClip;

    private bool isActivated;

    List<GameObject> fruitList;

    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
        anim = GetComponent<Animation>();
        fruitList = new List<GameObject>();

        foreach (GameObject fruit in fruitList)
        {
            fruit.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
       // de
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Trigger in Mushroom");
            if(Input.GetMouseButton(0) && !isActivated)
            {
                anim.Play(mushroomJumpAnimClip.name);
                isActivated = true;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Trigger in Mushroom");
            if (Input.GetMouseButton(0) && !isActivated)
            {
                anim.Play(mushroomJumpAnimClip.name);
                isActivated = true;
            }
        }
    }
}
