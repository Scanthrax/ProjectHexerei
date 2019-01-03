using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PotionSystem : MonoBehaviour
{
    public static PotionSystem instance;


    Queue<DeliverySlots> DeliveryBelt = new Queue<DeliverySlots>(5);

    public PotionObject[] tempPotions;

    PlayerResource playerResource;



    public Image potionSlot;

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

    int numberKey;

    Dictionary<int, PotionObject> numKeyToPotionDict = new Dictionary<int, PotionObject>();



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    /// <summary>
    /// Is there a potion in hand?
    /// </summary>
    /// <returns></returns>
    bool IsPotionLoaded()
    {
        return potionInHand != null;
    }

    void Start()
    {
        playerResource = PlayerResource.instance;


        for (int i = 1; i <= 5; i++)
        {
            numKeyToPotionDict.Add(i, null);
        }

        numKeyToPotionDict[1] = tempPotions[0];
        numKeyToPotionDict[2] = tempPotions[1];


        //DeliveryBelt.Enqueue(tempPotion);


        player = GameObject.FindGameObjectWithTag("Player").transform;

        numberKey = 0;
    }


    void Update()
    {
        #region Capture which number key have we pressed this frame
        if (Input.GetKeyDown(KeyCode.Alpha1))
            numberKey = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            numberKey = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            numberKey = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            numberKey = 4;
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            numberKey = 5;
        else
            // a value of 0 means no num key has been pressed
            numberKey = 0;
        #endregion

        // if we press a number key...
        if (numberKey != 0)
        {
            // Only craft potions when the queue is under 5 potions
            if (DeliveryBelt.Count < 5)
            {
                // if there is a potion assigned to the number slot...
                if (numKeyToPotionDict[numberKey] != null)
                {
                    // get the costs of the potion
                    int mushCost = numKeyToPotionDict[numberKey].plantMushCost;
                    int mineralCost = numKeyToPotionDict[numberKey].mineralMushCost;

                    // if we have the appropriate amount of resources, continue on
                    if (mushCost <= playerResource.plantMush &&
                        mineralCost <= playerResource.mineralMush)
                    {
                        UIManager.instance.ActiveSource.Play();
                        print("potion has been crafted!");

                        // consume the costs
                        playerResource.plantMush -= mushCost;
                        playerResource.mineralMush -= mineralCost;

                        // set the UI text for the mush
                        UIManager.instance.SetText();

                        // hold a temp variable so we can manipulate & add to the delivery belt
                        DeliverySlots temp = new DeliverySlots(numKeyToPotionDict[numberKey]);
                        // set up the gameobject for the potion on the belt
                        temp.deliveryBeltTransform.SetParent(slots[DeliveryBelt.Count].parent);
                        temp.deliveryBeltTransform.position = slots[DeliveryBelt.Count].position;
                        temp.deliveryBeltTransform.GetComponent<Image>().sprite = temp.potion.image;
                        // put the gameobject in queue
                        DeliveryBelt.Enqueue(temp);

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


        if (Input.GetKeyDown(KeyCode.Space))
        {
            #region Putting potion in hand
            // only attempt to load when we don't have a potion in hand
            if (!IsPotionLoaded())
            {
                // we can only load if we have potions on the belt
                if (DeliveryBelt.Count > 0)
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
                    print("Delivery belt is empty!");
                    UIManager.instance.ErrorSource.Play();
                }
            }
            else
            {
                print("We already have a potion in hand!");
                UIManager.instance.ErrorSource.Play();
            }
            #endregion
        }

        if (IsPotionLoaded())
        {
            #region Mouse scroll up
            if (Input.mouseScrollDelta.y > 0f)
            {
                print("overhand throw");
                InstantiatePotion(true, potionInHand);
                potionSlot.sprite = SpriteManager.instance.empty;
                potionInHand = null;
            }
            #endregion
            #region Mouse Scroll down
            else if (Input.mouseScrollDelta.y < 0f)
            {
                print("underhand");
                InstantiatePotion(false, potionInHand);
                potionSlot.sprite = SpriteManager.instance.empty;
                potionInHand = null;
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
        //player.GetComponent<NavMeshAgent>().isStopped = true;
        //thrownPotion = true;

        var pot = Instantiate(this.potion, player.transform.position, Quaternion.Euler(90,0,0)).GetComponent<Potion>();
        pot.SetStartAndEnd(Crosshair.instance.LimitedCrosshair.position, overhand);
        pot.potion = potion;
        whooshSource.clip = AudioManager.instance.GetRandomSound(AudioManager.instance.Whoosh);
        whooshSource.Play();
        //pot.GetComponent<SpriteRenderer>().sprite = potion.image;
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
