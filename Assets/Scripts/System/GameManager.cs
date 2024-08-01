using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    List<IUpdater> updaters = new List<IUpdater>();

    void Update()
    {
        foreach (var updater in updaters)
        {
            updater.Update();
        }
    }

    public void SubscribeUpdate(IUpdater updater)
    {
        if (!updaters.Contains(updater))
        {
            updaters.Add(updater);
        }
    }

    public void UnsubscribeUpdate(IUpdater updater)
    {
        if (updaters.Contains(updater))
        {
            updaters.Remove(updater);
        }
    }
}
