using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MyCustomEditor : EditorWindow
{
    [MenuItem("Window/UI Toolkit/MyCustomEditor")]
    public static void ShowExample()
    {
        MyCustomEditor wnd = GetWindow<MyCustomEditor>();
        wnd.titleContent = new GUIContent("MyCustomEditor");
    }

    [SerializeField]
    private VisualTreeAsset m_UXMLTree;

    // The VisualTreeAsset is a template for a visual tree. It is a serialized representation of a visual tree.
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    private int m_ClickCount = 0;

    private const string m_ButtonPrevix = "Button";

    

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        Label label = new Label("These controls were created using C# code.");
        root.Add(label);

        Button button = new Button();
        button.name = "button3";
        button.text = "This is button3.";
        root.Add(button);

        Toggle toggle = new Toggle();
        toggle.name = "toggle3";
        toggle.label = "Number?";
        root.Add(toggle);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/MyCustomEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        root.Add(m_UXMLTree.Instantiate());

        //Call the event handler
        SetupButtonHandler();
    }

    private void SetupButtonHandler()
    {
        VisualElement root = rootVisualElement;

        // Gets alist of all the buttons in the hierarchy (root UI)
        var buttons = root.Query<Button>();
        // Call RegisterHandler for each button
        buttons.ForEach(RegisterHandler);
    }

    private void RegisterHandler(Button button)
    {
        // When a button is clicked, the PrintClickMessage method is called
        button.RegisterCallback<ClickEvent>(PrintClickMessage);
    }


    private void PrintClickMessage(ClickEvent evt)
    {

        VisualElement root = rootVisualElement;

        ++m_ClickCount;

        // Get the button that was clicked
        Button button = evt.currentTarget as Button;
        // Extract the number from the button name
        string buttonNumber = button.name.Substring(m_ButtonPrevix.Length);
        string toggleName = "toggle" + buttonNumber;
        // Find the toggle with the corresponding number
        Toggle toggle = root.Q<Toggle>(toggleName);

        Debug.print("Button was clicked!" + (toggle.value ? " Count: " + m_ClickCount : ""));
        
    }


}
