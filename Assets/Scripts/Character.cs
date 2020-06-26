// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using DemonSlayer;

// namespace DemonSlayer.Combat
// {
//     public class Character : MonoBehaviour
//     {
//         [SerializeField] string Name { get; set; }
//         [SerializeField] int Level { get; set; }
//         [SerializeField] int Strength { get; set; }
//         [SerializeField] int Stamina { get; set; }
//         [SerializeField] int Endurance { get; set; }
//         [SerializeField] int Dexterity { get; set; }
//         [SerializeField] int Agility { get; set; }
//         [SerializeField] int Luck { get; set; }
//         private int _currentHP;
//         [SerializeField]
//         public int CurrentHP
//         {
//             get
//             {
//                 return _currentHP;
//             }
//             set
//             {

//             }
//         }
//         [SerializeField] int MaxHP { get; set; }
//         [SerializeField] int CurrentBreathCapacity { get; set; }
//         [SerializeField] int MaxBreathCapacity { get; set; }
//         [SerializeField] int CurrentTensionMeter { get; set; }
//         [SerializeField] float Attack { get; set; }
//         [SerializeField] float Defense { get; set; }
//         [SerializeField] float AttackSpeed { get; set; }
//         [SerializeField] float CriticalRate { get; set; }
//         [SerializeField] float CriticalDamage { get; set; }

//         public void AttackEnemy(Player enemy)
//         {
//             PlayerMovement playerMovement = new PlayerMovement();
//             playerMovement.isAttack = true;
//             int Damage = 0;
//             Damage = this.Strength + (int)this.Attack;
//             // enemy.Character.CurrentHP -= Damage;
//         }

//         void OnCollisionEnter2D(Collision2D _colInfo)
//         {
//             Player _player = _colInfo.collider.GetComponent<Player>();
//             if (_player != null)
//             {
//                 this.AttackEnemy(_player);
//             }
//         }

//     }

// }