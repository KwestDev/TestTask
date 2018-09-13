using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoHandler : MonoBehaviour {

    public static List<UndoCommandCenter> commandUndoList = new List<UndoCommandCenter>();
    public static List<UndoCommandCenter> commandRedoList = new List<UndoCommandCenter>();

    // Use this for initialization
    void Start () {
		
	}
	
    public void doUndo ()
    {
        if (commandUndoList.Count > 0)
        {
            int id = commandUndoList.Count - 1;
            UndoCommandCenter temp = commandUndoList[id];
            Debug.Log(temp.undoProperty);
            Debug.Log(temp.index1);
            Debug.Log(temp.modifiedObject.name);
            temp.UndoFunction();

            commandRedoList.Add(temp);

            commandUndoList.RemoveAt(id);
           
        }
    }
   
    public void doRedo ()
    {
        if (commandRedoList.Count > 0)
        {
            int id = commandRedoList.Count - 1;
            UndoCommandCenter temp = commandRedoList[id];
            temp.RedoFunction();
            commandUndoList.Add(temp);
            commandRedoList.RemoveAt(id);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
