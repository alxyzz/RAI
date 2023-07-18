using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatOption
{
    public enum ChatOptionEffect
    {
        TAKE_BUDGET,
        GIVE_BUDGET,
        INSULT,
        PRAISE,
    }
    public ChatOptionEffect type;
    public int effectAmt;//how much effect this has
    public bool violentContent;
    public Kingdom target;

    public string chatText = "Put a stop to trade routes.\nTarget: [k] | [op]\nThis would harm our relationship.";

    public string GetText()
    {
        string b = chatText.Replace("[k]", target.kingdomName);
        if (target.ourKingOpinion > 5)
        {
            b = b.Replace("[op]", "Our king loves them.");
        }
        else if (target.ourKingOpinion < 0)
        {
            b = b.Replace("[op]", "Our king hates them.");
        }
        else
        {
            b = b.Replace("[op]", "Our king is ambivalent.");
        }
        return b;
    }

    public ChatOption(string text, ChatOptionEffect t, int effectamt, Kingdom ta, bool g = false)
    {
        
        chatText = text;
        chatText = chatText.Replace("[k]", ta.kingdomName);
        target = ta;
        effectAmt = effectamt;
        type = t;
        violentContent = g;
        Debug.Log("Initialized chatOption with type " + t.ToString()+ " targeting " + target.kingdomName);

    }




}
