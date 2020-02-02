using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityLot
{
    protected Vector3 m_location;
    protected Vector3 m_size;
    protected Building m_building;
    public Building Building => m_building;

    public CityLot(Building building, Vector3 pos, Vector3 size)
    {
        m_building = building;
        m_location = pos;
        m_size = size;
    }
}

