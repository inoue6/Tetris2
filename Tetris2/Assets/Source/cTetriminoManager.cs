using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cTetriminoManager {
	const int BlockNum = 4;

	static cTetriminoManager s_instance;
	LinkedList<int> m_orders = new LinkedList<int> ();
	cTetrimino m_tetrimino;		// テトリミノ.
	cTetrimino m_next;

	private cTetriminoManager () {

	}

	public static cTetriminoManager GetInstance () {
		if (s_instance == null) {
			s_instance = new cTetriminoManager ();
		}

		return s_instance;
	}

	public void Initialize () {
		InitializeOrder ();
		CreateTatrimino ();
		CreateNext ();
	}

	void InitializeOrder () {
		for (int i = 0; i < 10; i++) {
			AddOrder ();
		}
	}

	void AddOrder () {
		for (int i = 0; i < 5; i++) {
			int order = Random.Range (0, 7);
			if (m_orders.Find (order) == null || i == 4) {
				m_orders.AddLast (order);
				return;
			}
		}
	}

	public void CreateTatrimino () {
		m_tetrimino = new cTetrimino ();
		//cTetrimino
		m_tetrimino.CreateTatrimino (m_orders.First.Value);
		m_orders.RemoveFirst ();
		AddOrder ();
	}

	public void CreateNext () {
		int order = m_orders.First.Value;
		m_next = new cTetrimino ();
		m_next.CreateTatrimino (order);
		m_next.SetPosition (new Vector3 (18f, 5f, 0));
	}

	public void SetNext () {
		int order = m_orders.First.Value;

		switch (order) {
		case cTetrimino.I_Tetrimino:
			m_next.IForm ();
			for (int i = 0; i < BlockNum; i++) {
				m_next.GetBlocks () [i].SetMaterial (eMaterialType.LightBlue);
			}
			break;
		case cTetrimino.O_Tetrimino:
			m_next.OForm ();
			for (int i = 0; i < BlockNum; i++) {
				m_next.GetBlocks () [i].SetMaterial (eMaterialType.Yellow);
			}
			break;
		case cTetrimino.T_Tetrimino:
			m_next.TForm ();
			for (int i = 0; i < BlockNum; i++) {
				m_next.GetBlocks () [i].SetMaterial (eMaterialType.Purple);
			}
			break;
		case cTetrimino.S_Tetrimino:
			m_next.SForm ();
			for (int i = 0; i < BlockNum; i++) {
				m_next.GetBlocks () [i].SetMaterial (eMaterialType.YellowGreen);
			}
			break;
		case cTetrimino.Z_Tetrimino:
			m_next.ZForm ();
			for (int i = 0; i < BlockNum; i++) {
				m_next.GetBlocks () [i].SetMaterial (eMaterialType.Red);
			}
			break;
		case cTetrimino.L_Tetrimino:
			m_next.LForm ();
			for (int i = 0; i < BlockNum; i++) {
				m_next.GetBlocks () [i].SetMaterial (eMaterialType.Orange);
			}
			break;
		case cTetrimino.J_Tetrimino:
			m_next.JForm ();
			for (int i = 0; i < BlockNum; i++) {
				m_next.GetBlocks () [i].SetMaterial (eMaterialType.Blue);
			}
			break;
		}

		m_next.SetPosition (new Vector3 (18f, 5f, 0));
	}

	public cTetrimino GetTetrimino () {
		return m_tetrimino;
	}
}
