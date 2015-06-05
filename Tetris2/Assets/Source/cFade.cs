using UnityEngine;
using System.Collections;

public class cFade : MonoBehaviour {
	private Color m_fadeColor;		// フェードのカラー.
	private float m_time;		// 経過時間.

	// フェードの開始.
	public void StartFade () {
		m_time = 0f;
	}

	// フェードの更新.
	public bool UpdateFade (eFade fade) {
		float from = 0;
		float to = 0;
		switch (fade) {
		case eFade.FadeIn:
			from = 1f;
			break;
		case eFade.FadeOut:
			m_fadeColor = Color.black;
			to = 1f;
			break;
		}

		if (m_time <= 1f) {
			m_fadeColor.a = Mathf.Lerp (from, to, m_time / 1);
			m_time += Time.deltaTime;

			return true;
		}
		else {
			return false;
		}
	}

	// テクスチャ作成.
	public void OnGUI () {
		GUI.color = this.m_fadeColor;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
	}
}
