using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNI_Management.Common.Utility
{
    public static class UNI_ManagementLogger
    {
        private static Logger? _exceptionLogger;

        public static Logger Logger
        {
            get
            {
                try
                {
                    return _exceptionLogger ?? (_exceptionLogger = LogManager.GetCurrentClassLogger());
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("============================");
                    Trace.WriteLine(ex.ToString());
                    Trace.WriteLine("============================");
                    return LogManager.CreateNullLogger();
                }
            }
        }
    }
}
