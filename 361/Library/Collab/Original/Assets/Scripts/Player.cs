using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : movement
{

    float h, v;
    Rigidbody2D rb2d;
    WeaponController wc;
    // Start is called before the first frame update
    void Start()
    {
        
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (h != 0 && !isMoving) StartCoroutine(MoveHorizontal(h, rb2d));
        else if (v != 0 && !isMoving) StartCoroutine(MoveVertical(v, rb2d));
    }
    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            wc = GetComponentInChildren<WeaponController>();
            wc.Fire();
        }
    }
}
