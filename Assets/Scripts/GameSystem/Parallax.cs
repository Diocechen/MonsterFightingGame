using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPosition;
    [SerializeField] private GameObject target;
    [SerializeField] private float parallaxEffect; //basically when target moves, it will slower the movement of the sprite depends on how small this value is.

    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = target.transform.position.x * (1 - parallaxEffect);
        float distance = target.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        //Change the sprite position before camera go beyond the sprite.
        if (temp > startPosition + length)
        {
            startPosition += length;
        }
        else if (temp < startPosition - length)
        {
            startPosition -= length;
        }
    }
}
