using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public static Fruit _instance;

    private int points = -1;

    public void OnPick()
    {
        points++;

        float randomX = Random.Range(-10, 11);
        float randomY = Random.Range(-10, 11);

        transform.position = new Vector2(randomX, randomY);
    }

    public void OnDeath()
    {
        if (points > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", points);
        }

        OnPick();

        points = 0;
    }

    void Awake()
    {
        _instance = this;

        if (PlayerPrefs.HasKey("BestScore") == false)
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }

        OnPick();
    }

    void OnGUI()
    {
        GUI.Label(new Rect(160f, 20f, 100f, 20f), "Score: " + points);

        if (PlayerPrefs.GetInt("BestScore") > 0)
        {
            GUI.Label(new Rect(160f, 40f, 100f, 20f), "Best: " + PlayerPrefs.GetInt("BestScore"));
        }
    }
}
