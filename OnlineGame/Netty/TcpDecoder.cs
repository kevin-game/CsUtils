using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System.Text.Json;
using KikiNet.Common;
using System;

namespace KikiNet.Netty;

public class TcpDecoder<T>  : ReplayingDecoder<int>
{
    private ISerializer<T> _serializer;
    public TcpDecoder(ISerializer<T> serializer) : base(0)
    {
        _serializer = serializer;
    }
    
    protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
    {
        var length = input.ReadUnsignedShort();
        var bytes = input.ReadBytes(length).Array;
        var obj = _serializer.Deserialize(bytes);
        if (obj != null)
            output.Add(obj);
    }
}