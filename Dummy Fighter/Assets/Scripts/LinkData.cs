using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkData  {

    // Use this for initialization
    public int Idle ;
    public int Attack;
    public int Dodge;
    public string Type;

	void Start () {
		
	}

    public LinkData()
    {
        Idle = 0;
        Attack = 0;
        Dodge = 0;
        Type = "";

    }

    public LinkData(int idle, int attack, int dodge, string type)
    {
        Idle = idle;
        Attack = attack;
        Dodge = dodge;
        Type = type;

    }

    public LinkData (GameObject element)
    {
        string [] state = new string[] { "Think", "Watch", "Attack", "Dodge" };
        var link = element.GetComponent<Links>();
        Idle = link.Idle;
        Attack = link.Attack;
        Dodge = link.Dodge;
        Type = state[(int)link.State];
        
      
    }
    public void Display ()
    {
        Debug.Log(Idle +"" + Attack+"" + Dodge + " " + Type);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
