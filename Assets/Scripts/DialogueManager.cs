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
            SceneTransition sceneTransition = FindObjectOfType<SceneTransition>();
            sceneTransition.TransitNewScene();
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
         int space = GetWordSize(" ", dialogueText.font, dialogueText.fontSize);
 
         string newText = string.Empty;
         int count = 0;
         for (int i = 0; i < words.Length; i++) {
             int size = GetWordSize(words[i], dialogueText.font, dialogueText.fontSize);
 
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
         return newText;
     }
 
     private int GetWordSize (string word, Font font, int fontSize) {
         char[] arr = word.ToCharArray();
         CharacterInfo info;
         int size = 0;
         for (int i = 0; i < arr.Length; i++) {
             font.GetCharacterInfo(arr[i], out info, fontSize);
             size += info.advance;
         }
         return size;
     }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }
}
