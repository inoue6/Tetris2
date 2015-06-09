using UnityEngine;
using System.Collections;

public class cTitleUpdate : MonoBehaviour {
	const int Size = 3;		// 通常のテトリミノ配列のサイズ.
	const int ISize = 4;		// Iテトリミノの配列のサイズ.

	// Use this for initialization
	void Start () {
		cSceneManager.GetInstance ().Initialize ();
	}
	
	// Update is called once per frame
	void Update () {
		cTitleStateManager.Instance.Update ();

	}
}
