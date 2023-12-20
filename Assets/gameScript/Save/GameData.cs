using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int playerLevel;
    public float playerCurrentXP;
    public int worldLevel, oldLevel;
    public float playerHP;

    public Vector3 fieldPlayerPosition;
    public SerializableDictionary<Vector2, bool> enemyFieldData; //bool value determines whether the object is active
    public SerializableDictionary<Vector2, bool> interactablesFieldData; //false if destroyed, otherwise true.
    public SerializableDictionary<Vector2, bool> wallFieldData;
    public List<string> specificInteractables;//keep null

    public GameData()
    {
        //-------------------------WorldLevelSpawner----------------------------------------------------------------------------------------------------------------

        worldLevel = 1; 
        wallFieldData = new SerializableDictionary<Vector2, bool>();
        interactablesFieldData = new SerializableDictionary<Vector2, bool>();
        enemyFieldData = new SerializableDictionary<Vector2, bool>();
        fieldPlayerPosition = Vector3.zero - Vector3.forward * 5;
        specificInteractables = new List<string>();

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //-------------------------Player----------------------------------------------------------------------------------------------------------------

        playerLevel = 1; playerCurrentXP = 0; playerHP = PlayerStat.maxHP; 

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    }
}
