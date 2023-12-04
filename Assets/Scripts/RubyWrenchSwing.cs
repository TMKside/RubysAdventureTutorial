using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyWrenchSwing : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteOn;
    public CapsuleCollider2D hitbox;
    
    // Code Written by Isaiah, sprite made by Juliana
    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            spriteOn.enabled = false;
            hitbox.enabled = false;
            //print("hey~");
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("WrenchSwing"))
        {
            spriteOn.enabled = true;
            hitbox.enabled = true;
            //print(":)");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("WrenchSwing");
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.Fix();
        }
    }
}
