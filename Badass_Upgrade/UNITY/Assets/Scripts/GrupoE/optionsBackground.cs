using UnityEngine;
using System.Collections;

public class optionsBackground : MonoBehaviour {
	
	public GUITexture optionsBg;
	Rect optBg;
	
	// Use this for initialization
	void Start () {
		optBg=optionsBg.pixelInset;
		optBg.width=Screen.width;
		optBg.x=-1*Screen.width/2;
		optBg.height=Screen.height;
		optBg.y=-1*Screen.height/2;
		optionsBg.pixelInset=optBg;
	}
}
