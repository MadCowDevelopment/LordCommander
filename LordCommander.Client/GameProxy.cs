﻿using System;
using System.Net;
using System.Reactive.Subjects;
using LordCommander.Shared;
using Microsoft.AspNet.SignalR.Client;

namespace LordCommander.Client
{
    public class GameProxy
    {
        private IHubProxy _proxy;

        private readonly Subject<string> _playerJoinedQueue = new Subject<string>();
        private readonly Subject<string> _playerLeftQueue = new Subject<string>();
        private readonly Subject<string> _playerConnected = new Subject<string>();
        private readonly Subject<PlayerDto> _currentPlayerChanged = new Subject<PlayerDto>();
        private readonly Subject<GameDto> _gameStarted = new Subject<GameDto>();

        private readonly Subject<ConnectionStateInfo> _stateChanged = new Subject<ConnectionStateInfo>();
        private ConnectionStateInfo _state;

        public void Connect(LoginResult loginInfo)
        {
            var connection = new HubConnection(ServerConstants.Base);
            connection.StateChanged += connection_StateChanged;
            connection.CookieContainer = new CookieContainer();
            connection.CookieContainer.Add(loginInfo.AuthCookie);
            connection.Headers.Add("myauthtoken", loginInfo.AccessToken);

            _proxy = connection.CreateHubProxy("GameHub");
            _proxy.On<string>("PlayerJoinedQueue", p => _playerJoinedQueue.OnNext(p));
            _proxy.On<string>("PlayerLeftQueue", p => _playerLeftQueue.OnNext(p));
            _proxy.On<string>("PlayerConnected", p => _playerConnected.OnNext(p));
            _proxy.On<GameDto>("GameStarted", p => _gameStarted.OnNext(p));
            _proxy.On<PlayerDto>("CurrentPlayerChanged", p => _currentPlayerChanged.OnNext(p));

            connection.Start().Wait();
        }

        private ConnectionStateInfo State
        {
            get { return _state; }
            set
            {
                _state = value;
                _stateChanged.OnNext(_state);
            }
        }

        public IObservable<string> PlayerJoinedQueue { get { return _playerJoinedQueue; } }

        public IObservable<string> PlayerLeftQueue { get { return _playerLeftQueue; } }

        public IObservable<string> PlayerConnected { get { return _playerConnected; } }

        public IObservable<ConnectionStateInfo> StateChanged { get { return _stateChanged; } }

        public IObservable<PlayerDto> CurrentPlayerChanged { get { return _currentPlayerChanged; } }

        public IObservable<GameDto> GameStarted { get { return _gameStarted; } } 

        public void SignIn()
        {
            _proxy.Invoke("SignIn", Environment.MachineName);
        }

        public void Queue()
        {
            _proxy.Invoke("Queue");
        }

        public void LeaveQueue()
        {
            _proxy.Invoke("LeaveQueue");
        }

        private void connection_StateChanged(StateChange stateChange)
        {
            switch (stateChange.NewState)
            {
                case ConnectionState.Connected:
                    State = ConnectionStateInfo.Connected;
                    break;
                default:
                    State = ConnectionStateInfo.Disconnected;
                    break;
            }
        }
    }

    public enum ConnectionStateInfo
    {
        Connected,
        Disconnected
    }
}
