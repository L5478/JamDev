using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PowerUpSelector : MonoBehaviour
{
#region Singleton
    public static PowerUpSelector instance;
    public static PowerUpSelector Instance { get => instance; }
#endregion

    public GameObject powerupsGO;
    public GameObject grayBackground;
    public List<PowerUpSO> listPowerUps;
    public List<Button> btnList;
    public Text coinsText;
    public Button skipBtn;

    //Summary end screen (this is ugly)
    public Text ScoreL;
    public Text NormalL;
    public Text EliteL;
    public Text RepairedL;
    public Text HosedL;
    public Text ExplodedL;

    //Summary end screen (this is even uglier)
    public Text ScoreW;
    public Text NormalW;
    public Text EliteW;
    public Text RepairedW;
    public Text HosedW;
    public Text ExplodedW;

    //Summary end screen private
    private int normalMolesSlammed = 0;
    private int eliteMolesSlammed = 0;
    private int molesWaterHosed = 0;
    private int planksPlaced = 0;
    private int holesExploded = 0;
    private bool gameOver = false;
    private bool canBuy = true;

    private int coins = 0;
    private PlayerInput playerInput;
    private Animator animator;
    public float waitTime = 15f;
    private AudioSource audioSource;
    public AudioClip clickSound;

    private void Awake()
    {
        #region Singleton
        if (instance != null && instance != this)
            Destroy(instance);
        else
            instance = this;
        #endregion
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerInput = FindObjectOfType<PlayerInput>();
        animator = GetComponent<Animator>();
        Invoke("StartPowerupsAnimation", waitTime);
        AdjustCoins(0);

        for (int i = 0; i < btnList.Count; i++)
        {
            SetPowerupInfo(listPowerUps[i], btnList[i]);
        }
    }

    private void StartPowerupsAnimation()
    {
        if (!gameOver)
        {
            animator.SetTrigger("ShowPowerUps");
            Time.timeScale = .15f;
            for (int i = 0; i < btnList.Count; i++)
            {
                if (listPowerUps[i].cost > coins)
                {
                    btnList[i].enabled = false;
                    btnList[i].GetComponentInChildren<RawImage>(true).enabled = true;
                }
                else
                {
                    btnList[i].enabled = true;
                    btnList[i].GetComponentInChildren<RawImage>(true).enabled = false;
                }
            }
        }
    }

    private void SetPowerupInfo(PowerUpSO SO, Button btn)
    {
        btn.GetComponentsInChildren<Text>()[0].text = SO.name;
        btn.GetComponentsInChildren<Text>()[1].text = SO.cost.ToString();
        btn.GetComponent<Image>().sprite = SO.spriteImg;

        btn.onClick.AddListener(() => OnPowerUpSelect(SO.name));
        btn.onClick.AddListener(() => AdjustCoins(-SO.cost));
        btn.onClick.AddListener(() => DisableButtons());
    }

    private void DisableButtons()
    {
        for (int i = 0; i < btnList.Count; i++)
        {
            btnList[i].enabled = false;
        }
    }
    public void AdjustCoins(int amount)
    {
        coins += amount;
        coinsText.text = coins.ToString();
    }

    public void AddNormalMole()
    {
        normalMolesSlammed++;
    }
    public void AddEliteMole()
    {
        eliteMolesSlammed++;
    }

    public void AddWaterHosed()
    {
        molesWaterHosed++;
    }

    public void AddPlanks()
    {
        planksPlaced++; ;
    }

    public void AddHolesExploded()
    {
        holesExploded++;
    }

    public void ShowLostScreen()
    {
        if (!gameOver)
        {
            int ScoreInt = normalMolesSlammed * 50 + eliteMolesSlammed * 100 + planksPlaced * 25 + molesWaterHosed * 25 + holesExploded * 200;
            CancelInvoke();
            animator.SetTrigger("EndScreen");
            ScoreL.text = ScoreInt.ToString();
            NormalL.text += normalMolesSlammed.ToString();
            EliteL.text += eliteMolesSlammed.ToString();
            RepairedL.text += planksPlaced.ToString();
            HosedL.text += molesWaterHosed.ToString();
            ExplodedL.text += holesExploded.ToString();
        }
        gameOver = true;
    }

    public void ShowVictoryScreen()
    {
        if (!gameOver)
        {
            int ScoreInt = normalMolesSlammed * 50 + eliteMolesSlammed * 100 + planksPlaced * 25 + molesWaterHosed * 25 + holesExploded * 200;
            CancelInvoke();
            animator.SetTrigger("VictoryScreen");
            ScoreW.text = ScoreInt.ToString();
            NormalW.text += normalMolesSlammed.ToString();
            EliteW.text += eliteMolesSlammed.ToString();
            RepairedW.text += planksPlaced.ToString();
            HosedW.text += molesWaterHosed.ToString();
            ExplodedW.text += holesExploded.ToString();
        }
        gameOver = true;
    }

    public void OnPowerUpSelect(string sPowerUp = null)
    {
        Time.timeScale = 1;
        if (sPowerUp != null)
        {
            playerInput.SetCurrentPowerUp(sPowerUp);
        }
        animator.SetTrigger("Hide");
        Invoke("StartPowerupsAnimation", waitTime * 2f);
    }

    public void PlayAgain()
    {
        audioSource.PlayOneShot(clickSound);
        SceneManager.LoadScene(1);
    }

    public void GoToMenu()
    {
        audioSource.PlayOneShot(clickSound);
        SceneManager.LoadScene(0);
    }

    // NOT IN USE RIGHT NOW
    public PowerUpSO GetRandomPowerUp()
    {
        int totalWeight = 0;
        //add up total weight values
        foreach (var PU in listPowerUps)
        {
            totalWeight += PU.weight;
        }

        //Get random int in range
        float randomN = Random.Range(0, totalWeight);

        //Keep subtracting untill you find your random weighed number
        for (int i = 0; i < listPowerUps.Count; i++)
        {
            if (randomN < listPowerUps[i].weight)
            {
                return listPowerUps[i];
            }
            randomN -= listPowerUps[i].weight;
        }
        return null;
    }
}
