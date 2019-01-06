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


    public Transform canvas;



    public void SpewThings(GameObject go, Vector3 position,int amount, int damage)
    {
        for (int i = 0; i < amount; i++)
        {
            var temp = Instantiate(go, position, Quaternion.Euler(90,0,0), canvas);
            temp.GetComponent<Rigidbody>().AddForce((Random.value * 300f) - 150f, 0, 100f);
            temp.GetComponent<Text>().text = damage.ToString();
        }
    }
}
