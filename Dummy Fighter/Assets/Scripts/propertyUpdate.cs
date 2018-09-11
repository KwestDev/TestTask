using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propertyUpdate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPress(int index)
    {
        transform.parent.parent.parent.GetComponent<Links>().OnPropertyClick(index);
    }
}
