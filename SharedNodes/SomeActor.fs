﻿namespace SharedNodes

#if INTERACTIVE
#r @"..\..\bin\Akka.dll"
#r @"..\..\bin\Akka.FSharp.dll"
#r @"..\..\bin\Akka.Remote.dll"
#r @"..\..\bin\FSharp.PowerPack.dll"
#endif

open System
open Akka.FSharp
open Akka.Actor
open Akka.Remote
open Akka.Configuration


type SomeActor() =
     inherit Actor()

     //let mutable index = 0

     override x.OnReceive(message) =
        let senderAddress = ``base``.Self.Path.ToStringWithAddress()
        let originalColor = Console.ForegroundColor

        Console.ForegroundColor <- if senderAddress.Contains("localactor") then 
                                      ConsoleColor.Red
                                   else ConsoleColor.Green  

        printfn "%s got %A" senderAddress message

        Console.ForegroundColor <- originalColor

//
//let remoteServer = 
//    spawn system "RemoteServer"
//    <| fun mailbox ->
//        let rec loop() =
//            actor {
//                let! message = mailbox.Receive()
//                let sender = mailbox.Sender()
//                match box message with
//                | :? string -> 
//                        printfn "Message receice -> %s" message
//                        sender <! sprintf "Echo from remote - %s" message
//                        return! loop()
//                | _ ->  failwith "unknown message"
//            } 
//        loop()