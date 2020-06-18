using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (BLACKBOARD_Aspiradora))]
public class Aspiradora : MonoBehaviour
{
    // Start is called before the first frame update
    BLACKBOARD_Aspiradora blackboard;
    public GameObject AspirableObj;
    public static List<GameObject> AspirableObjects = new List<GameObject>();
    int AspirableObjectsSize;
    float minDist;
    GameObject MinDistObject;
    float Force;
    public GameObject NewLookAtObject;
    public float ListObjects;
    float MaxObj;
    GameObject MaxObject;

    GameObject Player;

    public float Biomass;

    public float MaxBiomass;

    public GameObject LastObjectCatched;

    private GameObject DestroyDoors;

    private GameObject[] LeavesBT;

    CamAnimations camAnimations;

    public bool startFinalCamShake = false;

    public GameObject[] Cinematicas;

    public int wichCinematicYouWant;

    private FMOD.Studio.EventInstance AbsorbSound0, AbsorbSound1, AbsorbSound2;
    private int soundN = 0;

    void Start()
    {
        camAnimations = FindObjectOfType<CamAnimations>();
        blackboard = GetComponent<BLACKBOARD_Aspiradora>();
        Player = GameObject.FindGameObjectWithTag("Player");
        DestroyDoors = GameObject.FindGameObjectWithTag("DestroyDoors");
        LeavesBT = GameObject.FindGameObjectsWithTag("LeavesBigTree");
        Cinematicas = GameObject.FindGameObjectsWithTag("Cinematic");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(AspirableObjects.Count);
        ListObjectsIn();
        //Debug.Log(ObjectToLookAt());
        //Debug.Log(AspirableObjects.Count);
        if(AspirableObjects.Count == 0)
        {
            MinDistObject = null;
            minDist = 0;
            MaxObject = null;
            MaxObj = 1;
        }
        //TimeToDisapear();
    }
    public void AddObjects(GameObject x)
    {
        //Debug.Log("AÑADIENDO MIERDA");
        AspirableObjects.Add(x);
        x.gameObject.GetComponent<AspirableObject>().IAmInList = true;   
    }
    public void RemoveObjects(GameObject x)
    {
        //Debug.Log("BORRANDO MIERDA");
        AspirableObjects.Remove(x);
        x.gameObject.GetComponent<AspirableObject>().IAmInList = false;    
    }

    public GameObject ObjectToLookAt()
    {
        if(AspirableObjects.Count != 0)
        {
            foreach(GameObject obj in AspirableObjects)
            {
                //Debug.Log("EOEO");
                WithObjectIsNear(obj.gameObject);
            }   
        }
        else
        {
            //Debug.Log("NoHayObjetoAlQueMirar");
            MinDistObject = null;
        }
        return MinDistObject; 
        
    }
    public GameObject BiggestObject()
    {
        foreach(GameObject obj in AspirableObjects)
        {
            WithObjectIsBigger(obj.gameObject);
        }
        return MaxObject;
    }
    public void WithObjectIsNear(GameObject x)
    {
        if(x != null)
        {
            float actualdist;       
            actualdist = Vector3.Distance(this.transform.position, x.transform.position);
            if(actualdist < minDist || minDist == 0)
            {
                minDist = actualdist;
                MinDistObject = x;
            } 
        }
        else
        {
            //Debug.Log("NO HAY OBJETO; NI CERCA NI LEJOS");
        } 
    }
    public void WithObjectIsBigger(GameObject x)
    {
        if(x != null)
        {
            float actualSize;        
            actualSize = Vector3.Distance(this.transform.position, x.transform.position);
            if(actualSize > MaxObj || MaxObj == 1)
            {
                MaxObj = actualSize;
                MaxObject = x;
            }   
        }
        else
        {
            //Debug.Log("NO HAY OBJETO; NI GRANDE NI PEQUEÑO");
        }
       
    }
    public float ForceToThePlayer()
    {
        GameObject x;
        x = BiggestObject();
        if(x != null)
        {
            foreach(GameObject obj in AspirableObjects)
            {
                if(obj == x)
                {
                    Force =  x.GetComponent<AspirableObject>().ForceToAbsorb;
                }
            }
        }
        else
        {
            Force = 1;
        }
        return Force;
        
    }
    public void ListObjectsIn()
    {
        ListObjects = AspirableObjects.Count;
    }

    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "AspirableObject" && Player.GetComponent<HippiCharacterController>().Absorving == true)
        {
            if(other.gameObject.GetComponent<AspirableObject>().IAmMagnetic == false)
            {
                //Debug.Log(other.gameObject.GetComponent<AspirableObject>().Heart);
                RemoveObjects(other.gameObject);
                Player.GetComponent<HippiCharacterController>().blackboard.Biomassa += other.gameObject.GetComponent<AspirableObject>().Biomass;
                Player.GetComponent<HippiCharacterController>().SumLife(1);
                int n = Random.Range(0, 3);
                if (n == 0) if (!SoundManager.Instance.isPlaying(AbsorbSound0)) AbsorbSound0 = SoundManager.Instance.PlayEvent(GameManager.Instance.AbsorbableBig, GameManager.Instance.m_player.transform);
                    else if (n == 1) if (!SoundManager.Instance.isPlaying(AbsorbSound1)) AbsorbSound1 = SoundManager.Instance.PlayEvent(GameManager.Instance.AbsorbableNormal, transform);
                        else if (!SoundManager.Instance.isPlaying(AbsorbSound2)) AbsorbSound2 = SoundManager.Instance.PlayEvent(GameManager.Instance.AbsorbableSmall, transform);
                if (!SoundManager.Instance.isPlaying(AbsorbSound0)) AbsorbSound0 = SoundManager.Instance.PlayEvent(GameManager.Instance.AbsorbableBig, transform);
                //if (!SoundManager.Instance.isPlaying(AbsorbSound1)) AbsorbSound1 = SoundManager.Instance.PlayEvent(GameManager.Instance.AbsorbableNormal, transform);
                //if (!SoundManager.Instance.isPlaying(AbsorbSound2)) AbsorbSound2 = SoundManager.Instance.PlayEvent(GameManager.Instance.AbsorbableSmall, transform);
                //Player.GetComponent<HippiCharacterController>().currentHealth += 0.5f;
                if (other.gameObject.GetComponent<AspirableObject>().Heart)
                {
                    Debug.Log("ASPIRADO A THE FUCKING BIGONE");
                    ActivateTheCorrectCinematic();
                    Player.GetComponent<HippiCharacterController>().AfectedByTheGas = true;
                    Player.GetComponent<HippiCharacterController>().blackboard.ResistanceToTheGas = 3;
                    Player.GetComponent<HippiCharacterController>().SumLife(100);
                    Player.GetComponent<HippiCharacterController>().blackboard.RoketMan = true;
                    DestroyDoors.GetComponent<DestroyDoors>().DestroyAllDoors();
                    camAnimations.FinalCameraShakeStart();
                    foreach(GameObject obj in LeavesBT)
                    {
                        Destroy(obj.gameObject);
                    }
                   Destroy(other.gameObject);
                   //KillPlanet();
                   
                }

                Destroy(other.gameObject);
            }
            else
            {
                other.gameObject.GetComponent<AspirableObject>().PlayerForward = Player.gameObject.transform.forward;
                //other.gameObject.GetComponent<AspirableObject>().PlayerForward = -other.gameObject.transform.forward;
                other.gameObject.GetComponent<AspirableObject>().Shooting();
                RemoveObjects(other.gameObject);
                
               
            }    
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "AspirableObject" && Player.GetComponent<HippiCharacterController>().Absorving == true)
        {
            if(other.gameObject.GetComponent<AspirableObject>().IAmMagnetic == false)
            {
                //Debug.Log("ASPIRADO");
                RemoveObjects(other.gameObject);
                Biomass += other.gameObject.GetComponent<AspirableObject>().Biomass;
                Player.GetComponent<HippiCharacterController>().SumLife(other.gameObject.GetComponent<AspirableObject>().LifeForThePlayer);
                
                Destroy(other.gameObject);
            }
        }
    }

    public void KillPlanet()
    {
        Debug.Log("ULTIM ITEM MORT");
       
        Player.GetComponent<HippiCharacterController>().AfectedByTheGas = true;
        Player.GetComponent<HippiCharacterController>().blackboard.ResistanceToTheGas = 3;  
        Player.GetComponent<HippiCharacterController>().blackboard.RoketMan = true;
        DestroyDoors.GetComponent<DestroyDoors>().DestroyAllDoors();
        foreach(GameObject obj in LeavesBT)
        {
            Destroy(obj.gameObject);
        }
        startFinalCamShake = true;

        SoundManager.Instance.PlayOneShotSound(GameManager.Instance.Earthquake, transform);

        //Start final camera shake
        
        
    }
    public void ActivateTheCorrectCinematic()
    {
        foreach(GameObject obj in Cinematicas)
        {
            if(obj.GetComponent<Cinamatic1>().CinematicOrder == wichCinematicYouWant)
            {
                obj.GetComponent<Cinamatic1>().ImActive = true;
                obj.gameObject.SetActive(true);
            }
        }
    }
}
