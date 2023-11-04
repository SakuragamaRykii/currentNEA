
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : Entity, IOverPrevention
{

    public static PlayerMovement instance; //make this a singleton
    private Rigidbody2D rb;
    [SerializeField] private GameObject invMenu;
    public static bool boosting;

    void Start()
    {
        boosting = false;
        if(!PlayerStat.levelThread.IsAlive) PlayerStat.levelThread.Start();

        if (Inventory.currentlyEquipped == null)
        {
            WeaponItem defaultSword = new WeaponItem("Sword", 100);
            Inventory.insert(defaultSword);
            Inventory.currentlyEquipped = defaultSword;

        }
        rb = GetComponent<Rigidbody2D>();
        setupHitbox();
        //invMenu.SetActive(false);
    }


    void Update()
    {
        PreventHPOver();
        ManageColEvent(CheckCollision());
        Move();
        ToggleInventory();

    }

    public void PreventHPOver()
    {
        if (PlayerStat.currentHP > PlayerStat.maxHP) PlayerStat.currentHP = PlayerStat.maxHP;
    }

    void ToggleInventory()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(invMenu.activeInHierarchy) invMenu.SetActive(false);
            else invMenu.SetActive(true);

        }
    }

    void ManageColEvent(GameObject[] type){
        //        Debug.Log(type);
        if (type != null)
        {
            if (hasTag(type, "Enemy"))
            {
                SceneManager.LoadScene("TBAndSelectionScene");
                Debug.Log("GONE");
                boosting = false;
            }

            if (hasTag(type, "Wall")) rb.velocity = -rb.velocity;

            if (hasTag(type, "XPUP")) PlayerStat.currentEXP += PlayerStat.expToNextLevel / 4;
        }

    }

    private void Move()
    {
        float hor = Input.GetAxis("Horizontal") * 15;
        float ver = Input.GetAxis("Vertical") * 15;
        Vector2 move = new Vector2(hor, ver);
        rb.velocity = move;
        Quaternion to = Quaternion.LookRotation(Vector3.forward, move);
        if (hor != 0 || ver != 0)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, to, 2);
    }
}
