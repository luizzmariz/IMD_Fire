using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    public bool invicible = false;
    public float kickRange = 3f;
    public float kickStrength = 1f;
    public float sitRange = 2f;
    public float load_burn = 1f, load_asphyxiate = 2f;
    public float regainStaminaDelay = 5f;

    public string stamina_death = "Não se canse tanto!";
    public string fire_death = "Não chegue perto do fogo!";
    public string smoke_death = "Não chegue perto da fumaça!";
    public string window_death = "Não abra janelas em um incêndio!";
    public string elevator_death = "Não entre no elevador!";
    public string dontgoup_death = "Não suba as escadas em um incêndio!";
    public string timeover_death = "Você demorou demais a sair!<br>Já tentou usar as saídas de emergência?";

    public AudioClip mainMusic_audio;

    private int score = 1000;
    private float Hp = 100;
    private string deathReason = "-";
    private float r_burn = 0f, r_asphyxiate = 0f, r_score = 0f;
    private bool sitDown = false;
    private Vector3 beforeSittingPos;
    private float stamina = 100f;
    private float lastReducedStamina = 0f;
    private float startY = 0;
    private float levelTime = 0f;

    [SerializeField] Healthbar StaminaBar;
    [SerializeField] Healthbar HealthBar;
    [SerializeField] GameObject Leg;
    private MyUtils myUtils;
    private Animator anim;
    private Collider coll;
    private SpawnController spwnControl;
    private GameObject myLeg;
    private AudioSource audioSource;
    private Menu settings;

    // Start is called before the first frame update
    void Start()
    {
        myUtils = GetComponent<MyUtils>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(mainMusic_audio);
        spwnControl = GameObject.FindWithTag("SpawnController").gameObject.GetComponent<SpawnController>();
        settings =  GameObject.FindWithTag("Settings").gameObject.GetComponent<Menu>();

        transform.position = spwnControl.NormalSpawn();
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Dying

        if (stamina < 0f)
        {
            GameOver(stamina_death);
        }

        if (Hp <= 0f)
        {
            GameOver(deathReason);
        }

        // Checking if the player is going up the stairs near a fire
        if (CheckForDanger("FirePoint", 2) && transform.position.y - startY > 2.5)
        {
            GameOver(dontgoup_death);
        }

        // GameOver if the player takes to long in the level
        if (levelTime > Levels.timeInLevels[Levels.currentLevel])
        {
            GameOver(timeover_death);
        }
        levelTime += Time.deltaTime;

        // Regaining stamina
        if (lastReducedStamina > 0)
        {
            lastReducedStamina -= Time.deltaTime;
        }
        else {
            updateStamina(+3f); // +3 stamina 
            lastReducedStamina = regainStaminaDelay;
        }

        // Updating score
        if (r_score > 0)
        {
            r_score -= Time.deltaTime;
        }
        else {
            score -= 5; //-5 score per second
            r_score = 1f;
        }

        // Kicking
        if (Input.GetKeyDown(KeyCode.R) && !sitDown && myLeg == null)
        {
            try {
                Kick(GetNearestInteractable(), kickStrength);
            }
            catch(Exception e)
            {
                Exception e_ = e; // Workaround ro remove an annoying warning
                Debug.Log("Nada para chutar :p");
            }

            // Losing stamina
            updateStamina(-3f);

            // Creating Leg
            myLeg = Instantiate(Leg, transform.position, Quaternion.Euler(GetFront()));
            myLeg.transform.parent = transform;
        }

        // Sitting
        if (Input.GetKeyDown(KeyCode.F))
        {
            SitInChair(GetNearestInteractable("Chair"));
        }

        // --DEBUG--
        if (Input.GetKeyDown(KeyCode.X))
        {
            //PlayerPrefs.SetFloat("Volume", 0.29f);
            settings.pauseGame();
        }

        // Taking damage from fire
        if (CheckForDanger("FirePoint"))
        {
            if (r_burn >= load_burn)
            {
                updateHealth(-5f);
                Debug.Log("Fire, Hp: " + Hp);
                deathReason = fire_death;
                r_burn = 0;
            }

            r_burn += Time.deltaTime;
        }

        // Taking damage from smoke
        if (CheckForDanger("Smoke"))
        {
            if (r_asphyxiate >= load_asphyxiate)
            {
                updateHealth(-5f);
                Debug.Log("Smoke, Hp: " + Hp);
                deathReason = smoke_death;
                r_asphyxiate = 0;
            }

            r_asphyxiate += Time.deltaTime;
        }

        // adjusting leg rotation
        if (myLeg != null)
        {
            Vector3 ang = GetFront();
            myLeg.transform.eulerAngles = new Vector3(0, ang.y, 0);
        }
    }

    #region SittingDown / Chair Interactions
    // Toggles between sitting and getting up a chair
    void SitInChair(GameObject chair)
    {
        Chair chairScript = chair.GetComponent<Chair>();
        if (chairScript.IsSittable)
        {
            Vector3 chairPos = chair.transform.Find("SittingPosition").transform.position;
            anim.enabled = true;

            if (sitDown) // getting up
            {
                anim.SetBool("SittingDown", !sitDown);
                anim.Play("GettingUp");
            }
            else { // sitting down

                // Setting the player to a 'sitting down' state
                coll.isTrigger = true; // in order to not collide with the chair
                beforeSittingPos = transform.position; // saving the position of the player before sitting down
                transform.position = chairPos; // teleporting the player to the chair
                UpdateParentPos(); // in order to play the animation using the player's relative position

                anim.SetBool("SittingDown", !sitDown);
                anim.Play("SittingDown");
            }

            sitDown = !sitDown;
            chairScript.hasPlayer = sitDown;
        }
    }

    // Reverts player to a 'not sitting down' state
    // To be called when the 'GettingUp' animation ends
    void getUpFromChair()
    {
        anim.enabled = false;
        coll.isTrigger = false;

        transform.position = beforeSittingPos;
        UpdateParentPos();
    }
    #endregion

    void Kick(GameObject targetObj, float strength = 1f)
    {
        // TODO Play Animation

        Vector3 dir = Quaternion.Euler(20, 0, 0) * (targetObj.transform.position - transform.position).normalized;
        targetObj.gameObject.GetComponent<I_Interactable>().Kick(dir, strength);
    }

    bool CheckForDanger(string danger, float dis_modifier = 1f)
    {
        GameObject[] lista = GameObject.FindGameObjectsWithTag(danger);

        foreach(GameObject g in lista)
        {
            float distanceToDanger = Vector3.Distance(g.transform.position, transform.position);
            float dangerRadius = g.GetComponent<ParticleSpread>().GetRadius();

            if (distanceToDanger <= dangerRadius*g.transform.localScale.x*dis_modifier)
            {
                return true;
            }
        }

        return false;
    }

    // General Utility Functions

    Vector3 GetFront()
    {
        Vector3 cameraAng = transform.Find("Camera").transform.eulerAngles;
        return new Vector3(0, cameraAng.y - 90f, 0);
    }

    public void updateStamina(float v)
    {
        if (v < 0)
        {
            lastReducedStamina = regainStaminaDelay;
            stamina += v;
        }
        else { // adding stamina
            float newStamina = stamina + v;
            stamina = newStamina > 100f ? 100f : newStamina;
        }
        StaminaBar.updateValue(stamina);
    }

    public void updateHealth(float v)
    {
        Hp += v;
        HealthBar.updateValue(Hp);
    }

    void UpdateParentPos()
    {
        transform.parent.position += transform.localPosition;
        transform.localPosition = Vector3.zero;
    }

    GameObject GetNearestInteractable(string filter = "")
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, kickRange, 
        LayerMask.GetMask("Interactable"));

        return myUtils.GetNearestInteractable(myUtils.ToGameObjectArray(hitColliders), filter);
    }

    public bool isSatDown() {
        return sitDown;
    }

    // Meta
    public void GameOver(string reason)
    {
        if (!invicible) {
            Cursor.lockState = CursorLockMode.None;
            PlayerPrefs.SetString("DeathReason", reason);
            SceneManager.LoadScene("GameOver");
        }
    }

    public void GameVictory()
    {
        Cursor.lockState = CursorLockMode.None;
        PlayerPrefs.SetInt("Score", score);
        SceneManager.LoadScene("GameVictory");
    }
}
