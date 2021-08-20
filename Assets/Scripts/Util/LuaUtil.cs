public class LuaUtil : Singleton<LuaUtil>
{
    public byte[] StringToBytes_UTF8(string data)
    {
        return System.Text.Encoding.UTF8.GetBytes(data);
    }
    public string BytesToString_UTF8(byte[] data)
    {
        return System.Text.Encoding.UTF8.GetString(data);
    }
}