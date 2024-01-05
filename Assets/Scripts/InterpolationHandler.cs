using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolationHandler : MonoBehaviour
{


    //public Queue<Constants.InputPacket> inputQueue;
    //private float timer;
    //private int currentTick;
    //private float minTimeBetweenTicks;
    //
    //Client client;
    //
    //public static InterpolationHandler instance;
    //
    //public InterpolationHandler(Client _client)
    //{
    //    client = _client;
    //}
    //
    //
    //private void Awake()
    //{
    //    instance = this;
    //}
    //
    //// Start is called before the first frame update
    //
    //
    //// Update is called once per frame
    //void Update()
    //{
    //    
    //}
    //
    //private void FixedUpdate()
    //{
    //    
    //}
    //
    //public void AddToQueue(Packet _packet)
    //{
    //
    //    bool[] _inputs = new bool[_packet.ReadInt()];
    //    for (int i = 0; i < _inputs.Length; i++)
    //    {
    //        _inputs[i] = _packet.ReadBool();
    //    }
    //
    //    Quaternion _rotation = _packet.ReadQuaternion();
    //
    //    int _clientTick = _packet.ReadInt();
    //
    //    inputQueue.Enqueue(
    //        new Constants.InputPacket()
    //        { input = _inputs, tick = _clientTick, rotation = _rotation }
    //        );
    //}
}
