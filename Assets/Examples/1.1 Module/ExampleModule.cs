using UnityEngine;

public class ExampleModule : MonoBehaviour
{
    public KMSelectable[] buttons;

    int correctIndex;
    bool isActivated = false;

    void Start()
    {
        Init();

        GetComponent<KMBombModule>().OnActivate += ActivateModule;
    }

    void Init()
    {	
		correctIndex =3 ;
//        correctIndex = Random.Range(0, 4);
		string[] label = new string[4] {"☉", "Ƹ", "सं", "Ψ"};

        for(int i = 0; i < buttons.Length; i++)
        {
//            string label = i == correctIndex ? "@" : "X";

            TextMesh buttonText = buttons[i].GetComponentInChildren<TextMesh>();
            buttonText.text = label[i];
            int j = i;
            buttons[i].OnInteract += delegate () { OnPress(j == correctIndex); return false; };
        }
    }

    void ActivateModule()
    {
        isActivated = true;
    }

    void OnPress(bool correctButton)
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch();

        if (!isActivated)
        {
            Debug.Log("Pressed button before module has been activated!");
            GetComponent<KMBombModule>().HandleStrike();
        }
        else
        {
            Debug.Log("Pressed " + correctButton + " button");
            if (correctButton)
            {
                GetComponent<KMBombModule>().HandlePass();
            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
            }
        }
    }
}
