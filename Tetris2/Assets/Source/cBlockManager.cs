using UnityEngine;
using System.Collections;

public class cBlockManager : MonoBehaviour {
	const int formWidth = 14;		// 落下したブロックの有無を格納する配列の横のサイズ.
	const int formHeight = 24;		// 落下したブロックの有無を格納する配列の縦のサイズ.
	const int objectWidth = 10;		// 落下したブロックを格納する配列の横のサイズ.
	const int objectHeight = 20;		// 落下したブロックを格納する配列の縦のサイズ.
	const int blockNum = 4;		// ブロックの数.
	const int xMin = 2;		// ブロックの有無の配列のx座標の格納最少数.
	const int xMax = 11;		// ブロックの有無の配列のx座標の格納最大数.
	const int yMin = 2;		// ブロックの有無の配列のy座標の格納最少数.
	const int yMax = 21;		// ロックの有無の配列のy座標の格納最大数.

	static cBlockManager s_instance;		// インスタンス
	cBlock [,] m_blocks = new cBlock [objectHeight, objectWidth];		// オブジェクトを格納.
	bool [,] m_form = new bool [formHeight, formWidth];		// 有無を格納.

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

	// インスタンスを取得.
	// 戻り値：インスタンス.
	public static cBlockManager GetInstance () {
		if (s_instance == null) {
			GameObject gameObject = new GameObject ("BlockManager");
			s_instance = gameObject.AddComponent<cBlockManager> ();
		}
		
		return s_instance;
	}

	// ブロックを追加.
	// 第一引数：ブロック.
	// 第二引数：格納する配列の添え字のxライン
	// 第三引数：格納する配列の添え字のyライン
	public void AddBlock (cBlock [] blocks, int [] x, int [] y) {
		for (int i = 0; i < blockNum; i++) {
			m_blocks[y [i], x [i]] = blocks [i];
			m_form [y [i], x [i]] = true;
		}
	}

	// ブロックを削除.
	// 第一引数：削除を確認するラインの先頭.
	// 戻り値：消したライン数
	public int DeleteBlock (int positionY) {
		int count = 0;

		for (int py = positionY; py < positionY + blockNum; py++) {
			for (int dx = xMin; dx <= xMax; dx++) {
				if (dx >= xMax) {
					for (int x = xMin; x <= xMax; x++) {
						Destroy (m_blocks[py, x].GetCube ());
						m_form [py, x] = false;
					}

					for (int y = py; y >= yMin-1; y--) {
						for (int x = xMin; x <= xMax; x++) {
							m_blocks [y, x] = m_blocks [y-1, x];
							m_form [y, x] = m_form [y-1, x];
						}
					}

					count++;
				}
			}
		}

		return count;
	}

	// 配列の初期化.
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

	// 指定された部分のブロックの有無を取得.
	// 第一引数：取得するポイントのx座標.
	// 第二引数：取得するポイントのy座標.
	// 第三引数：配列のサイズ.
	// 戻り値：ブロックの有無.
	public bool [,] GetCollision (int px, int py, int size) {
		bool [,] form = new bool [size, size];

		for (int dy = py, y = 0; dy < py+3; dy++, y++) {
			for (int dx = px, x = 0; dx < px+3; dx++, x++) {
				form [y, x] = m_form [dy, dx];
			}
		}

		return form;
	}
}
