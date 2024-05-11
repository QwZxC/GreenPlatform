"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/comment-hub").build();
document.getElementById("sendButton").disabled = true;

connection.on("JoinGroup", async function (comments) {
    const articleOwnerId = document.getElementById("ownerId").value;
    async function getAuthorizedUserId() {
        const response = await fetch('/Account/GetAuthorizedUserId');
        return await response.json();
    }
    var authorizedUserId = await getAuthorizedUserId();
    for (let i = 0; i < comments.length; i++) {
        var div = document.createElement("div");
        div.setAttribute('class', 'd-flex flex-column bd-highlight mb-3')
        document.getElementById("commentsList").appendChild(div);
        if (authorizedUserId == comments[i].creator.id || authorizedUserId == articleOwnerId) {
            div.innerHTML = `
                <div id="${comments[i].id}" class="p-2 card bd-highlight">
                    <div class=d-flex flex-row>
                        <h2 class="me-2">${comments[i].creator.login}</h2>
                        <label class="text-center align-content-center me-2">${formatDate(comments[i].creationDate)}</label>
                        <form action="/${comments[i].id}?articleId=${comments[i].articleId}"
                              method="post">
                            <button data-comment="deleteComment" class="button" type="submit"><i class="fa-solid fa-trash"></i></button>    
                        </form>
                    </div>
                    <h4>${comments[i].content}</h4>
                </div>
            `;
            const deletebuttons = div.querySelectorAll('[data-comment="deleteComment"]');

            deletebuttons.forEach((el) => {
                el.addEventListener('click', function () {
                    connection.invoke('DeleteComment', comments[i].id).catch(function (err) {
                        return console.error(err.toString());
                    });
                });
            });
        }
        else {
            div.innerHTML = `
                <div id="${comments[i].id}" class=" card p-2 bd-highlight">
                    <div class=d-flex flex-row>
                        <h2 class="me-2">${comments[i].creator.login}</h2>
                        <label class="text-center align-content-center me-2">${formatDate(comments[i].creationDate)}</label>
                    </div>
                    <h4>${comments[i].content}</h4>
                </div>
            `;
        }
    }
});

connection.on("ReceiveMessage", async function (commentFromDb) {
    const articleOwnerId = document.getElementById("ownerId").value;
    var div = document.createElement("div");
    div.setAttribute('class', 'd-flex flex-column bd-highlight mb-3')
    document.getElementById("commentsList").appendChild(div);

    async function getAuthorizedUserId() {
        const response = await fetch('/Account/GetAuthorizedUserId');
        return await response.json();
    }
    var authorizedUserId = await getAuthorizedUserId();

    if (authorizedUserId == commentFromDb.creator.id || authorizedUserId == articleOwnerId) {
        div.innerHTML = `
                <div id="${commentFromDb.id}" class="p-2 card bd-highlight">
                    <div class=d-flex flex-row>
                        <h2 class="me-2">${commentFromDb.creator.login}</h2>
                        <label class="text-center align-content-center me-2">${formatDate(commentFromDb.creationDate)}</label>
                        <form action="/${commentFromDb.id}?articleId=${commentFromDb.articleId}"
                              method="post">
                            <button id="${commentFromDb.id}" data-comment="deleteComment" class="button" type="submit" ><i class="fa-solid fa-trash"></i></button>    
                        </form>
                    </div>
                    <h4>${commentFromDb.content}</h4>
                </div>
            `;
        const deletebutton = div.querySelector('[data-comment="deleteComment"]');

        deletebutton.addEventListener('click', function () {
            connection.invoke('DeleteComment', commentFromDb.id).catch(function (err) {
                return console.error(err.toString());
            });
        });
    }
    else {
        div.innerHTML = `
                <div id="${commentFromDb.id}" class="p-2 card bd-highlight">
                    <div class=d-flex flex-row>
                        <h2 class="me-2">${commentFromDb.creator.login}</h2>
                        <label class="text-center align-content-center me-2">${formatDate(commentFromDb.creationDate)}</label>
                    </div>
                    <h4>${commentFromDb.content}</h4>
                </div>
            `;
    }
});

connection.on("DeleteComment", function (commentId) {
    const div = document.getElementById(commentId);
    div.remove();
});

connection.start().then(function () {
    var articleId = document.getElementById("articleId").value;
    connection.invoke("JoinGroup", articleId).catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
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

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear(),
        hour = '' + d.getHours(),
        minute = '' + d.getMinutes();

    if (hour.length < 2)
        hour = '0' + hour
    if (minute.length < 2)
        minute = '0' + minute
    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;


    var creationDate = [day, month, year].join('.') + ' в ' + [hour, minute].join(':');

    return creationDate;
}