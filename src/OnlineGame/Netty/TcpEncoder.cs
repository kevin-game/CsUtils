using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using KikiNet.Common;

namespace KikiNet.Netty;

public class TcpEncoder<T> : MessageToByteEncoder<T>
{
    private ISerializer<T> _serializer;
    public TcpEncoder(ISerializer<T> serializer)
    {
        _serializer = serializer;
    }
    protected override void Encode(IChannelHandlerContext context, T message, IByteBuffer output)
    {
        var bytes = _serializer.Serialize(message);
        if (bytes != null)
            output.WriteBytes(bytes);
    }
}
