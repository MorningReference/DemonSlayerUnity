using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DemonSlayer.Assets.Scripts.Saving
{
    public class SaveLoadPlayer : MonoBehaviour
    {
        [SerializeField] SaveSystem saveSystem;
        [SerializeField] Player player;
        public void SavePlayerState()
        {
            Debug.Log("** From SaveLoadPlayer Script Level is: " + player.currentStage);
            SaveSystem.SavePlayer(player);
        }
        public void LoadPlayerState()
        {
            PlayerData data = SaveSystem.LoadPlayer();
            player.playerLevel = data.playerLevel;
            player.currentHealth = data.currentHealth;
            player.maxHealth = data.maxHealth;
            player.currentTension = data.currentTension;
            player.maxTension = data.maxTension;
            player.currentBreath = data.currentBreath;
            player.maxBreath = data.maxBreath;
            player.currentStage = data.currentStage;
        }
        public void LoadSceneFromState()
        {
            PlayerData data = SaveSystem.LoadPlayer();
            SceneManager.LoadScene(data.currentStage);
        }
    }
}
