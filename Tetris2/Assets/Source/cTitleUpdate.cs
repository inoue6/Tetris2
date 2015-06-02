using UnityEngine;
using System.Collections;

public class cTitleUpdate : MonoBehaviour {
	cTetrimino tetrimino;
	float time = 0f;
	// Use this for initialization
	void Start () {
		cSceneManager.GetInstance ().Initialize ();
		cMaterialManager.GetInstance ().Initialize ();
		tetrimino = new cTetrimino ();
		tetrimino.CreateTatrimino (cTetrimino.eTetriminoType.eI_Tetrimino);
	}
	
	// Update is called once per frame
	void Update () {
		cTitleStateManager.Instance.Update ();

		if (Input.GetKeyDown (KeyCode.A)) {
			tetrimino.Rotation (0, 3);
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			tetrimino.Rotation (3, 0);
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			tetrimino.Move (-1);
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			tetrimino.Move (1);
		}

		if (time >= 1f) {
			tetrimino.Fall ();
			time = 0f;
		}

		time += Time.deltaTime;
	}
}
