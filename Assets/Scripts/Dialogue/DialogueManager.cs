using TMPro;
using System.Collections;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // Karakterlerin adlarýný gösterecek TextMeshPro UI elemanlarý
    public TextMeshProUGUI character1Name;
    public TextMeshProUGUI character2Name;

    // Karakterlerin konuþma metinlerini gösterecek TextMeshPro UI elemanlarý
    public TextMeshProUGUI character1Text;
    public TextMeshProUGUI character2Text;

    // Diyalog verilerini saklayan ScriptableObject
    public Dialogue dialogue;

    // Þu anki diyalog satýrýnýn indeksini tutar
    private int currentLine = 0;
    private bool isTyping = false; // Yazma iþleminin devam edip etmediðini kontrol eder
    private bool isWaitingForNext = false; // Kullanýcýnýn sonraki satýra geçmesini bekler

    // Baþlangýçta diyalog göstermek için coroutine'i baþlatýr
    void Start()
    {
        StartCoroutine(DisplayDialogue());
    }

    void Update()
    {
        // Kullanýcý Space tuþuna bastýðýnda ve yazma iþlemi tamamlandýðýnda bir sonraki satýra geçer
        if (Input.GetKeyDown(KeyCode.Space) && !isTyping && isWaitingForNext)
        {
            currentLine++;
            if (currentLine < dialogue.lines.Length)
            {
                StartCoroutine(DisplayDialogueLine(dialogue.lines[currentLine]));
            }
            isWaitingForNext = false; // Bir sonraki satýra geçildiðinde bekleme durumu sýfýrlanýr
        }
    }

    // Diyaloglarý sýrayla ve animasyonlu þekilde gösterecek coroutine
    IEnumerator DisplayDialogue()
    {
        yield return StartCoroutine(DisplayDialogueLine(dialogue.lines[currentLine]));
    }

    // Belirli bir diyalog satýrýný göstermek için coroutine
    IEnumerator DisplayDialogueLine(Dialogue.DialogueLine line)
    {
        isTyping = true; // Yazma iþleminin baþladýðýný belirtir

        // Hangi karakterin konuþtuðunu belirler ve adýný ve metnini ayarlar
        if (line.characterName == "Character1")
        {
            character1Name.text = line.characterName;
            yield return StartCoroutine(TypeSentence(character1Text, line.line));
        }
        else
        {
            character2Name.text = line.characterName;
            yield return StartCoroutine(TypeSentence(character2Text, line.line));
        }

        isTyping = false; // Yazma iþleminin bittiðini belirtir
        isWaitingForNext = true; // Kullanýcýnýn sonraki satýra geçmesini bekler
    }

    // Harf harf animasyonlu yazý yazma coroutine'i
    IEnumerator TypeSentence(TextMeshProUGUI textComponent, string sentence)
    {
        textComponent.text = ""; // Text elemanýný temizler
        foreach (char letter in sentence.ToCharArray())
        {
            textComponent.text += letter; // Harf harf ekler
            yield return new WaitForSeconds(0.05f); // Harfler arasý bekleme süresi
        }
    }
}
