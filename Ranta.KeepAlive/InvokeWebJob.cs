using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Ranta.KeepAlive
{
    public class InvokeWebJob : IJob
    {
        private ILog logger = LogManager.GetLogger(typeof(InvokeWebJob).FullName);

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                var urls = File.ReadAllLines(@"D:\Job\UrlToCall.txt");

                HttpClient client = new HttpClient();

                foreach (var url in urls)
                {
                    try
                    {
                        var result = client.GetAsync(url);

                        logger.Info(string.Format("Success to get from url {0}", url));
                    }
                    catch (Exception ex)
                    {
                        logger.Error(string.Format("Fail to get from {0}", url), ex);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Fail to Execute InvokeWebJob", ex);
            }
        }
    }
}
