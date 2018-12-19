using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpriteType { Ground, Grass, Rock, Tree}


public class SpriteManager : MonoBehaviour
{
    public static SpriteManager instance;

    public List<EnumToSprite> enumToSprite;



    Dictionary<SpriteType, Sprite> tileSprites;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);


        tileSprites = new Dictionary<SpriteType, Sprite>();

        foreach (var item in enumToSprite)
        {
            tileSprites.Add(item.type, item.sprite);
        }

    }

    public Sprite GetSprite(SpriteType type)
    {
        if(tileSprites.ContainsKey(type))
        {
            return tileSprites[type];
        }
        else
        {
            print("The type " + type.ToString() + " isn't in the dictionary!");
            return null;
        }
    }
}

[System.Serializable]
public struct EnumToSprite
{
    public SpriteType type;
    public Sprite sprite;
}