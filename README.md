# Networked System

This university project is a TCP server built in C# and runs in the terminal. It can accept multiple clients and uses locking to protect data corruption in critical sections.

## Technologies

• C#

• VS Code

• Terminal

## Features

• Server stores client data

• It can return, add and delete client data

• Multiple clients can be handled safely by threading and having dedicated client objects

• Mutex locking is used to protect critical sections

• Prints server stats every 10 seconds

## Commands

```console
mcs TupleSpaceServer.cs TupleSpaceWorker.cs

mono TupleSpaceServer.exe 50001
'''

### In client folder, compile and run at the same time:

'''
mcs TupleSpaceClient.cs && mono TupleSpaceClient.exe localhost 50001 sample_requests.txt
'''

### Or with multiple clients:

for i in $(seq 1 10)
do
mono TupleSpaceClient.exe localhost 50001 "concurrent_client_requests/client_${i}_request.txt" &
done
wait

## Process

The first step was verifying that the entered port was valid to use. After this it was parsed to the listener to start listening for clients.

After this I created a worker thread that can manage one connected client and parsed it a accepted client.

I then made the client able to connect and send requests to the worker, and the worker to send responses. To make this work properly I added framing.

I then made the server correctly handle requests, and then print out the stats.

Then I used a while loop so the server could accept concurrent clients.

## What I learned

### TCP Server

I learned how to make a multi-threaded TCP server. I learned how to set up a server socket using TcpListener and accept incoming client connections.

### Thread Safety

I developed my understanding of thread safety principles, like protecting shared resources, using critical sections, and ensuring atomic updates to a shared state.

### Communication Protocol

I learned how to convert bytes to text and vice versa, and then using that text-based protocol to form commands like READ, GET and PUT.

### Framing

To ensure that the client and server got the full messages, I used framing to ensure that the scripts kept listening until they got the full message.

## How it can be improved

• The client outputted too much text, cluttering the terminal, so reducing their output would be an improvement

• The commands are quite limited now, so maybe having more would make the server more useful

• The server was only tested on one machine, so maybe testing it on multiple would be better

## How to run

Clone the repository to your machine.

Then run the commands above.
