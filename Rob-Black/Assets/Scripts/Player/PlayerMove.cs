﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float playerSpeed;

    public Rigidbody rb;

    public Transform pivot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.right * Input.GetAxis("Horizontal") * playerSpeed, ForceMode.Force);
        rb.AddForce(transform.forward * Input.GetAxis("Vertical") * playerSpeed, ForceMode.Force);
    }
}
