

using UnityEngine.UIElements;

public class Checklist : UIElementTemplate
{
    VisualElement checklistContainer;
    Label checklistTitle;
    Row row1;
    Row row2;
    int collectibleCount = 0;


    protected override void deinitListeners()
    {
        GlobalEvents.onCollected -= incrementCollectible;
    }

    protected override void generateContent()
    {
        checklistContainer = Create("ChecklistRoot");
        checklistTitle = Create<Label>("Title");
        checklistTitle.text = "Checklist";

        if (StateManager.Instance == null)
        {
            row1 = Create<Row>("Row");
            row1.setText("Collect The Pieces 0/6");
            row2 = Create<Row>("Row");
            row2.setText("Put The Picture Together");
        }
        else if (StateManager.Instance.phase == 1)
        {
            row1 = Create<Row>("Row");
            row1.setText("Collect The Pieces 0/6");
            row2 = Create<Row>("Row");
            row2.setText("Put The Picture Together");
        }
        else
        {
            row1 = Create<Row>("Row");
            row1.setText("Put The Picture Together");
            row1.checkCompleted();
            row2 = Create<Row>("Row");
            row2.setText("Enter The Back Garden");
        }

        checklistContainer.Add(checklistTitle);
        checklistContainer.Add(row1);
        checklistContainer.Add(row2);

        root.Add(checklistContainer);
        GlobalEvents.onCollected += incrementCollectible;
    }

    public void incrementCollectible()
    {
        collectibleCount++;
        row1.setText("Collect The Pieces " + collectibleCount + "/6");
        if (collectibleCount == 6)
        {
            row1.checkCompleted();    
        }
    }
}