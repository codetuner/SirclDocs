﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Security;

namespace SirclDocs.Website.Logging
{
    public class RequestLogExceptionFilter : BaseRequestLogFilter
    {
        public RequestLogExceptionFilter(RequestDelegate next) : base(next)
        { }

        public override void PreInvoke(HttpContext context, RequestLogger requestLogger)
        { }

        public override void PostInvoke(HttpContext context, RequestLogger requestLogger)
        {
            var ex = requestLogger.Exception;
            if (ex != null)
            {
                if (requestLogger.StoreLog == null) requestLogger.StoreLog = true;
                var aspect = (ex is SecurityException) ? LogAspect.Security : LogAspect.Error;
                requestLogger.SetException(aspect.Name, true, ex);
                requestLogger.WriteLine(ex.ToString());
                WriteExceptionData(requestLogger, "Ex", ex);
            }
        }

        protected void WriteExceptionData(RequestLogger logger, string path, Exception ex)
        {
            if (ex.Data != null)
            {
                foreach (DictionaryEntry entry in ex.Data)
                {
                    try
                    {
                        logger.WithData(path + "." + Convert.ToString(entry.Key), Convert.ToString(entry.Value) ?? String.Empty);
                    }
                    catch (Exception) { }
                }
            }
            if (ex.InnerException != null)
            {
                WriteExceptionData(logger, path + ".Inner", ex.InnerException);
            }
            if (ex is AggregateException agex)
            {
                var i = 0;
                foreach (var subex in agex.InnerExceptions)
                {
                    WriteExceptionData(logger, path + ".Inner[" + i + "]", subex);
                    i++;
                }
            }
        }
    }
}
