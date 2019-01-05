using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : DamageableParent
{
    public static Player instance;

    public RectTransform rageBar;

    const float barWidth = 643f;

    public float rage;
    const float maxRage = 1800f;

    public float rageUnitInterval { get { return rage / maxRage; } }

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
        rage = maxRage;
    }

    private void Update()
    {
        rageBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rageUnitInterval * barWidth);

        rage -= Time.deltaTime;
    }
    public void SetHealthText()
    {
        UIManager.instance.PlayerHealthText.text = health.ToString();
    }

    public void ReduceRage(float reduction)
    {
        rage -= reduction;
    }

}
