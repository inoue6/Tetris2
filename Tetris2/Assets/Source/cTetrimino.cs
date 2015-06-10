using UnityEngine;
using System.Collections;

public class cTetrimino {
	// テトリミノのタイプ.
	public const int I_Tetrimino = 0;
	public const int O_Tetrimino = 1;
	public const int T_Tetrimino = 2;
	public const int S_Tetrimino = 3;
	public const int Z_Tetrimino = 4;
	public const int L_Tetrimino = 5;
	public const int J_Tetrimino = 6;
	
	const int Size = 3;		// 通常のテトリミノ配列のサイズ.
	const int ISize = 4;		// Iテトリミノの配列のサイズ.
	const int BlockNum = 4;		// キューブオブジェクトの数.
	const int MoveSpeed = 1;	// 移動するスピード.
	const float InitializeX = 5f;	// ブロックが生成されるx座標.
	const float InitializeY = 4f;	// ブロックが生成されるy座標.
	const float InitializeZ = 0f;	// ブロックが生成されるz座標.
	const int CheckUnder = 18;
	const int MaxUnder = 21;
	const int LeftWall = 1;
	const int RightWall = 12;

	cBlock [] m_blocks = new cBlock [BlockNum];		// ブロック.
	bool [,] m_form;		// テトリミノ形状　true・ブロックがある　falseブロックはない.
	int m_type;		// テトリミノのタイプ.
	Vector3 m_position;		// テトリミノの現在の場所.
	int m_size;

	// テトリミノ生成.
	// 第一引数：テトリミノのタイプ.
	public void CreateTatrimino (int type) {
		InitializeForm (type);
		m_position = new Vector3 (InitializeX, InitializeY-m_size, InitializeZ);
		SetPosition (m_position);
	}

	// 形状の初期化.
	public void InitializeForm (int type) {
		m_type = type;

		switch (m_type) {
		case I_Tetrimino:
			ITetrimino ();
			break;
		case O_Tetrimino:
			OTetrimino ();
			break;
		case T_Tetrimino:
			TTetrimino ();
			break;
		case S_Tetrimino:
			STetrimino ();
			break;
		case Z_Tetrimino:
			ZTetrimino ();
			break;
		case L_Tetrimino:
			LTetrimino ();
			break;
		case J_Tetrimino:
			JTetrimino ();
			break;
		}
	}

	public void SetPosition (Vector3 position) {
		int count = 0;
		for (int dy = 0; dy < m_size; dy++) {
			for(int dx = 0; dx < m_size; dx++) {
				if(m_form [dy, dx]) {
					m_blocks [count++].SetPosition (new Vector3 (dx+position.x, dy+position.y, 0f));
				}
			}
		}
	}

	// 形状を管理する配列の初期化.
	// 配列のサイズ.
	void FormSize (int size) {
		m_size = size;
		m_form = new bool [m_size, m_size];

		for (int dy = 0; dy < m_size; dy++) {
			for (int dx = 0; dx < m_size; dx++) {
				m_form [dy, dx] = false;
			}
		}
	}

	// Iテトリミノ.
	void ITetrimino () {
		IForm ();

		for (int i = 0; i < BlockNum; i++) {
			m_blocks[i] = new cBlock();
			m_blocks[i].CreateCube ();
			m_blocks[i].SetMaterial (eMaterialType.LightBlue);
		}
	}

	public void IForm () {
		FormSize (ISize);

		m_form [2, 0] = true;
		m_form [2, 1] = true;
		m_form [2, 2] = true;
		m_form [2, 3] = true;
	}

	//Oテトリミノ.
	void OTetrimino () {
		OForm ();

		for (int i = 0; i < BlockNum; i++) {
			m_blocks[i] = new cBlock();
			m_blocks[i].CreateCube ();
			m_blocks[i].SetMaterial (eMaterialType.Yellow);
		}
	}

	public void OForm () {
		FormSize (Size);
		
		m_form [1, 0] = true;
		m_form [1, 1] = true;
		m_form [2, 0] = true;
		m_form [2, 1] = true;
	}

	// Tテトリミノ.
	void TTetrimino () {
		TForm ();
		
		for (int i = 0; i < BlockNum; i++) {
			m_blocks[i] = new cBlock();
			m_blocks[i].CreateCube ();
			m_blocks[i].SetMaterial (eMaterialType.Purple);
		}
	}

	public void TForm () {
		FormSize (Size);
		
		m_form [1, 0] = true;
		m_form [1, 1] = true;
		m_form [1, 2] = true;
		m_form [2, 1] = true;
	}

	// Sテトリミノ.
	void STetrimino () {
		SForm ();
		
		for (int i = 0; i < BlockNum; i++) {
			m_blocks[i] = new cBlock();
			m_blocks[i].CreateCube ();
			m_blocks[i].SetMaterial (eMaterialType.YellowGreen);
		}
	}

	public void SForm () {
		FormSize (Size);
		
		m_form [1, 1] = true;
		m_form [1, 2] = true;
		m_form [2, 0] = true;
		m_form [2, 1] = true;
	}

	// Zテトリミノ.
	void ZTetrimino () {
		ZForm ();
		
		for (int i = 0; i < BlockNum; i++) {
			m_blocks[i] = new cBlock();
			m_blocks[i].CreateCube ();
			m_blocks[i].SetMaterial (eMaterialType.Red);
		}
	}

	public void ZForm () {
		FormSize (Size);
		
		m_form [1, 0] = true;
		m_form [1, 1] = true;
		m_form [2, 1] = true;
		m_form [2, 2] = true;
	}

	// Lテトリミノ.
	void LTetrimino () {
		LForm ();
		
		for (int i = 0; i < BlockNum; i++) {
			m_blocks[i] = new cBlock();
			m_blocks[i].CreateCube ();
			m_blocks[i].SetMaterial (eMaterialType.Orange);
		}
	}

	public void LForm () {
		FormSize (Size);
		
		m_form [1, 0] = true;
		m_form [1, 1] = true;
		m_form [1, 2] = true;
		m_form [2, 0] = true;
	}

	// Jテトリミノ.
	void JTetrimino () {
		JForm ();
		
		for (int i = 0; i < BlockNum; i++) {
			m_blocks[i] = new cBlock();
			m_blocks [i].CreateCube ();
			m_blocks [i].SetMaterial (eMaterialType.Blue);
		}
	}

	public void JForm () {
		FormSize (Size);
		
		m_form [1, 0] = true;
		m_form [1, 1] = true;
		m_form [1, 2] = true;
		m_form [2, 2] = true;
	}

	// 移動.
	// 第一引数：移動する値.
	public void Move (int moveX) {
		for (int i = 0; i < BlockNum; i++) {
			int blockPosition = (int)(m_blocks [i].GetCube ().transform.position.x + moveX);
			if (blockPosition < 2 || blockPosition > 11) {
				return;
			}
		}

		int px = (int)m_position.x + moveX;
		int py = (int)m_position.y;
		bool [,] form = cBlockManager.GetInstance ().GetCollision (px, py, m_size);

		for (int dy = 0; dy < m_size; dy++) {
			for (int dx = 0; dx < m_size; dx++) {
				if (m_form [dy, dx] && form [dy, dx]) {
					return;
				}
			}
		}

		for (int i = 0; i < BlockNum; i++) {
			m_blocks [i].MovePosition (new Vector3 (moveX, 0, 0));
		}

		m_position.x += moveX;
	}

	// 回転.
	// 第一引数：回転時の配列添え字x.
	// 第二引数：回転時の配列添え字y.
	public void Rotation (int rx, int ry) {
		if (m_type == O_Tetrimino) {
			return;
		}

		// 回転後のテトリミノの形状.
		bool [,] afterForm = new bool [m_size, m_size];
		int [] afterX = new int [BlockNum];
		int [] afterY = new int [BlockNum];
		int count = 0;
		for (int dy = 0; dy < m_size; dy++) {
			for (int dx = 0; dx < m_size; dx++) {
				int x = Mathf.Abs (rx - dy);
				int y = Mathf.Abs (ry - dx);
				afterForm [y, x] = m_form [dy, dx];

				if (afterForm [y, x]) {
					afterX [count] = x;
					afterY [count++] = y;
				}
			}
		}

		for (int i = 0; i < BlockNum; i++) {
			int blockPositionY = (int)(afterY [i] + m_position.y);
			if (blockPositionY > MaxUnder) {
				return;
			}
		}

		int px = (int)m_position.x;
		int py = (int)m_position.y;
		int check = CheckCollision (afterForm, px, py);
		int moveX = 0;

		if (check == 3) {
			return;
		}
		if (check == 0) {
			if (m_type == I_Tetrimino) {
				return;
			}
			for (int i = 0; i < BlockNum; i++) {
				int blockPosition = (int)(afterX [i] + m_position.x);
				if (CheckCollision (afterForm, px+1, py) != -1 || blockPosition+1 >= RightWall) {
					return;
				}
			}
			moveX = 1;
		}
		if (check == 2) {
			if (m_type == I_Tetrimino) {
				return;
			}
			for (int i = 0; i < BlockNum; i++) {
				int blockPosition = (int)(afterX [i] + m_position.x);
				if (CheckCollision (afterForm, px-1, py) != -1 || blockPosition-1 <= LeftWall) {
					return;
				}
			}
			moveX = -1;
		}
		if (check == 1) {
			return;
		}

		// 壁を蹴る時.
		for (int i = 0; i < BlockNum; i++) {
			int blockPosition = (int)(afterX [i] + m_position.x);
			if (blockPosition <= LeftWall) {
				if (m_type == I_Tetrimino) {
					return;
				}
				if (CheckCollision (afterForm, px+1, py) != -1) {
					return;
				}
				moveX = 1;
			}
			else if (blockPosition >= RightWall) {
				if (m_type == I_Tetrimino) {
					return;
				}
				if (CheckCollision (afterForm, px-1, py) != -1) {
					return;
				}
				moveX = -1;
			}
		}

		count = 0;
		for (int dy = 0; dy < m_size; dy++) {
			for (int dx = 0; dx < m_size; dx++) {
				m_form [dy, dx] = afterForm [dy, dx];
				if(m_form [dy, dx]) {
					m_blocks [count++].SetPosition (new Vector3 (dx + m_position.x + moveX, dy + m_position.y, 0f));
				}
			}
		}

		m_position.x += moveX;
	}

	// 落下.
	public bool Fall () {
		int x = (int)(m_position.x);
		int y = (int)(m_position.y + MoveSpeed);
		bool [,] fallAlreadyForm = cBlockManager.GetInstance ().GetCollision (x, y, m_size);
		bool [,] tetriminoForm = cTetriminoManager.GetInstance ().GetTetrimino ().GetForm ();
		
		for (int dy = 0; dy < m_size; dy++) {
			for (int dx = 0; dx < m_size; dx++) {
				if (tetriminoForm [dy, dx] && fallAlreadyForm [dy, dx]) {
					return false;
				}
			}
		}

		if (m_position.y >= CheckUnder) {
			for (int i = 0; i < BlockNum; i++) {
				if ((GetBlockPosition () [i].y + m_position.y + 1) > MaxUnder) {
					return false;
				}
			}
		}

		for (int i = 0; i < BlockNum; i++) {
			m_blocks [i].MovePosition (new Vector3 (0, -MoveSpeed, 0));
		}

		m_position.y += MoveSpeed;

		return true;
	}

	// 衝突判定.
	// 第一引数：ブロックマネージャーから持ってくるポイントx.
	// 第二引数：ブロックマネージャーから持ってくるポイントy.
	// 戻り値：０・左側衝突　１・真ん中衝突　２・右側衝突　−１・衝突していない.
	int CheckCollision (bool [,] form, int px, int py) {
		bool [,] collisionForm = cBlockManager.GetInstance ().GetCollision (px, py, m_size);

		for (int dy = 0; dy < m_size; dy++) {
			for (int dx = 0; dx < m_size; dx++) {
				if (form [dy, dx] && collisionForm [dy, dx]) {
					return dx;
				}
			}
		}

		return -1;
	}

	// テトリミノのタイプを取得.
	// 戻り値：テトリミノのタイプ.
	public int GetTetriminoType () {
		return m_type;
	}

	public Vector3 [] GetBlockPosition () {
		Vector3 [] position = new  Vector3 [BlockNum];
		int count = 0;

		for (int dy = 0; dy < m_size; dy++) {
			for (int dx = 0; dx < m_size; dx++) {
				if (m_form [dy, dx]) {
					position [count++] = new Vector3 (dx, dy, 0f);
				}
			}
		}

		return position;
	}

	public Vector3 GetTetriminoPosition () {
		return m_position;
	}

	public bool [,] GetForm () {
		return m_form;
	}

	public int GetSize () {
		return m_size;
	}

	public cBlock [] GetBlocks () {
		return m_blocks;
	}
}
