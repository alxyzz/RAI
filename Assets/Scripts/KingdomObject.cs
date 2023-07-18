using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Kingdom", menuName = "Kingdom")]
public class KingdomObject : ScriptableObject
{


    public string kingdomName;
    public string description;
    public float opinionOfPlayer; //what this kingdom thinks of you, the king's advisor.
    public float ourKingOpinion; //how much the king likes this nation. helping a liked nation or harming a disliked nation makes him like you more. doing the opposite, does the opposite
    public float playerStake; //wether player profits from helping or harming these nations

    public PersonObject Ambassador;

    public Sprite kingdomImage;
    
    

   

}
