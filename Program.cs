using Azure;
using Azure.Communication.Email;
using Azure.Identity;

//Endpoint do Communication Service (Menu Settings - Keys)
EmailClient emailClient = new EmailClient(new Uri("https://<NOME-INSTANCIA-COMMSERVER>.communication.azure.com/"),new DefaultAzureCredential());

//MailFrom addresses do Email Communication Services Domain
var sender = "donotreply@<GUID-DOMAIN-AZURE-COMMSERVEREMAIL>.azurecomm.net";

var toRecipients = new List<EmailAddress>
{
  new EmailAddress("..."),
  new EmailAddress("..."),
};

EmailRecipients emailRecipients = new EmailRecipients(toRecipients);

var emailContent = new EmailContent("Welcome to Azure Communication Service Email APIs.")
{
    PlainText = "This email message is sent from Azure Communication Service Email.",
    Html = "<html><body><h1>Quick send email test</h1><br/><h4>This email message is sent from Azure Communication Service Email.</h4><p>This mail was sent using .NET SDK!!</p></body></html>"
};
var emailMessage = new EmailMessage(
    senderAddress: sender,
    emailRecipients,
    emailContent);

try
{
    Console.WriteLine("Sending email...");
    EmailSendOperation emailSendOperation = await emailClient.SendAsync(Azure.WaitUntil.Completed,emailMessage);
    
    EmailSendResult statusMonitor = emailSendOperation.Value;
    
    Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");

    /// Get the OperationId so that it can be used for tracking the message for troubleshooting
    string operationId = emailSendOperation.Id;
    Console.WriteLine($"Email operation id = {operationId}");
}
catch (RequestFailedException ex)
{
    /// OperationID is contained in the exception message and can be used for troubleshooting purposes
    Console.WriteLine($"Email send operation failed with error code: {ex.ErrorCode}, message: {ex.Message}");
}