using UnityEngine;

public class BlockView : MonoBehaviour
{
    [SerializeField] NodeType type;

    ScriptNode node;
    BlockView nextView;
    BlockView prevView;

    internal ScriptNode Node => node;
    internal BlockView NextView => nextView;


    private void Awake()
    {
        node = new ScriptNode(type);    
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

        prevView.nextView = null;
        prevView.node.SetOutput(FlowPort.Next, null);

        prevView = null;
    }
}
