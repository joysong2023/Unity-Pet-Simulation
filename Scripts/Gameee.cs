using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameee : MonoBehaviour
{
    public GameObject happinessText;
    public GameObject hungerText;

    public GameObject namePanel;
    public GameObject nameInput;
    public GameObject nameText;

    public GameObject robot;

    void Update()
    {
        happinessText.GetComponent<Text>().text = "" + robot.GetComponent<Robot>().happiness;
        hungerText.GetComponent<Text>().text = "" + robot.GetComponent<Robot>().hunger;
        nameText.GetComponent<Text>().text = "" + robot.GetComponent<Robot>().name;
    }

    public void triggerNamePanel(bool b)
    {
        namePanel.SetActive(!namePanel.activeInHierarchy);

        if(b)
        {
            robot.GetComponent<Robot>().name = nameInput.GetComponent<InputField>().text;
            PlayerPrefs.SetString("name", robot.GetComponent<Robot>().name);
        }
    }

    public void buttonBehavior(int i)
    {
        switch(i)
        {
            case (0):
            default:

                break;
            case (1):
                break;
            case (2):
                break;
            case (3):
                break;
            case (4):
                robot.GetComponent<Robot>().saveRobot();
                Application.Quit();
                break;
        }
    }
}
