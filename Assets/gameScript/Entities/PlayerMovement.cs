
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : Entity//, IPlayerControl
{
    private Rigidbody2D rb;
    public static GameObject invMenu;
    public static bool boosting;

    void Start()
    {
        boosting = false;
        if (!PlayerStat.levelThread.IsAlive) PlayerStat.levelThread.Start();

        if (Inventory.currentlyEquipped == null)
        {
            WeaponItem defaultSword = new WeaponItem("Sword", 100);
            Inventory.insert(defaultSword);
            Inventory.currentlyEquipped = defaultSword;

        }
        rb = GetComponent<Rigidbody2D>();
        setupHitbox();
    }

    
    public override void LoadData(GameData data)
    {
        PlayerStat.level = data.playerLevel;
        PlayerStat.currentEXP = data.playerCurrentXP;
    }
    public override void SaveData(ref GameData data)
    {
        data.playerLevel = PlayerStat.level;
        data.playerCurrentXP = PlayerStat.currentEXP;

    }

    private void Update()
    {
        CheckCollision();
        ToggleInventory();


    }

    void FixedUpdate()
    {
        Move();

    }



    void ToggleInventory()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(invMenu.activeInHierarchy) invMenu.SetActive(false);
            else invMenu.SetActive(true);
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, to, 30);
    }
}
