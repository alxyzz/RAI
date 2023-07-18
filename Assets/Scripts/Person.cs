using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person 
{
    public string personName;
    public string description;
    public float playerReputation = 40;


    public Sprite normalFace;
    public Person(PersonObject b)
    {
        personName = b.personName;
        description = b.description;
        normalFace = b.normalFace;
        if (personName == "Hypomaxis")
        {
            playerReputation = 50;
        }
        Debug.Log("Initialized person with name " + personName);
    }

}
