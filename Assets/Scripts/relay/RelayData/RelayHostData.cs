using System;

public struct RelayHostData
{
    /* obtain a join code from which another client is able to access our game using*/
    public string JoinCode;

    /* IP address of the relay server*/
    public string IPv4Address;

    /* port of the relay server*/
    public ushort Port;
    public Guid AllocationID;
    public byte[] AllocationIDBytes;
    public byte[] ConnectionData;
    public byte[] Key;
}