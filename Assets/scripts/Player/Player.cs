using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float stamina = 100f;
    public float kickRange = 3f;
    public float kickStrength = 1f;
    public float sitRange = 2f;
    public float load_burn = 1f, load_asphyxiate = 2f;

    public string stamina_death = "Não se canse tanto!";
    public string fire_death = "Não chegue perto do fogo!";
    public string smoke_death = "Não chegue perto da fumaça!";
    public string window_death = "Não abra janelas em um incêndio!";

    private int Hp = 20;
    private string deathReason = "-";
    private float r_burn = 0f, r_asphyxiate = 0f;
    private bool sitDown = false;
    private Vector3 beforeSittingPos;

    private MyUtils myUtils;
    private Animator anim;
    private Collider coll;
    private SpawnController spwnControl;
    [SerializeField] Stamina StaminaBar;
    [SerializeField] GameObject Leg;

    // Start is called before the first frame update
    void Start()
    {
        myUtils = GetComponent<MyUtils>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider>();
        spwnControl = GameObject.FindWithTag("SpawnController").gameObject.GetComponent<SpawnController>();

        transform.position = spwnControl.NormalSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        // Dying

        if (stamina < 0f)
        {
            GameOver(stamina_death);
        }

        if (Hp <= 0)
        {
            GameOver(deathReason);
        }

        // Kicking
        if (Input.GetKeyDown(KeyCode.R) && !sitDown)
        {
            stamina -= 20f;
            StaminaBar.updateEnergy(stamina);

            //var leg = Instantiate(Leg, transform.position, Quaternion.identity);
            //leg.transform.eulerAngles = transform.Find("Camera").transform.eulerAngles;
            //Debug.Log(leg.transform.position);

            Kick(GetNearestInteractable(), kickStrength);
        }

        // Sitting
        if (Input.GetKeyDown(KeyCode.F))
        {
            SitInChair(GetNearestInteractable("Chair"));
        }

        // --DEBUG--
        if (Input.GetKeyDown(KeyCode.X))
        {
            transform.position = spwnControl.NormalSpawn();
        }

        // Taking damage from fire
        if (CheckForDanger("FirePoint"))
        {
            if (r_burn >= load_burn)
            {
                Hp -= 1;
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
                Hp -= 1;
                Debug.Log("Smoke, Hp: " + Hp);
                deathReason = smoke_death;
                r_asphyxiate = 0;
            }

            r_asphyxiate += Time.deltaTime;
        }
    }

    #region SittingDown / Chair Interactions
    // Toggles between sitting and getting up a chair
    void SitInChair(GameObject chair)
    {
        if (chair.GetComponent<Chair>().IsSittable)
        {
            Vector3 chairPos = chair.transform.position;
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

        Vector3 dir = (targetObj.transform.position - transform.position).normalized;
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

    // Game Restart
    public void GameOver(string reason)
    {
        Cursor.lockState = CursorLockMode.None;
        PlayerPrefs.SetString("DeathReason", reason);
        SceneManager.LoadScene("GameOver");
    }
}
