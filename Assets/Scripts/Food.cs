using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typeOfFoodEnum { meat, plant}

public class Food : MonoBehaviour
{
    public typeOfFoodEnum typeOfFood;
    public float animationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimateSprite();
    }

    private void AnimateSprite()
    {
        var scaleVector = new Vector3(Mathf.Sin(Time.time * animationSpeed) , 1f, 1f);
        transform.localScale = scaleVector;
    }
}
