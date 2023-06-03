using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TIme_Control_Script : MonoBehaviour
{

    public GameObject timecontrolpanel;
    public GameObject time;
    public GameObject playButton;
    public Text White_Time;
    public Text Black_Time;
    public float zero = 0;
    public float zero2 = 0;
    public float zero3 = 0;
    public float zero_black = 0;
    public float zero2_black = 0;
    public float zero3_black = 0;
    public string colon = ":";
    public float clock = 0;
    public float clock_black = 0;
    public float starting_time;
    public bool gamestarted = false;
    public GameObject LogicManager;
    public Logic_Management_Script logic_manager;
    public bool timestart = false;
    public bool increment_by_2 = false;
    public bool increment_by_1 = false;


    void Start()
    {

    }

    void Awake()
    {
        logic_manager = LogicManager.GetComponent<Logic_Management_Script>();
    }

    public void displaytime()
    {
        White_Time.text = zero.ToString() + zero2.ToString() + colon + zero3.ToString() + clock.ToString("0.00");
        Black_Time.text = zero_black.ToString("0") + zero2_black.ToString("0") + colon + zero3_black.ToString("0") + clock_black.ToString("0.00");
    }

    public void play()
    {
        timecontrolpanel.SetActive(false);
        gamestarted = true;
        time.SetActive(true);
    }

    public void fifteen()
    {
        starting_time = 15 * 60;
        zero = 1;
        zero2 = 5;
        zero3 = 0;
        zero_black = 1;
        zero2_black = 5;
        zero3_black = 0;
        playButton.SetActive(true);
        displaytime();
    }

    public void ten()
    {
        starting_time = 10 * 60;
        zero = 1;
        zero2 = 0;
        zero3 = 0;
        zero_black = 1;
        zero2_black = 0;
        zero3_black = 0;
        playButton.SetActive(true);
        displaytime();
    }

    public void five()
    {
        starting_time = 5 * 60;
        zero = 0;
        zero2 = 5;
        zero3 = 0;
        zero_black = 0;
        zero2_black = 5;
        zero3_black = 0;
        playButton.SetActive(true);
        displaytime();
    }

    public void five_inc_2()
    {
        starting_time = 5 * 60;
        zero = 0;
        zero2 = 5;
        zero3 = 0;
        zero_black = 0;
        zero2_black = 5;
        zero3_black = 0;
        increment_by_2 = true;
        playButton.SetActive(true);
        displaytime();
    }

    public void three()
    {
        starting_time = 2 * 60;
        zero = 0;
        zero2 = 3;
        zero3 = 0;
        zero_black = 0;
        zero2_black = 3;
        zero3_black = 0;
        playButton.SetActive(true);
        displaytime();
    }

    public void three_inc_one()
    {
        starting_time = 2 * 60;
        zero = 0;
        zero2 = 3;
        zero3 = 0;
        zero_black = 0;
        zero2_black = 3;
        zero3_black = 0;
        increment_by_1 = true;
        playButton.SetActive(true);
        displaytime();
    }

    public void time_spent()
    {
           if (clock <= 0)
            {
                zero3 -= 1;
                clock = 9.99f;
            }
            if (zero3 < 0)
            {
                zero2 -= 1;
                zero3 = 5;
            }
            if(clock >= 10f)
            {
                clock = clock - 10;
                zero3 += 1;
            }
            if(zero3>=6)
            {
                zero3 = 0;
                zero2 += 1;
            }
            if(zero2 >=10)
            {
                zero2 = 0;
                zero++;
            }
            if (zero2 < 0)
            {
                zero -= 1;
                zero2 = 9;
            }
            
            if (clock_black <= 0)
            {
                zero3_black -= 1;
                clock_black = 9.99f;
            }
            if (clock_black >= 10f)
            {
                clock_black = clock_black - 10;
                zero3_black += 1;
            }
            if (zero3_black >= 6)
            {
                zero3_black = 0;
                zero2_black += 1;
            }
            if (zero2_black >= 10)
            {
                zero2_black = 0;
                zero_black++;
            }
            if (zero3_black < 0)
            {
                zero2_black -= 1;
                zero3_black = 5;
            }
            if (zero2_black < 0)
            {
                zero_black -= 1;
                zero2_black = 9;
            }
            //displaytime();

        if (logic_manager.White_Turn)
        {
            clock -= Time.deltaTime * 1;
            White_Time.text = zero.ToString() + zero2.ToString() + colon + zero3.ToString() + clock.ToString("0.00");
        }
        else
        {
            clock_black -= Time.deltaTime * 1;
            Black_Time.text = zero_black.ToString("0") + zero2_black.ToString("0") + colon + zero3_black.ToString("0") + clock_black.ToString("0.00");
        }

    }

    void Update()
    {
        if(gamestarted && timestart)
        time_spent();
    }
}
