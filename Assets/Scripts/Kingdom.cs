using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kingdom
{


    public string kingdomName;
    public string description;
    public float opinionOfPlayer; //what this kingdom thinks of you, the king's advisor.
    public float ourKingOpinion; //how much the king likes this nation. helping a liked nation or harming a disliked nation makes him like you more. doing the opposite, does the opposite
    public float playerStake; //wether player profits from helping or harming these nations
    public float Prosperity; //0 to 100
    public Person Ambassador;


    bool ruined = false;

    public Sprite kingdomImage;

    public Kingdom(KingdomObject b)
    {
        Ambassador = b.Ambassador.getPerson();
        kingdomName = b.kingdomName;
        description = b.description;
        opinionOfPlayer = b.opinionOfPlayer;
        ourKingOpinion = b.ourKingOpinion;
        playerStake = b.playerStake;

        kingdomImage = b.kingdomImage;
        Prosperity = Random.Range(0, 100);

        Debug.Log("Initialized Kingdom with name " + kingdomName);
    }
}
