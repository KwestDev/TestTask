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

            dragHandeler.itemBeingDragged.transform.SetParent(transform);
            var dragState = dragHandeler.itemBeingDragged.transform.GetComponent<Links>().State;

            if (_LibraryType == LibraryType.ChainLibrary)
            {

                List<Transform> list = new List<Transform>();
                for (int i = 0; i < transform.parent.childCount; i++)
                {

                    list.Add(transform.parent.GetChild(i));
                }

                var index = list.IndexOf(gameObject.transform);

                ChainInspector.Add(index, dragHandeler.itemBeingDragged);

                if (dragState == LinkState.Think || dragState == LinkState.Watch)
                {
                    dragHandeler.itemBeingDragged.transform.Find("GoToDisplay").gameObject.SetActive(true);

                }



            }
            else
            {
                dragHandeler.itemBeingDragged.transform.Find("GoToDisplay").gameObject.SetActive(false);
                ChainInspector.Remove(dragHandeler.itemBeingDragged);

            }
        
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
        }
    }
    #endregion
}