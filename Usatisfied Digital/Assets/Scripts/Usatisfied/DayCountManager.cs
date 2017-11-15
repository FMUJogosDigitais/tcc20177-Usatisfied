using UnityEngine;

public class DayCountManager : MonoBehaviour {

    [SerializeField]
    Transform content;
    [SerializeField]
    GameObject dayCount;

    public Color[] daysColor;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        int days = GameManager.GetInstance().MaxDaysGame;
        if (days > 0)
        {
            for (int x = 0; x < days; x++)
            {
                GameObject go = Instantiate<GameObject>(dayCount, content);
                go.GetComponent<DayCountController>().SetColor(daysColor);
                go.GetComponent<DayCountController>().Init(x);
            }
        }
        
    }
}
