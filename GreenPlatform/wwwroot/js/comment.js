"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/comment-hub").build();

document.getElementById("sendButton").disabled = true;

connection.on("JoinGroup", function (comments) {
    for (let i = 0; i < comments.length; i++) {
        var div = document.createElement("div");
        div.setAttribute('class', 'd-flex flex-column bd-highlight mb-3')
        document.getElementById("commentsList").appendChild(div);
        div.innerHTML = `
            <div class="p-2 bd-highlight">
                <h3>${comments[i].creator.login}</h3>
                <h4>${comments[i].content}</h4>
            </div>
        `;
    }
});

connection.on("ReceiveMessage", function (commentFromDb) {
    var div = document.createElement("div");
    div.setAttribute('class', 'd-flex flex-column bd-highlight mb-3')
    document.getElementById("commentsList").appendChild(div);
    div.innerHTML = `
            <div class="p-2 bd-highlight">
                <h3>${commentFromDb.creator.login}</h3>
                <h4>${commentFromDb.content}</h4>
            </div>
        `
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