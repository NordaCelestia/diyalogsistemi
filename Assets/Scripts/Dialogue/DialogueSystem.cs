using UnityEngine;

// Bu s�n�f� ScriptableObject olarak tan�ml�yoruz, b�ylece Unity Editor'da
// diyalog verilerini kolayca olu�turabiliriz.
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue")]
public class Dialogue : ScriptableObject
{
    // Diyalog sat�rlar�n� temsil eden i� s�n�f
    [System.Serializable]
    public class DialogueLine
    {
        public string characterName; // Konu�an karakterin ad�
        [TextArea(3, 10)]
        public string line; // Karakterin s�yleyece�i replik
    }

    public DialogueLine[] lines; // Diyalog sat�rlar�n� i�eren dizi
}
