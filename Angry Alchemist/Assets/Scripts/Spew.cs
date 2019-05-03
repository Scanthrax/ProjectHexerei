using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spew : MonoBehaviour
{
    #region Singleton
    public static Spew instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    #endregion

    public float spewDist;

    public Transform canvas;

    private void Start()
    {
        spewDist = 1200f;
    }

    public void SpewThings(GameObject go, Vector3 position,int amount, int damage, Color color)
    {
        for (int i = 0; i < amount; i++)
        {
            var temp = Instantiate(go, position, Quaternion.Euler(90,0,0), canvas);
            temp.GetComponent<Rigidbody>().AddForce((Random.value * spewDist) - (spewDist * 0.5f), 0, 100f);
            var temp2 = temp.GetComponent<Text>();
            temp2.text = damage.ToString();
            temp2.color = color;
        }
    }
}
