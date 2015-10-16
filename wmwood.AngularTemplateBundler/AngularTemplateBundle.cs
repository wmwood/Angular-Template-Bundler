using System.Web.Optimization;

namespace wmwood.AngularTemplateBundler
{
    public class AngularTemplateBundle : Bundle
    {
        public AngularTemplateBundle(string virtualPath, string moduleName)
            : base(virtualPath, new AngularTemplateTransform(moduleName), new JsMinify())
        {
        }
    }
}