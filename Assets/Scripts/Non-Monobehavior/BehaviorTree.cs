
using System;
using System.Collections.Generic;
using UnityEngine;

// �⺻ ��� Ŭ����
public abstract class Node
{
    public abstract NodeState Tick();
}

// ���� ��� (Selector)
public class Selector : Node
{
    private List<Node> _nodes;

    public Selector(List<Node> nodes)
    {
        _nodes = nodes;
    }

    public override NodeState Tick()
    {
        foreach (var node in _nodes)
        {
            var state = node.Tick();
            if (state == NodeState.Success)
            {
                return NodeState.Success;
            }
        }
        return NodeState.Failure;
    }
}

// ���� ��� (Sequence)
public class Sequence : Node
{
    private List<Node> _nodes;

    public Sequence(List<Node> nodes)
    {
        _nodes = nodes;
    }

    public override NodeState Tick()
    {
        foreach (var node in _nodes)
        {
            var state = node.Tick();
            if (state == NodeState.Failure)
            {
                return NodeState.Failure;
            }
        }
        return NodeState.Success;
    }
}

// ���� ��� (Condition)
public class Condition : Node
{
    private Func<bool> _condition;

    public Condition(Func<bool> condition)
    {
        _condition = condition;
    }

    public override NodeState Tick()
    {
        return _condition() ? NodeState.Success : NodeState.Failure;
    }
}

// ���� ��� (ActionNode)
public class ActionNode : Node
{
    private Action _action;

    public ActionNode(Action action)
    {
        _action = action;
    }

    public override NodeState Tick()
    {
        _action();
        return NodeState.Success;
    }
}


// ��� ��� (WaitNode)
public class WaitNode : Node
{
    private float waitTime;
    private float startTime;
    private bool isWaiting;

    public WaitNode(float time)
    {
        waitTime = time;
        isWaiting = false;
    }

    public override NodeState Tick()
    {
        if (!isWaiting)
        {
            startTime = Time.time;
            isWaiting = true;
        }

        if (Time.time - startTime >= waitTime)
        {
            isWaiting = false; // Reset isWaiting for next Tick
            return NodeState.Success;
        }

        return NodeState.Running;
    }
}


// ��� ����
public enum NodeState
{
    Success,
    Failure,
    Running
}
