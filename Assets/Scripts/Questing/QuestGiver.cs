using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NPC
{
    public bool AssignedQuest { get; set; }
    public bool Helped { get; set; }
    public string[] dialog;

    [SerializeField]
    private GameObject quests;

    [SerializeField]
    private string questType;
    private Quest Quest { get; set; }

    QuestGiver()
    {
        AssignedQuest = false;
        Helped = false;
    }
    public override void Interact()
    {
        playerAgent.isStopped = true; //stop before too near. Social distancing.
        playerAgent.isStopped = false;
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        if (!AssignedQuest && !Helped)
        {
            base.Interact();
            AssignQuest();
        }
        else if(AssignedQuest && !Helped)
        {
            CheckQuest();
        }
        else
        {
            DialogueSystem.Instance.AddNewDialogue(new string[] { "Yes?" }, name);
        }
    }

    void AssignQuest()
    {
        AssignedQuest = true;
        Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));
    }

    void CheckQuest()
    {
        if (Quest.Completed)
        {
            Quest.GiveReward();
            Helped = true;
            AssignedQuest = false;
            DialogueSystem.Instance.AddNewDialogue(new string[] {"Thank you! Here's your reward." }, name);
        }
        else
        {
            DialogueSystem.Instance.AddNewDialogue(new string[] { "Please help us!"}, name);
        }
    }
}
