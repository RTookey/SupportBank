// See https://aka.ms/new-console-template for more information


using NLog;
using NLog.Config;
using NLog.Targets;
using SupportBank;


var config = new LoggingConfiguration();
var target = new FileTarget { FileName = @"C:\Work\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
config.AddTarget("File Logger", target);
config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
LogManager.Configuration = config;

SupportBankApplication application = new ();
application.Run();