using UnityEngine;
using System.Collections;

public class walkingSound : MonoBehaviour {
	
	public AudioClip[] walkSounds;
	public int oneSound;
	Vector3 posInicial,aux;
	// Use this for initialization
	void Start () {
		oneSound=0;
		posInicial= transform.position;
		
	
	}
	
	// Update is called once per frame
	void Update () {
		aux= posInicial-transform.position;
		if(aux.sqrMagnitude>4){
			posInicial=transform.position;
			AudioSource.PlayClipAtPoint(walkSounds[oneSound],posInicial);
			++oneSound;
			if(oneSound==2){ 
				oneSound=0;
			}
		}
	}
		

}
