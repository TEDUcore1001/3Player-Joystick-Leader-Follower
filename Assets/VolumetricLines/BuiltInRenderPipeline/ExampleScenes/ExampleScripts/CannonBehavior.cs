using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

	public Transform m_cannonRot;
	public Transform m_muzzle;
	public GameObject m_shotPrefab;
	public Texture2D m_guiTexture;

	// Use this for initialization
	void Start () 
	{
		//InvokeRepeating("spawnLaser", 0, 3f);
		//GameObject go = Instantiate(m_shotPrefab, m_muzzle.position, m_muzzle.rotation);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	//void OnGUI()
	//{
	//	GUI.DrawTexture(new Rect(0f, 0f, m_guiTexture.width / 2, m_guiTexture.height / 2), m_guiTexture);
	//}

	void spawnLaser()
    {
		
	}
}
