using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public string nextScene;

    public GameObject audioManager;

    public GameObject transitionManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio");
        transitionManager = GameObject.FindGameObjectWithTag("Transition");
    }
    public void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1) * 50 * Time.deltaTime);
    }    

    public void Portal()
    {
        transitionManager.GetComponent<TransitionScript>().Close();
        audioManager.GetComponent<AudioManagement>().portalSound.Play();

        StartCoroutine(waitToLoadScene(2));
    }

    public IEnumerator waitToLoadScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(nextScene);
    }
}
