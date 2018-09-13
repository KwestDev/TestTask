using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animcomp : MonoBehaviour {

    // Use this for initialization
     public Animator anim;
    void Start() {

    }

    public void UpdateBar()
    {
        anim.SetBool("Attack", false);
        anim.SetBool("AttackReturn", false);
        anim.SetBool("Dodge", false);
        anim.SetBool("DodgeReturn", false);
        SimulationController.turnstate = true;

    }
	// Update is called once per frame
	void Update () {
		
	}
}
