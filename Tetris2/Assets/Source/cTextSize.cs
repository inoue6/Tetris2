using UnityEngine;
using System.Collections;

public class cTextSize : MonoBehaviour {
	const int Width = 800;
	const int Height = 600;
	const int FontSize = 30;
	
	Vector2 m_screenSize;
	[SerializeField]
	GUIText m_guiText;

	// Use this for initialization
	void Start () {
		m_screenSize = new Vector2 (Width, Height);
		m_guiText.fontSize = FontSize;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_screenSize != new Vector2 (Screen.width, Screen.height)) {
			UpdateFontSize ();
		}

		m_guiText.text = "Score：" + cScore.GetInstance ().GetScore ();
	}

	void UpdateFontSize () {
		m_screenSize = new Vector2 (Screen.width, Screen.height);
		float fontSize = FontSize * (m_screenSize.x / Width);
		if(fontSize > FontSize * (m_screenSize.y / Height)) {
			fontSize = FontSize * (m_screenSize.y / Height);
		}
		m_guiText.fontSize = (int)fontSize;
	}
}
