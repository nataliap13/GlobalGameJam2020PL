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
        LikedPlants = new List<TypeOfPlantEnum>();
        HatedPlants = new List<TypeOfPlantEnum>();
        switch (typeOfFish)
        {
            case typeOfFishEnum.peaceful:
                {
                    LikedPlants.Add(TypeOfPlantEnum.Type1);
                    HatedPlants.Add(TypeOfPlantEnum.Type2);
                    break;
                }
            case typeOfFishEnum.aggresive:
                {
                    LikedPlants.Add(TypeOfPlantEnum.Type2);
                    HatedPlants.Add(TypeOfPlantEnum.Type1);
                    break;
                }
        }

        if (transform.localScale.x < 0)
        {
            speed *= -1;
        }

        ChooseFishToEat(2);
    }

    public typeOfFishEnum typeOfFish = typeOfFishEnum.peaceful;
    public sizeOfFishEnum sizeOfFish = sizeOfFishEnum.small;
    private Fish TargetFishToEat;
    private bool GoHunt = false;
    public List<TypeOfPlantEnum> LikedPlants { get; private set; }
    public List<TypeOfPlantEnum> HatedPlants { get; private set; }

    private float speed = 5;

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal();
        if (GoHunt == true && TargetFishToEat != null)
        {
            //if(FindObjectOfType<Food>())
            { }
            float step = 1 * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, TargetFishToEat.transform.position.y), step);
        }
        else if (TargetFishToEat == null)
        {
            GoHunt = false;
            ChooseFishToEat(2);
        }
    }

    private IEnumerator TimerToHunt(int timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);
        GoHunt = true;
    }

    private void ChooseFishToEat(int timeInSeconds)
    {
        var fish = FindObjectsOfType<Fish>();

        foreach (var f in fish)
        {
            if (f.sizeOfFish < sizeOfFish)
            {
                TargetFishToEat = f;
                StartCoroutine(TimerToHunt(2));
                break;
            }
        }
    }

    private void MoveHorizontal()
    { transform.Translate(speed * Time.deltaTime, 0, 0); }

    private void MoveVertical()
    { transform.Translate(0, speed * Time.deltaTime, 0); }

    public void Flip()
    {
        var localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        gameObject.transform.localScale = localScale;
        speed *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var otherFish = collision.GetComponent<Fish>();
        if (otherFish != null)
        {
            if (sizeOfFish > otherFish.sizeOfFish)
            {
                Destroy(otherFish.gameObject);
            }
        }
    }
}
