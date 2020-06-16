using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class AnimationEvents : MonoBehaviour
{
    public GameObject runDustParticles;

    public LayerMask grassLayer;
    public LayerMask rockLayer;

    
    void RunStep()
    {
        SoundManager.Instance.PlayOneShotSound(GameManager.Instance.stepGround, GameManager.Instance.m_player.transform);
        Instantiate(runDustParticles, transform.position, runDustParticles.transform.rotation);
        /*
        Ray groundRay = new Ray(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), -Vector3.up);
        RaycastHit raycastHit;

        if (Physics.Raycast(groundRay, out rayHit, 2f, grassLayer.value))
            SoundManager.Instance.PlayOneShotSound(GameManager.Instance.stepGrass, GameManager.Instance.m_player.transform);

        if (Physics.Raycast(groundRay, out rayHit, 2f, rockLayer.value))
            SoundManager.Instance.PlayOneShotSound(GameManager.Instance.stepGround, GameManager.Instance.m_player.transform);
        
        
        if (Physics.Raycast(groundRay, out raycastHit, 4f))
        {
            Debug.DrawLine(transform.position, raycastHit.point, Color.red, 5f);
            if (raycastHit.collider.transform.tag == "Grass")
                SoundManager.Instance.PlayOneShotSound(GameManager.Instance.stepGrass, GameManager.Instance.m_player.transform);
            if (raycastHit.collider.transform.tag == "Canyon")
                SoundManager.Instance.PlayOneShotSound(GameManager.Instance.stepGround, GameManager.Instance.m_player.transform);

        }
        */

    }
    
    /*
    void RunStep()
    {
        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if(hit.transform.tag == "Grass")
                SoundManager.Instance.PlayOneShotSound(GameManager.Instance.stepGrass, GameManager.Instance.m_player.transform);
            if(hit.transform.tag == "Canyon")
                SoundManager.Instance.PlayOneShotSound(GameManager.Instance.stepGround, GameManager.Instance.m_player.transform);
        }
    }
    */
    /*
    void RunStepL()
    {
        SoundManager.Instance.PlayOneShotSound(GameManager.Instance.stepGrass, GameManager.Instance.m_player.transform);

    }
    */
    void GrassStep()
    {

    }

    void RockStep()
    {

    }
}
