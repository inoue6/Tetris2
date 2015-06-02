using UnityEngine;
using System.Collections;

public class cBlock {
	GameObject m_cube;
	Material m_material = new Material (Shader.Find ("Specular"));

	public void CreateCube () {
		m_cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
	}
	
	public void SetPosition (Vector3 position) {
		m_cube.transform.position = position;
	}

	public void SetColor (eMaterialType type) {
		m_material = cMaterialManager.GetInstance ().GetMaterial (type);

		Renderer renderer = m_cube.GetComponent<Renderer> ();
		renderer.material = m_material;
	}

	public void MovePosition (Vector3 position) {
		m_cube.transform.Translate (position);
	}
}
