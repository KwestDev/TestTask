using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainData : MonoBehaviour {

    // Use this for initialization
    public static List<LinkData> chain;

    public static void RefreshChain()
    {
        chain = new List<LinkData>();

    }
    public ChainData()
    {
        
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
