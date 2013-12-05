using UnityEngine;
using System.Collections;
		
//bestScore.cs - This script will take control of the main menu options
		
public class bestScore : MonoBehaviour {
	
	//Variable
	public GUIText bestScoreNumber;
	
	// Use this for initialization
	void Start () {
		bestScoreNumber.text=PlayerPrefs.GetInt("ScoreNivel1").ToString();
	}
}