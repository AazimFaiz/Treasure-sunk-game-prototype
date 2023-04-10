using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    public Slider slider;

    // Update is called once per frame
    public void setHealth(int health)
    {
        slider.value = health;
    }

    public void setHealth(float health)
    {
        slider.value = health;
    }
}
