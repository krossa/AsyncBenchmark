var app = WebApplication.CreateBuilder(args).Build();
var delay = 2000;
int requestsCount = 0;

//ab -n 100 -c 100 -k -s 120 "http://localhost:5000/sync"
app.MapGet("/sync", () => Thread.Sleep(delay));

app.MapGet("/async", async () => await Task.Delay(delay));

app.Use(async (context, next) =>
            {
                Interlocked.Increment(ref requestsCount);
                await next();
            });

new Thread(_ =>
{
    while (true)
    {
        ThreadPool.GetAvailableThreads(out var available, out var _);
        ThreadPool.GetMaxThreads(out var maxThreads, out var _);
        ThreadPool.GetMinThreads(out var minThreads, out var _);
        Console.WriteLine($"Requests: {Volatile.Read(ref requestsCount)} - Active: {maxThreads - available}, Available: {available}, Min: {minThreads}, Max: {maxThreads}");
        Thread.Sleep(1000);
    }
})
{
    IsBackground = true,
}.Start();

app.Run("http://localhost:5000");
