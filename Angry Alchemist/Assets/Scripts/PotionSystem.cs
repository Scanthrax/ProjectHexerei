using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Linq;
using System;

/// <summary>
/// This class is responsible for handling the potion crafting logic during exploration.
/// The player will be crafting potions from the mush stored in the muschmaschine.
/// </summary>
public class PotionSystem : MonoBehaviour
{
    /// <summary>
    /// The singleton instance of this class
    /// </summary>
    public static PotionSystem instance;

    /// <summary>
    /// The delivery belt will hold the potions that are queued up for the player
    /// </summary>
    Queue<UIPotion> deliveryBelt = new Queue<UIPotion>(5);

    /// <summary>
    /// These are the potions that the player will be able to craft
    /// </summary>
    public PotionObject[] recipes;

    /// <summary>
    /// A reference to the PlayerResource class
    /// </summary>
    PlayerResource playerResource;

    /// <summary>
    /// The image that displays which potion is currently in our hand
    /// </summary>
    public Image potionSlot;





    public GameObject potion;


    public AudioSource whooshSource;
    public AudioSource loadPotionSource;

    Transform player;

    public Transform potionContainerBelt { get; }


    public AnimationCurve MovePotions;

    public float MovePotionDuration;

    public bool thrownPotion;

    public bool handThrow;



    public UIPotionSlot handSlot;
    public UIHotkeySlot[] hotkeySlots;
    public UIPotionSlot[] deliveryBeltSlots;


    int potionSlotKey;
    int autoPotionKey;

    Dictionary<int, UIHotkeySlot> numKeyToPotionDict = new Dictionary<int, UIHotkeySlot>();


    Queue<PotionObject> craftTimes = new Queue<PotionObject>(6);
    float craftTimer = 0f;
    PotionObject craftPotion = null;
    int amountOfPotions = 0;


    private void Awake()
    {
        #region Singleton
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        #endregion
    }

    /// <summary>
    /// Returns true if the player currently has a potion in their hand
    /// </summary>
    /// <returns></returns>
    public bool isPotionInHand
    {
        get
        {
            return handSlot.potion != null;
        }
    }

    void Start()
    {
        playerResource = PlayerResource.instance;


        for (int i = 0; i < hotkeySlots.Length; i++)
        {
            numKeyToPotionDict.Add(hotkeySlots[i].mapping, hotkeySlots[i]);

            if(i != 4)
            {
                var temp = ObjectPooler.instance.SpawnFromPool(PoolType.UIPotion, Vector3.zero, Quaternion.identity).GetComponent<UIPotion>();
                temp.Initialize(recipes[i]);
                hotkeySlots[i].PutPotionInSlot(temp);
            }

        }

        player = GameObject.FindGameObjectWithTag("Player").transform;

        potionSlotKey = 0;
        autoPotionKey = 0;

        Array.Reverse(deliveryBeltSlots);

        UIManager.instance.SetMushTubesText();
    }


    void Update()
    {



        #region Toggle overhand vs underhand with Tab
        // if we press tab...
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // toggle the overhand boolean, which will influence how the player throws potions
            handThrow = !handThrow;

            // debug print the state we just toggled to
            print(handThrow ? "Overhand" : "Underhand");
        }
        #endregion


        #region Capture which potion slot key have we pressed this frame
        if (Input.GetKeyDown(KeyCode.Alpha1))
            potionSlotKey = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            potionSlotKey = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            potionSlotKey = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            potionSlotKey = 4;
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            potionSlotKey = 5;
        else
            // a value of 0 means no num key has been pressed
            potionSlotKey = 0;
        #endregion


        CraftPotion(potionSlotKey);


        CraftPotion(autoPotionKey);
        autoPotionKey = 0;


        if(craftTimer > 0f)
        {
            craftTimer -= Time.deltaTime;
        }
        else
        {
            if(craftPotion != null)
            {
                UIManager.instance.ActiveSource.Play();


                // only attempt to load when we don't have a potion in hand
                if (!isPotionInHand)
                {
                    var temp = ObjectPooler.instance.SpawnFromPool(PoolType.UIPotion, handSlot.transform.position, Quaternion.identity).GetComponent<UIPotion>();
                    temp.Initialize(craftPotion);
                    handSlot.PutPotionInSlot(temp);
                }
                else
                {

                    var temp = ObjectPooler.instance.SpawnFromPool(PoolType.UIPotion, deliveryBeltSlots[deliveryBelt.Count].transform.position, Quaternion.identity).GetComponent<UIPotion>();
                    temp.transform.SetParent(deliveryBeltSlots[deliveryBelt.Count].transform.parent);
                    temp.Initialize(craftPotion);
                    deliveryBelt.Enqueue(temp);
                }
            }

            if (craftTimes.Count > 0)
            {
                var temp = craftTimes.Dequeue();
                craftTimer = temp.craftTime;
                craftPotion = temp;
            }
            else
            {
                craftPotion = null;
            }
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            // do we have a potion in our hand?
            if (isPotionInHand)
            {

                // create a potion object
                InstantiatePotion(handThrow, handSlot.potion);
                // make the sprite empty since we no longer have a potion in our hand
                ObjectPooler.instance.DespawnFromPool(PoolType.UIPotion, handSlot.uiPotion.gameObject);
                // set potion in hand to null
                if (deliveryBelt.Count > 0)
                    handSlot.PutPotionInSlot(deliveryBelt.Dequeue());
                else
                    handSlot.GetPotionFromSlot();
            }
            else
            {
                print("We do not have a potion in hand!");
                UIManager.instance.ErrorSource.Play();
            }
        }
    }


    void CraftPotion(int key)
    {
        #region Attemp to craft potion if slot has been pressed
        // if we press a potion slot key...
        if (numKeyToPotionDict.ContainsKey(key) && numKeyToPotionDict[key].potion != null)
        {
            // Only craft potions when the queue is under 5 potions
            if (amountOfPotions < 6)
            {
                // get the potion that is mapped to the potion slot
                var potion = numKeyToPotionDict[key];

                // if there is a potion assigned to the number slot...
                if (potion != null)
                {
                    // get the costs of the potion
                    int mushCost = potion.potion.plantMushCost;
                    int mineralCost = potion.potion.mineralMushCost;
                    int creatureCost = potion.potion.creatureMushCost;
                    int demonCost = potion.potion.demonMushCost;

                    // if we have the appropriate amount of resources, continue on
                    if (mushCost <= playerResource.plantMush &&
                        mineralCost <= playerResource.mineralMush &&
                        creatureCost <= playerResource.creatureMush &&
                        demonCost <= playerResource.demonMush)
                    {
                        // play an audio cue to indicate that we are crafting a potion
                        UIManager.instance.ActiveSource.Play();
                        print("crafting potion!");

                        // consume the costs that we gathered above
                        playerResource.plantMush -= mushCost;
                        playerResource.mineralMush -= mineralCost;
                        playerResource.creatureMush -= creatureCost;
                        playerResource.demonMush -= demonCost;

                        // set the UI text for the mush tubes
                        UIManager.instance.SetMushTubesText();

                        craftTimes.Enqueue(potion.potion);
                        amountOfPotions++;
                        

                        //// hold a temp variable so we can manipulate & add to the delivery belt
                        //DeliverySlots temp = new DeliverySlots(potion.potion);

                        //// set up the gameobject for the potion on the belt
                        //temp.deliveryBeltTransform.SetParent(slots[DeliveryBelt.Count].parent);
                        //temp.deliveryBeltTransform.position = slots[DeliveryBelt.Count].position;
                        //temp.deliveryBeltTransform.GetComponent<Image>().sprite = temp.potion.image;
                        //var textObject = Instantiate(timerTextObject, temp.deliveryBeltTransform);
                        //temp.textTimer = textObject.GetComponent<Text>();
                        //// put the gameobject in queue
                        //DeliveryBelt.Enqueue(temp);

                        //temp.timer = temp.potion.craftTime;

                    }
                    else
                    {
                        print("Insufficient materials!");
                        UIManager.instance.ErrorSource.Play();
                    }
                }
                else
                {
                    print("There is no potion in this slot!");
                    UIManager.instance.ErrorSource.Play();
                }
            }
            else
            {
                print("The delivery belt is at full capacity!");
                UIManager.instance.ErrorSource.Play();
            }
        }
        #endregion
    }


    /// <summary>
    /// Creates a potion gameobject.  We pass in a bool that indicates whether we are throwing overhand vs underhand.
    /// We also pass in the potion so the gameobject can acquire the potion's attributes.
    /// </summary>
    /// <param name="overhand"></param>
    /// <param name="potion"></param>
    void InstantiatePotion(bool overhand, PotionObject potion)
    {
        amountOfPotions--;
        var pot = Instantiate(this.potion, player.transform.position, Quaternion.Euler(90, 0, 0)).GetComponent<Potion>();
        pot.SetStartAndEnd(Crosshair.instance.LimitedCrosshair.position, overhand);
        pot.potion = potion;
        whooshSource.clip = AudioManager.instance.GetRandomSound(AudioManager.instance.Whoosh);
        whooshSource.Play();
    }


    public void ShiftPotions()
    {
        StartCoroutine(AnimateMove());
    }


    IEnumerator AnimateMove()
    {


        // iterate through the queue by converting it to an array
        var array = deliveryBelt.ToArray();

        Vector3[] origin = new Vector3[array.Length];
        Vector3[] target = new Vector3[origin.Length];

        for (int i = 0; i < deliveryBelt.Count; i++)
        {
            //origin[i] = array[i].localPosition;
            target[i] = deliveryBeltSlots[i].transform.localPosition;
        }


        // timer for moving the menu
        float journey = 0f;
        // percentage of completion, used for finding position on animation curve
        float percent = 0f;

        // keep adjusting the position while there is time
        while (journey <= MovePotionDuration)
        {
            // add to timer
            journey = journey + Time.deltaTime;
            // calculate percentage
            percent = Mathf.Clamp01(journey / MovePotionDuration);
            // find the percentage on the curve
            float curvePercent = MovePotions.Evaluate(percent);
            // adjust the position of the menu


            for (int i = 0; i < array.Length; i++)
            {
                //if(array[i].deliveryBeltTransform != null)
                //    array[i].deliveryBeltTransform.localPosition = Vector3.LerpUnclamped(origin[i], target[i], curvePercent);
            }

            // wait a frame
            yield return null;
        }

    }

}
