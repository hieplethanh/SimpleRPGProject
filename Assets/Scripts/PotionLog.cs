using UnityEngine;
using System.Collections;
using System;

public class PotionLog : MonoBehaviour, IConsumable
{
    public void Consume()
    {
        GameObject.FindWithTag("Player").GetComponent<Player>().TakeDamage(-50);
        Destroy(gameObject);
    }

    public void Consume(CharacterStats stats)
    {
        Debug.Log("You drank a swig of the potion. Rad!");
    }
}
