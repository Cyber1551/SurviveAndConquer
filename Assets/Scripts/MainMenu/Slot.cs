using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler {

    public string type;
	public GameObject spell
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

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(spell);
        if (spell == null)
        {
            Debug.Log(type);
            if (type == "" ||type == null || type == GameObject.Find("Inventory").GetComponent<SpellList>().getSpellElement(DragHandler.itemDragged.GetComponent<Image>().sprite))
            {
                DragHandler.itemDragged.transform.SetParent(transform);
                DragHandler.itemDragged.transform.position = transform.position;

                //transform.GetChild(0).GetComponent<Text>().text = DragHandler.itemDragged.GetComponent<SpellButton>().name;
                DragHandler.itemDragged.GetComponent<LayoutElement>().ignoreLayout = false;

            }


        }
    }
}
