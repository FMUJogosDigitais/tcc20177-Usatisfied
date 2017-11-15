using UnityEngine;
using UnityEngine.UI;
public class DayCountController : MonoBehaviour {

    [SerializeField]
    int myDayref;

    Color activeColor;
    Color disableColor;
    Color pastColor;


    private void OnEnable()
    {
        Init(myDayref);
    }

    public void SetColor(Color[] setcolor)
    {
        activeColor = setcolor[0];
        pastColor = setcolor[1];
        disableColor = setcolor[2];
    }

    public void Init(int myref)
    {
        myDayref = myref;
        if (GameManager.GetInstance().TotalDay == myDayref)
        {
            GetComponent<Image>().color = activeColor;
        }else if (GameManager.GetInstance().TotalDay > myDayref)
        {
            GetComponent<Image>().color = pastColor;
        }
        else
        {
            GetComponent<Image>().color = disableColor;
        }

    }
}
