using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSounds : MonoBehaviour
{
    [SerializeField]
    private AudioSource playerFootAudio;
    [SerializeField]
    private AudioClip footClip;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnFootStep()
    {
        playerFootAudio.PlayOneShot(footClip);
    }
   
}
