using UnityEngine;

namespace DemonSlayer.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterName characterName = null;
        [System.Serializable]
        class ProgressionCharacterName
        {
            [SerializeField] CharacterName characterName;
            [SerializeField] float[] health;
        }
    }
}