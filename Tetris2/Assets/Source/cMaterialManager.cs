using UnityEngine;
using UnityEditor;
using System.Collections;

public class cMaterialManager : MonoBehaviour {
	static cMaterialManager s_instance;		// インスタンス.

	// 各色のマテリアル.
	Material m_red;
	Material m_blue;
	Material m_yellow;
	Material m_purple;
	Material m_orange;
	Material m_lightBlue;
	Material m_yellowGreen;
	
	bool m_initializeFlag = false;		// 初期化フラグ.

	void Awake () {
		if (s_instance == null) {
			DontDestroyOnLoad (gameObject);
			return;
		}
		cMaterialManager instance = gameObject.GetComponent<cMaterialManager> ();
		if (instance != null) {
			Destroy (gameObject);
		}
	}

	// インスタンスを取得.
	// 戻り値：インスタンス.
	public static cMaterialManager GetInstance () {
		if(s_instance == null) {
			GameObject gameObject = new GameObject ("MaterialManager");
			s_instance = gameObject.AddComponent<cMaterialManager> ();
		}

		return s_instance;
	}

	// マテリアルの初期化.
	public void Initialize () {
		if (!m_initializeFlag) {
			m_red = AssetDatabase.LoadAssetAtPath ("Assets/Material/Tetrimino/Red.mat", typeof(Material)) as Material;
			m_blue = AssetDatabase.LoadAssetAtPath ("Assets/Material/Tetrimino/Blue.mat", typeof(Material)) as Material;
			m_yellow = AssetDatabase.LoadAssetAtPath ("Assets/Material/Tetrimino/Yellow.mat", typeof(Material)) as Material;
			m_orange = AssetDatabase.LoadAssetAtPath ("Assets/Material/Tetrimino/Orange.mat", typeof(Material)) as Material;
			m_purple = AssetDatabase.LoadAssetAtPath ("Assets/Material/Tetrimino/Purple.mat", typeof(Material)) as Material;
			m_lightBlue = AssetDatabase.LoadAssetAtPath ("Assets/Material/Tetrimino/LightBlue.mat", typeof(Material)) as Material;
			m_yellowGreen = AssetDatabase.LoadAssetAtPath ("Assets/Material/Tetrimino/YellowGreen.mat", typeof(Material)) as Material;
		}

		m_initializeFlag = true;
	}

	// マテリアル取得.
	// 第一引数：マテリアルのタイプ.
	// 戻り値：マテリアル.
	public Material GetMaterial (eMaterialType type) {
		Material material = null;

		switch (type) {
		case eMaterialType.LightBlue:
			material = m_lightBlue;
			break;
		case eMaterialType.Yellow:
			material = m_yellow;
			break;
		case eMaterialType.Purple:
			material = m_purple;
			break;
		case eMaterialType.YellowGreen:
			material = m_yellowGreen;
			break;
		case eMaterialType.Red:
			material = m_red;
			break;
		case eMaterialType.Orange:
			material = m_orange;
			break;
		case eMaterialType.Blue:
			material = m_blue;
			break;
		}

		return material;
	}
}
