using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthController : MonoBehaviour
{
  public int maxBossHealth = 100;
  public int currentBossHealth;

  public BossHealth bossHealth;

    public static BossHealthController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
  {
    currentBossHealth = maxBossHealth;
    bossHealth.setBossMaxHealth(maxBossHealth);
    bossHealth.setBossHealth(maxBossHealth);
  }

  void Update()
  {
    if (Input.GetKeyDown("o")) {
      Debug.Log("O key was pressed.");
      DecreaseHealth(10);
    } else if (Input.GetKeyDown("p")) {
      Debug.Log("P key was pressed.");
      IncreaseHealth(10);
    }
  }

  public void DecreaseHealth(int value) {
    currentBossHealth -= value;
    if (currentBossHealth < 0) {
      currentBossHealth = 0;
    }
    bossHealth.setBossHealth(currentBossHealth);
  }

  public void IncreaseHealth(int value) {
    currentBossHealth += value;
    if (currentBossHealth > maxBossHealth) {
      currentBossHealth = maxBossHealth;
    }
    bossHealth.setBossHealth(currentBossHealth);
  }

}
