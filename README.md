# Networked System

This university project is a TCP server build in C# and runs in terminal. It can accept multiple clients and uses locking to protect data corruption in critical sections. 

# Technolgies

 • C#
 
 • VS code
 
 • Terminal

# Features

 • Server stores client data
 
 • It can return, add and delete client data

 • Multiple clients can be handled safely by threading and having dedicated client objects

 • Mutex locking is used to protect critical sections

 • Prints server stats every 10 seconds

 # Commands

mcs TupleSpaceServer.cs TupleSpaceWorker.cs
  
mono TupleSpaceServer.exe 50001
    
### In client folder, compiling and running at the same time

mcs TupleSpaceClient.cs && mono TupleSpaceClient.exe localhost 50001 sample_requests.txt
    
### Or with multiple clients
    
for i in $(seq 1 10)
    
do
    
mono TupleSpaceClient.exe localhost 50001 "concurrent_client_requests/client_${i}_request.txt" &

done
    
wait

# Process

The first step was verifying that the entered port was valid to use. After this it was parsed to the listner to start listening for clients

After this I created a worker thread that can manage one connected client and parsed it a accepted client. 

I then made the client able to connect to connect and send requests to the worker. And the worker to send responses. To make this work propoly I added in framing.

I then made the server correctly handle requests, and  then print out the stats

Then I used a while loop so the server could accept concurrent clients

# What I learned
## TCP Server

I learned how to make a multi threaded TCP server. I learned how to set up a server socket using TcpListener and accept incoming client connections

## Thread Safety

I develped my understanding of thread safety principles, like protecting shared resources, using critical sections and ensuring atomic updates to a shared state

## Communication Protocol

I learned how to convert bytes to text and vice versa and then using that text based protocol to for commands like READ, GET and PUT

## Framing

To ensure that the client and servers got the full messages I used framing to ensure that the scripts kept listening until they got the full message

# How it can be improved

• The clients outputted to much text cluttering the terminal, so reducing there output would be an improvement

• The commands are quite limited now so maybe having more would make the server more useful

• The server was only tested on one machine so maybe testing it on multiple would be better

# How to run

Clone the repository to your machine

Then run the commands above
