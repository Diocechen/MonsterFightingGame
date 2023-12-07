using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLoot : MonoBehaviour
{
    [SerializeField] private GameObject loot;
    [SerializeField] private float dropOffset;

    public void Drop()
    {
        Vector2 dropPoint = new Vector2(transform.position.x, transform.position.y - dropOffset);
        Instantiate(loot, dropPoint, Quaternion.identity);
    }
}
