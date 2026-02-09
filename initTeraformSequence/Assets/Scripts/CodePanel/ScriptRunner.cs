using UnityEngine;

class ScriptRunner : MonoBehaviour
{
    [SerializeField] BlockView nodeView;

    public void RunUI()
    {
        Run(nodeView.Node);
    }

    void Run(ScriptNode start)
    {
        ScriptNode current = start;

        while (current != null)
        {
            Execute(current);
            current = current.GetOutput(FlowPort.Next);
        }
    }

    void Execute(ScriptNode node)
    {
        switch (node.Type)
        {
            case NodeType.Start:
                break;

            case NodeType.Move:
                int steps = node.GetInt();
                Debug.Log("Move " + steps + " steps");
                break;

            case NodeType.Rotate:
                int angle = node.GetInt();
                Debug.Log("Rotate " + angle + " degrees");
                break;
        }
    }
}
