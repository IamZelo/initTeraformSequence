using System.Collections.Generic;
using UnityEngine;

enum NodeType
{
    Start,
    Move,
    Rotate
}

enum FlowPort
{
    Next,
    Loop,
    LoopExit
}

class ScriptNode
{
    private NodeType type;
    private Dictionary<FlowPort, ScriptNode> outputs;

    internal ScriptNode(NodeType type)
    {
        this.type = type;
        outputs = new Dictionary<FlowPort, ScriptNode>();
    }

    internal NodeType Type => type;

    internal void SetOutput(FlowPort port, ScriptNode node)
    {
        outputs[port] = node;
    }

    internal ScriptNode GetOutput(FlowPort port)
    {
        return outputs.TryGetValue(port, out var n) ? n : null;
    }
}
