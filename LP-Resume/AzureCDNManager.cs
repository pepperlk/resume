using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace System.Web.Mvc
{
    public static class HtmlHelperCDNExt
    {
        public static string CDNEndpoint(this HtmlHelper helper)
        {
            return LP_Resume.AzureCDNManager.GetEndpoint();
        }
    }
}


namespace LP_Resume
{
    public class AzureCDNManager
    {

        private static AzureCDNConfig _config = new AzureCDNConfig();

        public static void Init(Action<AzureCDNConfig> configfun)
        {
            configfun(_config);
            _config.Init();
        }

        internal static string GetEndpoint()
        {
            return _config.GetEndpoint();
        }
    }

    public class AzureCDNConfig
    {
        private CloudStorageAccount _account;
        private string _endpoint;
        private List<string> _folderstoupload;

        public AzureCDNConfig()
        {
            _folderstoupload = new List<string>();
        }


        internal void AzureStoregeConnectionString(string p)
        {
            _account = CloudStorageAccount.Parse(p);


        }

        internal void Endpoint(string p)
        {
            _endpoint = p;
        }

        internal void Folder(string folder)
        {
            _folderstoupload.Add(folder);
        }

        internal void External(string p)
        {

        }

        internal void Init()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            var container = "staticfiles";

            var path = version.ToString().Replace(".", "_");


            var http = HttpContext.Current;

#if DEBUG

            path = "debug" + "/" + path;

#endif

            if (string.IsNullOrEmpty(_endpoint))
            {
                _endpoint = (_account.BlobEndpoint.AbsoluteUri.TrimEnd('/') + "/");
            }

            _endpoint = _endpoint + container + "/" + path.TrimEnd('/');



            var cli = _account.CreateCloudBlobClient();

            var newProperties = CurrentProperties(cli);

            //Set service to new version:
            newProperties.DefaultServiceVersion = "2013-08-15"; //"2012-02-12"; // "2011-08-18"; // null;
            cli.SetServiceProperties(newProperties);

            var addRule = true;
            if (addRule)
            {

                //Add a wide open rule to allow uploads:
                var ruleWideOpenWriter = new CorsRule()
                {
                    AllowedHeaders = { "*" },
                    AllowedOrigins = { "*" },
                    AllowedMethods =
                        CorsHttpMethods.Options |
                        CorsHttpMethods.Post |
                        CorsHttpMethods.Put |
                        CorsHttpMethods.Merge,
                    ExposedHeaders = { "*" },
                    MaxAgeInSeconds = (int)TimeSpan.FromDays(5).TotalSeconds
                };
                newProperties.Cors.CorsRules.Add(ruleWideOpenWriter);
                cli.SetServiceProperties(newProperties);
            }

            var cont = cli.GetContainerReference(container);
            cont.CreateIfNotExists();
            cont.SetPermissions(new Microsoft.WindowsAzure.Storage.Blob.BlobContainerPermissions { PublicAccess = Microsoft.WindowsAzure.Storage.Blob.BlobContainerPublicAccessType.Blob });

            var rootdir = http.Server.MapPath("~");

            foreach (var folder in _folderstoupload)
            {

                var files = new DirectoryInfo(http.Server.MapPath(folder)).EnumerateFiles("*", SearchOption.AllDirectories);

                foreach (var file in files)
                {
                    var loc = path + "/" + file.FullName.Replace(rootdir, "").Replace("\\", "/");

                    var blob = cont.GetBlockBlobReference(loc);
                    blob.DeleteIfExists();
                    var fs = file.Open(FileMode.Open);
                    blob.UploadFromStream(fs);
                    fs.Dispose();
                    //blob.UploadFromFile(file.FullName, FileMode.Create);


                }


            }



        }


        private ServiceProperties CurrentProperties(CloudBlobClient blobClient)
        {
            var currentProperties = blobClient.GetServiceProperties();
            if (currentProperties != null)
            {
                if (currentProperties.Cors != null)
                {
                    Console.WriteLine("Cors.CorsRules.Count          : " + currentProperties.Cors.CorsRules.Count);
                    for (int index = 0; index < currentProperties.Cors.CorsRules.Count; index++)
                    {
                        var corsRule = currentProperties.Cors.CorsRules[index];
                        Console.WriteLine("corsRule[index]              : " + index);
                        foreach (var allowedHeader in corsRule.AllowedHeaders)
                        {
                            Console.WriteLine("corsRule.AllowedHeaders      : " + allowedHeader);
                        }
                        Console.WriteLine("corsRule.AllowedMethods      : " + corsRule.AllowedMethods);

                        foreach (var allowedOrigins in corsRule.AllowedOrigins)
                        {
                            Console.WriteLine("corsRule.AllowedOrigins      : " + allowedOrigins);
                        }
                        foreach (var exposedHeaders in corsRule.ExposedHeaders)
                        {
                            Console.WriteLine("corsRule.ExposedHeaders      : " + exposedHeaders);
                        }
                        Console.WriteLine("corsRule.MaxAgeInSeconds     : " + corsRule.MaxAgeInSeconds);
                    }
                }
                Console.WriteLine("DefaultServiceVersion         : " + currentProperties.DefaultServiceVersion);
                Console.WriteLine("HourMetrics.MetricsLevel      : " + currentProperties.HourMetrics.MetricsLevel);
                Console.WriteLine("HourMetrics.RetentionDays     : " + currentProperties.HourMetrics.RetentionDays);
                Console.WriteLine("HourMetrics.Version           : " + currentProperties.HourMetrics.Version);
                Console.WriteLine("Logging.LoggingOperations     : " + currentProperties.Logging.LoggingOperations);
                Console.WriteLine("Logging.RetentionDays         : " + currentProperties.Logging.RetentionDays);
                Console.WriteLine("Logging.Version               : " + currentProperties.Logging.Version);
                Console.WriteLine("MinuteMetrics.MetricsLevel    : " + currentProperties.MinuteMetrics.MetricsLevel);
                Console.WriteLine("MinuteMetrics.RetentionDays   : " + currentProperties.MinuteMetrics.RetentionDays);
                Console.WriteLine("MinuteMetrics.Version         : " + currentProperties.MinuteMetrics.Version);
            }
            return currentProperties;
        }

        internal string GetEndpoint()
        {
            return _endpoint;
        }
    }
}