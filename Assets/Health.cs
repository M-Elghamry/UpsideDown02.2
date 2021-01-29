using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public AudioSource hitSound;
    public Animator anim;

    public int HP;

    public float time;

    public Text timeText;
    public Text HPText;
    void Start()
    {
       
    }

    void FixedUpdate()
    {
        if (HP == 0)
        {
            //SceneManager.LoadScene("Boss");
        }
        time += .1f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            anim.SetTrigger("Hit");
            hitSound.Play();
            HP -= 1;

            timeText.text = time.ToString();
            HPText.text = HP.ToString();
        }
    }
}
