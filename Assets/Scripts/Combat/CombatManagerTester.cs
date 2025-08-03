 using System;
 using System.Collections.Generic;
 using UnityEngine;

 public class CombatManagerTester : MonoBehaviour
 {
     public static List<EnemyType> enemies;
     
     
     [SerializeField] private EnemyType[] types;
     
     private void Awake()
     {
         CombatManager.SetEnemiesToFightAgainst(types);
         // CombatManager.SetEnemiesToFightAgainst(EnemyType.NormalAdam, EnemyType.Tazi);


         foreach (var enemy in enemies)
         {
             
         }
     }
 }
