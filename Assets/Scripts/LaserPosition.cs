using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPosition : MonoBehaviour
{
    public GameObject muzzle;
    public GameObject muzzle2;

    public Vector3 laserPos;
    public Vector3 laserPos2;

    public Vector3 muzzlePos;
    public Vector3 muzzlePos2;

    public bool rotated;


    // Start is called before the first frame update
    void Start()
    {
        rotated = false;
    }

    // Update is called once per frame
    void Update()
    {
        muzzle = GameObject.Find("Muzzle");
        muzzle2 = GameObject.Find("Muzzle2");

        laserPos = transform.position;

        muzzlePos = muzzle.transform.position;
        muzzlePos2 = muzzle2.transform.position;

        
        
        if (gameObject.CompareTag("Laser2"))
        {
            laserPos = new Vector3(muzzlePos2.x, muzzlePos2.y, muzzlePos2.z);
            if (rotated == false)
            {
                rotated = true;
                transform.Rotate(Vector3.left, 180);
            }
            
        } else if (gameObject.CompareTag("Laser"))
        {
            laserPos = new Vector3(muzzlePos.x, muzzlePos.y, muzzlePos.z);
        }

        transform.position = laserPos;

        //Debug.Log("laser : " + transform.position);
        //Debug.Log("Muzzle : " + muzzle.transform.position);

    }
}
