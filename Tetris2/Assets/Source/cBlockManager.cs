using UnityEngine;
using System.Collections;

public class cBlockManager : MonoBehaviour {
	const int Width = 14;		// 落下したブロックの有無を格納する配列の横のサイズ.
	const int Height = 24;		// 落下したブロックの有無を格納する配列の縦のサイズ.
	const int BlockNum = 4;		// ブロックの数.
	const int xMin = 2;		// ブロックの有無の配列のx座標の格納最少数.
	const int xMax = 11;		// ブロックの有無の配列のx座標の格納最大数.
	const int yMin = 2;		// ブロックの有無の配列のy座標の格納最少数.
	const int yMax = 21;		// ロックの有無の配列のy座標の格納最大数.

	static cBlockManager s_instance;		// インスタンス
	cBlock [,] m_blocks = new cBlock [Height, Width];		// オブジェクトを格納.
	bool [,] m_form = new bool [Height, Width];		// 有無を格納.
	int[] m_deletePositionY = new int [BlockNum];
	float m_flashingTime = 0f;

	public float FlashingTime {
		set {
			m_flashingTime = value;
		}
	}

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
		for (int i = 0; i < BlockNum; i++) {
			m_blocks[y [i], x [i]] = blocks [i];
			m_form [y [i], x [i]] = true;
		}
	}

	public int [] DeletePosition (int positionY) {
		int count = 0;
		for (int py = positionY; py < positionY + BlockNum; py++) {
			for (int dx = xMin; dx <= xMax; dx++) {
				if (!m_form [py, dx]) {
					m_deletePositionY [count++] = 0;
					break;
				}
				if (dx == xMax) {
					m_deletePositionY [count++] = py;
				}
			}
		}

		return m_deletePositionY;
	}

	// ブロックを削除.
	// 第一引数：削除を確認するラインの先頭.
	// 戻り値：消したライン数
	public int DeleteBlock () {
		int count = 0;

		for (int i = 0; i < BlockNum; i++) {
			if (m_deletePositionY [i] != 0) {
				for (int dx = xMin; dx <= xMax; dx++) {
					if (dx >= xMax) {
						for (int x = xMin; x <= xMax; x++) {
							Destroy (m_blocks[m_deletePositionY [i], x].GetCube ());
							m_form [m_deletePositionY [i], x] = false;
						}

						for (int y = m_deletePositionY [i]; y >= yMin-1; y--) {
							for (int x = xMin; x <= xMax; x++) {
								m_blocks [y, x] = m_blocks [y-1, x];
								m_form [y, x] = m_form [y-1, x];
								if (m_form [y, x]) {
									m_blocks [y, x].SetPosition (new Vector3 (x, y, 0));
								}
							}
						}

						count++;
					}
				}
			}
		}

		return count;
	}

	// 配列の初期化.
	public void Initialize () {
		for (int dy = 0; dy < Height; dy++) {
			for (int dx = 0; dx < Width; dx++) {
				m_blocks [dy, dx] = new cBlock ();
			}
		}

		for (int dy = 0; dy < Height; dy++) {
			for (int dx = 0; dx < Width; dx++) {
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

		for (int dy = py, y = 0; dy < py+size; dy++, y++) {
			for (int dx = px, x = 0; dx < px+size; dx++, x++) {
				if (dy >= Height) {
					form [y, x] = false;
					continue;
				}
				form [y, x] = m_form [dy, dx];
			}
		}

		return form;
	}

	public int [] GetDeletePositionY () {
		return m_deletePositionY;
	}

	public bool FlashingTetrimino () {
		m_flashingTime += Time.deltaTime;

		if ((m_flashingTime < 0.2f) || (m_flashingTime >= 0.4f && m_flashingTime < 0.6f) || (m_flashingTime >= 0.8f && m_flashingTime < 1f)) {
			for (int i = 0; i < BlockNum; i++) {
				if (m_deletePositionY [i] == 0) {
					continue;
				}
				for (int dx = xMin; dx <= xMax; dx++) {
					m_blocks [m_deletePositionY [i], dx].SetMaterial (eMaterialType.Transparency);
				}
			}
		}
		else if ((m_flashingTime >= 0.2f && m_flashingTime < 0.4f) || (m_flashingTime >= 0.6f && m_flashingTime < 0.8f)) {
			for (int i = 0; i < BlockNum; i++) {
				if (m_deletePositionY [i] == 0) {
					continue;
				}
				for (int dx = xMin; dx <= xMax; dx++) {
					m_blocks [m_deletePositionY [i], dx].SetMaterial (m_blocks [m_deletePositionY [i], dx].GetType ());
				}
			}
		}

		if (m_flashingTime >= 1f) {
			return true;
		}
		return false;
	}
}
