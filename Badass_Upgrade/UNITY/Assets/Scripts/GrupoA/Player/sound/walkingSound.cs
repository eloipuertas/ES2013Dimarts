using UnityEngine;
using System.Collections;

public class walkingSound : MonoBehaviour {
	
	public AudioClip[] walkSounds;
	public int oneSound;
	Vector3 posInicial,aux,posActual;
	bool onPlataforma;
	// Use this for initialization
	void Start () {
		oneSound=0;
		posInicial= transform.position;
		posInicial.y = 0;
		onPlataforma = false;
	}
	
	// Update is called once per frame
	void Update () {
		aux= posInicial-transform.position;
		aux.y = 0;
        if(!onPlataforma && aux.sqrMagnitude>4){
            posInicial=transform.position;
			posInicial.y = 0;
			AudioSource.PlayClipAtPoint(walkSounds[oneSound],transform.position,0.15F);
			++oneSound;
			if(oneSound==2){ 
				oneSound=0;
			}
		}
	}
		

}
