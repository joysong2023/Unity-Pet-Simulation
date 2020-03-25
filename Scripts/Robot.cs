using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Robot : MonoBehaviour
{ 
    [SerializeField]
    private int _hunger;
    [SerializeField]
    private int _happiness;
    [SerializeField]
    private string _name;

    private bool _serverTime;
    

    void Start()
    {
        PlayerPrefs.SetString("then", "12/23/2019 17:50:12");
        updateStatus();
        if (!PlayerPrefs.HasKey("name"))
            PlayerPrefs.SetString("name", "Robot");
        _name = PlayerPrefs.GetString("name");
    }

    void Update()
    {

        GetComponent<Animator>().SetBool("jump", gameObject.transform.position.y > -2.9f);

        if(Input.GetMouseButtonUp(0))
        {
            GetComponent<Rigidbody>().AddForce(new Vector2(0, 1000000));
        }
    }

    void updateStatus()
    {
        //search in all the keys that they have and it checks for hunger
        if(!PlayerPrefs.HasKey("_hunger"))
        {
            _hunger = 100;
            PlayerPrefs.SetInt("_hunger", _hunger);
        }
        else
        {
            _hunger = PlayerPrefs.GetInt("_hunger");
        }

        //search in all the keys that they have and it checks for happiness
        if (!PlayerPrefs.HasKey("_happiness"))
        {
            _happiness = 100;
            PlayerPrefs.SetInt("_happiness", _happiness);
        }
        else
        {
            _happiness = PlayerPrefs.GetInt("_happiness");
        }

        if (!PlayerPrefs.HasKey("then"))
            PlayerPrefs.SetString("then", getStringTime());

        TimeSpan ts = getTimeSpan();

        //when you don't play 2h the hunger decreases by 2
        _hunger -= (int)(ts.TotalHours * 2);
        if (_hunger < 0)
            _hunger = 0;

        //depends on the hunger, if that decreases with 2, then happiness decreases with 2 multiplied by hunger
        _happiness -= (int)((100 - _hunger) * (ts.TotalHours / 5));

        if (_happiness < 0)
            _happiness = 0;

        //Debug.Log(getTimeSpan().ToString());
        //Debug.Log(getTimeSpan().TotalHours);

        if (_serverTime)
            updateServer();
        else
            InvokeRepeating("updateDevice", 0f, 30f);
    }

    void updateServer()
    {

    }

    void updateDevice()
    {
        PlayerPrefs.SetString("then", getStringTime());
    }

    TimeSpan getTimeSpan()
    {
        if (_serverTime)
            return new TimeSpan();
        else
            return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("then"));
    }

    string getStringTime()
    {
        DateTime now = DateTime.Now;
        return now.Month + "/" + now.Day + "/" + now.Year + " " + now.Hour + ":" + now.Minute + ":" + now.Second;
    }

    public int hunger
    {
        get { return _hunger; }
        set { _hunger = value; }
    }

    public int happiness
    {
        get { return _happiness; }
        set { _happiness = value; }
    }

    public string name
    {
        get { return _name; }
        set { _name = value; }
    }

    public void saveRobot()
    {
        if (!_serverTime)
            updateDevice();
        PlayerPrefs.SetInt("_hunger", _hunger);
        PlayerPrefs.SetInt("_happiness", _happiness);
    }
}
