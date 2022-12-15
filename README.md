# AsyncBenchmark

To run the benchmark, you can use the ApacheBenchmark tool 
https://httpd.apache.org/docs/2.4/programs/ab.htm

Sample use:
ab -n 100 -c 100 -k -s 120 “http://localhost:5000/async"

c — Number of concurrent clients working simultaneously.

n — Number of requests to perform for the benchmarking session.

k — Enable the HTTP KeepAlive feature, i.e., perform multiple requests within one HTTP session. The default is no KeepAlive.

s — Maximum number of seconds to wait before the socket times out.
