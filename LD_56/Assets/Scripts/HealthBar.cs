using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image imgHealthBar;

    public void HealthBarDown(int damage)
    {
        imgHealthBar.fillAmount = imgHealthBar.fillAmount - (damage * 0.01f);
    }

    public void HealthBarUp(int heal)
    {
        imgHealthBar.fillAmount = imgHealthBar.fillAmount + (heal * 0.01f);
    }

    public void HealthBarReset()
    {
        imgHealthBar.fillAmount = 1;
    }

    // Yes this script is a terrible way of doing it but it should work
}
