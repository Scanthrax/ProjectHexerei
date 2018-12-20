using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionBar : MonoBehaviour
{
    public GameObject[] potions;
    public PotionObject[] potionsObj;

    public void Init(int i, PotionObject potion)
    {
        potions[i - 1].GetComponent<Image>().sprite = potion.image;
    }

    private void OnValidate()
    {
        for (int i = 1; i < potions.Length; i++)
        {
            Init(i, potionsObj[i]);
        }
    }
}
