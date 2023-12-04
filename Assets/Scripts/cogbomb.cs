using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cogbomb : MonoBehaviour
{
    // Start is called before the first frame update
    // Cog attacks were coded by Juliana
    public ParticleSystem cogfx;
    private RubyController rubyController;

    void Start()
    {
        GameObject rubyControllerObject = GameObject.FindWithTag("RubyController"); //this line of code finds the RubyController script by looking for a "RubyController" tag on Ruby

        if (rubyControllerObject != null)
        {
            rubyController = rubyControllerObject.GetComponent<RubyController>(); //and this line of code finds the rubyController and then stores it in a variable
            print ("Found the RubyConroller Script!");
        }

        if (rubyController == null)
        {
            print ("Cannot find GameController Script!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}
