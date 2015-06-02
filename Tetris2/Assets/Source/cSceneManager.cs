using UnityEngine;
using System.Collections;

public class cSceneManager : MonoBehaviour {
	enum eState {
		NothingIsDone,
		FadeIn,
		Transition,
		FadeOut,
	}

	static cSceneManager s_instance;
	cFade m_fade;
	eScene m_scene;
	eState m_state;

	void Awake () {
		if (s_instance == null) {
			DontDestroyOnLoad (gameObject);
			return;
		}
		cSceneManager instance = gameObject.GetComponent<cSceneManager> ();
		if (instance != null) {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		Transit (eState.NothingIsDone);
	}
	
	// Update is called once per frame
	void Update () {
		switch (m_state) {
		case eState.NothingIsDone:
			break;
		case eState.FadeOut:
			m_fade.UpdateFade (eFade.FadeOut);
			if(!m_fade.UpdateFade (eFade.FadeOut)) {
				Transit (eState.Transition);
			}
			break;
		case eState.Transition:
			UpdateSceneTransition ();
			break;
		case eState.FadeIn:
			if(!m_fade.UpdateFade (eFade.FadeIn)) {
				Transit (eState.NothingIsDone);
			}
			break;
		}
	}

	// インスタンスの取得.
	public static cSceneManager GetInstance () {
		if (s_instance == null) {
			GameObject gameObject = new GameObject("SceneManager");
			s_instance = gameObject.AddComponent<cSceneManager> ();
		}
		return s_instance;
	}

	// 切り替えるシーンをセット.
	public void SetNextScene (eScene nextScene) {
		m_scene = nextScene;
		Transit (eState.FadeOut);
	}

	// ステート切り替え.
	void Transit (eState nextState) {
		switch (nextState) {
		case eState.NothingIsDone:
			break;
		case eState.FadeOut:
			m_fade.StartFade ();
			break;
		case eState.Transition:
			break;
		case eState.FadeIn:
			m_fade.StartFade ();
			break;
		}

		m_state = nextState;
	}

	void UpdateSceneTransition () {
		switch (m_scene) {
		case eScene.Title:
			Application.LoadLevel ("Title");
			break;
		case eScene.GameMain:
			Application.LoadLevel ("GameMain");
			break;
		}

		Transit (eState.FadeIn);
	}

	// 
	public void Initialize () {
		cFade fade = gameObject.GetComponent<cFade> ();
		if (fade == null) {
			m_fade = gameObject.AddComponent<cFade> ();
		}
		else {
			m_fade = fade;
		}
	}
}
