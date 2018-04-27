using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignUnlitColor : MonoBehaviour {
	public Color unlitColor;

	void Start () {
		GetComponent<Renderer>().material.SetColor("_Color", unlitColor);	
	}
	
	//void Update () {
	//	GetComponent<Renderer>().material.SetColor("_Color", unlitColor);
	//}
}
