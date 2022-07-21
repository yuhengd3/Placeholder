using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
  public int maxEnemyHealth;
  public int currentEnemyHealth;

  public EnemyHealth enemyHealth;

  void Start()
  {
    currentEnemyHealth = maxEnemyHealth;
    enemyHealth.setEnemyHealth(maxEnemyHealth);
  }

  void Update()
  {
    if (Input.GetKeyDown("n")) {
      DecreaseEnemyHealth(1);
    } else if (Input.GetKeyDown("m")) {
      IncreaseEnemyHealth(1);
    }
  }

  void DecreaseEnemyHealth(int value) {
    currentEnemyHealth -= value;
    if (currentEnemyHealth < 0) {
      currentEnemyHealth = 0;
    }
    enemyHealth.setEnemyHealth(currentEnemyHealth);
  }

  void IncreaseEnemyHealth(int value) {
    currentEnemyHealth += value;
    if (currentEnemyHealth > maxEnemyHealth) {
      currentEnemyHealth = maxEnemyHealth;
    }
    enemyHealth.setEnemyHealth(currentEnemyHealth);
  }

}
