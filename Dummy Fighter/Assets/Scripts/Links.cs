using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LinkState { Think, Watch, Attack, Dodge  };

public class Links : MonoBehaviour {

    public int Idle { get; set; }
    public int Dodge { get; set; }
    public int Attack { get; set; }
    public LinkState State { get; set; }
   
    public Links()
    { }

    public Links (LinkState state)
    {
        State = state;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
   



}
