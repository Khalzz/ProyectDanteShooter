using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    static public int playerLife;
    public TextMeshPro lifeText;
    // Start is called before the first frame update
    void Start()
    {
        playerLife = 100;
    }

    // Update is called once per frame
    void Update()
    {
        lifeText.text = (playerLife.ToString() + "%");
        if (playerLife <= 0)
        {
            SceneManager.LoadScene("TestingScene");
        }
    }

    static public void GettingDamage(int damage)
    {
        playerLife -= damage;
        print(playerLife);
    }


}
