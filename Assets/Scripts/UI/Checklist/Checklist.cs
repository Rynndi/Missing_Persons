

using UnityEngine.UIElements;

public class Checklist : UIElementTemplate
{
    VisualElement checklistContainer;
    Label checklistTitle;
    Row row1;
    Row row2;
    Row row3;
    Row row4;


    protected override void deinitListeners()
    {
        
    }

    protected override void generateContent()
    {
        checklistContainer = Create("ChecklistRoot");
        checklistTitle = Create<Label>("Title");
        checklistTitle.text = "Checklist";
        row1 = Create<Row>("Row");
        row1.setText("Kill The Target");
        row2 = Create<Row>("Row");
        row2.setText("Clean The Blood");
        row3 = Create<Row>("Row");
        row3.setText("Hide The Body");
        row4 = Create<Row>("Row");
        row4.setText("Falsify Evidence");

        checklistContainer.Add(checklistTitle);
        checklistContainer.Add(row1);
        checklistContainer.Add(row2);
        checklistContainer.Add(row3);
        checklistContainer.Add(row4);

        root.Add(checklistContainer);
    }
}