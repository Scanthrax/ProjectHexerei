using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;


    public AudioSource ErrorSource;
    public AudioSource ActiveSource;

    public Text PlayerHealthText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void SetText()
    {
        plantMush.text = PlayerResource.instance.plantMush.ToString();
        mineralMush.text = PlayerResource.instance.mineralMush.ToString();
        creatureMush.text = PlayerResource.instance.creatureMush.ToString();
    }

    public Text plantMush, mineralMush, creatureMush;
}
