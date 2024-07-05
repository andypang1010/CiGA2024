using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text npcText, dialogueText;
    public GameObject player;

    bool dialogueIsPlaying;
    Story currentStory;
    
    void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) 
        && Physics.Raycast(player.transform.position, player.transform.forward, out RaycastHit hit, 1f)
        && hit.collider.gameObject.CompareTag("NPC")) {
            if (!dialoguePanel.activeSelf) {

                // Enter dialogue mode
                EnterDialogueMode(hit.collider.gameObject.name, hit.collider.gameObject.GetComponent<NPCDialogue>().inkJson);
            }

            else {
                // Progress through dialogue
                ContinueStory();
            }

        }
    }

    void EnterDialogueMode(string npcName, TextAsset inkJson) {
        currentStory = new Story(inkJson.text);

        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        player.GetComponent<PlayerController>().enabled = false;

        npcText.text = npcName;

        ContinueStory();
    }

    void ExitDialogueMode() {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        player.GetComponent<PlayerController>().enabled = true;

        dialogueText.text = "";
    }

    void ContinueStory() {
        if (currentStory.canContinue) {
            dialogueText.text = currentStory.Continue();
        }

        else {
            ExitDialogueMode();
        }
    }
}
