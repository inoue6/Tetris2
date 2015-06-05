using UnityEngine;
using System.Collections;

public class cTitleStateManager {
	// ステート.
	enum eState {
		Waiting,
		TransitionScene,
	}

	private static cTitleStateManager s_instance;		// インスタンス.
	private eState m_state;		// ステート.

	// コンストラクタ.
	private cTitleStateManager () {

	}

	// インスタンス.
	public static cTitleStateManager Instance {
		get {
			if(s_instance == null) {
				s_instance = new cTitleStateManager ();
			}

			return s_instance;
 		}
	}

	// 更新.
	public void Update () {
		switch (m_state) {
		case eState.Waiting:
			UpdateWaiting ();
			break;
		case eState.TransitionScene:
			break;
		}
	}

	// 次のステートをセット.
	void Transit (eState nextState) {
		switch (nextState) {
		case eState.Waiting:
			StartWaiting ();
			break;
		case eState.TransitionScene:
			break;
		}
		m_state = nextState;
	}

	// 待ちの開始.
	void StartWaiting () {

	}

	// 待ちの更新.
	void UpdateWaiting () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			Transit (eState.TransitionScene);
			cSceneManager.GetInstance ().SetNextScene (eScene.GameMain);
			GameObject ob = GameObject.Find ("StateManager");
			ob.AddComponent<cSceneManager> ();
		}
	}

	// 遷移のスタート.
	void StartSceneTransition () {

	}

	// 遷移の更新.
	void UpdateSceneTransition () {
	
	}
}
