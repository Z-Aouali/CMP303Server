using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerHandle
{

    


    public static void WelcomeReceived(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();

        Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
        if (_fromClient != _clientIdCheck)
        {
            Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
        }
        Server.clients[_fromClient].SendIntoGame(_username);
    }

    public static void PlayerMovement(int _fromClient, Packet _packet)
    {
        bool[] _inputs = new bool[_packet.ReadInt()];
        for (int i = 0; i < _inputs.Length; i++)
        {
            _inputs[i] = _packet.ReadBool();
        }
        Quaternion _rotation = _packet.ReadQuaternion();
        try
        {
        Server.clients[_fromClient].player.SetInput(_inputs, _rotation);

        }
        catch
        {
            Server.clients[_fromClient].Disconnect();
        }
    }

    public static void PlayerShoot(int _fromClient, Packet _packet)
    { 
        Vector3 _shotDir = _packet.ReadVector3();

        Server.clients[_fromClient].player.Shoot(_shotDir);

        
    }

    //public static void PlayerSync(int _fromClient, Packet _packet)
    //{
    //    Vector3 _position = _packet.ReadVector3();
    //    Quaternion _rotation = _packet.ReadQuaternion();
    //
    //    //Server.clients[_fromClient].player.Sync(_position, _rotation);
    //}
}
