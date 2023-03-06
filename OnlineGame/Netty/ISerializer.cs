namespace KikiNet.Common;

public interface ISerializer<TMessage>
{
    TMessage? Deserialize(byte[] data);
    byte[] Serialize(TMessage message);
}
