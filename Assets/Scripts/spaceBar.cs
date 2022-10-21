using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceBar : MonoBehaviour
{
    // Start is called before the first frame update

    public float forceConst = 10;

    public Rigidbody rb;

    public GameObject player;

    public GameObject udp_comm;
    public UdpSocket udpScript;

    public int length;
    public int flag = 1;

    private Vector3 leadPos;
    private Vector3 follPos;

    double leadTemp;

    public float roundedFloat;

    public bool check = false;
    public bool arrayIsEmpty = false;

    private sbyte[] temp;
    private sbyte[] myArray;
    private sbyte[] newArray;
    private sbyte[] follArray;
    private sbyte[] leadArray;

    // Start is called once at start.
    private void Start()
    {
        //Variable Definings
        flag = -1;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame.
    void FixedUpdate()
    {
        //Variable Definings

        udp_comm = GameObject.Find("udp_comm");
        udpScript = udp_comm.GetComponent<UdpSocket>();

        //Assigning the coming message from UdpSocket script to myArray variable

        myArray = udpScript.message;

        //if (myArray.Length < 16)
        //{
        //    arrayIsEmpty = true;
        //}
        //else
        //{
        //    flag = myArray[16];
        //}




        //Debug.Log("flag = " + flag);

        if (flag == 0)
        {
            //Array.Resize<sbyte>(ref myArray, myArray.Length - 1);

            length = myArray.Length;

            //newArray = myArray.RemoveAt(length - 1);

            //SplitArray(myArray, out follArray, out leadArray);

            //Logging the message array's length

            Debug.Log("Message Array Length " + myArray.Length);

            //converting sbyte array to array (not sure if its working)

            //byte[] convertedfollArray = (byte[])(Array)follArray;
            //byte[] convertedleadArray = (byte[])(Array)leadArray;

            byte[] convertedleadArray = (byte[])(Array)myArray;

            //converting the bit arrays to float and round to 2 decimals

            double leadDouble = System.BitConverter.ToDouble(convertedleadArray, 0);

            leadTemp = leadDouble;

            float leadFloat = Convert.ToSingle(leadDouble);

            float roundedleadFloat = (float)Math.Round(leadFloat * 100f) / 100f;

            // Physics Calculations

            ChangePos(roundedleadFloat);

        }
    }

    void Split<T>(T[] array, int index, out T[] first, out T[] second)
    {
        first = array.Take(index).ToArray();
        second = array.Skip(index).ToArray();
    }

    void SplitArray<T>(T[] array, out T[] first, out T[] second)
    {
        Split(array, array.Length / 2, out first, out second);
    }

    void ChangePos(float a)
    {

        leadPos = new Vector3(transform.position.x, a, transform.position.z);

        transform.position = leadPos;

    }
}
