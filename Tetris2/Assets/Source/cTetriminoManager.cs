using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cTetriminoManager {
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
		m_tetrimino.CreateTatrimino (m_orders.First.Value);
		m_orders.RemoveFirst ();
	}

	public void CreateNext () {
		int order = m_orders.First.Value;
		m_next = new cTetrimino ();
		m_next.CreateTatrimino (order);
		m_next.SetPosition (new Vector3 (10f, 10f, 0));
	}

	public void SetNext () {
		int order = m_orders.First.Value;

		switch (order) {
		case cTetrimino.I_Tetrimino:
			m_next.IForm ();
			break;
		case cTetrimino.O_Tetrimino:
			m_next.OForm ();
			break;
		case cTetrimino.T_Tetrimino:
			m_next.TForm ();
			break;
		case cTetrimino.S_Tetrimino:
			m_next.SForm ();
			break;
		case cTetrimino.Z_Tetrimino:
			m_next.ZForm ();
			break;
		case cTetrimino.L_Tetrimino:
			m_next.LForm ();
			break;
		case cTetrimino.J_Tetrimino:
			m_next.JForm ();
			break;
		}

		m_next.SetPosition (new Vector3 (10f, 10f, 0));
	}

	public cTetrimino GetTetrimino () {
		return m_tetrimino;
	}
}
