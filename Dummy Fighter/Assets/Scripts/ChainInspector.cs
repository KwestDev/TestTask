using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainInspector : MonoBehaviour {

	// Use this for initialization
    public List<Links> Chain { get; set; }
	void Start () {
        Chain = new List<Links>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
