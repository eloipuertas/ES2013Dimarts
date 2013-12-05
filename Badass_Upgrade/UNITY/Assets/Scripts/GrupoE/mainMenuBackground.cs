using UnityEngine;
using System.Collections;
		
public class mainMenuBackground : MonoBehaviour {
	
	public GUITexture mainMenuBg;
	Rect menuBg;
	
	void Start () {
		menuBg=mainMenuBg.pixelInset;
		menuBg.width=Screen.width;
		menuBg.x=-1*Screen.width/2;
		menuBg.height=Screen.height;
		menuBg.y=-1*Screen.height/2;
		mainMenuBg.pixelInset=menuBg;
	}

}