using UnityEngine;

public class ForPoedit : MonoBehaviour
{

    private void ListTextForPoedt()
    {
        GetText("Choose your language!");
        GetText("English");
        GetText("Portuguese");
        GetText("Spanish");
        GetText("English (United States)");
        GetText("Portuguese (Brazil)");
        GetText("OK");
    }
    #region DUMMY FUNCTIONS
    private void GetText(string dummy) { }
    private void GetTextPlural(string dummy1, string dummy2, float intdummy) { }
    #endregion
}
