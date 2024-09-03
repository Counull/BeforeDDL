using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Authority;
using Best.HTTP.Shared;
using Best.HTTP.Shared.Logger;
using Grpc.Core;
using GRPC.NET;
using Grpc.Net.Client;
using Sirenix.OdinInspector;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Serialization;

public class GrpcManager : MonoBehaviour {
    [SerializeField] private string Url;
    [SerializeField] private ushort Prot = 50051;
    private GrpcChannel _channel;

    private AccountService.AccountServiceClient _accountServiceClient;

    private static readonly Metadata AuthMetadata = new Metadata
        {{"Authorization", "Bearer f623f5c2-46a8-4bdc-9ac5-45358a615a54"}};

    private void Awake() { }

    // Start is called before the first frame update
    void Start() {
        HTTPManager.Setup();

        //  HTTPManager.Logger.Level = Loglevels.All; // Enable all log levels
        var address = $"{Url}:{Prot}";
        _channel = GrpcChannel.ForAddress(address, new GrpcChannelOptions {
            HttpHandler = new GRPCBestHttpHandler()
        });

        _accountServiceClient = new AccountService.AccountServiceClient(_channel);
    }


    [Button]
    async void SendSmsRequest() {
        var async = _accountServiceClient.SendSmsCodeAsync(new SendSmsCodeRequest()
            {PhoneNumber = "14214423334", Purpose = SmsCodePurpose.Register});
        await async.ResponseHeadersAsync;
        await async.ResponseAsync;
        Debug.Log(async.ResponseAsync.Result.Success);
        Debug.Log(async.ResponseAsync.Result.Message);
    }

    [Button]
    async void SendRegisterRequest(uint smsCode) {
        var async = _accountServiceClient.RegisterAsync(new RegisterRequest()
            {PhoneNumber = "12444123313", Username = "Lxxr", Password = "aaweqQQSSW", Code = smsCode}
        );
        await async.ResponseHeadersAsync;
        await async.ResponseAsync;
        Debug.Log(async.ResponseHeadersAsync.Result.Count);
        Debug.Log(async.ResponseAsync.Result.Message);
    }


    // Update is called once per frame
    void Update() { }
}