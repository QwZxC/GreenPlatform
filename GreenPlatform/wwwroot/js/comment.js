"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/comment-hub").build();

document.getElementById("sendButton").disabled = true;

connection.on("JoinGroup", function (comments) {
    for (let i = 0; i < comments.length; i++) {
        var li = document.createElement("li");
        document.getElementById("commentsList").appendChild(li);
        li.textContent = `${comments[i].creator.login} ${comments[i].content}`;
    }
});

connection.on("ReceiveMessage", function (commentFromDb) {
    var li = document.createElement("li");
    document.getElementById("commentsList").appendChild(li);
    li.textContent = `${commentFromDb.creator.login} ${commentFromDb.content}`;
});

connection.start().then(function () {
    var articleId = document.getElementById("articleId").value;
    console.log(articleId)
    connection.invoke("JoinGroup", articleId).catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("sendButton").disabled = false;
}).catch (function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var content = document.getElementById("messageInput").value;
    var articleId = document.getElementById("articleId").value;
    var li = document.createElement("li");
    document.getElementById("messageInput").value = "";
    connection.invoke("SendComment", { "content": content, "articleId": articleId }).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});