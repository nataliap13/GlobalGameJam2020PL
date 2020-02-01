using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typeOfFishEnum { plantEating, meatEating }
public enum sizeOfFishEnum { small, medium, big }

public class Fish : MonoBehaviour
{
    public typeOfFishEnum typeOfFish = typeOfFishEnum.plantEating;
    public sizeOfFishEnum sizeOfFish = sizeOfFishEnum.small;
    public IdeaEatManager ideaEatManager;//set In Unity
    public List<TypeOfPlantEnum> LikedPlants { get; private set; }
    public List<TypeOfPlantEnum> HatedPlants { get; private set; }
    public bool eatPlants = false;

    private GameObject TargetToEat;
    private bool GoHunt = false;
    private float speed = 4;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        if(typeOfFish == typeOfFishEnum.meatEating)
        {
            gameManager.aggressiveFishAlive.Add(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LikedPlants = new List<TypeOfPlantEnum>();
        HatedPlants = new List<TypeOfPlantEnum>();
        switch (typeOfFish)
        {
            case typeOfFishEnum.plantEating:
                {
                    LikedPlants.Add(TypeOfPlantEnum.Type1);
                    HatedPlants.Add(TypeOfPlantEnum.Type2);
                    break;
                }
            case typeOfFishEnum.meatEating:
                {
                    LikedPlants.Add(TypeOfPlantEnum.Type2);
                    HatedPlants.Add(TypeOfPlantEnum.Type1);
                    break;
                }
        }

        if (transform.localScale.x < 0)
        { speed *= -1; }
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal();
        if (GoHunt == true && TargetToEat != null)
        {
            float step = 1 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, TargetToEat.transform.position.y,transform.position.z), step);
            //fix strange bug with strange Z
            var pos = transform.localPosition;
            pos.z = 0;
            transform.localPosition = pos;
        }
        else if (typeOfFish == typeOfFishEnum.meatEating && TargetToEat == null)
        {
            GoHunt = false;
            ChooseTargetToEat(4);
        }
    }

    private IEnumerator TimerToHunt(int timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);
        ideaEatManager.SetActive(false);
        GoHunt = true;
    }

    public void ChooseTargetToEat(int timeInSeconds)
    {
        if (timeInSeconds > 0)
        {//pop up think cloud
            ideaEatManager.SetTargetSprite(typeOfFish);
            ideaEatManager.SetActive(true);
        }

        var food = FindObjectsOfType<Food>();
        foreach (var f in food)
        {
            TargetToEat = f.gameObject;
            StartCoroutine(TimerToHunt(timeInSeconds));
            return;
        }

        var fish = FindObjectsOfType<Fish>();
        foreach (var f in fish)
        {
            if (f.sizeOfFish < sizeOfFish)
            {
                TargetToEat = f.gameObject;
                StartCoroutine(TimerToHunt(timeInSeconds));
                return;
            }
        }
    }

    private void MoveHorizontal()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        var pos = transform.localPosition;
        pos.z = 0;
        transform.localPosition = pos;
    }

    public void Flip()
    {
        var localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        gameObject.transform.localScale = localScale;
        speed *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TargetToEat != null)
        {
            var food = collision.GetComponent<Food>();
            var targetFood = TargetToEat.GetComponent<Food>();
            if (food != null && targetFood != null && food == targetFood)
            {
                Destroy(food.gameObject);
                print(gameObject.name + " ate " + collision.gameObject.name);
                GoHunt = false;
                return;
            }

            var otherFish = collision.GetComponent<Fish>();
            var targetFish = TargetToEat.GetComponent<Fish>();
            if (otherFish != null && targetFish != null && otherFish == targetFish)
            {
                Destroy(otherFish.gameObject);
                print(gameObject.name + " ate " + collision.gameObject.name);
                GoHunt = false;
                return;
            }
        }
    }
}
