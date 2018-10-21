using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    private const int SLOTS = 6;
    public int currentSlot = 0;

    Spell[] splls = new Spell[6];
    
    SpellList sl;
    public void Start()
    {
        foreach (Transform t in transform.GetChild(0).transform)
        {
            
            t.GetChild(0).GetComponent<Image>().sprite = null;
                
            
        }
        string[] spells = PlayerData.spellBook;
         sl = GetComponent<SpellList>();
        for (int i = 0; i < spells.Length; i++ )
        {
            Debug.Log(spells[i]);
            Spell spl = sl.getSpell(spells[i]);
            if (spl != null)
            {
                splls[i] = spl;
                Transform t = transform.GetChild(0).GetChild(i).transform;
                
                    
                t.GetChild(0).GetComponent<Image>().sprite = spl.img;
               
                    
                

            }
        }
        UpdateSpells();
        
    }

    public void UpdateSpells()
    {
        MagicFireProjectile mfp = transform.parent.parent.parent.GetComponent<MagicFireProjectile>();

        Spell currentSpell = splls[currentSlot];
       Debug.Log(currentSpell);

        if (currentSpell != null)
        {
            string type = sl.getSpellType(currentSpell.img);
            switch (type)
            {
                case "Projectile":

                    mfp.projectiles = currentSpell.gameObject;
                    break;
            }
        }
        else
        {
            mfp.projectiles = null;
        }
            
        
       
          
    
       
     
            
       
    }

}
