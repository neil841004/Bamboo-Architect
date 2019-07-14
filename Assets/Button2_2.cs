using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button2_2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindWithTag("Result")){
			this.GetComponent<BoxCollider>().enabled=false;
		}
		else if (!GameObject.FindWithTag("Result")){
			this.GetComponent<BoxCollider>().enabled=true;
		}
	}
}
