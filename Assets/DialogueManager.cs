using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Dialogue[] conversations;
    private Queue<string> sentences;
    private int index = 0;
    public Text nameText, dialogueText;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        DialogueTrigger dialogueTrigger = FindObjectOfType<DialogueTrigger>();
        conversations = dialogueTrigger.conversations;
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue conversation)
    {
        animator.SetBool("isOpen", true);
        nameText.text = conversation.name;

        sentences.Clear();
        
        foreach (string sentence in conversations[index].sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        index++;
        if (index < conversations.Length)
        {
            StartDialogue(conversations[index]);
            return;
        } else
        {
            Debug.Log("End of dialogue.");
            animator.SetBool("isOpen", false);
        }
    }

    public void setIndex(int index)
    {
        this.index = index;
    }
}
