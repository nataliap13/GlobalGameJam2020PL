using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdeaEatManager : MonoBehaviour
{
    public SpriteRenderer fishSprite;
    public void SetTargetSprite(GameObject target)
    {
        fishSprite.sprite = target.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    public void SetActive(bool active)
    {
        print(active);
        gameObject.SetActive(active);
    }
}
