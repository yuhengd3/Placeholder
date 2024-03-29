using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
  public int maxHealth;
  public int currentHealth;

  public Health health;

  void Start()
  {
    currentHealth = maxHealth;
    health.setHealth(maxHealth);
  }

  void Update()
  {
    if (Input.GetKeyDown("k")) {
      DecreaseHealth(1);
    } else if (Input.GetKeyDown("l")) {
      IncreaseHealth(1);
    }
  }

  public void DecreaseHealth(int value) {
    currentHealth -= value;
    if (currentHealth < 0) {
      currentHealth = 0;
    }
    health.setHealth(currentHealth);
  }

  public void IncreaseHealth(int value) {
    currentHealth += value;
    if (currentHealth > maxHealth) {
      currentHealth = maxHealth;
    }
    health.setHealth(currentHealth);
  }

}
