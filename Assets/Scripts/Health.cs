using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

  public int health;

  public Image[] healthIcons;
  public Sprite fullHealthSprite;
  public Sprite emptyHealthSprite;

  public void setHealth (int newHealth) {
    health = newHealth;
  }

  void Update()
  {
    for (int i = 0; i < healthIcons.Length; i++) {
      if (i < health) {
        healthIcons[i].sprite = fullHealthSprite;
      } else {
        healthIcons[i].sprite = emptyHealthSprite;
      }
    }
  }

}
