using UnityEngine;
using System.Collections;

public class cTitleStateManager {
	enum eState {
		Waiting,
		TransitionScene,
	}

	private static cTitleStateManager s_instance;
	private eState m_state;

	private cTitleStateManager () {

	}

	public static cTitleStateManager Instance {
		get {
			if(s_instance == null) {
				s_instance = new cTitleStateManager ();
			}

			return s_instance;
 		}
	}

	public void Update () {
		switch (m_state) {
		case eState.Waiting:
			UpdateWaiting ();
			break;
		case eState.TransitionScene:
			break;
		}
	}

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

	void StartWaiting () {

	}

	void UpdateWaiting () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			Transit (eState.TransitionScene);
			cSceneManager.GetInstance ().SetNextScene (eScene.GameMain);
			GameObject ob = GameObject.Find ("StateManager");
			ob.AddComponent<cSceneManager> ();
		}
	}

	void StartSceneTransition () {

	}

	void UpdateSceneTransition () {
	
	}
}
