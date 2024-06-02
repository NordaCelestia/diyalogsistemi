using TMPro;
using System.Collections;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // Karakterlerin adlar�n� g�sterecek TextMeshPro UI elemanlar�
    public TextMeshProUGUI character1Name;
    public TextMeshProUGUI character2Name;

    // Karakterlerin konu�ma metinlerini g�sterecek TextMeshPro UI elemanlar�
    public TextMeshProUGUI character1Text;
    public TextMeshProUGUI character2Text;

    // Diyalog verilerini saklayan ScriptableObject
    public Dialogue dialogue;

    // �u anki diyalog sat�r�n�n indeksini tutar
    private int currentLine = 0;
    private bool isTyping = false; // Yazma i�leminin devam edip etmedi�ini kontrol eder
    private bool isWaitingForNext = false; // Kullan�c�n�n sonraki sat�ra ge�mesini bekler

    // Ba�lang��ta diyalog g�stermek i�in coroutine'i ba�lat�r
    void Start()
    {
        StartCoroutine(DisplayDialogue());
    }

    void Update()
    {
        // Kullan�c� Space tu�una bast���nda ve yazma i�lemi tamamland���nda bir sonraki sat�ra ge�er
        if (Input.GetKeyDown(KeyCode.Space) && !isTyping && isWaitingForNext)
        {
            currentLine++;
            if (currentLine < dialogue.lines.Length)
            {
                StartCoroutine(DisplayDialogueLine(dialogue.lines[currentLine]));
            }
            isWaitingForNext = false; // Bir sonraki sat�ra ge�ildi�inde bekleme durumu s�f�rlan�r
        }
    }

    // Diyaloglar� s�rayla ve animasyonlu �ekilde g�sterecek coroutine
    IEnumerator DisplayDialogue()
    {
        yield return StartCoroutine(DisplayDialogueLine(dialogue.lines[currentLine]));
    }

    // Belirli bir diyalog sat�r�n� g�stermek i�in coroutine
    IEnumerator DisplayDialogueLine(Dialogue.DialogueLine line)
    {
        isTyping = true; // Yazma i�leminin ba�lad���n� belirtir

        // Hangi karakterin konu�tu�unu belirler ve ad�n� ve metnini ayarlar
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

        isTyping = false; // Yazma i�leminin bitti�ini belirtir
        isWaitingForNext = true; // Kullan�c�n�n sonraki sat�ra ge�mesini bekler
    }

    // Harf harf animasyonlu yaz� yazma coroutine'i
    IEnumerator TypeSentence(TextMeshProUGUI textComponent, string sentence)
    {
        textComponent.text = ""; // Text eleman�n� temizler
        foreach (char letter in sentence.ToCharArray())
        {
            textComponent.text += letter; // Harf harf ekler
            yield return new WaitForSeconds(0.05f); // Harfler aras� bekleme s�resi
        }
    }
}
