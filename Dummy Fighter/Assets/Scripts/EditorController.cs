using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorController : MonoBehaviour {


    public List<GameObject> states;
    //public List<GameObject> stack = new List<GameObject>();
	// Use this for initialization
	void Start () {
        Undo.ClearAll();
		
        for (int i=0; i<states.Count; i++)
        {
            states[i].GetComponent<Links>().State = (LinkState)i;
            Debug.Log((LinkState)i);
        }
	}
	
    public void UndoButton()
    {
       // Undo.RegisterCompleteObjectUndo(stack,"undoing");
    }
    public void RedoButton()
    {
       // Undo.PerformRedo();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
