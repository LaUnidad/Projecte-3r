using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    [SerializeField]
    private int m_distanceFromPlayer = 5;

    private GameObject m_player;

    public List<ActivatorItem> m_activatorItems;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameManager.Instance.m_player;
        m_activatorItems = new List<ActivatorItem>();

        StartCoroutine("CheckActivation");
    }

    IEnumerator CheckActivation()
    {
        List<ActivatorItem> l_removeList = new List<ActivatorItem>();

        if (m_activatorItems.Count > 0)
        {
            foreach (ActivatorItem item in m_activatorItems)
            {
                if (Vector3.Distance(m_player.transform.position, item.itemPos) > m_distanceFromPlayer)
                {
                    if (item.item == null)
                    {
                        l_removeList.Add(item);
                    }

                    else
                    {
                        item.item.SetActive(false);
                    }
                }
                else
                {
                    if (item.item == null)
                    {
                        l_removeList.Add(item);
                    }
                    else
                    {
                        item.item.SetActive(true);
                    }
                }
            }
        }

        yield return new WaitForSeconds(0.01f);

        if (l_removeList.Count > 0)
        {
            foreach (ActivatorItem item in l_removeList)
            {
                m_activatorItems.Remove(item);
            }
        }

        yield return new WaitForSeconds(0.01f);
        StartCoroutine("CheckActivation");
    }

   
}

public class ActivatorItem 
{
    public GameObject item;
    public Vector3 itemPos;
}

