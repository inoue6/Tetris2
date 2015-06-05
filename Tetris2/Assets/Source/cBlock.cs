using UnityEngine;
using System.Collections;

public class cBlock {
	GameObject m_cube;		// キューブオブジェクト.
	Material m_material = new Material (Shader.Find ("Specular"));		// マテリアル.

	// キューブオブジェクト生成.
	public void CreateCube () {
		m_cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
	}

	// キューブの座標ブロック移動.
	public void SetPosition (Vector3 position) {
		Vector3 setPosition = new Vector3 (position.x, -position.y, position.z);
		m_cube.transform.position = setPosition;
	}

	// マテリアルを設定.
	public void SetMaterial (eMaterialType type) {
		m_material = cMaterialManager.GetInstance ().GetMaterial (type);

		Renderer renderer = m_cube.GetComponent<Renderer> ();
		renderer.material = m_material;
	}

	// ブロック移動.
	public void MovePosition (Vector3 position) {
		m_cube.transform.Translate (position);
	}

	// ゲームオブジェクト取得.
	public GameObject GetCube () {
		return m_cube;
	}
}
