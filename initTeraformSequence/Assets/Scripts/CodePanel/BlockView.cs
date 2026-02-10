using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BlockView : MonoBehaviour
{
    [SerializeField] NodeType type;
    [SerializeField] int energyCost;
    [SerializeField] TMP_InputField inputField;

    ScriptNode node;
    BlockView nextView;
    BlockView prevView;
    ItemSlot slot;

    internal ScriptNode Node => node;
    internal BlockView NextView => nextView;
    internal BlockView PrevView => prevView;


    private void Awake()
    {
        InitializeBlock();
    }

    internal void InitializeBlock()
    {
        node = new ScriptNode(type, energyCost);
        slot = GetComponentInChildren<ItemSlot>();
    }

    void Start()
    {
        if (inputField != null)
            inputField.onEndEdit.AddListener(OnValueChanged);
    }

    void OnValueChanged(string value)
    {
        if (int.TryParse(value, out int v))
            node.SetInt(v);
    }
    internal void LinkNext(BlockView next)
    {
        next.Unlink();  //Prevents multiple parents

        nextView = next;
        next.prevView = this;
        node.SetOutput(FlowPort.Next, next.node);
    }

    internal void Unlink()
    {
        if (prevView == null) return;
        prevView.slot.CanvasGroup.blocksRaycasts = true;
        prevView.nextView = null;
        prevView.node.SetOutput(FlowPort.Next, null);

        prevView = null;
    }
}
