using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;


public class AnimatedSlider : MonoBehaviour
{

    public Slider player_Health_Main;
    public Slider player_Health_Back;


    public int player_Current_Health;
    public int player_Max_Health;


    private bool animate_Health;


    private float half_Health;
    private float quarter_Health;

    public float animated_Health_Speed;

    void UpdateHealth()
    {
        //you call this anytime the players max health changes just to set it correctly on the sliders or there value will remain
        //The same as when the scene started for example leveling up in a real time rpg may increase max  health so call this
        //function.
        player_Health_Main.minValue = 0;
        player_Health_Main.maxValue = player_Max_Health;
        player_Health_Main.value = player_Current_Health;

        player_Health_Back.minValue = 0;
        player_Health_Back.maxValue = player_Max_Health;
        player_Health_Back.value = player_Current_Health;
    }

    void Start()
    {
        //update this every time the max health gets increased or decreased
        UpdateHealth();
        UpdateHealthPercentages();
        TakeDamage(35);
    }

    void UpdateHealthPercentages()
    {
        float half = player_Max_Health;
        half = half / 100 * 50;
        //50% Hp it will turn yellow or what color set in update

        Debug.Log(half.ToString());
        half_Health = Mathf.RoundToInt(half);

        float quarter = player_Max_Health;
        //25% Hp it will turn red or what color set in update
        quarter = quarter / 100 * 25;
        quarter_Health = Mathf.RoundToInt(quarter);

    }
    void Update()
    {
        if (player_Health_Main.value <= 0)
        {
            player_Health_Main.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (player_Health_Main.value > 0)
        {
            player_Health_Main.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        }
        if (player_Health_Back.value <= 0)
        {
            player_Health_Back.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (player_Health_Back.value > 0)
        {
            player_Health_Back.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        }

        if (animate_Health)
        {
            if (player_Health_Main.value <= half_Health)
            {
                player_Health_Main.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new Color32(195, 214, 24, 255);
            }
            if (player_Health_Main.value <= quarter_Health)
            {
                player_Health_Main.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new Color32(24, 214, 195, 255);
            }
        }
    }

    IEnumerator EffectedHealth()
    {
        //after rolling damage on the player call this coroutine
        animate_Health = true;
        //these do loops is what animated the health per health speed you can also add division or times etc to smooth it out.
        do
        {
            yield return new WaitForSeconds(animated_Health_Speed);
            player_Health_Main.value -= 1;
        } while (player_Health_Main.value > player_Current_Health);
        yield return new WaitForSeconds(0.2f);
        do
        {
            yield return new WaitForSeconds(animated_Health_Speed);
            player_Health_Back.value -= 1;
        } while (player_Health_Back.value > player_Current_Health);
        yield return new WaitForSeconds(0.5f);
        animate_Health = false;
        //the animate health bool is used so that update doesn't get access to it 100 times per real second there is no
        //point in calling animated health conditions if there is no health changed.
        yield return new WaitForSeconds(0.01f);
        StopCoroutine(EffectedHealth());
    }

    void TakeDamage(int amount)
    {
        player_Current_Health -= amount;
        StartCoroutine(EffectedHealth());
    }
}