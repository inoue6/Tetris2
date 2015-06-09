using UnityEngine;
using System.Collections;

public class cScore {
	static cScore s_instance;
	int m_score = 0;

	private cScore () {

	}

	public static cScore GetInstance () {
		if (s_instance == null) {
			s_instance = new cScore ();
		}

		return s_instance;
	}

	public void AddScore (int deleteLine, float nowSpeed) {
		int deleteBonus = deleteLine * 2;
		int speedBonus = (int)((1 - nowSpeed) * 1000);

		m_score += deleteBonus * 100 + speedBonus;
	}

	public int GetScore () {
		return m_score;
	}

	public void Initialize () {
		m_score = 0;
	}
}
