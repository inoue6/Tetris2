using UnityEngine;
using System.Collections;

public class cBlockManager : MonoBehaviour {
	const int formWidth = 14;
	const int formHeight = 24;
	const int objectWidth = 10;
	const int objectHeight = 20;
	const int blockNum = 4;

	static cBlockManager s_instance;
	cBlock [,] m_blocks = new cBlock [objectHeight, objectWidth];
	bool [,] m_form = new bool [formHeight, formWidth];

	void Awake () {
		if (s_instance == null) {
			DontDestroyOnLoad (gameObject);
			return;
		}
		cBlockManager instance = gameObject.GetComponent<cBlockManager> ();
		if (instance != null) {
			Destroy (gameObject);
		}
	}

	public static cBlockManager GetInstance () {
		if(s_instance == null) {
			GameObject gameObject = new GameObject ("BlockManager");
			s_instance = gameObject.AddComponent<cBlockManager> ();
		}
		
		return s_instance;
	}

	public void AddBlock (cBlock [] blocks, Vector2 [] position) {
		for (int i = 0; i < blockNum; i++) {
			int x = (int)(position[i].x);
			int y = (int)(position[i].y);
			m_blocks[y, x] = blocks [i];
			m_form [y, x] = true;
		}
	}

	public void Initialize () {
		for (int dy = 0; dy < objectHeight; dy++) {
			for (int dx = 0; dx < objectWidth; dx++) {
				m_blocks [dy, dx] = new cBlock ();
			}
		}

		for (int dy = 0; dy < formHeight; dy++) {
			for (int dx = 0; dx < formWidth; dx++) {
				m_form [dy, dx] = false;
			}
		}
	}
}
