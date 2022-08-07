# Hello, App Runner!

This episode: Amazon CloudWatch Alarms. In this [Hello, Cloud](https://davidpallmann.hashnode.dev/hello-cloud) blog series, we're covering the basics of AWS 

In this post we'll introduce CloudWatch Alarms and use them with a "Hello, Cloud" .NET program to monitor AWS resources and send alert notifications. We'll do this step-by-step, making no assumptions other than familiarity with C# and Visual Studio. We're using Visual Studio 2022 and .NET 6.

## Our Hello, CloudWatch Alarms Project

We'll first develop a simulated order generation console program and an order processing Lambda function, connected by an SQS queue for order messages. We'll create 2 CloudWatch alarms. The first will alert when orders are not being processed. The second will alert on errors during order processing. SNS email notifications will be sent when either alarm enters the alarm state.

See the blog post for the tutorial to create this project and run it on AWS.

