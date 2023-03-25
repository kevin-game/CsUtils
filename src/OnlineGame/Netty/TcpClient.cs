using System.Net;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KikiNet.Common;

namespace KikiNet.Netty;

public class TcpClient<T>
{
    private IEventLoopGroup _group;
    private IChannel _bootstrapChannel;
    private readonly ISerializer<T> _serializer;

    public TcpClient(ISerializer<T> serializer)
    {
        _serializer = serializer;
    }

    public Task Start(string ip, int port)
    {
        return RunClientAsync(ip, port);
    }

    public async Task Stop()
    {
        await Task.WhenAll(
            _bootstrapChannel.CloseAsync(),
            _group.ShutdownGracefullyAsync(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2))
        );
    }
    
    private async Task RunClientAsync(string ip, int port)
    {
        _group = new MultithreadEventLoopGroup(1);

        try
        {
            Bootstrap bootstrap = new();
            bootstrap.Group(_group);
            bootstrap.Channel<TcpSocketChannel>();
            bootstrap
                //.Option(ChannelOption.RcvbufAllocator, new AdaptiveRecvByteBufAllocator())
                //.Option(ChannelOption.Allocator, PooledByteBufferAllocator.Default)
                .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    //pipeline.AddLast("IdelChecker", new IdleStateHandler(50, 50, 0));
                    pipeline.AddLast(new TcpEncoder<T>(_serializer), new TcpDecoder<T>(_serializer), new TcpHandler<T>());
                }));

            var ipe = new IPEndPoint(IPAddress.Parse(ip), port);
            _bootstrapChannel = await bootstrap.ConnectAsync(ipe);
            Console.WriteLine($"连接网关服务器成功！目标端口号：{port}");
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e.Message);
            throw new Exception("启动TcpClient失败！\n" + e.StackTrace);//TODO: new exception type
        }
    }

    public Task WriteAsync(T message)
    {
        return _bootstrapChannel.WriteAndFlushAsync(message);
    }
    
}