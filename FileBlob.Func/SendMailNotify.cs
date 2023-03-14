using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Configuration;

namespace FileBlob.Function;

public class SendMailNotify
{
    [FunctionName("SendMailNotify")]
    public static async Task Run(
        [BlobTrigger("filecontainer/{name}", Connection = "my21storage_STORAGE")] Stream myBlob,
        string name,
        IDictionary<string, string> metadata,
        ILogger log
    )
    {
        var emailTo = metadata["email"];
        var apiKey = Environment.GetEnvironmentVariable("SendGridApiKey");
        var emailFrom = Environment.GetEnvironmentVariable("SendGridEmailFrom");
        log.LogInformation(
            $"C# Blob trigger function Processed blob\n Name:{name} \n and email is {emailTo}, emailFrom {emailFrom}"
        );
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(emailFrom);
        var subject = "File Uploaded";
        var to = new EmailAddress(emailTo);
        var plainTextContent = $"Hello, your file \"{name}\" was succesfully uploaded!";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, "");
        var response = await client.SendEmailAsync(msg);
    }
}
