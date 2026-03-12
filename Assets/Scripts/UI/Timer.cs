using Unity.VisualScripting;
using UnityEngine.UIElements;

public class Timer : UIElementTemplate
{
    Label timer;
    protected override void deinitListeners()
    {
    }

    protected override void generateContent()
    {
        timer = Create<Label>("timer");
        root.Add(timer);
        timer.text = "temp text";
    }

    public void updateTimer(float time)
    {
        if (timer != null)
        {
            timer.text = time.ToString();
        }
    }
}