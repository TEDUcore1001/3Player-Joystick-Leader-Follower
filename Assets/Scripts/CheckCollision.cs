using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
	public GameObject laserObject;
    public GameObject laser2Object;

    public VolumetricLines.VolumetricLineBehavior laserScript;
    public VolumetricLines.VolumetricLineBehavior laserScript2;

	public bool laserTriggered;
    public bool laser2Triggered;

    // Start is called before the first frame update
    void Start()
    {

		laserTriggered = false;
        laser2Triggered = false;

        laserObject = GameObject.FindGameObjectWithTag("Laser");
        laser2Object = GameObject.FindGameObjectWithTag("Laser2");

        laserScript = laserObject.GetComponent<VolumetricLines.VolumetricLineBehavior>();
        laserScript2 = laser2Object.GetComponent<VolumetricLines.VolumetricLineBehavior>();

    }

    // Update is called once per frame
    void Update()
    {
        if (laserTriggered == true)
        {
            laserScript.m_material.color = new Color(0, 10, 10, 0);
            laserScript.m_lineColor = new Color(0, 10, 10, 0);
            laserScript.LineColor = new Color(0, 10, 10, 0);
            laserScript.m_lightSaberFactor = 1F;
            laserScript.LineWidth = 2f;
        } else if (laserTriggered == false)
        {
            laserScript.m_material.color = new Color(255, 0, 0 , 0);
            laserScript.m_lineColor = new Color(255, 0, 0, 0);
            laserScript.LineColor = new Color(255, 0, 0, 0);
            laserScript.m_lightSaberFactor = 1F;
            laserScript.LineWidth = 2f;
        }

        if (laser2Triggered == true)
        {
            laserScript2.m_material.color = new Color(0, 10, 10, 0);
            laserScript2.m_lineColor = new Color(0, 10, 10, 0);
            laserScript2.LineColor = new Color(0, 10, 10, 0);
            laserScript2.m_lightSaberFactor = 1F;
            laserScript2.LineWidth = 2f;
        }
        else if (laser2Triggered == false)
        {
            laserScript2.m_material.color = new Color(255, 0, 0, 0);
            laserScript2.m_lineColor = new Color(255, 0, 0, 0);
            laserScript2.LineColor = new Color(255, 0, 0, 0);
            laserScript2.m_lightSaberFactor = 1F;
            laserScript2.LineWidth = 2f;
        }

    }

	private void OnTriggerEnter(Collider other)
	{

		if (gameObject.tag == "Enemy" && other.gameObject.tag == "Laser")
		{
			laserTriggered = true;
			Debug.Log("Dart.");
		}

        if (gameObject.tag == "Enemy" && other.gameObject.tag == "Laser2")
        {
            laser2Triggered = true;
            Debug.Log("Dart.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.tag == "Enemy" && other.gameObject.tag == "Laser")
        {
            laserTriggered = false;
        }
        if (gameObject.tag == "Enemy" && other.gameObject.tag == "Laser2")
        {
            laser2Triggered = false;
        }
     
    }
}
