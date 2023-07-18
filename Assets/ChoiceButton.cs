using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChoiceButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeReference] TextMeshProUGUI texty;
    ChatOption related;
    public void Setup(ChatOption option)
    {

        related = option;
        texty.text = option.chatText;
    }

    public void ClickMe()
    {
       // Debug.Log("Clicked chat option for option " + related.GetText());
        Debug.Log("Clicked chat option for option ");
        GameManager.Instance.OnClickChatOption(related);
    }


   

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Moused over.");
        GameManager.Instance.WhenEnter(related.target.description, related.target.kingdomName, related.target.Ambassador.personName, related.target.Ambassador.description, related.target.kingdomImage, related.target.Ambassador.normalFace);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Instance.WhenExit();

    }
}
