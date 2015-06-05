using UnityEngine;
using System.Collections;

public class cGhost {
	const int BlockNum = 4;
	const int MaxFloorDistance = 20;
	const int MaxDown = 21;

	cBlock [] m_blocks = new cBlock [BlockNum];		// ブロック.
	Vector3[] m_positions = new Vector3 [BlockNum];

	public void CreateGhost () {
		for (int i = 0; i < BlockNum; i++) {
			m_blocks [i] = new cBlock ();
			m_blocks [i].CreateCube ();
			//m_blocks [i].SetMaterial (eMaterial.Ghost);
		}
	}

	public void SetGhost (cTetrimino tetrimino) {
		int px = (int)tetrimino.GetTetriminoPosition ().x;
		int py = (int)tetrimino.GetTetriminoPosition ().y;
		int pz = (int)tetrimino.GetTetriminoPosition ().z;
		Vector3 [] positions = tetrimino.GetBlockPosition ();
		int size = tetrimino.GetSize ();

		for (int i = 4-size; i+py < MaxFloorDistance; i++) {
			bool [,] form = cBlockManager.GetInstance ().GetCollision (px, py+i, size);

			for (int dy = 0; dy < size; dy++) {
				for (int dx = 0; dx < size; dx++) {
					if (tetrimino.GetForm () [dy, dx] && form [dy, dx]) {
						for (int j = 0; j < BlockNum; j++) {
							m_positions [j] = new Vector3 (positions [j].x+px, positions [j].y+py-i, pz);
							m_blocks [j].SetPosition (m_positions [j++]);
						}

						return;
					}
				}
			}
		}

		int floorDistance = MaxFloorDistance;

		for (int i = 0; i <= MaxFloorDistance; i++) {
			int moveY = 0;
			for (int j = 0; j < BlockNum; j++) {
				if ((positions [j].y+i) > MaxFloorDistance && i < floorDistance) {
					floorDistance = i;
				}

				if (moveY < (positions [j].y+i)-MaxDown) {
					moveY = (int)((positions [j].y+i)-MaxDown);
				}
				
				if ((positions [j].y+i) > MaxFloorDistance && i == BlockNum-1) {
					for (int k = 0; k < BlockNum; k++) {
						m_positions [k] = new Vector3 (positions [k].x+px, positions [k].y+floorDistance+moveY, pz);
						m_blocks [k].SetPosition (m_positions [k]);
					}

					return;
				}
			}
		}

		for (int k = 0; k < BlockNum; k++) {
			m_positions [k] = new Vector3 (positions [k].x+px, positions [k].y+floorDistance, pz);
			m_blocks [k].SetPosition (m_positions [k]);
		}
		
		
		/*for (int i = 0; i < BlockNum; i++) {
			for (int j = 0; j < 21; j++) {
				if ((positions [i].y+j) >= 21 && j < floorDistance) {
					int moveY = 0;
					for  (int k = 0; k < BlockNum; k++) {
						if (floorDistance > j) {
							floorDistance = j;
						}

						if (moveY < (positions [k].y+j)-21) {
							moveY = (int)((positions [k].y+j)-21);
						}
					}

					for (int k = 0; k < BlockNum; k++) {
						m_positions [k] = new Vector3 (positions [k].x+px, positions [k].y+floorDistance+moveY, pz);
						m_blocks [k].SetPosition (m_positions [k]);
					}

					return;
				}
			}
		}

		for (int k = 0; k < BlockNum; k++) {
			m_positions [k] = new Vector3 (positions [k].x+px, positions [k].y+floorDistance, pz);
			m_blocks [k].SetPosition (m_positions [k]);
		}*/
	}
	
	public void DeleteGhost (cTetrimino tetrimino) {
		for (int i = 0; i < BlockNum; i++) {
			int ghostX = (int)m_positions [i].x;
			int ghostY = (int)m_positions [i].y;
			for (int j = 0; j < BlockNum; j++) {
				int tetriminoX = (int)(tetrimino.GetBlockPosition () [j].x + tetrimino.GetTetriminoPosition ().x);
				int tetriminoY = (int)(tetrimino.GetBlockPosition () [j].y + tetrimino.GetTetriminoPosition ().y);
				if (ghostX == tetriminoX && ghostY == tetriminoY) {
					m_blocks [i].SetPosition (new Vector3 (100f, 100f, 0));
				}
			}
		}
	}
}
