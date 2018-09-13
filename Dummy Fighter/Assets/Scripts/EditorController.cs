using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LitJson;
using UniRx;



public class EditorController : MonoBehaviour {


    public List<GameObject> states;
    public GameObject slot;

  

    void LoadSession (List<LinkData> pchain)
    {
        if (pchain.Count < 1)
            return;
        List<Transform> slots = new List<Transform>();
        for (int r=0; r < pchain.Count; r++ )
        {


        }
        Transform inspector = GameObject.FindGameObjectWithTag("ChainInspector").transform;
        for (int r = 0; r < pchain.Count; r++)
        {

            var gm = GameObject.Instantiate<GameObject>(slot);
            gm.transform.SetParent(inspector);
        }
        for (int i=0; i< inspector.childCount; i++)
        {
            slots.Add(inspector.GetChild(i));
        }
        int index = 0;
        foreach (var item in pchain)
        {
            var newLink = Instantiate<GameObject>(states[0]);
            newLink.transform.SetParent(slots[index++]);
            newLink.GetComponent<Links>().setLink(item);
            ChainInspector.Chain.Add(newLink);
            Text description = newLink.transform.GetChild(0).GetComponent<Text>();

            var newLinkstate = newLink.GetComponent<Links>();
            states.Add(newLink);

            switch (newLinkstate.State)
            {

                case LinkState.Attack:
                    description.text = "ATTACK"; 
                    break;
                case LinkState.Dodge:
                    description.text = "DODGE";
                    break;
                case LinkState.Think:
                    description.text = "THINK";
                    newLink.transform.Find("GoToDisplay").gameObject.SetActive(true);
                    newLinkstate.propertyBoxes[0].transform.Find("TextValue").GetComponent<Text>().text = newLinkstate.Idle.ToString() ;
                    newLinkstate.propertyBoxes[1].transform.Find("TextValue").GetComponent<Text>().text = newLinkstate.Attack.ToString();
                    newLinkstate.propertyBoxes[2].transform.Find("TextValue").GetComponent<Text>().text = newLinkstate.Dodge.ToString();
                    break;
                case LinkState.Watch:
                    description.text = "WATCH";
                    newLink.transform.Find("GoToDisplay").gameObject.SetActive(true);
                    newLinkstate.propertyBoxes[0].transform.Find("TextValue").GetComponent<Text>().text = newLinkstate.Idle.ToString();
                    newLinkstate.propertyBoxes[1].transform.Find("TextValue").GetComponent<Text>().text = newLinkstate.Attack.ToString();
                    newLinkstate.propertyBoxes[2].transform.Find("TextValue").GetComponent<Text>().text = newLinkstate.Dodge.ToString();
                    break;


            }

        }
    }

    public static bool enemyScene = false;
    
	void Start () {
        Undo.ClearAll();
		
        for (int i=0; i<states.Count; i++)
        {
            states[i].GetComponent<Links>().State = (LinkState)i;
           // Debug.Log((LinkState)i);
        }

        List<LinkData> PlayerChain = new List<LinkData>();
        List<LinkData> EnemyChain = new List<LinkData>();
        if (File.Exists(Application.dataPath + "/StreamingAssets/Player.json"))
         PlayerChain = JsonMapper.ToObject<List<LinkData>>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Player.json"));
        if (File.Exists(Application.dataPath + "/StreamingAssets/Enemy.json"))
            EnemyChain = JsonMapper.ToObject<List<LinkData>>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Enemy.json"));

        if (GameObject.FindGameObjectsWithTag("SceneIdentifier").Length>1)
            LoadSession(EnemyChain);
        else
            LoadSession(PlayerChain);


              
        
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

    public void OnFightPressed ()
    {

        ChainData.RefreshChain();
        foreach (var item in ChainInspector.Chain)
        {
            if (!item.name.Equals("empty"))
                ChainData.chain.Add(new LinkData(item));
        }

       
        var saveData = JsonMapper.ToJson(ChainData.chain);
       // Debug.Log(saveData);

        if (enemyScene)
        {
            File.WriteAllText(Application.dataPath + "/StreamingAssets/Enemy.json", saveData);
           
            SceneManager.LoadScene(1);
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/StreamingAssets/Player.json", saveData);
            SceneManager.LoadScene(0);
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
            

            var objectText = objectLink.propertyBoxes[_index].transform.Find("TextValue").GetComponent<Text>();
            var increment = index ? 1 : -1;

            switch (_index)
            {
                case 0:
                    objectLink.Idle += increment;
                    break;
                case 1:
                    objectLink.Attack += increment;
                    break;
                case 2:
                    objectLink.Dodge += increment;
                    break;
            }


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
