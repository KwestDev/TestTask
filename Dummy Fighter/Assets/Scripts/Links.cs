using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum LinkState { Think, Watch, Attack, Dodge };
public enum ActiveProperty { Idle, Attack, Dodge};

public class Links : MonoBehaviour {

    public int Idle { get; set; }
    public int Dodge { get; set; }
    public int Attack { get; set; }
    public LinkState State { get; set; }
    public ActiveProperty Property { get; set; }
    public List<GameObject> propertyBoxes; 
    public bool BoxActive { get; set; }
    private EditorController control = GameObject.FindGameObjectWithTag("EditorController").GetComponent<EditorController>();
    public  void OnPropertyClick (int index)
    {

        control.disableProperties();
        var propertyObject = propertyBoxes[index].transform.Find("colorChild").gameObject;
        propertyObject.SetActive(true);
        Property = (ActiveProperty) index;
        BoxActive = true;
        


    }
  
    public Links()
    { }

    public Links (LinkState state)
    {
        State = state;
    }
    void Start () {

        BoxActive = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
   



}
