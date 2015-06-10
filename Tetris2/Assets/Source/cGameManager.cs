using UnityEngine;
using UnityEditor;
using System.Collections;

public class cGameMainManager : MonoBehaviour {
	const float InitializeX = 5f;	// ブロックが生成されるx座標.
	const float InitializeY = 4f;	// ブロックが生成されるy座標.
	const float FollSpeed = 1f;
	const int MoveSpeed = 1;		// 移動スピード.
	const int BlockNum = 4;
	
	enum eState {
		Tutorial,
		GameStart,
		GameUpdate,
		CreateTetrimino,
		DeleteTetrimino,
		SetNext,
		GameOver,
		Score,
		Flashing,
	}

	static cGameMainManager s_instance;
	GameObject m_tutorialObject;
	GameObject m_gameOverObject;
	cGhost m_ghost;
	eState m_state;
	float m_time = 0f;
	float m_speed = FollSpeed;
	int m_deleteCount = 0;
	float m_downTime = 0.1f;
	int m_deleteLine = 0;
	float m_waitTime = 0f;

	void Awake () {
		if (s_instance == null) {
			DontDestroyOnLoad (gameObject);
			return;
		}
		cGameMainManager instance = gameObject.GetComponent<cGameMainManager> ();
		if (instance != null) {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		switch (m_state) {
		case eState.Tutorial:
			UpdateTutorial ();
			break;
		case eState.GameStart:
			UpdateGameStart ();
			break;
		case eState.CreateTetrimino:
			UpdateCreateTetrimino ();
			break;
		case eState.GameUpdate:
			UpdateGameUpdate ();
			break;
		case eState.DeleteTetrimino:
			UpdateDeleteTetrimino ();
			break;
		case eState.SetNext:
			UpdateSetNext ();
			break;
		case eState.GameOver:
			UpdateGameOver ();
			break;
		case eState.Score:
			UpdateScore ();
			break;
		case eState.Flashing:
			UpdateFlashing ();
			break;
		}
	}

	public void Initialize () {
		m_tutorialObject = GameObject.Find ("Description");
		m_gameOverObject = GameObject.Find ("GameOver");
		Transit (eState.Tutorial);
	}

	void Transit (eState nextState) {
		switch (nextState) {
		case eState.Tutorial:
			StartTutorial ();
			break;
		case eState.GameStart:
			StartGameStart ();
			break;
		case eState.CreateTetrimino:
			StartCreateTetrimino ();
			break;
		case eState.GameUpdate:
			StartGameUpdate ();
			break;
		case eState.DeleteTetrimino:
			break;
		case eState.SetNext:
			StartSetNext ();
			break;
		case eState.GameOver:
			StartGameOver ();
			break;
		case eState.Score:
			StartScore ();
			break;
		case eState.Flashing:
			StartFlashing ();
			break;
		}

		m_state = nextState;
	}

	public static cGameMainManager GetInstance () {
		if (s_instance == null) {
			GameObject gameObject = new GameObject ("GameMainManager");
			s_instance = gameObject.AddComponent<cGameMainManager> ();
		}

		return s_instance;
	}

	void StartTutorial () {
		m_tutorialObject.transform.position = new Vector3 (6.34f, -10.6f, -18.79f);
	}

	void UpdateTutorial () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			m_tutorialObject.transform.position = new Vector3 (100f, 100f, 100f);
			Transit (eState.GameStart);
		}
	}

	void StartGameStart () {
		cMaterialManager.GetInstance ().Initialize ();
		cTetriminoManager.GetInstance ().Initialize ();
		cBlockManager.GetInstance ().Initialize ();
		cScore.GetInstance ().Initialize ();
		m_ghost = new cGhost ();
		m_ghost.CreateGhost ();
		m_ghost.SetGhost (cTetriminoManager.GetInstance ().GetTetrimino ());
		m_ghost.DeleteGhost (cTetriminoManager.GetInstance ().GetTetrimino ());
	}

	void UpdateGameStart () {
		Transit (eState.SetNext);
	}

	void StartCreateTetrimino () {

	}

	void UpdateCreateTetrimino () {
		cTetriminoManager.GetInstance ().CreateTatrimino ();

		int size = cTetriminoManager.GetInstance ().GetTetrimino ().GetSize ();
		bool [,] fallAlreadyForm = cBlockManager.GetInstance ().GetCollision ((int)InitializeX, (int)InitializeY - size, size);
		bool [,] tetriminoForm = cTetriminoManager.GetInstance ().GetTetrimino ().GetForm ();
		
		for (int dy = 0; dy < size; dy++) {
			for (int dx = 0; dx < size; dx++) {
				if (tetriminoForm [dy, dx] && fallAlreadyForm [dy, dx]) {
					for (int i = 0; i < BlockNum; i++) {
						Destroy (cTetriminoManager.GetInstance ().GetTetrimino ().GetBlocks () [i].GetCube ());
					}
					m_ghost.DeleteGhost ();
					Transit (eState.GameOver);
					return;
				}
			}
		}

		Transit (eState.SetNext);
	}

	void StartGameOver () {
		m_gameOverObject.transform.position = new Vector3 (7.08f, -9.56f, -1);
	}

	void UpdateGameOver () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			cSceneManager.GetInstance ().SetNextScene (eScene.Title);
			Destroy (cMaterialManager.GetInstance ().gameObject);
			Destroy (cBlockManager.GetInstance ().gameObject);
			Destroy (gameObject);
		}
	}

	void StartSetNext () {
		cTetriminoManager.GetInstance ().SetNext ();
		m_ghost.SetGhost (cTetriminoManager.GetInstance ().GetTetrimino ());
		m_ghost.DeleteGhost (cTetriminoManager.GetInstance ().GetTetrimino ());
		m_waitTime = 0f;
	}

	void UpdateSetNext () {
		m_waitTime += Time.deltaTime;
		if (m_waitTime >= 0.2f) {
			Transit (eState.GameUpdate);
		}
	}

	void StartGameUpdate () {

	}

	void UpdateGameUpdate () {
		m_time += Time.deltaTime;
		m_ghost.DeleteGhost (cTetriminoManager.GetInstance ().GetTetrimino ());

		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (m_downTime >= 0.1f) {
				cTetriminoManager.GetInstance ().GetTetrimino ().Move (-MoveSpeed);
				m_downTime = 0f;
			}
			m_downTime += Time.deltaTime;
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			m_downTime = 0.1f;
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			if (m_downTime >= 0.1f) {
				cTetriminoManager.GetInstance ().GetTetrimino ().Move (MoveSpeed);
				m_downTime = 0f;
			}
			m_downTime += Time.deltaTime;
		}
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			m_downTime = 0.1f;
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			cTetriminoManager.GetInstance ().GetTetrimino ().Rotation (0, cTetriminoManager.GetInstance ().GetTetrimino ().GetSize ()-1);
		}
		
		if (Input.GetKeyDown (KeyCode.S)) {
			cTetriminoManager.GetInstance ().GetTetrimino ().Rotation (cTetriminoManager.GetInstance ().GetTetrimino ().GetSize ()-1, 0);
		}

		if (m_state != eState.Flashing) {
			m_ghost.SetGhost (cTetriminoManager.GetInstance ().GetTetrimino ());
			m_ghost.DeleteGhost (cTetriminoManager.GetInstance ().GetTetrimino ());
		}

		if (m_time >= m_speed) {
			FallTetrimino ();
			m_time = 0f;
			return;
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			if (m_downTime >= 0.1f) {
				FallTetrimino ();
				m_downTime = 0f;
			}
			m_downTime += Time.deltaTime;
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			m_downTime = 0.1f;
		}
	}

	void FallTetrimino () {
		bool fall =  cTetriminoManager.GetInstance ().GetTetrimino ().Fall ();

		if (fall) {
			return;
		}

		int [] x = new int[BlockNum];
		int [] y = new int[BlockNum];
		
		for (int i = 0; i < BlockNum; i++) {
			x [i] = (int)(cTetriminoManager.GetInstance ().GetTetrimino ().GetBlockPosition () [i].x + cTetriminoManager.GetInstance ().GetTetrimino ().GetTetriminoPosition ().x);
			y [i] = (int)(cTetriminoManager.GetInstance ().GetTetrimino ().GetBlockPosition () [i].y + cTetriminoManager.GetInstance ().GetTetrimino ().GetTetriminoPosition ().y);
		}
		
		cBlockManager.GetInstance ().AddBlock (cTetriminoManager.GetInstance ().GetTetrimino ().GetBlocks (), x, y);

		int positionY = (int)cTetriminoManager.GetInstance ().GetTetrimino ().GetTetriminoPosition ().y;
		cBlockManager.GetInstance ().DeletePosition (positionY);
		for (int i = 0; i < BlockNum; i++) {
			if (cBlockManager.GetInstance ().GetDeletePositionY () [i] != 0) {
				Transit (eState.Flashing);
				return;
			}
		}

		Transit (eState.CreateTetrimino);
	}

	void StartScore () {
		cScore.GetInstance ().AddScore (m_deleteCount, m_speed);
		Debug.Log (cScore.GetInstance ().GetScore ());
	}

	void UpdateScore () {
		Transit (eState.CreateTetrimino);
	}

	void StartFlashing () {
		cBlockManager.GetInstance ().FlashingTime = 0f;
		m_ghost.DeleteGhost ();
	}

	void UpdateFlashing () {
		if (cBlockManager.GetInstance ().FlashingTetrimino ()) {
			Transit (eState.DeleteTetrimino);
		}
	}

	void StartDeleteTetrimino () {

	}

	void UpdateDeleteTetrimino () {
		m_deleteCount = cBlockManager.GetInstance ().DeleteBlock ();
		m_deleteLine += m_deleteCount;
		if (m_deleteLine >= 10) {
			m_deleteLine = 0;
			m_speed -= 0.03f;
			if (m_speed <= 0.01f) {
				m_speed = 0.01f;
			}
		}
		if (m_deleteCount > 0) {
			Transit (eState.Score);
			return;
		}
	}
}
