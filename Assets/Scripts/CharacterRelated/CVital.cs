using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CVital 
{
    private PrimaryVitals type;
    protected PrimaryVitals Type
    {
        get { return type; }
    }
    public float MaxValue { get; set; }
    public float MinValue { get; set; }

    public CVital()
    {

    }
    public CVital(PrimaryVitals type) : this()
    {
        this.type = type;
    }
}