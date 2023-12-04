using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSplash : MonoBehaviour
{
    // All code, visuals, and sounds were made by Juliana
    // Start is called before the first frame update
    AudioSource splashSource;
    public AudioClip splashClip;
    ParticleSystem splash;
    private int nParticles = 0;

    void Start()
    {
        splash = GetComponent<ParticleSystem>();
        splashSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        int count = splash.particleCount;
        if (count > nParticles)
        {
            print ("splash");
            splashSource.PlayOneShot(splashClip, 0.5f);
        }
        nParticles = count;
    }
}
