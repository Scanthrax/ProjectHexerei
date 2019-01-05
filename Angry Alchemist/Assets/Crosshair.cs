using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    Transform MouseCrosshair;
    public Transform LimitedCrosshair;
    public Transform player;
    float range;
    public float baseRange;
    public float rageRange;

    public static Crosshair instance;

    public Transform aimReticle;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        MouseCrosshair = transform;
    }

    void Update()
    {
        range = baseRange + (rageRange * Player.instance.rageUnitInterval);

        var MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        

        MousePosition = new Vector3(MousePosition.x, 0, MousePosition.z);

        

        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //var allowedPos = mousePos - initialPos;
        //allowedPos = Vector3.ClampMagnitude(allowedPos, 2.0);
        //transform.position = initialPos + allowedPos;

        var allowedPos = MousePosition - player.position;
        allowedPos = Vector3.ClampMagnitude(allowedPos, range);

        LimitedCrosshair.transform.position = player.position + allowedPos;

        // dark crosshair
        MouseCrosshair.position = MousePosition;



        if (PotionSystem.instance.IsPotionLoaded())
        {
            aimReticle.gameObject.SetActive(true);
            aimReticle.position = LimitedCrosshair.position;
            aimReticle.GetComponent<SpriteRenderer>().sprite = PotionSystem.instance.potionInHand.reticle;
        }
        else
            aimReticle.gameObject.SetActive(false);


        







    }
}
