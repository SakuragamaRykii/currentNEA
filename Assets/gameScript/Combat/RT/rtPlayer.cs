using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rtPlayer : Entity
{
    public static rtPlayer instance; //make this a singleton
    private Rigidbody2D rb;
    public GameObject weapon;
    public float currentCd;
    public GameObject cam;
    
    void Start()
    {
        cam.GetComponent<cameraMove>().SetUp();
        if (instance == null) instance = this; else Destroy(gameObject);
        rb = GetComponent<Rigidbody2D>();
       // weapon = Inventory.currentlyEquipped.weapon;
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

    void FixedUpdate()
    {
        Move();
    }
    
    private void Update()
    {
        Inventory.Fist();
        weapon = Inventory.currentlyEquipped.weapon;
        Attack();
        ManageColEvent(CheckCollision());
    }

    void Attack()//GameObject to
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentCd <= 0)
        {
            Instantiate(weapon, transform.position, transform.rotation);
            currentCd = Inventory.currentlyEquipped.cd;
        }
        else currentCd -= Time.deltaTime;
    }
    void ManageColEvent(GameObject[] type)
    {
        DynList<GameObject> list = DynList<GameObject>.ToDList(type);
        Debug.Log(list);

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
