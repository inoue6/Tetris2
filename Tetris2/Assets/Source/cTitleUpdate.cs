using UnityEngine;
using System.Collections;

public class cTitleUpdate : MonoBehaviour {
	const int Size = 3;		// 通常のテトリミノ配列のサイズ.
	const int ISize = 4;		// Iテトリミノの配列のサイズ.
	const int MoveSpeed = 1;		// 移動スピード.


	cGhost ghost;
	float time = 0f;
	// Use this for initialization
	void Start () {
		cSceneManager.GetInstance ().Initialize ();
		cMaterialManager.GetInstance ().Initialize ();
		cTetriminoManager.GetInstance ().Initialize ();
		ghost = new cGhost ();
		ghost.CreateGhost ();
	}
	
	// Update is called once per frame
	void Update () {
		cTitleStateManager.Instance.Update ();

		if (Input.GetKeyDown (KeyCode.A)) {
			cTetriminoManager.GetInstance ().GetTetrimino ().Rotation (cTetriminoManager.GetInstance ().GetTetrimino ().GetSize ()-1, 0);
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			cTetriminoManager.GetInstance ().GetTetrimino ().Rotation (0, cTetriminoManager.GetInstance ().GetTetrimino ().GetSize ()-1);
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			cTetriminoManager.GetInstance ().GetTetrimino ().Move (-MoveSpeed);
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			cTetriminoManager.GetInstance ().GetTetrimino ().Move (MoveSpeed);
		}

		if (time >= 1f) {
			cTetriminoManager.GetInstance ().GetTetrimino ().Fall ();
			time = 0f;
		}

		cTetriminoManager.GetInstance ().SetNext ();
		ghost.SetGhost (cTetriminoManager.GetInstance ().GetTetrimino ());
		ghost.DeleteGhost (cTetriminoManager.GetInstance ().GetTetrimino ());

		time += Time.deltaTime;
	}
}
