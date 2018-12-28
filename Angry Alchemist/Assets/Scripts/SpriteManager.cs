using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpriteManager : MonoBehaviour
{
    public static SpriteManager instance;

    public List<EnumToSprite> enumToSprite;

    public List<MushToSprite> mushToSprite;

    public Sprite empty;

    Dictionary<string, Sprite> spriteDictionary;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);


        spriteDictionary = new Dictionary<string, Sprite>();

        foreach (var item in enumToSprite)
        {
            spriteDictionary.Add(item.type.ToString(), item.sprite);
        }
        foreach (var item in mushToSprite)
        {
            spriteDictionary.Add(item.type.ToString(), item.sprite);
        }

    }

    public Sprite GetSprite(Type type)
    {
        return ObtainSprite(type.ToString());
    }

    public Sprite GetSprite(MushType type)
    {
        return ObtainSprite(type.ToString());
    }

    Sprite ObtainSprite(string type)
    {
        if (spriteDictionary.ContainsKey(type))
        {
            return spriteDictionary[type];
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
    public Type type;
    public Sprite sprite;
}

[System.Serializable]
public struct MushToSprite
{
    public MushType type;
    public Sprite sprite;
}