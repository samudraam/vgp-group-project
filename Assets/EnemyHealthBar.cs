using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy{

public class EnemyHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider slider;

    public void UpdateHealthBar(float currVal, float maxVal){
        slider.value = currVal/maxVal;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
}
