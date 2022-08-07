using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using System.Text.Json;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ProcessOrder;

public class Function
{
    /// <summary>
    /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
    /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
    /// region the Lambda function is executed in.
    /// </summary>
    public Function()
    {

    }


    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
    /// to respond to SQS messages.
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
    {
        foreach(var message in evnt.Records)
        {
            await ProcessMessageAsync(message, context);
        }
    }

    private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
    {
        context.Logger.LogInformation($"Processed message {message.Body}");

        var order = JsonSerializer.Deserialize<Order>(message.Body)!;

        context.Logger.LogLine($"Order Id {order.Id}");
        foreach(var item in order.Items)
        {
            context.Logger.LogLine($"{item}");
            if (item=="Widget")
            {
                throw new ApplicationException("Out of inventory: Widget");
            }
        }

        await Task.CompletedTask;
    }
}

public class Order
{
    public string Id { get; set; } = null!;
    public List<string> Items { get; set; } = null!;

    public Order() { }

    public Order(string id, string[] items)
    {
        Id = id;
        Items = new List<string>(items);
    }

}