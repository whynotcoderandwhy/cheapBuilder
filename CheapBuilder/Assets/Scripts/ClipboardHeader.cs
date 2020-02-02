using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ClipboardHeader : MonoBehaviour
{

    [SerializeField]
    protected Text m_HouseWorth;
    [SerializeField]
    protected Text m_HouseQuality;
    [SerializeField]
    protected Text m_Reward;
    [SerializeField]
    protected Text m_DueDate;

    public bool SetHeader(float worth, float quality, float reward, int duedate)
    {
        m_HouseWorth.text = worth.ToString();
        m_HouseQuality.text = quality.ToString();
        m_Reward.text = reward.ToString();
        m_DueDate.text = duedate.ToString();
        return true;
    }

}
