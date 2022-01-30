using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UserResources : MonoBehaviour
{
    [Header("Building")]
    public Resource Wax;
    public Resource Resin;
    public Resource Propolis;
    [Header("Disposable")]
    public Resource Nectar;
    public Resource Power;
    public Resource Water;

    [Header("Supporting")]
    public Resource Honey;
    public Resource Amber;

    [Header("Statistic")]
    public Resource Experience;
    public Resource BaseLevel;

    [HideInInspector] public List<Resource> resources = new List<Resource>();
    public Buyer buyer;
    private void Start()
    {
        buyer = GetComponent<Buyer>();
        buyer.userResources = this;

        SetResourcesToList();

        RecoveryResources();

        if (PlayerPrefs.GetInt("GameStarted") == 0)
        {
            SetStartValues();
        }
    }
    private void SetResourcesToList()
    {
        resources.Add(Wax);
        resources.Add(Resin);
        resources.Add(Propolis);
        resources.Add(Nectar);
        resources.Add(Power);
        resources.Add(Water);
        resources.Add(Honey);
        resources.Add(Amber);
        resources.Add(Experience);
        resources.Add(BaseLevel);
    }
    private void RecoveryResources() 
    {
        foreach(Resource res in resources)
        {
            res.RecoveryValue();
        }
    }
    private void SetStartValues()
    {
        Wax.SetValue(15000);
        Resin.SetValue(15000);
        Propolis.SetValue(15000);
            
        Nectar.SetValue(15000);
        Power.SetValue(15000);
        Water.SetValue(15000);

        Honey.SetValue(3000);

        BaseLevel.SetValue(1);
        PlayerPrefs.SetInt("GameStarted", 1);
    }

}
[System.Serializable]
public class Resource
{
    public string name;
    public Text uiText;
    public int value { get; private set; }

    public void SetValue(int Value)
    {
        value += Value;
        value = value > 0 ? value : 0;
        PlayerPrefs.SetInt(name, value);
        uiText.text = value.ToString();
    }
    public void RecoveryValue()
    {
        value = PlayerPrefs.GetInt(name, value);
        uiText.text = value.ToString();
    }
}
