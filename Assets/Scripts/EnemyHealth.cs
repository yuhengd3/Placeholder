using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
  public int enemyHealth;

  public Image[] healthIcons;
  public Sprite fullHealthSprite;
  public Sprite emptyHealthSprite;

  public void setEnemyHealth (int newHealth) {
    enemyHealth = newHealth;
  }

  void Update()
  {
    for (int i = 0; i < healthIcons.Length; i++) {
      if (i < enemyHealth) {
        healthIcons[i].sprite = fullHealthSprite;
      } else {
        healthIcons[i].sprite = emptyHealthSprite;
      }
    }
  }

}
