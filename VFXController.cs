using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    [SerializeField] private ParticleSystem trailsVFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }
     public void StartTrails()
    {
        trailsVFX.Play();
    }
  
}
