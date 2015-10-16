using System.IO;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace wmwood.AngularTemplateBundler
{
    public class AngularTemplateTransform : IBundleTransform
    {
        private const string ContentType = "text/javascript";
        private const string FooterText = "}])})()";
        private readonly string _moduleName;

        public AngularTemplateTransform(string moduleName)
        {
            _moduleName = moduleName;
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            var responseContent = new StringBuilder();
            responseContent.Append(GetHeaderText());

            foreach (var bundleFile in response.Files)
            {
                responseContent.Append(GetBundleText(bundleFile));
            }

            responseContent.Append(FooterText);

            FormatResponse(response);
            response.Content = responseContent.ToString();
        }

        private string GetHeaderText()
        {
            return string.Format("(function() {{ \"use strict\"; angular.module(\"{0}\").run([\"$templateCache\", function($templateCache) {{", _moduleName);
        }

        private static string GetBundleText(BundleFile bundleFile)
        {
            return string.Format("$templateCache.put(\"{0}\", \"{1}\");", GetTemplatePath(bundleFile), GetJavascriptEncodedContent(bundleFile));
        }

        private static string GetTemplatePath(BundleFile bundleFile)
        {
            return VirtualPathUtility.ToAbsolute(bundleFile.IncludedVirtualPath);
        }

        private static string GetJavascriptEncodedContent(BundleFile bundleFile)
        {
            using (var streamReader = new StreamReader(BundleTable.VirtualPathProvider.GetFile(bundleFile.IncludedVirtualPath).Open()))
            {
                return HttpUtility.JavaScriptStringEncode(streamReader.ReadToEnd().Trim());
            }
        }

        private static void FormatResponse(BundleResponse response)
        {
            response.Files = new BundleFile[] {};
            response.ContentType = ContentType;
        }
    }
}