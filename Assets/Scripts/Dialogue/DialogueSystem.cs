using UnityEngine;

// Bu sýnýfý ScriptableObject olarak tanýmlýyoruz, böylece Unity Editor'da
// diyalog verilerini kolayca oluþturabiliriz.
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue")]
public class Dialogue : ScriptableObject
{
    // Diyalog satýrlarýný temsil eden iç sýnýf
    [System.Serializable]
    public class DialogueLine
    {
        public string characterName; // Konuþan karakterin adý
        [TextArea(3, 10)]
        public string line; // Karakterin söyleyeceði replik
    }

    public DialogueLine[] lines; // Diyalog satýrlarýný içeren dizi
}
