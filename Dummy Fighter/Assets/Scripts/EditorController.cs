using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


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
        foreach (var obj in states)
        {
            if (obj.GetComponent<Links>().BoxActive)
            {


                var propertyObjects = obj.transform.Find("GoToDisplay");

                for (int i=0; i<propertyObjects.childCount; i++)
                {

                    propertyObjects.transform.GetChild(i).GetComponent<Image>().color = Color.white;

                }
                obj.GetComponent<Links>().BoxActive = false;
            }

        }

    }

    public void OnPropertyChange (bool index)
    {

       // Debug.Log("working");
        var obj = states.Find(x => x.GetComponent<Links>().BoxActive);
        if (obj!=null)
        {
            var objectLink = obj.GetComponent<Links>();
            int _index = (int)objectLink.Property;
            switch (_index)
            {
                case 0:
                    objectLink.Idle++;
                    break;
                case 1:
                    objectLink.Attack++;
                    break;
                case 2:
                    objectLink.Dodge++;
                    break;
            }

            var objectText = objectLink.propertyBoxes[_index].transform.Find("TextValue").GetComponent<Text>();
            var increment = index ? 1 : -1;
            

            objectText.text = (Convert.ToInt32(objectText.text) + increment).ToString();
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
