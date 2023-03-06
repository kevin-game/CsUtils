using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KikiNet.Common;

namespace KikiNet.Netty;

public class TcpServer<T>
{
    private IEventLoopGroup _bossGroup;
    private IEventLoopGroup _workerGroup;
    private IChannel _bootstrapChannel;
    private readonly ISerializer<T> _serializer;

    public TcpServer(ISerializer<T> serializer)
    {
        _serializer = serializer;
    }

    public Task Start(int port)
    {
        return RunServerAsync(port);
    }

    public async Task Stop()
    {
        await Task.WhenAll(
            _bootstrapChannel.CloseAsync(),
            _bossGroup.ShutdownGracefullyAsync(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2)),
            _workerGroup.ShutdownGracefullyAsync(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2))
        );
    }
    
    private async Task RunServerAsync(int port)
    {
        _bossGroup = new MultithreadEventLoopGroup(1);
        _workerGroup = new MultithreadEventLoopGroup();

        try
        {
            ServerBootstrap bootstrap = new();
            bootstrap.Group(_bossGroup, _workerGroup);
            bootstrap.Channel<TcpServerSocketChannel>();
            bootstrap
                .Option(ChannelOption.SoBacklog, 4096)
                .Option(ChannelOption.RcvbufAllocator, new AdaptiveRecvByteBufAllocator())
                .Option(ChannelOption.Allocator, PooledByteBufferAllocator.Default)
                 .ChildOption(ChannelOption.SoKeepalive, true)
                 .ChildOption(ChannelOption.TcpNodelay, true)
                .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    pipeline.AddLast("IdelChecker", new IdleStateHandler(50, 50, 0));
                    pipeline.AddLast(new TcpEncoder<T>(_serializer), new TcpDecoder<T>(_serializer), new TcpHandler<T>());
                }));

            _bootstrapChannel = await bootstrap.BindAsync(port);

            Console.WriteLine($"启动网关服务器成功！监听端口号：{port}");
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e.Message);
            throw new Exception("启动TcpClient失败！\n" + e.StackTrace);//TODO: new exception type
        }
    }
}