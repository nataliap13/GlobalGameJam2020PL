using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typeOfFishEnum { peaceful, aggresive }
public enum sizeOfFishEnum { small, medium, big }
public class Fish : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimerToHungry(5));
    }

    const int maxHappiness = 100;
    int hapiness = 50;
    public typeOfFishEnum typeOfFish = typeOfFishEnum.peaceful;
    public sizeOfFishEnum sizeOfFish = sizeOfFishEnum.small;
    private Fish TargetFishToEat;

    //minNumberOfFishToHapiness of my type/spiecies
    public int minNumberOfFishToHapiness = 1;
    public int maxNumberOfFishToHapiness = 1;

    public float speed = 5;

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal();
        if(TargetFishToEat != null)
        {
            float step = speed * Time.deltaTime;

            // move sprite towards the target location
            var target = TargetFishToEat.transform.localPosition;
            print(target);
            var globalTargetPosition = transform.TransformPoint(TargetFishToEat.transform.localPosition);
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(transform.position.x, globalTargetPosition.y), step);
        }
    }

    private IEnumerator TimerToHungry(int timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);
        ChooseFishToEat();
    }

    private void ChooseFishToEat()
    {
        var fish = FindObjectsOfType<Fish>();
        
        foreach (var f in fish)
        {
            if(f.sizeOfFish < sizeOfFish)
            {
                TargetFishToEat = f;
                break;
            }
        }
    }

    public void MoveHorizontal()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    public void MoveVertical()
    {
        
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    public void Flip ()
    {
        var localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        gameObject.transform.localScale = localScale;
        speed *= -1;
    }
}
