using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoxes: MonoBehaviour
{
    // Start is called before the first frame update

    const float forceConstant = 100f;
    [SerializeField] public float forceValue;

    public PlayerController playerController;
    public PlayerController playerController2;
    public GameManager gmScript;
    public CubePath cPath;

    private Vector3 forceVector;
    private Vector3 errorVector;
    private Vector3 cubePos;

    private Rigidbody cubeRigidbody;

    public int deneme;

    public GameObject player;
    public GameObject player2;
    public GameObject gmObject;

    private float upperRange;
    private float lowerRange;
    public float errorFloat;
    public float errorFloat2;
    public int maxError = 5;
    public float totalScore;
    public float totalScore2;

    void Start()
    {

        player = GameObject.Find("Player");
        player2 = GameObject.Find("SecondPlayer");
        gmObject = GameObject.Find("GameManager");

        playerController = player.GetComponent<PlayerController>();
        playerController2 = player.GetComponent<PlayerController>();
        gmScript = gmObject.GetComponent<GameManager>();
        cPath = gameObject.GetComponent<CubePath>();

        cubeRigidbody  = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
        
    void Update()
    {
        

    }

    private void FixedUpdate()
    {
        CheckPosition();

        //cubePos = new Vector3(transform.position.x, cPath.roundedFloat, transform.position.z);

        ////transform.position = cubePos;

        //Debug.Log(cubePos.x + " " + " " + cubePos.y + " " + " " + cubePos.z);

    }

    void CheckPosition()
    {
        errorVector = transform.position - player.transform.position;
        
        errorFloat = Mathf.Abs(transform.position.x - player.transform.position.x);
        errorFloat2 = Mathf.Abs(transform.position.x - player2.transform.position.x);

        if (cPath.flag == 0)
        {

            totalScore += (20 / gmScript.playTime) * (maxError - errorFloat) * Time.fixedDeltaTime;
            totalScore2 += (20 / gmScript.playTime) * (maxError - errorFloat2) * Time.fixedDeltaTime;
            Debug.Log("in");

            if (totalScore < 0)
            {
                totalScore = 0;
            }
            if (totalScore2 < 0)
            {
                totalScore2 = 0;
            }

        } else if (cPath.flag == 1)
        {
            totalScore = 0;
            totalScore2 = 0;
        }

        

    }
}
