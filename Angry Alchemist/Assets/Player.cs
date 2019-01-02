using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : DamageableParent
{
    public static Player instance;

    public RectTransform rageBar;

    [Range(0f,1f)]
    public float Rage;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        SetHealthText();
    }

    private void Update()
    {
        rageBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Rage * 643f);

    }
    public void SetHealthText()
    {
        UIManager.instance.PlayerHealthText.text = health.ToString();
    }

}
