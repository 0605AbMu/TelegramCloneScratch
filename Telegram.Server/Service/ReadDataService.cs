using Telegram.Client;
using Telegram.Server.Models;
using TelegramChat.Domain;
using TelegramChat.Service.Interface;
using TelegramClient.Auth.Auth;
using TelegramClient.Auth.Domain;

namespace Telegram.Server.Service;

public class ReadDataService : IReadDataService
{
    private readonly SaveDataModels<IChatService> _chatServiceSaveData;
    private readonly SaveDataModels<IClientService> _clientServiceSaveData;
    private readonly SaveDataModels<IMessageService> _messageServiceSaveData;
    private readonly IServiceBase _serviceBase;
    private readonly SaveDataModels<IUserService> _userServiceSaveData;


    public ReadDataService(
        //Bu save data model telegram.Client dagi user lar uchun
        SaveDataModels<IClientService> clientServiceSaveData,
        //Bu save data model Telegram.Auth dagi user lar uchun
        SaveDataModels<IUserService> userServiceSaveData,
        //Bu save data model Telegram.Chat dagi user lar uchun
        SaveDataModels<IChatService> chatServiceSaveData,
        //Bu save data model Telegram.Chat dagi message lar uchun
        SaveDataModels<IMessageService> messageServiceSaveData,
        IServiceBase serviceBase)
    {
        _clientServiceSaveData = clientServiceSaveData;
        _userServiceSaveData = userServiceSaveData;
        _chatServiceSaveData = chatServiceSaveData;
        _messageServiceSaveData = messageServiceSaveData;
        _serviceBase = serviceBase;
    }

    public void LoadUserServiceData()
    {
        _userServiceSaveData.Service.AddRange(_serviceBase.Read<User>(_userServiceSaveData.FilePath));
    }

    public void LoadClientServiceData()
    {
        _clientServiceSaveData.Service.SetClientsList(
            _serviceBase.Read<Client.Domain.Client>(_clientServiceSaveData.FilePath));
    }

    public void LoadChatServiceData()
    {
        _chatServiceSaveData.Service.SetModel(_serviceBase.Read<Chat>(_chatServiceSaveData.FilePath));
    }

    public void LoadMessageServiceData()
    {
        _messageServiceSaveData.Service.SetModel(_serviceBase.Read<Message>(_messageServiceSaveData.FilePath));
    }
}