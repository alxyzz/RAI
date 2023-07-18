using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Person", menuName = "Person")]
public class PersonObject : ScriptableObject
{
    //the script associated to these does nothing, they're just the mouthpieces of a kingdom.
    public string personName;
    public string description;

    public Sprite normalFace;


    public Person getPerson()
    {
        Person b = new Person(this);
        return b;
    }
}
