using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionSystem : MonoBehaviour
{
    Queue<DeliverySlots> DeliveryBelt = new Queue<DeliverySlots>(5);

    public PotionObject tempPotion;

    PlayerResource playerResource;



    public Image potionSlot;

    public PotionObject PotionInHand;

    public bool potionLoaded;

    public GameObject potion;


    public AudioSource whooshSource;
    public AudioSource loadPotionSource;

    CraftPotions[] craftPotions;

    public Transform[] slots;

    Transform player;

    public Transform potionContainerBelt { get; }

    public struct CraftPotions
    {
        public PotionObject potion;
        public KeyCode numKey;
    }

    public class DeliverySlots
    {
        public PotionObject potion;
        public Transform deliveryBeltTransform;

        public DeliverySlots(PotionObject pot)
        {
            potion = pot;
            deliveryBeltTransform = new GameObject("test",typeof(Image)).transform;
        }

        

        public void DestroyGO()
        {
            Destroy(deliveryBeltTransform.gameObject);
        }
    }


    void Start()
    {
        potionLoaded = false;
        playerResource = PlayerResource.instance;

        craftPotions = new CraftPotions[5];

        craftPotions[0].potion = tempPotion;
        craftPotions[0].numKey = KeyCode.Alpha1;

        //DeliveryBelt.Enqueue(tempPotion);


        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        // Only craft potions when the queue is under 5 potions
        if (DeliveryBelt.Count < 5)
        {
            // if we press an ability key...
            if (Input.GetKeyDown(craftPotions[0].numKey))
            {
                // get the costs of the potion
                int mushCost = craftPotions[0].potion.plantMushCost;
                int mineralCost = craftPotions[0].potion.mineralMushCost;

                // if we have the appropriate amount of resources, continue on
                if (mushCost <= playerResource.plantMush &&
                    mineralCost <= playerResource.mineralMush)
                {
                    print("potion has been crafted!");

                    // consume the costs
                    playerResource.plantMush -= mushCost;
                    playerResource.mineralMush -= mineralCost;

                    // set the UI text for the mush
                    UIManager.instance.SetText();

                    // hold a temp variable so we can manipulate & add to the delivery belt
                    DeliverySlots temp = new DeliverySlots(craftPotions[0].potion);
                    // set up the gameobject for the potion on the belt
                    temp.deliveryBeltTransform.SetParent(slots[DeliveryBelt.Count].parent);
                    temp.deliveryBeltTransform.position = slots[DeliveryBelt.Count].position;
                    temp.deliveryBeltTransform.GetComponent<Image>().sprite = temp.potion.image;
                    // put the gameobject in queue
                    DeliveryBelt.Enqueue(temp);

                }
                else
                    print("Insufficient materials!");
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            #region Putting potion in hand
            // only attempt to load when we don't have a potion in hand
            if (!potionLoaded)
            {
                // we can only load if we have potions on the belt
                if (DeliveryBelt.Count > 0)
                {
                    // we now have a potion in hand
                    potionLoaded = true;
                    // get the potion in the queue
                    var temp = DeliveryBelt.Dequeue();

                    PotionInHand = temp.potion;
                    print("Holding " + PotionInHand.name);
                    // put the potion's sprite in the slot
                    potionSlot.sprite = PotionInHand.image;

                    // iterate through the queue by converting it to an array
                    var array = DeliveryBelt.ToArray();
                    for (int i = 0; i < array.Length; i++)
                    {
                        // the position of each potion will shift to the right one slot
                        array[i].deliveryBeltTransform.position = slots[DeliveryBelt.Count-1-i].position;
                    }



                    temp.DestroyGO();
                }
                else
                    print("Delivery belt is empty!");
            }
            else
                print("We already have a potion in hand!");
            #endregion
        }

        if (potionLoaded)
        {
            #region Mouse scroll up
            if (Input.mouseScrollDelta.y > 0f)
            {
                print("overhand throw");
                InstantiatePotion(true, PotionInHand);
                potionSlot.sprite = SpriteManager.instance.empty;
                PotionInHand = null;
            }
            #endregion
            #region Mouse Scroll down
            else if (Input.mouseScrollDelta.y < 0f)
            {
                print("underhand");
                InstantiatePotion(false, PotionInHand);
                potionSlot.sprite = SpriteManager.instance.empty;
                PotionInHand = null;
            }
            #endregion
        }





        //if (potionLoaded)
        //{
        //    if (playerResource.plantMush >= PotionInHand.plantMushCost)
        //    {
        //        if (Input.mouseScrollDelta.y > 0f)
        //        {
        //            print("overhand");
        //            InstantiatePotion(true, PotionInHand);
        //            potionSlot.sprite = empty;
        //            whooshSource.clip = AudioManager.instance.GetRandomSound(AudioManager.instance.Whoosh);
        //            whooshSource.Play();
        //        }
        //        else if (Input.mouseScrollDelta.y < 0f)
        //        {
        //            print("underhand");
        //            InstantiatePotion(false, PotionInHand);
        //            potionSlot.sprite = empty;
        //            whooshSource.clip = AudioManager.instance.GetRandomSound(AudioManager.instance.Whoosh);
        //            whooshSource.Play();
        //        }
        //    }
        //}
    }


    void InstantiatePotion(bool overhand, PotionObject potion)
    {
        potionLoaded = false;
        var pot = Instantiate(this.potion, player.transform.position, Quaternion.Euler(90, Random.value * 360f, 0)).GetComponent<Potion>();
        pot.SetStartAndEnd(Camera.main.ScreenToWorldPoint(Input.mousePosition), overhand);
        pot.potion = potion;
        //pot.GetComponent<SpriteRenderer>().sprite = potion.image;
    }
}
