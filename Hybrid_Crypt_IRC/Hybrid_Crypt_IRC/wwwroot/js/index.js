"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/cryptChat").build();

document.getElementById("sendPassword").disabled = true;

connection.on("EnterPassWord")

document.getElementById("sendPassword").addEventListener("click", function (event) {
    var password = document.getElementById("GroupPassword").value;
    connection.invoke("EnterPassWord", password).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});