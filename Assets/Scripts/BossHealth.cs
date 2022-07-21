using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{

	public Slider slider;

	public void setBossMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;
	}

  public void setBossHealth(int health)
	{
		slider.value = health;
	}

}
