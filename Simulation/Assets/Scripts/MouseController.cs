using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private float speed = 1.5f;
    private GameObject[] cats;
    private GameObject[] cheeses;

    private int closestCat;
    private int closestCheese;

    private Vector3 randomLocation;

    private int away = -1;
    private int towards = 1;

    // Start is called before the first frame update
    void Start()
    {
        cats = GameObject.FindGameObjectsWithTag("Cat");
        cheeses = GameObject.FindGameObjectsWithTag("Cheese");
        createRandomLocation();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the search for cheese
        cheeses = GameObject.FindGameObjectsWithTag("Cheese");

        // Update the search for cats
        cats = GameObject.FindGameObjectsWithTag("Cat");

        // Find closest
        if (cheeses.Length != 0)
        {
            closestCheese = GetClosest(cheeses);

            // Move toward the cheese
            Movement(cheeses[closestCheese].transform.position, towards);

        }
        else if(cats.Length != 0)
        {
            closestCat = GetClosest(cats);

            // Move away from the cat
            Movement(cats[closestCat].transform.position, away);
        }
        else if(cheeses.Length == 0 && cats.Length == 0)
        {
            // Move to random location

            //Movement(randomLocation, towards);

            // If at randomLocation, generate new randomLocation
            if ((randomLocation - transform.position).magnitude < 0.2f)
            {
                createRandomLocation();
            }

        }

    }


    private void Movement(Vector3 target, int whichWay = 1)
    {

        if (whichWay == away)
        {
            target = transform.position + (transform.position - target);
        }

        // Look in correct direction
        transform.LookAt(target);

        // Move toward target
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    private int GetClosest(GameObject[] objects)
    {
        // Find a mouse --> Get closest mouse
        float[] distance = new float[objects.Length];
        for (int i = 0; i < (objects.Length); i++)
        {
            // Compare location vectors (distance to each mouse)
            distance[i] = (transform.position - objects[i].transform.position).magnitude;

        }

        // Determine smallest distance
        return GetIndexOfLowestValue(distance);
    }
    private void createRandomLocation()
    {
        randomLocation = new Vector3(Random.Range(-3.0f, 3.0f), 0.05f, Random.Range(-3.0f, 3.0f));
    }

    // Eat the mouse
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Cheese")
        {
            //Eat the mouse
            Destroy(col.gameObject);
        }
    }

    public int GetIndexOfLowestValue(float[] arr)
    {
        float value = float.PositiveInfinity;
        int index = -1;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] < value)
            {
                index = i;
                value = arr[i];
            }
        }
        return index;
    }

}
