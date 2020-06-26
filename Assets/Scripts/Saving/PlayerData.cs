using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int currentHealth;
    public int maxHealth;
    public int currentTension;
    public int maxTension;
    public int currentBreath;
    public int maxBreath;
    public int playerLevel;
    public int currentStage;

    public PlayerData(Player player)
    {
        playerLevel = player.playerLevel;
        currentHealth = player.currentHealth;
        maxHealth = player.maxHealth;
        currentTension = player.currentTension;
        maxTension = player.maxTension;
        currentBreath = player.currentBreath;
        maxBreath = player.maxBreath;
        currentStage = player.currentStage;
    }

}
