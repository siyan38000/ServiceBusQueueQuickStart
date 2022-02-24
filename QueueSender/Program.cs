using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using QueueSender.Models;

public class Program
{
    // connection string to your Service Bus namespace
    static string connectionString = "Endpoint=sb://projet-archi.servicebus.windows.net/;SharedAccessKeyName=Test;SharedAccessKey=2R/kIwrJ+1QnBHMLx11GiFN77vHJ95tyPXx5S+T2FzY=;EntityPath=main-queue";

    // name of your Service Bus queue
    static string queueName = "main-queue";

    // the client that owns the connection and can be used to create senders and receivers
    static ServiceBusClient client;

    // the sender used to publish messages to the queue
    static ServiceBusSender sender;

    // number of messages to be sent to the queue
    private const int numOfMessages = 3;

    static async Task SendMessages(string message)
    {
        // The Service Bus client types are safe to cache and use as a singleton for the lifetime
        // of the application, which is best practice when messages are being published or read
        // regularly.
        //
        // Create the clients that we'll use for sending and processing messages.
        client = new ServiceBusClient(connectionString);
        sender = client.CreateSender(queueName);

        // create a batch 
        using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

            // try adding a message to the batch
            if (!messageBatch.TryAddMessage(new ServiceBusMessage(message)))
            {
                // if it is too large for the batch
                throw new Exception($"The message is too large to fit in the batch.");
            }

        try
        {
            // Use the producer client to send the batch of messages to the Service Bus queue
            await sender.SendMessagesAsync(messageBatch);
            Console.WriteLine($"message envoyé : "+message);
        }
        finally
        {
            // Calling DisposeAsync on client types is required to ensure that network
            // resources and other unmanaged objects are properly cleaned up.
            await sender.DisposeAsync();
            await client.DisposeAsync();
        }
    }
    public static void Main()
    {
        var startTimeSpan = TimeSpan.Zero;
        var periodTimeSpan = TimeSpan.FromSeconds(30);

        var timer = new Timer((e) =>
        {
            string messageToSend = GenerateRandomMessage();
            SendMessages(messageToSend);
            messageToSend = GenerateStock();
            SendMessages(messageToSend);
        }, null, startTimeSpan, periodTimeSpan);
        Console.ReadKey();
    }
    public static string GenerateRandomMessage()
    { 
        Random random = new Random();

        Camtar camion1 = new Camtar();
        camion1.Name = "Renault Trafic";
        camion1.Id = 1;
        camion1.IsLeft = true;
        camion1.IsArrived = false;
        camion1.date = DateTime.Now;

        Camtar camion2 = new Camtar();
        camion2.Name = "Iveco Daily";
        camion2.Id = 2;
        camion2.IsLeft = true;
        camion2.IsArrived = false;
        camion2.date = DateTime.Now;

        Camtar camion3 = new Camtar();
        camion3.Name = "Mercedes-Benz Sprinter";
        camion3.Id = 3;
        camion3.IsLeft = false;
        camion3.IsArrived = true;
        camion3.date = DateTime.Now;

        Camtar camion4 = new Camtar();
        camion4.Name = "Renault Master";
        camion4.Id = 4;
        camion4.IsLeft = false;
        camion4.IsArrived = true;
        camion4.date = DateTime.Now;

        Camtar[] camions = new Camtar[4];
        camions[0] = camion1;
        camions[1] = camion2;
        camions[2] = camion3;
        camions[3] = camion4;

        int rndCamtar = random.Next(camions.Length);
        string message = JsonSerializer.Serialize(camions[rndCamtar]);
        return message;
    }
    public static string GenerateStock()
    {
        References ref1 = new References();
        ref1.Id = 1;
        ref1.Name = "Lit";
        ref1.Quantity = 4;

        References ref2 = new References();
        ref2.Id = 2;
        ref2.Name = "Matelas";
        ref2.Quantity = 2;

        References ref3 = new References();
        ref3.Id = 3;
        ref3.Name = "Table";
        ref3.Quantity = 4;

        References ref4 = new References();
        ref4.Id = 4;
        ref4.Name = "Chaufage";
        ref4.Quantity = 8;

        References[] refs = new References[4];
        refs[0] = ref1;
        refs[1] = ref2;
        refs[2] = ref3;
        refs[3] = ref4;
        string message = JsonSerializer.Serialize(refs);
        return message;
    }
}
