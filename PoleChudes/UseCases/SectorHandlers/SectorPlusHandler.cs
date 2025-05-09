﻿using PoleChudes.Domain.Entities;
using PoleChudes.Domain.Interfaces;

namespace PoleChudes.UseCases.SectorHandlers;

public class SectorPlusHandler : ISectorHandler
{
    private PresenterManager _presenterManager;

    public SectorPlusHandler(PresenterManager presenterManager)
    {
        _presenterManager = presenterManager;
    }

    public async void Handle()
    {
        _presenterManager.SetMessage("SectorPlus");
        await Task.Delay(1500);
        _presenterManager.SetMessage(string.Empty);
    }
}
