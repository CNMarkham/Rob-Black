using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float playerSpeed;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // look at mouse

        // Vector3 mousePos = Input.mousePosition;
        // mousePos.z = -(transform.position.x - Camera.main.transform.position.x);
        // Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        // worldPos.y = 0;
        // transform.LookAt(worldPos);

        // It works it's just WASD doesn't work too well
        // WASD movement

        rb.AddForce(transform.forward * 10 * Input.GetAxis("Vertical"));

    }
}
