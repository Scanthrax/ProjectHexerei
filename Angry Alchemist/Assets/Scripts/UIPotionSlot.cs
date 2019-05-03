using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPotionSlot : MonoBehaviour
{
    public UIPotion uiPotion;
    public PotionObject potion;



    public bool isEmpty
    {
        get
        {
            return potion == null;
        }
    }

    public void PutPotionInSlot(UIPotion pot)
    {
        uiPotion = pot;
        potion = uiPotion.potion;
        uiPotion.transform.SetParent(transform);
        uiPotion.transform.position = transform.position;
    }



    public UIPotion GetPotionFromSlot()
    {
        var pot = uiPotion;
        uiPotion = null;
        potion = null;
        return pot;
    }
}
