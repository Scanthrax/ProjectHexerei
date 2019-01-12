using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

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
    /// The delivery belt will the potions that are queued up for the player
    /// </summary>
    Queue<DeliverySlots> DeliveryBelt = new Queue<DeliverySlots>(5);

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

    /// <summary>
    /// The potion that is currently in our hand
    /// </summary>
    public PotionObject potionInHand;



    public GameObject potion;


    public AudioSource whooshSource;
    public AudioSource loadPotionSource;


    public Transform[] slots;

    Transform player;

    public Transform potionContainerBelt { get; }


    public PotionObject potion2;

    public AnimationCurve MovePotions;
    public float MovePotionDuration;

    public bool thrownPotion;

    public bool handThrow;

    public GameObject timerTextObject;


    public class DeliverySlots
    {
        public PotionObject potion;
        public Transform deliveryBeltTransform;

        public Text textTimer;

        public float timer;



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

    int potionSlotKey;

    Dictionary<int, PotionObject> numKeyToPotionDict = new Dictionary<int, PotionObject>();



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    /// <summary>
    /// Returns true if the player currently has a potion in their hand
    /// </summary>
    /// <returns></returns>
    public bool IsPotionInHand
    {
        get
        {
            return potionInHand != null;
        }
    }

    void Start()
    {
        playerResource = PlayerResource.instance;


        for (int i = 1; i <= 5; i++)
        {
            numKeyToPotionDict.Add(i, null);
        }

        numKeyToPotionDict[1] = recipes[0];
        numKeyToPotionDict[2] = recipes[1];
        numKeyToPotionDict[3] = recipes[2];
        numKeyToPotionDict[4] = recipes[3];


        //DeliveryBelt.Enqueue(tempPotion);


        player = GameObject.FindGameObjectWithTag("Player").transform;

        potionSlotKey = 0;

        UIManager.instance.SetMushTubesText();
    }


    void Update()
    {



        #region Toggle overhand vs underhand with Tab
        // if we press tab...
        if(Input.GetKeyDown(KeyCode.Tab))
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

        #region Attemp to craft potion if slot has been pressed
        // if we press a potion slot key...
        if (potionSlotKey != 0)
        {
            // Only craft potions when the queue is under 5 potions
            if (DeliveryBelt.Count < 5)
            {
                // get the potion that is mapped to the potion slot
                var potion = numKeyToPotionDict[potionSlotKey];

                // if there is a potion assigned to the number slot...
                if (potion != null)
                {
                    // get the costs of the potion
                    int mushCost = potion.plantMushCost;
                    int mineralCost = potion.mineralMushCost;
                    int creatureCost = potion.creatureMushCost;
                    int demonCost = potion.demonMushCost;

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




                        // hold a temp variable so we can manipulate & add to the delivery belt
                        DeliverySlots temp = new DeliverySlots(potion);

                        // set up the gameobject for the potion on the belt
                        temp.deliveryBeltTransform.SetParent(slots[DeliveryBelt.Count].parent);
                        temp.deliveryBeltTransform.position = slots[DeliveryBelt.Count].position;
                        temp.deliveryBeltTransform.GetComponent<Image>().sprite = temp.potion.image;
                        var textObject = Instantiate(timerTextObject, temp.deliveryBeltTransform);
                        temp.textTimer = textObject.GetComponent<Text>();
                        // put the gameobject in queue
                        DeliveryBelt.Enqueue(temp);

                        temp.timer = temp.potion.craftTime;

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



        // do we have a potion in our hand?
        if (IsPotionInHand)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // create a potion object
                InstantiatePotion(handThrow, potionInHand);
                // make the sprite empty since we no longer have a potion in our hand
                potionSlot.sprite = SpriteManager.instance.empty;
                // set potion in hand to null
                potionInHand = null;
                // we have just thrown a potion. this bool is used to prevent us from loading in another potion,
                // since spacebar is used for loading a potion into our hand & also to launch the potion
                thrownPotion = true;
            }
        }



        if (!thrownPotion)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                #region Putting potion in hand
                // only attempt to load when we don't have a potion in hand
                if (!IsPotionInHand)
                {
                    // we can only load if we have potions on the belt
                    if (DeliveryBelt.Count > 0)
                    {
                        if (DeliveryBelt.Peek().timer <= 0f)
                        {

                            UIManager.instance.ActiveSource.Play();
                            // get the potion in the queue
                            var temp = DeliveryBelt.Dequeue();

                            potionInHand = temp.potion;
                            print("Holding " + potionInHand.name);
                            // put the potion's sprite in the slot
                            potionSlot.sprite = potionInHand.image;

                            //// iterate through the queue by converting it to an array
                            //var array = DeliveryBelt.ToArray();
                            //for (int i = 0; i < DeliveryBelt.Count; i++)
                            //{
                            //    //print(i + array[i].potion.name);
                            //    // the position of each potion will shift to the right one slot
                            //    array[i].deliveryBeltTransform.position = slots[i].position;
                            //}

                            ShiftPotions();

                            temp.DestroyGO();
                        }
                        else
                        {
                            print("Potion is not ready yet!");
                            UIManager.instance.ErrorSource.Play();
                        }
                    }
                    else
                    {
                        print("Delivery belt is empty!");
                        UIManager.instance.ErrorSource.Play();
                    }
                }
                //else
                //{
                //    print("We already have a potion in hand!");
                //    UIManager.instance.ErrorSource.Play();
                //}
                #endregion

                thrownPotion = false;
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


            var array = DeliveryBelt.ToArray();
            for (int i = 0; i < DeliveryBelt.Count; i++)
            {
                if (array[i].timer >= 0f)
                {
                    array[i].timer -= Time.deltaTime;
                    array[i].textTimer.GetComponent<Text>().text = array[i].timer.ToString();
                }
            }
        }


    }

    /// <summary>
    /// Creates a potion gameobject.  We pass in a bool that indicates whether we are throwing overhand vs underhand.
    /// We also pass in the potion so the gameobject can acquire the potion's attributes.
    /// </summary>
    /// <param name="overhand"></param>
    /// <param name="potion"></param>
    void InstantiatePotion(bool overhand, PotionObject potion)
    {

        var pot = Instantiate(this.potion, player.transform.position, Quaternion.Euler(90,0,0)).GetComponent<Potion>();
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
        var array = DeliveryBelt.ToArray();

        Vector3[] origin = new Vector3[array.Length];
        Vector3[] target = new Vector3[origin.Length];

        for (int i = 0; i < DeliveryBelt.Count; i++)
        {
            origin[i] = array[i].deliveryBeltTransform.localPosition;
            target[i] = slots[i].localPosition;
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
                if(array[i].deliveryBeltTransform != null)
                    array[i].deliveryBeltTransform.localPosition = Vector3.LerpUnclamped(origin[i], target[i], curvePercent);
            }
            
            // wait a frame
            yield return null;
        }

    }

}
