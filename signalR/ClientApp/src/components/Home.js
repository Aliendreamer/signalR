import React, { useEffect, useState } from 'react';
import * as signalR from "@microsoft/signalr";

export const Home = () => {
    const [data,setData] = useState([]);
    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:7030/data") // this should be configuration of course
            .build();
        connection.on("ReceiveMessage", (data) => setData(data));
        connection.start().catch((err) => console.log(err));
    },[])
    console.log(data)
    return (
        <div className="wrapper">
            <div className="content">
                <p className="text">{data.map(v => `${v.currency}:${v.value}`).join(" ")}</p>
            </div>
        </div>
    );
  
}