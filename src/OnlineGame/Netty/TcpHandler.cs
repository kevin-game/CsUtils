using DotNetty.Transport.Channels;

namespace KikiNet.Netty;

public class TcpHandler<T> : SimpleChannelInboundHandler<T>
{
    protected override void ChannelRead0(IChannelHandlerContext ctx, T msg)
    {
        Console.WriteLine($"receive msg:{msg}");
    }
    
    public override void ChannelActive(IChannelHandlerContext ctx)
    {
        Console.WriteLine($"new channel");
        base.ChannelActive(ctx);
    }
    
    public override void ChannelInactive(IChannelHandlerContext ctx)
    {
        base.ChannelInactive(ctx);
    }
    
    public override void ExceptionCaught(IChannelHandlerContext ctx, Exception exception)
    {
        base.ExceptionCaught(ctx,exception);
    }
}