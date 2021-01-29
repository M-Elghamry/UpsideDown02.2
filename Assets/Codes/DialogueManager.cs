using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public int index;
    public float typingSpeed;
    public int ButtonsPopUp;
    public GameObject devil;
    public Transform spawnDevilFrom;

    public GameObject continueButton;
    [SerializeField] private Animator ButtonAnim;




    void start()
    {

        textDisplay.text = sentences[0];
        StartCoroutine(Type());     
        ButtonAnim.SetBool("canChoose", false);
    }

    void Update()
    {
        if(textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }

        if (index == ButtonsPopUp)
        {
            ButtonAnim.SetBool("canChoose", true);
        }
        else
        {
            ButtonAnim.SetBool("canChoose", false);
        }


    }

    

   public IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

    }



    public void SpawnDevil()
    {
        Instantiate(devil, spawnDevilFrom.position, spawnDevilFrom.rotation);
        ButtonAnim.SetBool("canChoose", false);
        index = 0;
    }

    public void OnbuttonDo()
    {
        SceneManager.LoadScene("SceneBoss_Angel");
    }



    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (index < sentences.Length - 1 )
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }

}