using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Vector3 forceVector;
    private float forceConstant = 100f;
    [SerializeField] public float forceValue = 100f;


    // Start is called before the first frame update
    void Start()
    {

        forceVector = new Vector3(0f, forceConstant * forceValue, 0f);

        Physics.gravity = new Vector3(0, -9.8F, 0);

        playerRigidbody = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            playerRigidbody.AddForce(forceVector * Time.deltaTime);
        }

    }
}
