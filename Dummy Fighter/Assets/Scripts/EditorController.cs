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
           // Debug.Log((LinkState)i);
        }
	}
	public void disableProperties ()
    {
        foreach (var obj in states )
        {
            if (obj.GetComponent<Links>().BoxActive)
            {

                var propertyObjects = obj.transform.Find("GoToDisplay").GetComponentsInChildren<GameObject>();
                foreach (var item in propertyObjects)
                {

                    item.transform.Find("colorChild").gameObject.SetActive(false);
                }
            }

        }

    }

    public void OnPropertyChange (bool index)
    {

        


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
