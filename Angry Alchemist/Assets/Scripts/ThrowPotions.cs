using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowPotions : MonoBehaviour
{
    public PlayerResource playerResource;


    public Image potionSlot;

    public PotionObject potionObj;

    public bool potionLoaded;
    public AnimationCurve animCurve;
    public GameObject potion;

    public Sprite empty;

    void Start()
    {
        potionLoaded = false;
        playerResource = PlayerResource.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerResource.plantMush >= potionObj.plantMushCost)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("potion loaded");
                potionLoaded = true;

                // load potion into UI slot
                potionSlot.sprite = potionObj.image;

            }
        }

        if (potionLoaded)
        {
            if (playerResource.plantMush >= potionObj.plantMushCost)
            {
                if (Input.mouseScrollDelta.y > 0f)
                {
                    print("overhand");
                    InstantiatePotion(true, potionObj);
                    potionSlot.sprite = empty;
                }
                else if (Input.mouseScrollDelta.y < 0f)
                {
                    print("underhand");
                    InstantiatePotion(false, potionObj);
                    potionSlot.sprite = empty;
                }
            }
        }
    }


    void InstantiatePotion(bool overhand, PotionObject potion)
    {
        potionLoaded = false;
        var pot = Instantiate(this.potion, transform.position, Quaternion.Euler(90,Random.value * 360f,0)).GetComponent<Potion>();
        pot.SetStartAndEnd(Camera.main.ScreenToWorldPoint(Input.mousePosition), overhand);
        pot.potion = potion;
        playerResource.plantMush -= potion.plantMushCost;
        UIManager.instance.plantMush.text = playerResource.plantMush.ToString();
        //pot.GetComponent<SpriteRenderer>().sprite = potion.image;
    }
}
