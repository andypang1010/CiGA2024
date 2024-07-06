using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public GameObject player;

    [Header("Dialogue")]
    public GameObject dialoguePanel;
    public TMP_Text npcText, dialogueText;

    [Header("Choices")]
    public GameObject[] choices;
    TMP_Text[] choicesText;

    Story currentStory;

    public TextMeshProUGUI TimeText;

    void Start()
    {
        dialoguePanel.SetActive(false);

        choicesText = new TMP_Text[choices.Length];
        int index = 0;

        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TMP_Text>();
            choice.SetActive(false);
            index++;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject[] NPCs = Physics.OverlapSphere(player.transform.position, 1f, LayerMask.GetMask("NPC")).Select(collider => collider.gameObject).ToArray();

            if (NPCs.Length == 0) {
                return;
            }

            float closestDistance = Mathf.Infinity;
            GameObject closestNPC = null;

            foreach (GameObject npc in NPCs) {
                float currentDistance = Vector3.Distance(player.transform.position, npc.transform.position);
                if (currentDistance < closestDistance) {
                    closestDistance = currentDistance;
                    closestNPC = npc;
                }
            }

            if (!dialoguePanel.activeSelf)
            {
                EnterDialogueMode(closestNPC.name, closestNPC.GetComponent<NPCDialogue>().inkJson);
            }

            else if (dialoguePanel.activeSelf
            && currentStory.currentChoices.Count == 0)
            {
                ContinueStory();
            }
        }
    }

    void EnterDialogueMode(string npcName, TextAsset inkJson)
    {
        currentStory = new Story(inkJson.text);

        dialoguePanel.SetActive(true);
        player.GetComponent<PlayerController>().enabled = false;

        npcText.text = npcName;

        TimeText.GetComponent<TimeTextUpdate>().StopTimer();

        ContinueStory();
    }

    void ExitDialogueMode()
    {
        dialoguePanel.SetActive(false);
        player.GetComponent<PlayerController>().enabled = true;

        dialogueText.text = "";

        TimeText.GetComponent<TimeTextUpdate>().ResumeTimer();
    }

    void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }

        else
        {
            ExitDialogueMode();
        }

        DisplayChoices();
    }

    void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        int index = 0;

        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;

            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(player.transform.position, 1f);
    }
}
