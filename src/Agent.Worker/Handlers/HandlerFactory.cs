using Microsoft.VisualStudio.Services.Agent.Util;
using Microsoft.VisualStudio.Services.Agent.Worker;
using System;
using System.Collections.Generic;

namespace Microsoft.VisualStudio.Services.Agent.Worker.Handlers
{
    [ServiceLocator(Default = typeof(HandlerFactory))]
    public interface IHandlerFactory : IAgentService
    {
        IHandler Create(
            IExecutionContext executionContext,
            HandlerData data,
            Dictionary<string, string> inputs,
            string taskDirectory,
            string filePathInputRootDirectory);
    }

    public sealed class HandlerFactory : AgentService, IHandlerFactory
    {
        public IHandler Create(
            IExecutionContext executionContext,
            HandlerData data,
            Dictionary<string, string> inputs,
            string taskDirectory,
            string filePathInputRootDirectory)
        {
            // Validate args.
            Trace.Entering();
            ArgUtil.NotNull(executionContext, nameof(executionContext));
            ArgUtil.NotNull(data, nameof(data));
            ArgUtil.NotNull(inputs, nameof(inputs));
            ArgUtil.NotNull(taskDirectory, nameof(taskDirectory));

            // Create the handler.
            IHandler handler;
            if (data is NodeHandlerData)
            {
                // Node.
                handler = HostContext.CreateService<INodeHandler>();
                (handler as INodeHandler).Data = data as NodeHandlerData;
            }
            else if (data is PowerShell3HandlerData)
            {
                // PowerShell3.
                handler = HostContext.CreateService<IPowerShell3Handler>();
                (handler as IPowerShell3Handler).Data = data as PowerShell3HandlerData;
            }
            else if (data is PowerShellExeHandlerData)
            {
                // PowerShellExe.
                handler = HostContext.CreateService<IPowerShellExeHandler>();
                (handler as IPowerShellExeHandler).Data = data as PowerShellExeHandlerData;
            }
            else if (data is ProcessHandlerData)
            {
                // Process.
                handler = HostContext.CreateService<IProcessHandler>();
                (handler as IProcessHandler).Data = data as ProcessHandlerData;
            }
            else if (data is PowerShellHandlerData)
            {
                // PowerShell.
                handler = HostContext.CreateService<IPowerShellHandler>();
                (handler as IPowerShellHandler).Data = data as PowerShellHandlerData;
            }
            else if (data is AzurePowerShellHandlerData)
            {
                // AzurePowerShell.
                handler = HostContext.CreateService<IAzurePowerShellHandler>();
                (handler as IAzurePowerShellHandler).Data = data as AzurePowerShellHandlerData;
            }
            else
            {
                // This should never happen.
                throw new NotSupportedException();
            }

            handler.ExecutionContext = executionContext;
            handler.FilePathInputRootDirectory = filePathInputRootDirectory;
            handler.Inputs = inputs;
            handler.TaskDirectory = taskDirectory;
            return handler;
        }
    }
}