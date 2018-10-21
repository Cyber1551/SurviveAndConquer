using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character : MonoBehaviour {

    public string username;

    public int maxHealth = 1000;
    public int currentHealth;

    public int maxMana = 1000;
    public int currentMana;

    public Slider[] healthBar;
    public Slider[] healthBack;
    public Text[] healthTxt;
    private bool animate_Health;


    private float half_Health;
    private float quarter_Health;

    public float animated_Health_Speed;
    public List<Spell> spells;
    //public List<Effects> effects;
    Vector3 randomizeInt = new Vector3(1, 0, 0);
    private void Awake()
    {
        
    }
    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
        currentMana = maxMana;
        UpdateHealth();
        UpdateHealthPercentages();

    }
    void UpdateHealth()
    {
        //you call this anytime the players max health changes just to set it correctly on the sliders or there value will remain
        //The same as when the scene started for example leveling up in a real time rpg may increase max  health so call this
        //function.
        foreach (Slider slider in healthBar)
        {
            slider.minValue = 0;
            slider.maxValue = maxHealth;
            slider.value = currentHealth;
        }
        foreach (Slider slider in healthBack)
        {
            slider.minValue = 0;
            slider.maxValue = maxHealth;
            slider.value = currentHealth;
        }

        
    }

    public void TakeDamage(Character shooter, int amt)
    {

        currentHealth -= amt;
        foreach (Text txt in healthTxt)
        {
            txt.text = currentHealth.ToString();
        }
       
        StartCoroutine(EffectedHealth());

        //showFloatingText(amt);

        
        if (currentHealth <= 0)
        {
            //Die
            Debug.Log(shooter.username + " killed " + username);
        } 
        
    }
    void ShowFloatingText(int amt)
    {
        Transform c = transform.Find("DamageSpawn").transform;
        
        GameObject go = PhotonNetwork.Instantiate("DamageText", c.position, Quaternion.identity, 0, null) as GameObject;
        RectTransform tempRect = go.GetComponent<RectTransform>();
        go.transform.SetParent(transform.Find("NonVisibleCanvas"));
        tempRect.transform.localPosition = go.transform.localPosition + go.transform.right * 2;
        tempRect.transform.localScale = go.transform.localScale;
        tempRect.transform.localRotation = go.transform.localRotation;

        go.GetComponent<Text>().text = amt.ToString();

    }
    IEnumerator EffectedHealth()
    {
        for (int i = 0; i < healthBar.Length; i++)
        {
            //after rolling damage on the player call this coroutine
            animate_Health = true;
            //these do loops is what animated the health per health speed you can also add division or times etc to smooth it out.
            do
            {
                yield return new WaitForSeconds(animated_Health_Speed);
                healthBar[i].value -= 2;
            } while (healthBar[i].value > currentHealth);
            yield return new WaitForSeconds(0.2f);
            do
            {
                yield return new WaitForSeconds(animated_Health_Speed);
                healthBack[i].value -= 3;
            } while (healthBack[i].value > currentHealth);
            yield return new WaitForSeconds(0.5f);
            animate_Health = false;
            //the animate health bool is used so that update doesn't get access to it 100 times per real second there is no
            //point in calling animated health conditions if there is no health changed.
            yield return new WaitForSeconds(0.01f);
        }
        StopCoroutine(EffectedHealth());
    }
    public void UpdateBars()
    {
        
        //healthBar.value = ((float)currentHealth / maxHealth);
        
        
    }
    void UpdateHealthPercentages()
    {
        float half = maxHealth;
        half = half / 100 * 50;
        //50% Hp it will turn yellow or what color set in update

        Debug.Log(half.ToString());
        half_Health = Mathf.RoundToInt(half);

        float quarter = maxHealth;
        //25% Hp it will turn red or what color set in update
        quarter = quarter / 100 * 25;
        quarter_Health = Mathf.RoundToInt(quarter);
        foreach (Text txt in healthTxt)
        {
            txt.text = currentHealth.ToString();
        }

    }

    // Update is called once per frame
    void Update () {

       
    }
}
