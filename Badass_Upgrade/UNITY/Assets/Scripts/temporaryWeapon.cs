using UnityEngine;
using System.Collections;

public class temporaryWeapon : MonoBehaviour {
	
	//public string modelWeapon;
    public int balasTotales;
    public int balasCargador;
    
    void Awake() {
		//this.modelWeapon = "Gun";
        balasTotales = 10;
        balasCargador = 100;
	}
    
//    public int getBalesTotals() {
//		return balesTotals;
//	}
//	
//	public int getBalesActualCarregador() {
//		return balesActualCarregador;
//	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
}