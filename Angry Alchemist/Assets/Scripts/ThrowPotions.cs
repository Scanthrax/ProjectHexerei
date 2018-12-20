using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPotions : MonoBehaviour
{
    public bool potionLoaded;
    public AnimationCurve animCurve;
    public GameObject potion;

    void Start()
    {
        potionLoaded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("potion loaded");
            potionLoaded = true;
        }

        if (potionLoaded)
        {
            if (Input.mouseScrollDelta.y > 0f)
            {
                print("overhand");
                InstantiatePotion(true);
            }
            else if (Input.mouseScrollDelta.y < 0f)
            {
                print("underhand");
                InstantiatePotion(false);
            }
        }
    }


    void InstantiatePotion(bool overhand)
    {
        potionLoaded = false;
        var pot = Instantiate(potion, transform.position, Quaternion.identity).GetComponent<Potion>();
        pot.SetStartAndEnd(Camera.main.ScreenToWorldPoint(Input.mousePosition), overhand);
    }
}
