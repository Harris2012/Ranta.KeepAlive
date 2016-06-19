using log4net;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Ranta.KeepAlive
{
    public partial class KnockService : ServiceBase
    {
        public KnockService()
        {
            InitializeComponent();

            logger = LogManager.GetLogger(this.GetType());
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            scheduler = schedulerFactory.GetScheduler();

            IJobDetail invokeWebJob = JobBuilder.Create<InvokeWebJob>()
                .WithIdentity("InvokeWebJob", "KnockService")
                .Build();

            ITrigger invokeWebTriger = TriggerBuilder.Create()
                .WithIdentity("InvokeWebTrigger", "KnockService")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(15).RepeatForever())
                .Build();

            scheduler.ScheduleJob(invokeWebJob, invokeWebTriger);
        }

        private readonly ILog logger;
        private IScheduler scheduler;

        protected override void OnStart(string[] args)
        {
            scheduler.Start();

            logger.Info("Quartz服务成功启动");
        }

        protected override void OnStop()
        {
            scheduler.Shutdown();

            logger.Info("Quartz服务成功终止");
        }

        protected override void OnPause()
        {
            scheduler.PauseAll();
        }

        protected override void OnContinue()
        {
            scheduler.ResumeAll();
        }
    }
}
