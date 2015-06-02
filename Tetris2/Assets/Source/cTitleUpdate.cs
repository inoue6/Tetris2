using UnityEngine;
using System.Collections;

public class cTitleUpdate : MonoBehaviour {
	// Use this for initialization
	void Start () {
		cSceneManager.GetInstance ().Initialize ();
		cMaterialManager.GetInstance ().Initialize ();
		cTetrimino tetrimino = new cTetrimino ();
		tetrimino.CreateTatrimino (cTetrimino.eTetriminoType.eI_Tetrimino);
	}
	
	// Update is called once per frame
	void Update () {
		cTitleStateManager.Instance.Update ();
	}
}
