/// <summary>
/// 网络事件协议ID枚举
/// </summary>
public enum SocketEvent
{
    HEARTBEAT = 0x0001,   //心跳包
    DISCONN = 0x0002,   //客户端主动断开
    KICKOUT = 0x0003,    //服务端踢出

    DATA = 0x1001,   //发送数据包
}
