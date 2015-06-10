using UnityEngine;
using System.Collections;

public class cGameMainUpdate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		cGameMainManager.GetInstance ().Initialize ();
		cSceneManager.GetInstance ().Initialize ();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
