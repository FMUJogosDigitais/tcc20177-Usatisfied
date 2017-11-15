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
        GetText("Play");
        GetText("Options");
        GetText("Option");
        GetText("Credit");
        GetText("Total days");
        GetText("Sleep");
        GetText("Feed");
        GetText("Career");
        GetText("Fun");
        GetText("Sports");
        GetText("Health");
        GetText("Schedule");
        GetText("Neighbors");
        GetText("Trafic");
        GetText("Laziness");
        GetText("Rain");
        GetText("Start whit Tutorial");
        GetText("Restart");
        GetText("Thank you for play!");

    }
    #region DUMMY FUNCTIONS
    private void GetText(string dummy) { }
    private void GetTextPlural(string dummy1, string dummy2, float intdummy) { }
    #endregion
}
