using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AudioSource[] soundEffects;
    public AudioSource soundEffect;
    public AudioSource soundEffect2;
    public AudioSource soundEffect3;
    public AudioSource soundEffect4;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreLabelText;

    public float tempScore;
    public float tempScore2;

    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI scoreLabelText2;

    public float playTime;
    public float seconds;
    public const float soundConstant = 1f;

    public bool isFinished;
    public bool scoreNoted;

    public int dividedScore;
    public int dividedScore2;

    public CubePath cubePath;
    public MoveBoxes moveBoxes;
    public CheckCollision collisionChecker;

    // Start is called before the first frame update
    void Start()
    {
        moveBoxes = GameObject.FindGameObjectWithTag("Enemy").GetComponent<MoveBoxes>();
        cubePath = GameObject.FindGameObjectWithTag("Enemy").GetComponent<CubePath>();
        collisionChecker = GameObject.FindGameObjectWithTag("Enemy").GetComponent<CheckCollision>();
        InvokeRepeating("PlaySound", 0, 0.04f);

        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        scoreLabelText = GameObject.Find("ScoreLabel").GetComponent<TextMeshProUGUI>();

        scoreText2 = GameObject.Find("Score2").GetComponent<TextMeshProUGUI>();
        scoreLabelText2 = GameObject.Find("ScoreLabel2").GetComponent<TextMeshProUGUI>();

        soundEffects = GetComponentsInParent<AudioSource>();

        soundEffect = soundEffects[0];
        soundEffect2 = soundEffects[1];
        soundEffect3 = soundEffects[2];
        soundEffect4 = soundEffects[3];

        Debug.Log("sfxp = " + soundEffect);
        Debug.Log("sfxp2 = " + soundEffect2);


        Debug.Log("sfx = " + soundEffects[0] + " " + soundEffects[1]);

        InvokeRepeating("ChangeScoreText", 0, 0.2f);
        playTime = 60f;
        seconds = 0;
        tempScore = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        soundEffect.pitch = 7.5f - (soundConstant * Mathf.Abs(moveBoxes.errorFloat) * 0.7f);
        soundEffect2.pitch = 7.5f - (soundConstant * Mathf.Abs(moveBoxes.errorFloat2) * 0.7f);




        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }


    }

    void ChangeScoreText()
    { 

        if (cubePath.flag == 0)
        {
            scoreNoted = false;
            scoreLabelText.text = "Player 1";
            scoreLabelText2.text = "Player 2";
            dividedScore = Mathf.RoundToInt(moveBoxes.totalScore / 10);
            dividedScore2 = Mathf.RoundToInt(moveBoxes.totalScore2 / 10);

            scoreText2.text = moveBoxes.totalScore2.ToString("0");
            scoreText.text = moveBoxes.totalScore.ToString("0");


        } else if (cubePath.flag == 63)
        {
            if (!scoreNoted)
            {
                tempScore = moveBoxes.totalScore;
                tempScore2 = moveBoxes.totalScore2;
                scoreNoted = true;
            }


            if (tempScore > tempScore2)
            {
                scoreLabelText.text = "Winner !";
                scoreLabelText2.text = "Finished.";

            } else
            {
                scoreLabelText.text = "Finished.";
                scoreLabelText2.text = "Winner !";
                
            }

            scoreText.text = tempScore.ToString("0");
            scoreText2.text = tempScore2.ToString("0");

            moveBoxes.totalScore = 0;
            moveBoxes.totalScore2 = 0;
        }

        

    }

    void PlaySound()
    {
        if (cubePath.flag == 0)
        {

            if (collisionChecker.laserTriggered)
            {
                soundEffect3.Play();
            }
            else
            {
                soundEffect3.Stop();

            }

            if (collisionChecker.laser2Triggered)
            {
                soundEffect4.Play();
            }
            else
            {
                soundEffect4.Stop();
            }

            soundEffect.Play();
            soundEffect2.Play();


        }
           
    }
}
