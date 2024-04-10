using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rgBD;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rgBD = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        
         anim.SetInteger("Walk", (int)inputX*-1);
        rgBD.velocity = new Vector3(rgBD.velocity.x, rgBD.velocity.y, inputX*-1);
    }
}
