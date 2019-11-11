using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerGunHandler : MonoBehaviour
{
  ParticleSystem particles;
  AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
    particles = GetComponent<ParticleSystem>();
    sound = GetComponent<AudioSource>();
    sound.Stop();
    }

    // Update is called once per frame
    void Update()
    {
    if (CrossPlatformInputManager.GetButton("Fire"))
    {
      var emission = particles.emission;
      emission.enabled = true;
      
    }
    else
    {
      var emission = particles.emission;
      emission.enabled = false;
      sound.Play();
    }
    }
}
