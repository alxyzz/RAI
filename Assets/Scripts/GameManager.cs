using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{




    #region singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }



    #endregion

    #region variables
    Kingdom relevantKingdom;
    Person relevantPerson;
    Person KING
    {
        get
        {
            if (_king == null)
            {
                Person b = new Person(kobject);
                _king = b;
            }
            return _king;
        }
    }    
    Person _king; //our king
    [SerializeReference]PersonObject kobject; //our king

    List<Kingdom> kingdoms
    {
        get
        {
            List<Kingdom> b = new();
            foreach (var item in content.Keys)
            {
                b.Add(item);
            }
            return b;
        }
    }
    List<Person> people
    {
        get
        {
            List<Person> b = new();
            foreach (var item in content.Values)
            {
                b.Add(item);
            }
            return b;
        }
    }
    Dictionary<Kingdom, Person> content = new();


    float personalFunds;

    float lastKingOpinionChange;
    float lastKingdomOpinionChange;

    [Header("Kingdoms")]
    [SerializeField] List<KingdomObject> KINGDOM_OBJECTS = new();




    [Header("Ambassador lines:")]

    [SerializeField] List<string> CHAT_DISAPPROVING = new();
    [SerializeField] List<string> CHAT_HATEFUL = new();
    [SerializeField] List<string> CHAT_APPROVING = new();
    [SerializeField] List<string> CHAT_LOVING = new();
    [SerializeField] List<string> CHAT_INVESTING = new();
    [SerializeField] List<string> CHAT_TAXING = new();

    [Header("Our king's lines:")]
    [SerializeField] List<string> CHAT_KING_DISAPPROVING = new();
    [SerializeField] List<string> CHAT_KING_HATEFUL = new();
    [SerializeField] List<string> CHAT_KING_APPROVING = new();
    [SerializeField] List<string> CHAT_KING_LOVING = new();
    #endregion

    #region random stuff


    Kingdom RANDOM_KINGDOM
    {
        get
        {
            return kingdoms[Random.Range(0, kingdoms.Count)];
        }
    }
    string RANDOM_KING_DISAPPROVING
    {
        get
        {
            return CHAT_KING_DISAPPROVING[Random.Range(0, CHAT_KING_DISAPPROVING.Count)];
        }
    }
    string RANDOM_KING_HATEFUL
    {
        get
        {
            return CHAT_KING_HATEFUL[Random.Range(0, CHAT_KING_HATEFUL.Count)];
        }
    }
    string RANDOM_KING_APPROVING
    {
        get
        {
            return CHAT_KING_APPROVING[Random.Range(0, CHAT_KING_APPROVING.Count)];
        }
    }
    string RANDOM_KING_LOVING
    {
        get
        {
            return CHAT_KING_LOVING[Random.Range(0, CHAT_KING_LOVING.Count)];
        }
    }


    string RANDOM_DISAPPROVING
    {
        get
        {
            return CHAT_DISAPPROVING[Random.Range(0, CHAT_DISAPPROVING.Count)];
        }
    }
    string RANDOM_HATEFUL
    {
        get
        {
            return CHAT_HATEFUL[Random.Range(0, CHAT_HATEFUL.Count)];
        }
    }
    string RANDOM_APPROVING
    {
        get
        {
            return CHAT_APPROVING[Random.Range(0, CHAT_APPROVING.Count)];
        }
    }
    string RANDOM_LOVING
    {
        get
        {
            return CHAT_LOVING[Random.Range(0, CHAT_LOVING.Count)];
        }
    }

    float INCOME_PER_TURN
    {
        get
        {
            float b = 0;
            foreach (var item in kingdoms)
            {
                if (item.playerStake > 0)
                {
                    b += ((item.playerStake / 100) * item.Prosperity);
                }
            }
            return b;
        }
    }

    #endregion


    #region References





    [SerializeReference] TextMeshProUGUI storybox;
    [SerializeReference] TextMeshProUGUI personalAssetsDisplay;

    [SerializeReference] TextMeshProUGUI popupMessagebox;
    [SerializeReference] GameObject popup;
    [SerializeReference] GameObject popup_DEFEAT;

    [SerializeReference] TextMeshProUGUI speakerName;
    [SerializeReference] TextMeshProUGUI speakerdescription;
    [SerializeReference] TextMeshProUGUI landName;
    [SerializeReference] TextMeshProUGUI landDescription;

    [SerializeReference] Image imageRepresentant;
    [SerializeReference] Image imageKingdom;


    [SerializeReference] GameObject decisionParent;
    [SerializeReference] GameObject prefabDecision;
    [SerializeReference] List<ChatOption> decisions = new();

    List<GameObject> choiceObjects = new();


    #endregion


    #region UnityMethods

    void Awake() //used for singleton
    {
        if (_instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            InitializeKingdoms();

        }

    }


    // Start is called before the first frame update
    void Start()
    {
        InitializeChatOptions();//only done once
        PopulateDecisionList();//do this after every choice
    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion



    #region Derived properties


    bool KingLikesCurrentFaction
    {
        get
        {
            return relevantKingdom.ourKingOpinion > 5;
        }
    }
    bool KingDislikesCurrentFaction
    {
        get
        {
            return relevantKingdom.ourKingOpinion < 0;
        }
    }

    #endregion

    #region Other Methods



    void InitializeKingdoms()
    {
        List<Kingdom> kdms = new();
        foreach (var item in KINGDOM_OBJECTS)
        {
            Kingdom b = new Kingdom(item);
            kdms.Add(b);
        }


        foreach (var item in kdms)
        {
            content.Add(item, item.Ambassador);
        }

    }


    void InitializeChatOptions()
    {
        //Put a stop to trade routes.\nTarget: [k] | King loves them.\nThis would harm our relationship.
        for (int i = 0; i < 3; i++)
        {
            List<ChatOption> news = new List<ChatOption>() {  new ChatOption("Break our trading pact.\nTarget: [k] |.\nThis would harm our relationship.",ChatOption.ChatOptionEffect.TAKE_BUDGET, 5, RANDOM_KINGDOM),
             new ChatOption("Have their envoy beaten.\nTarget: [k] |.\nThis would harm our relationship.",ChatOption.ChatOptionEffect.INSULT, 2, RANDOM_KINGDOM),
              new ChatOption("Offer them minor gifts.\nTarget: [k] |.\nThis would help our relationship.",ChatOption.ChatOptionEffect.PRAISE, 2, RANDOM_KINGDOM),
               new ChatOption("Call their pig of a king for what he is.\nTarget: [k] |.\nThis would harm our relationship.",ChatOption.ChatOptionEffect.INSULT, 5, RANDOM_KINGDOM),
                new ChatOption("Praise their wise decisions.\nTarget: [k] |.\nThis would help our relationship.",ChatOption.ChatOptionEffect.PRAISE, 3, RANDOM_KINGDOM),
                 new ChatOption("Sponsor a bandit gang.\nTarget: [k] |.\nThis would harm our relationship.",ChatOption.ChatOptionEffect.TAKE_BUDGET, 5, RANDOM_KINGDOM),
                  new ChatOption("Send soldiers to aid.\nTarget: [k] |.\nThis would aid our relationship.",ChatOption.ChatOptionEffect.GIVE_BUDGET, 5, RANDOM_KINGDOM),
                   new ChatOption("Offer them major gifts.\nTarget: [k] |.\nThis would help our relationship.",ChatOption.ChatOptionEffect.GIVE_BUDGET, 5, RANDOM_KINGDOM),

        };

            decisions.AddRange(news);
        }
       
    }


    void LogKingOpinion()
    {

        if (lastKingdomOpinionChange > 0)
        {
            AddLog("Your king was pleased by your last decision.");
        }
        else if (lastKingOpinionChange < 0)
        {
            AddLog("Your king was displeased by your last choice...");

        }

    }
    void PopUpMessage(string texty)
    {
        popup.SetActive(true);
        popupMessagebox.text = texty;


    }

    public void OnGenericPopUpClickOk()
    {
        popup.SetActive(false);
    }
    private void ProcessIncome()
    {
        float gain = INCOME_PER_TURN;
        if (gain > 0)
        {
            PopUpMessage("Your wise investments have lead to an income of " + gain.ToString() + " this week.");
            personalFunds += gain;
            AddLog("You gained " + gain.ToString() + " from your investments.");
        }


        string money = personalFunds.ToString() + " Driftshards";


        string op = KING.playerReputation.ToString();

        personalAssetsDisplay.text = "Personal Assets:\n"+ money+ "\n\n\nThe King's opinion:\n"+ op;
    }

    public void WhenEnter(string kingdomDescription, string kingdomName, string speakername, string speakerdesc, Sprite kingdomImage, Sprite ambassadorImage)
    {
        speakerdescription.text = speakerdesc;
        speakerName.text = speakername;
        landDescription.text = kingdomDescription;
        landName.text = kingdomName;

        imageKingdom.sprite = kingdomImage;
        imageKingdom.enabled = true;
        imageRepresentant.sprite = ambassadorImage;
        imageRepresentant.enabled = true;
    }

    public void WhenExit()
    {
        speakerdescription.text = "";
        speakerName.text = "";
        landDescription.text = "";
        landName.text = "";

        imageKingdom.sprite = null;
        imageKingdom.enabled = false;
        imageRepresentant.sprite = null;
        imageRepresentant.enabled = false;
    }




    [SerializeReference] GameObject ambassadorReplyPopUp;
    [SerializeReference] TextMeshProUGUI ambassadorReply;
    [SerializeReference] Image ambassador_UI_image_component;
    [SerializeReference] Image ambassadorFace;
    [SerializeReference] Sprite ambassadorReplyUI_GOOD, ambassadorReplyUI_BAD;
    ChatOption last;
    public void OnAmbassadorReplyAccept()
    {//Proceed to the next sequence of decisions

        ambassadorReplyPopUp.SetActive(false);
        DoDiplomaticModifier();
        LogKingOpinion();
        ProcessIncome();
        CheckIfLoss();
        PopulateDecisionList();

    }

    public void OnClickChatOption(ChatOption b)
    {
        last = b;


        ShowPopupInfo();


        void ShowPopupInfo()
        {
            ambassadorFace.sprite = b.target.Ambassador.normalFace;


            switch (b.type)
            {
                case ChatOption.ChatOptionEffect.TAKE_BUDGET:
                    b.target.opinionOfPlayer -= b.effectAmt * 5;
                    ambassador_UI_image_component.sprite = ambassadorReplyUI_BAD;
                    ambassadorReply.text = CHAT_TAXING[Random.Range(0, CHAT_TAXING.Count)];
                    b.target.playerStake += b.effectAmt;

                    break;
                case ChatOption.ChatOptionEffect.GIVE_BUDGET:
                    b.target.opinionOfPlayer += b.effectAmt * 5;
                    ambassador_UI_image_component.sprite = ambassadorReplyUI_GOOD;
                    ambassadorReply.text = CHAT_INVESTING[Random.Range(0, CHAT_INVESTING.Count)];
                    b.target.playerStake -= b.effectAmt;

                    break;
                case ChatOption.ChatOptionEffect.INSULT:

                    b.target.opinionOfPlayer -= b.effectAmt * 3;
                    ambassador_UI_image_component.sprite = ambassadorReplyUI_BAD;
                    if (b.target.opinionOfPlayer > 0)
                    {
                        ambassadorReply.text = CHAT_DISAPPROVING[Random.Range(0, CHAT_DISAPPROVING.Count)];
                    }
                    else
                        ambassadorReply.text = CHAT_HATEFUL[Random.Range(0, CHAT_HATEFUL.Count)];

                    break;
                case ChatOption.ChatOptionEffect.PRAISE:

                    b.target.opinionOfPlayer += b.effectAmt * 3;
                    ambassador_UI_image_component.sprite = ambassadorReplyUI_GOOD;

                    if (b.target.opinionOfPlayer > 50)
                    {
                        ambassadorReply.text = CHAT_LOVING[Random.Range(0, CHAT_LOVING.Count)];
                    }
                    else
                    {
                        ambassadorReply.text = CHAT_APPROVING[Random.Range(0, CHAT_APPROVING.Count)];
                    }
                    break;
                default:
                    break;
            }







            ambassadorReplyPopUp.SetActive(true);

        }
    }
    void DoDiplomaticModifier()
    {
        relevantKingdom = last.target;
        //last option stored in "last"
        string log = ""; //what we just did
        switch (last.type)
        {
            case ChatOption.ChatOptionEffect.TAKE_BUDGET:
                relevantKingdom.opinionOfPlayer -= last.effectAmt * 2;
                personalFunds += last.effectAmt * 50;
                if (KingLikesCurrentFaction)
                {
                    KING.playerReputation -= (last.effectAmt / 2);
                    lastKingOpinionChange = (-1 * (last.effectAmt / 2));
                }
                else if (KingDislikesCurrentFaction)
                {
                    KING.playerReputation += (last.effectAmt / 2);
                    lastKingOpinionChange = (1 * (last.effectAmt / 2));
                }
                break;
            case ChatOption.ChatOptionEffect.GIVE_BUDGET:
                relevantKingdom.opinionOfPlayer -= last.effectAmt * 2;
                personalFunds -= last.effectAmt * 50;
                if (KingDislikesCurrentFaction)
                {
                    KING.playerReputation -= (last.effectAmt / 2);
                    lastKingOpinionChange = (-1 * (last.effectAmt / 2));
                }
                else if (KingLikesCurrentFaction)
                {
                    KING.playerReputation += (last.effectAmt / 2);
                    lastKingOpinionChange = (1 * (last.effectAmt / 2));
                }
                break;
            case ChatOption.ChatOptionEffect.INSULT:
                relevantKingdom.opinionOfPlayer -= last.effectAmt;
                if (KingLikesCurrentFaction)
                {
                    KING.playerReputation -= (last.effectAmt / 3);
                    lastKingOpinionChange = (-1 * (last.effectAmt / 2));
                }
                else if (KingDislikesCurrentFaction)
                {
                    KING.playerReputation += (last.effectAmt / 3);
                    lastKingOpinionChange = (1 * (last.effectAmt / 2));
                }
                break;
            case ChatOption.ChatOptionEffect.PRAISE:
                relevantKingdom.opinionOfPlayer -= last.effectAmt;
                if (KingDislikesCurrentFaction)
                {
                    KING.playerReputation -= (last.effectAmt / 3);
                    lastKingOpinionChange = (-1 * (last.effectAmt / 2));
                }
                else if (KingLikesCurrentFaction)
                {
                    KING.playerReputation += (last.effectAmt / 3);
                    lastKingOpinionChange = (1 * (last.effectAmt / 2));
                }
                break;
            default:
                break;
        }


        AddLog(log);

    }
    void CheckIfLoss()
    {
        if (KING.playerReputation <= 0)
        {
            Lose();
        }


        void Lose()
        {
            popup_DEFEAT.SetActive(true);
            StartCoroutine(timedExit());
        }
    }

    System.Collections.IEnumerator timedExit()
    {


        yield return new WaitForSeconds(6f);
        Application.Quit();
    }
    int dirtyLogLimit = 0;
    void AddLog(string b)
    {
        dirtyLogLimit++;
        if (dirtyLogLimit >= 29)
        {
            storybox.text = "";
            dirtyLogLimit = 0;
        }
        storybox.text += b + System.Environment.NewLine;
    }

    void PopulateDecisionList()
    {
        foreach (var item in choiceObjects)
        {
            Destroy(item.gameObject);
        }
        List<ChatOption> newDecisions = new List<ChatOption>();
        List<ChatOption> step = new List<ChatOption>();
        foreach (var item in decisions)
        {
            step.Add(item);
        }
        for (int i = 0; i < 3; i++)
        {

            ChatOption bs = step[Random.Range(0, step.Count)];
            newDecisions.Add(bs);
            step.Remove(bs);

        }

        foreach (var item in newDecisions)
        {
            GameObject newchoice = Instantiate(prefabDecision, decisionParent.transform);
            newchoice.GetComponent<ChoiceButton>().Setup(item);
            choiceObjects.Add(newchoice);
        }
    }


    #endregion
}
