using System;
using System.IO;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Drawing;

namespace Douban
{
    public class Plugin : BasePlugin, IHasThumbImage
    {
        public override Guid Id => new Guid("1C174419-2981-44AC-B19A-2636C0E6BC34");
        public override string Name => "Douban";

        public override string Description => "Douban metadata provider";

        public Stream GetThumbImage()
        {
            var type = GetType();
            return type.Assembly.GetManifestResourceStream(type.Namespace + ".thumb.png");
        }
        public ImageFormat ThumbImageFormat => ImageFormat.Png;
    }
}
