using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    
    public void TriggerDialogue(){
        // setupConversations();
        DialogueManager manager = FindObjectOfType<DialogueManager>();
        manager.StartDialogue(dialogue);
    }

    // private void setupConversations(){
    //     conversations = new Dialogue[4];
    //     Dialogue dialogue = new Dialogue();
    //     dialogue.name = "Interviewer";
    //     dialogue.sentences = new string[1];
    //     dialogue.sentences[0] = "With us is presumtive Presidential nominee, Senator Morgan, for his first major interview since sweeping the Super Tuesday primary contests " + 
    //                             "less than twenty-four hours ago. Congratulations on your victories, Senator and for locking down the nomination so quickly.";
    //     conversations[0] = dialogue;

    //     dialogue = new Dialogue();
    //     dialogue.name = "Morgan";
    //     dialogue.sentences = new string[1];
    //     dialogue.sentences[0] = "Thank you very much, Anderson.";
    //     conversations[1] = dialogue;

    //     dialogue = new Dialogue();
    //     dialogue.name = "Interviewer";
    //     dialogue.sentences = new string[1];
    //     dialogue.sentences[0] = "Now that your opponents have suspended their campaigns, are you surprised at all with how quickly you've managed to dispatch your competition? " + 
    //                             "You were, after all, facing two candidates that were far better funded than yourself throughout most of the early primary season. " + 
    //                             "Yet here we are in the middle of March, and you're already uncontested and headed for the general election.";
    //     conversations[2] = dialogue;

    //     dialogue = new Dialogue();
    //     dialogue.name = "test";
    //     dialogue.sentences = new string[1];
    //     dialogue.sentences[0] = "testtext";
    //     conversations[3] = dialogue;
    // }
}
