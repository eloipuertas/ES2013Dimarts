using UnityEngine;
using System.Collections;

public class Escena2 : MonoBehaviour {
	
	Light llum;

	// Use this for initialization
	
	
	void Start () {
		
		
		StartCoroutine(Coroutine (10));
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	IEnumerator Coroutine (int temps) {
		yield return new WaitForSeconds(temps);
		engega_llums();
	}
	
	
	public void engega_llums(){
		print ("llums!");
		GameObject[] llums= GameObject.FindGameObjectsWithTag("llum2");
		for (int i=0;i<llums.Length;i++){
			llum=llums[i].GetComponent<Light>();
			llum.enabled=true;
		
		}
		
		
	}
	
	
	public void activa_enemics(){
		GameObject[] enemics= GameObject.FindGameObjectsWithTag("Enemy");
		for (int i=0;i<enemics.Length;i++){
			enemics[i].SendMessage("activar",SendMessageOptions.DontRequireReceiver);
			
		
		}
	
	}
	
	
}