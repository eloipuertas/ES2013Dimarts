using UnityEngine;
using System.Collections;

public class boss_trail_collider : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Destroy(this.gameObject,9.0f);
	}
	// Update is called once per frame
	void Update () {
	}
}
