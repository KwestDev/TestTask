using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UndoType { CreateLink , SwapLink, UpdateProperty }
public class UndoCommandCenter : MonoBehaviour {

    // Use this for initialization
    public GameObject modifiedObject { get; set; }
    public UndoType undoProperty { get; set; }
    public LinkState state { get; set; }
    public int index1 { get; set; }
    public int index2 { get; set; }
    public ActiveProperty property { get; set; }
    void Start () {
		
	}
	
    public void UndoFunction ()
    {
        switch (undoProperty)
        {

            case UndoType.CreateLink:
                ChainInspector.Chain.Remove(modifiedObject);
                GameObject.FindGameObjectWithTag("EditorController").GetComponent<EditorController>().states.Remove(modifiedObject);
                Destroy(modifiedObject);

                break;
            case UndoType.SwapLink:
                ChainInspector.Chain.Remove(modifiedObject);
                ChainInspector.Chain.Insert(index1, modifiedObject);
                modifiedObject.transform.SetParent(GameObject.FindGameObjectWithTag("ChainInspector").transform.GetChild(index1));
                break;
            case UndoType.UpdateProperty:
                var linkObj = modifiedObject.transform.parent.parent.GetComponent<Links>();
                if (property == ActiveProperty.Attack)
                    linkObj.Attack = index1;
                else if (property == ActiveProperty.Dodge)
                    linkObj.Dodge = index1;
                else if (property == ActiveProperty.Idle)
                    linkObj.Idle = index1;
                modifiedObject.transform.GetChild(0).GetComponent<Text>().text = index1.ToString();
                break;

        }
    }

    public void RedoFunction()
    {
        switch (undoProperty)
        {

            case UndoType.CreateLink:
              
                modifiedObject = Instantiate(GameObject.FindGameObjectWithTag("Link"));
                ChainInspector.Chain.Insert(index1,modifiedObject);
                GameObject.FindGameObjectWithTag("EditorController").GetComponent<EditorController>().states.Add(modifiedObject);
                modifiedObject.transform.SetParent(GameObject.FindGameObjectWithTag("ChainInspector").transform.GetChild(index1));
                var text = modifiedObject.transform.GetChild(0).GetComponent<Text>();
                switch (state)
                {
                    case LinkState.Attack:
                        text.text = "ATTACK";
                        break;
                    case LinkState.Dodge:
                        text.text = "DODGE";
                        break;
                    case LinkState.Watch:
                        text.text = "WATCH";
                        modifiedObject.transform.Find("GoToDisplay").gameObject.SetActive(true);
                        break;
                    case LinkState.Think:
                        text.text = "THINK";
                        modifiedObject.transform.Find("GoToDisplay").gameObject.SetActive(true);
                        break;

                }

                break;
            case UndoType.SwapLink:
                ChainInspector.Chain.Remove(modifiedObject);
                ChainInspector.Chain.Insert(index2, modifiedObject);
                modifiedObject.transform.SetParent(GameObject.FindGameObjectWithTag("ChainInspector").transform.GetChild(index2));
                break;
            case UndoType.UpdateProperty:
                if (modifiedObject != null)
                {
                    var linkObj = modifiedObject.transform.parent.parent.GetComponent<Links>();
                    if (property == ActiveProperty.Attack)
                        linkObj.Attack = index2;
                    else if (property == ActiveProperty.Dodge)
                        linkObj.Dodge = index2;
                    else if (property == ActiveProperty.Idle)
                        linkObj.Idle = index2;
                    modifiedObject.transform.GetChild(0).GetComponent<Text>().text = index2.ToString();

                }
                break;

        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
