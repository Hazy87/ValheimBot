var builder = WebApplication.CreateBuilder(args);
BootStrapper.Configure(builder);
var app = builder.Build();
var joinedResponseMaker = app.Services.GetService<IJoinedResponseMaker>();
var leftResponseMaker = app.Services.GetService<ILeftResponseMaker>();
var diedResponseMaker = app.Services.GetService<IDiedResponseMaker>();
var insultSendingService = app.Services.GetService<IInsultSendingService>();
app.MapPost("/joined", async (ValheimRequest request) =>
{
     Console.WriteLine(request.content);
     if (request.content.Contains("died"))
          return;
     var response = await joinedResponseMaker.GetResponse(request.content);
     await insultSendingService.SendMessage(response);
});
app.MapPost("/died", async (ValheimRequest request) =>
{
     Console.WriteLine(request.content);
     if (!request.content.Contains("died"))
          return;
     var response = await diedResponseMaker.GetResponse(request.content);
     await insultSendingService.SendMessage(response);
});
app.MapPost("/left", async (ValheimRequest request) =>
{
     Console.WriteLine(request.content);

     var response =  leftResponseMaker.GetResponse(request.content);
     await insultSendingService.SendMessage(response);
});
app.Run();

