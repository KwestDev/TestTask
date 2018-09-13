using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public enum LibraryType { ChainLibrary , LinkLibrary};

public class Slot : MonoBehaviour, IDropHandler
{
    public LibraryType _LibraryType { get; set; }


    void Start()
    {

        if (transform.parent.gameObject.name.Equals("LinksLibrary"))
            _LibraryType = LibraryType.LinkLibrary;
        else
            _LibraryType = LibraryType.ChainLibrary;

    }
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    #region IDropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {

        if (!item)
        {

            

            if (_LibraryType == LibraryType.ChainLibrary)
            {

                List<Transform> list = new List<Transform>();
                for (int i = 0; i < transform.parent.childCount; i++)
                {

                    list.Add(transform.parent.GetChild(i));
                }


                var slot = GameObject.Instantiate(gameObject);
                slot.transform.SetParent(transform.parent);
                UndoCommandCenter temp = new UndoCommandCenter();

                GameObject clone;
                if (dragHandeler.itemBeingDragged.transform.parent.GetComponent<Slot>()._LibraryType == LibraryType.ChainLibrary)
                {
                    clone = dragHandeler.itemBeingDragged;
                    temp.undoProperty = UndoType.SwapLink;
                    temp.index1 = list.IndexOf(clone.transform.parent.transform);

                }
                else
                {
                    clone = GameObject.Instantiate(dragHandeler.itemBeingDragged);
                    temp.undoProperty = UndoType.CreateLink;
                    temp.index1 = list.IndexOf(gameObject.transform);
                }

                clone.GetComponent<Links>().State = dragHandeler.itemBeingDragged.GetComponent<Links>().State;
                GameObject.FindGameObjectWithTag("EditorController").GetComponent<EditorController>().states.Add(clone);
               
                clone.transform.SetParent(transform);
                clone.transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
                var dragState = clone.GetComponent<Links>().State;
                
                temp.modifiedObject = clone;

                temp.state = clone.GetComponent<Links>().State;



                var index = list.IndexOf(gameObject.transform);

                temp.index2 = index;
                ChainInspector.Add(index, clone);

                UndoHandler.commandUndoList.Add(temp);
                UndoHandler.commandRedoList.Clear();

                if (dragState == LinkState.Think || dragState == LinkState.Watch)
                {
                    clone.transform.Find("GoToDisplay").gameObject.SetActive(true);

                }



            }
            else
            {
                dragHandeler.itemBeingDragged.transform.Find("GoToDisplay").gameObject.SetActive(false);
                //ChainInspector.Remove(dragHandeler.itemBeingDragged);

            }
        
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
        }
    }
    #endregion
}