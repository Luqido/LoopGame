 using System;
 using UnityEngine;

 public class CombatManagerTester : MonoBehaviour
 {
     [SerializeField] private EnemyType[] types;
     
     private void Awake()
     {
         CombatManager.SetEnemiesToFightAgainst(types);
     }
 }
