using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinksLibrary : MonoBehaviour {

    // Use this for initialization

    public List<Links> Library { get; set; }

    void Start () {
        Library = new List<Links>();
        Library.Add(new Links(LinkState.Think));
        Library.Add(new Links(LinkState.Watch));
        Library.Add(new Links(LinkState.Attack));
        Library.Add(new Links(LinkState.Dodge));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
