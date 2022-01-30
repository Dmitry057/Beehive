using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionButton : MonoBehaviour
{
    public Construction Construction;

    private PlantConstructions plantConstructions;
    private void Start()
    {
        plantConstructions = FindObjectOfType<PlantConstructions>();
    }
    public void ButtonFunction()
    {
        plantConstructions.TryToPlant(Construction);
    }
}