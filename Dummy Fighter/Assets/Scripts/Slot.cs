using UnityEngine;
using System.Collections;
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
            if (dragState == LinkState.Think || dragState == LinkState.Watch)
            {
                if (_LibraryType == LibraryType.ChainLibrary)
                    dragHandeler.itemBeingDragged.transform.Find("GoToDisplay").gameObject.SetActive(true);
                else
                    dragHandeler.itemBeingDragged.transform.Find("GoToDisplay").gameObject.SetActive(false);
            }
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
        }
    }
    #endregion
}