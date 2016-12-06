using UnityEngine;

/// <summary>
/// A simple module that requires the player to push the exactly button 5 times, but only
/// when the timer has a "1" in any position.
/// </summary>
public class ButtonMasherModule : MonoBehaviour
{
    public KMBombInfo BombInfo;
    public KMBombModule BombModule;
    public KMAudio KMAudio;
    public KMSelectable Button;
    public TextMesh Counter;

    protected int currentCount;

	protected void Start()
    {
        Button.OnInteract += HandlePress;
	}

    protected bool HandlePress()
    {
        KMAudio.PlaySoundAtTransform("tick", this.transform);

        string timerText = BombInfo.GetFormattedTime();

        if (currentCount < 5 && timerText.Contains("1"))
        {
            currentCount++;

            if (currentCount == 5)
            {
                BombModule.HandlePass();
            }
        }
        else
        {
            BombModule.HandleStrike();
        }

        Counter.text = currentCount.ToString();

        return false;
    }
}
