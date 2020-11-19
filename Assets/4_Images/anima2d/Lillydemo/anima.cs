using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anima : MonoBehaviour
{

    Animator animator ;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
        animator.SetBool("test", false);
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("test", true);
        }
    }
}
