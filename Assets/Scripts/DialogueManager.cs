using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Dialogue conversation;
    private Queue<string> sentences;
    public Text nameText, dialogueText;
    public Animator animator;
    
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        DialogueTrigger dialogueTrigger = FindObjectOfType<DialogueTrigger>();
        conversation = dialogueTrigger.dialogue;
        
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue conversation)
    {
        animator.SetBool("isOpen", true);
        i = 0;
        nameText.text = conversation.lines[i].character.fullName;

        sentences.Clear();
        
        foreach (Line sentence in conversation.lines) {
            sentences.Enqueue(sentence.text);
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
        i++;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = sentence;
        Canvas.ForceUpdateCanvases();
        dialogueText.text = string.Empty;
        
        sentence = GetFormattedText(dialogueText, sentence);
        // Debug.Log(sentence);
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private string GetFormattedText (Text dialogueText, string sentence) {
         string[] words = sentence.Split(' ');
 
         int width = Mathf.FloorToInt(dialogueText.rectTransform.sizeDelta.x);
        //  Debug.Log("width: " + width);
         int space = GetWordSize(" ", dialogueText.font, dialogueText.fontSize);
        //  Debug.Log("space: " + space);
 
         string newText = string.Empty;
         int count = 0;
         for (int i = 0; i < words.Length; i++) {
             int size = GetWordSize(words[i], dialogueText.font, dialogueText.fontSize);
            //  Debug.Log("word size: " + size);
 
             if (i == 0) {
                 newText += words[i];
                 count += size;
             }
             else if (count + space > width || count + space + size > width) {
                 newText += "\n";
                 newText += words[i];
                 count = size;
             }
             else if (count + space + size <= width) {
                 newText += " " + words[i];
                 count += space + size;
             }
         }
        //  Debug.Log("text after format: " + newText);
         return newText;
     }
 
     private int GetWordSize (string word, Font font, int fontSize) {
         char[] arr = word.ToCharArray();
        //  Debug.Log("word char length: " + arr.Length);
         CharacterInfo info;
         int size = 0;
         for (int i = 0; i < arr.Length; i++) {
             font.GetCharacterInfo(arr[i], out info, fontSize);
            //  Debug.Log("info.advance: " + info.advance);
             size += info.advance;
         }
         return size;
     }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }
}
