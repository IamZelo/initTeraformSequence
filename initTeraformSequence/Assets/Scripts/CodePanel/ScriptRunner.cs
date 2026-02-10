using System.Collections;
using UnityEngine;

public class ScriptRunner : MonoBehaviour
{
    [SerializeField] BlockView nodeView;
    [SerializeField] Robot rb;

    public void RunUI()
    {
        StopAllCoroutines();
        StartCoroutine(Run(nodeView.Node));
    }

    IEnumerator Run(ScriptNode start)
    {
        ScriptNode current = start;

        while (current != null)
        {
            yield return Execute(current);
            current = current.GetOutput(FlowPort.Next);
        }
    }

    IEnumerator Execute(ScriptNode node)
    {
        switch (node.Type)
        {
            case NodeType.Start:
                yield break;

            case NodeType.Move:
                {
                    int steps = node.GetInt();
                    Debug.Log("Move " + steps + " steps");
                    yield return rb.Move(steps);
                    break;
                }

            case NodeType.Rotate:
                {
                    int angle = node.GetInt();
                    Debug.Log("Rotate " + angle + " degrees");
                    yield return rb.Turn(angle);
                    break;
                }
        }
    }
}
