using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    Rigidbody rb;
    private bool hasSpawned = false;
    private GameObject clonedObject;
    public Vector3 fixedRotation;
    public float leftSide=-4.5f;
    public float rightSide=4.5f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame      
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            rb.AddForce(0, 5, 0);
        }

        if (Input.GetKey(KeyCode.D)&& transform.position.x < rightSide)
        {
            rb.AddForce(5, 0, 0);
        }

        if (Input.GetKey(KeyCode.A)&& transform.position.x > leftSide)
        {
            rb.AddForce(-5, 0,0 );
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(0, 0, 5);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(0, 0, -5);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!hasSpawned)
            {
                Quaternion rotation = Quaternion.Euler(fixedRotation);
                
                clonedObject = Instantiate(gameObject, transform.position, transform.rotation);
                Rigidbody clonedObjectRb = clonedObject.GetComponent<Rigidbody>();
                if (clonedObjectRb != null)
                {
                    clonedObjectRb.isKinematic = true;
                }
                hasSpawned = true;
            }
        }
    }
}