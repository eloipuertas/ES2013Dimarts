using UnityEngine;
using System.Collections;

public class creditsControl : MonoBehaviour {
	
	//Constants
	const int main_menu = 0;

	public float t;
	public TextMesh text;
	public Vector3 origen;
	public Vector3 destino;
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		origen = new Vector3(0,6,-10);
		destino = new Vector3(0,-108,-10);
		Camera.main.gameObject.transform.position = origen;
		text.text = "A nuestros familiares, amigos y a todos los que han\ncolaborado en la realización de este proyecto.";
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKey)
			Application.LoadLevel(main_menu);
		
		if(t>70)
			Application.LoadLevel(main_menu);
		t = Time.timeSinceLevelLoad;
		moveCamera(origen, destino, t);
	}
	
	void moveCamera(Vector3 o, Vector3 d, float t){
		Camera.main.gameObject.transform.position = Vector3.Lerp(o, d, t/60F);
	}
}