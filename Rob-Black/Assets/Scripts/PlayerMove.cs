using System.Collections;
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

    // Update is called once per frame
    void Update()
    {


        // It works it's just WASD doesn't work too well
        // WASD movement

        rb.AddForce(transform.right * 10 * Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed);
        rb.AddForce(transform.forward * 10 * Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed);

    }
}
