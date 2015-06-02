using UnityEngine;
using System.Collections;

public class cTetrimino {
	public enum eTetriminoType {
		eI_Tetrimino,
		eO_Tetrimino,
		eT_Tetrimino,
		eS_Tetrimino,
		eZ_Tetrimino,
		eL_Tetrimino,
		eJ_Tetrimino,
	}
	const int width = 4;
	const int height = 4;
	const int blockNum = 4;

	cBlock [] m_blocks = new cBlock [blockNum];
	bool [,] m_form = new bool [height, width];
	eTetriminoType m_type;

	public void CreateTatrimino (eTetriminoType type) {
		m_type = type;
		InitializeForm ();
	}

	void InitializeForm () {
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				m_form [y, x] = false;
			}
		}

		switch (m_type) {
		case eTetriminoType.eI_Tetrimino:
			ITetrimino ();
			break;
		case eTetriminoType.eO_Tetrimino:
			OTetrimino ();
			break;
		case eTetriminoType.eT_Tetrimino:
			TTetrimino ();
			break;
		case eTetriminoType.eS_Tetrimino:
			STetrimino ();
			break;
		case eTetriminoType.eZ_Tetrimino:
			ZTetrimino ();
			break;
		case eTetriminoType.eL_Tetrimino:
			LTetrimino ();
			break;
		case eTetriminoType.eJ_Tetrimino:
			JTetrimino ();
			break;
		}

		int count = 0;
		for (int y = 0; y < height; y++) {
			for(int x = 0; x < width; x++) {
				if(m_form [y, x]) {
					m_blocks [count++].SetPosition (new Vector3 (x, y, 0f));
				}
			}
		}
	}

	void ITetrimino () {
		m_form [0, 1] = true;
		m_form [1, 1] = true;
		m_form [2, 1] = true;
		m_form [3, 1] = true;

		for (int i = 0; i < blockNum; i++) {
			m_blocks[i] = new cBlock();
			m_blocks[i].CreateCube ();
			m_blocks[i].SetColor (eMaterialType.LightBlue);
		}
	}

	void OTetrimino () {
		m_form [1, 1] = true;
		m_form [2, 1] = true;
		m_form [1, 2] = true;
		m_form [2, 2] = true;

		for (int i = 0; i < blockNum; i++) {
			m_blocks[i].CreateCube ();
			m_blocks[i].SetColor (eMaterialType.Yellow);
		}
	}

	void TTetrimino () {
		m_form [2, 0] = true;
		m_form [1, 1] = true;
		m_form [2, 1] = true;
		m_form [2, 2] = true;
		
		for (int i = 0; i < blockNum; i++) {
			m_blocks[i].CreateCube ();
			m_blocks[i].SetColor (eMaterialType.Purple);
		}
	}

	void STetrimino () {
		m_form [2, 0] = true;
		m_form [1, 1] = true;
		m_form [2, 1] = true;
		m_form [1, 2] = true;
		
		for (int i = 0; i < blockNum; i++) {
			m_blocks[i].CreateCube ();
			m_blocks[i].SetColor (eMaterialType.YellowGreen);
		}
	}

	void ZTetrimino () {
		m_form [1, 0] = true;
		m_form [1, 1] = true;
		m_form [2, 1] = true;
		m_form [2, 2] = true;
		
		for (int i = 0; i < blockNum; i++) {
			m_blocks[i].CreateCube ();
			m_blocks[i].SetColor (eMaterialType.Red);
		}
	}

	void LTetrimino () {
		m_form [0, 1] = true;
		m_form [1, 1] = true;
		m_form [2, 1] = true;
		m_form [2, 2] = true;
		
		for (int i = 0; i < blockNum; i++) {
			m_blocks[i].CreateCube ();
			m_blocks[i].SetColor (eMaterialType.Orange);
		}
	}

	void JTetrimino () {
		m_form [2, 1] = true;
		m_form [0, 2] = true;
		m_form [1, 2] = true;
		m_form [2, 2] = true;
		
		for (int i = 0; i < blockNum; i++) {
			m_blocks[i].CreateCube ();
			m_blocks[i].SetColor (eMaterialType.Blue);
		}
	}
}
