using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemonSlayer.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] string characterName;
        [SerializeField] Progression progression = null;

    }

}