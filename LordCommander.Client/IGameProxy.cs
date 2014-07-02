﻿using System;
using System.Threading.Tasks;
using LordCommander.Shared;

namespace LordCommander.Client
{
    public interface IGameProxy
    {
        void Connect(LoginResult loginInfo);
        IObservable<string> PlayerJoinedQueue { get; }
        IObservable<string> PlayerLeftQueue { get; }
        IObservable<string> PlayerConnected { get; }
        IObservable<ConnectionStateInfo> StateChanged { get; }
        IObservable<PlayerDto> CurrentPlayerChanged { get; }
        IObservable<GameDto> GameStarted { get; }
        void SignIn();
        Task Queue();
        Task LeaveQueue();
    }
}