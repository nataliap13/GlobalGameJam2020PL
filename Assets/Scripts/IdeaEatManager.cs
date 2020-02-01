using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdeaEatManager : MonoBehaviour
{
    public SpriteRenderer fishSprite;
    public Sprite SpriteMeat;
    public Sprite SpritePlant;
    public void SetTargetSprite(typeOfFishEnum typeOfFish)
    {
        switch (typeOfFish)
        {
            case typeOfFishEnum.plantEating:
                {
                    fishSprite.sprite = SpritePlant;
                    break;
                }
            case typeOfFishEnum.meatEating:
                {
                    fishSprite.sprite = SpriteMeat;
                    break;
                }
        }
    }

    public void SetActive(bool active)
    {
        //print(active);
        gameObject.SetActive(active);
    }
}
