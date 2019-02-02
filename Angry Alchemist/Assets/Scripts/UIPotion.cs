using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPotion : MonoBehaviour
{
    public PotionObject potion;
    public Image sprite;

    public void Initialize(PotionObject pot)
    {
        potion = pot;
        sprite.sprite = pot.image;
    }
}
