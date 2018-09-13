using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainInspector : MonoBehaviour {

    // Use this for initialization
    public static List<GameObject> Chain;
    public GameObject slot;
	void Start () {
        Chain = new List<GameObject>();
        for (int i=0;i<5;i++)
        {
            Chain.Add(new GameObject("empty"));
        }
	}

  
	
    public static void Add(int index,GameObject element)
    {
        if (Chain.Exists(x => element))
            Chain.Remove(element);
            Chain.Insert(index, element);

        

    }

    public static void Remove (GameObject element)
    {
        if (Chain.Exists(x => element))
            Chain.Remove(element);
    }
	// Update is called once per frame
	void Update () {
		

	}
}
