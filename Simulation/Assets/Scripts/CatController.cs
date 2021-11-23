using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    private float speed = 1;
    private GameObject[] mice;

    private int closestMouse;

    private Vector3 randomLocation;

    // Start is called before the first frame update
    void Start()
    {
        mice = GameObject.FindGameObjectsWithTag("Mouse");
        createRandomLocation();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the search for mice
        mice = GameObject.FindGameObjectsWithTag("Mouse");
        // Find closest
        if (mice.Length != 0)
        {
            closestMouse = GetClosest(mice);

            // Move toward the mouse
            Movement(mice[closestMouse].transform.position);

        }
        else
        {
            // Move to random location

            Movement(randomLocation);

            // If at randomLocation, generate new randomLocation
            if((randomLocation - transform.position).magnitude < 0.2f)
            {
                createRandomLocation();
            }

        }

    }

    private void Movement(Vector3 target)
    {
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
        randomLocation = new Vector3(Random.Range(-3.0f, 3.0f), 0.1f, Random.Range(-3.0f, 3.0f));
    }

    // Eat the mouse
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Mouse")
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
