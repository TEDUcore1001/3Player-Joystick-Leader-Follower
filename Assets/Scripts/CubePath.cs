using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePath : MonoBehaviour
{
    // Start is called before the first frame update

    public float forceConst = 10;

    public Rigidbody rb;

    public GameObject player;
    public GameObject player2;

    public GameObject udp_comm;
    public UdpSocket udpScript;

    public int length;
    public int flag = 1;

    private Vector3 leadPos;
    private Vector3 follPos;
    private Vector3 foll2Pos;

    public float roundedFloat;

    public bool check = false;
    public bool arrayIsEmpty = false;

    private sbyte[] temp;
    private sbyte[] myArray;
    private sbyte[] newArray;
    private sbyte[] follArray;
    private sbyte[] secondFollArray;
    private sbyte[] leadArray;
    private sbyte[] triggerArray;

    // Start is called once at start.
    private void Start()
    {
        //Variable Definings
        flag = -1;
        player = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("SecondPlayer");
    }

    // Update is called once per frame.
    void FixedUpdate()
    {
        //Variable Definings

        udp_comm = GameObject.Find("udp_comm");
        udpScript = udp_comm.GetComponent<UdpSocket>();

        //Assigning the coming message from UdpSocket script to myArray variable

        myArray = udpScript.message;

        if (myArray.Length < 16)
        {
            arrayIsEmpty = true;
        } else
        {
            flag = myArray[31];
        }




        Debug.Log("flag = " + flag);

        if (flag == 0)
        {
            //Array.Resize<sbyte>(ref myArray, myArray.Length - 8);

            length = myArray.Length;

            //newArray = myArray.RemoveAt(length - 1);

            SplitArray(myArray, out follArray, out secondFollArray, out leadArray, out triggerArray);

            //Logging the message array's length

            Debug.Log("Message Array Length " + myArray.Length);

            Debug.Log(follArray.Length);
            Debug.Log(secondFollArray.Length);
            Debug.Log(leadArray.Length);
            Debug.Log(triggerArray.Length);

            //converting sbyte array to array (not sure if its working)

            byte[] convertedfollArray = (byte[])(Array)follArray;
            byte[] convertedfoll2Array = (byte[])(Array)secondFollArray;
            byte[] convertedleadArray = (byte[])(Array)leadArray;


            //converting the bit arrays to float and round to 2 decimals

            double follDouble = System.BitConverter.ToDouble(convertedfollArray, 0);
            double foll2Double = System.BitConverter.ToDouble(convertedfoll2Array, 0);
            double leadDouble = System.BitConverter.ToDouble(convertedleadArray, 0);

            float leadFloat = Convert.ToSingle(leadDouble);
            float follFloat = Convert.ToSingle(follDouble);
            float foll2Float = Convert.ToSingle(foll2Double);

            float roundedleadFloat = (float)Math.Round(leadFloat * 100f) / 100f;
            float roundedfollFloat = (float)Math.Round(follFloat * 100f) / 100f;
            float roundedfoll2Float = (float)Math.Round(foll2Float * 100f) / 100f;

            // Physics Calculations

            ChangePos(roundedleadFloat, roundedfollFloat, roundedfoll2Float);
            
        }
    }

    void Split<T>(T[] array, int index, out T[] first, out T[] second, out T[] third, out T[] fourth)
    {
        first = array.Take(index).ToArray();
        second = array.Skip(index).Take(index).ToArray();
        third = array.Skip(index*2).Take(index).ToArray();
        fourth = array.Skip(index*3).Take(index).ToArray();
    }

    void SplitArray<T>(T[] array, out T [] first, out T[] second, out T[] third, out T[] fourth)
    {
        Split(array, array.Length / 4, out first, out second, out third, out fourth);
    }

    void ChangePos(float a,float b, float c)
    {

        leadPos = new Vector3(a, 0f, transform.position.z);

        transform.position = leadPos;


        follPos = new Vector3(b, 4f, player.transform.position.z);

        player.transform.position = follPos;


        foll2Pos = new Vector3(c, -4f, player2.transform.position.z);

        player2.transform.position = foll2Pos;
    }



}
