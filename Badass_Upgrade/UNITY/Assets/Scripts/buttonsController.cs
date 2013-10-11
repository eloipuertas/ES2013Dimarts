using UnityEngine;
using System.Collections;

public class buttonsController : MonoBehaviour {
	
	public bool isAddButton = false;
	public bool isSubstractButton = false;
	public MainCharacter enem;
	
	public void OnMouseUpAsButton(){
		if(isAddButton)
			enem.enemies++;
		else if(isSubstractButton)
			enem.enemies--;
	}
}
