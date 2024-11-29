using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public TextMeshProUGUI dialogueText;

	public bool endDialohue;

    public Animator animator;

    private Queue<string> sentences;
	public GameObject dila;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}

	public void StartDialogue (Dialogue dialogue)
	{
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void StartMotuDialogue(MotuDialogue dialogue)
	{
		animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayMotuNextSentence();
	}

	public void DisplayMotuNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndMotuDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}
	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
            EndDialogue();
            return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
		endDialohue = true;
		Invoke("End", .5f);
    }
	void EndMotuDialogue()
	{
		animator.SetBool("isOpen", false);
		Invoke("End", .5f);

	}
	private void End()
    {
		dila.SetActive(false);
    }

}
