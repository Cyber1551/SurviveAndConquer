using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler
{
    public static GameObject itemDragged;
    
    Vector3 startPosition;
    Transform startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.gameObject.GetComponent<LayoutElement>().ignoreLayout = true;
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == startParent)
        {
            transform.position = startPosition;
        }
        
        this.gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject spelldesc = GameObject.Find("SpellDesc");
        Sprite spr = transform.GetComponent<Image>().sprite; 
        spelldesc.transform.GetChild(0).GetComponent<Image>().sprite = spr;
        SpellList sl = GameObject.Find("Inventory").GetComponent<SpellList>();
        string spellType = sl.getSpellType(spr);
        //Debug.Log(spellType);
        switch (spellType)
        {
            case "Projectile":
                Projectile spl = (Projectile)sl.getSpell(sl.getSpellName(spr, sl.getSpellElement(spr)));
                spelldesc.transform.GetChild(2).GetComponent<Text>().text = spl.spellName;
                string txt = "Damage: " + spl.damage + "\nType: " + spl.type + "\nCooldown: " + spl.timer + " seconds\nElement: " + spl.element + "\nSpeed: " + spl.speed + "\nPassive: Coming Soon\n";
                spelldesc.transform.GetChild(1).GetComponent<Text>().text = txt;
                break;
        }
        

           

        
    }

  
}
