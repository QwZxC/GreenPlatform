"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/comment-hub").build();
document.getElementById("sendButton").disabled = true;
const articleOwnerId = document.getElementById("ownerId").value;
const commentList = document.getElementById("commentsList")

connection.on("JoinGroup", async function (comments) {
    for (let i = 0; i < comments.length; i++) {
        var div = document.createElement("div");
        div.setAttribute('class', 'd-flex flex-column bd-highlight mb-3')
        document.getElementById("commentsList").appendChild(div);
        await createComment({ comment: comments[i], div });
    }
});

connection.on("ReceiveMessage", async function (commentFromDb) {
    var div = document.createElement("div");
    div.setAttribute('class', 'd-flex flex-column bd-highlight mb-3')
    commentList.insertBefore(div, commentList.firstChild);
    await createComment({ comment: commentFromDb, div });
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

async function getAuthorizedUserId() {
    const response = await fetch('/Account/GetAuthorizedUserId');
    return await response.json();
}

async function getUserAvatarPath(userId) {
    const response = await fetch(`/api/Images/${userId}`);
    return await response.text();
}

async function createComment({ comment, div }) {
    const authorizedUserId = await getAuthorizedUserId();
    const userAvatarPath = await getUserAvatarPath(comment.creator.id);
    if (authorizedUserId == comment.creator.id || authorizedUserId == articleOwnerId) {
        div.innerHTML = `
                <div id="${comment.id}" class="p-2 card bd-highlight">
                    <div class="d-flex flex-row justify align-content-start">
                        <img src="../image/${userAvatarPath}" class="image-comment card-img-top img-cover p-3" alt="Raeesh"/>
                        <div>
                            <a class="fs-5 link-secondary link-offset-2 link-offset-3-hover link-underline link-underline-opacity-0 link-underline-opacity-75-hover" href="/Account/PersonalAccount?login=${ comment.creator.login}">${comment.creator.login}</a>
                            <label class="text-center align-content-center me-2">${formatDate(comment.creationDate)}</label>
                        </div>
                        <form action="/${comment.id}?articleId=${comment.articleId}"
                              method="post"
                              class="ms-auto">
                            <button id="${comment.id}" data-comment="deleteComment" class="button" type="submit" ><i class="fa-solid fa-trash"></i></button>    
                        </form>
                    </div>
                    <h4>${comment.content}</h4>
                </div>
            `;
        const deletebutton = div.querySelector('[data-comment="deleteComment"]');

        deletebutton.addEventListener('click', function () {
            connection.invoke('DeleteComment', comment.id).catch(function (err) {
                return console.error(err.toString());
            });
        });
    }
    else {
        div.innerHTML = `
                <div id="${comment.id}" class="p-2 card bd-highlight">
                    <div class=d-flex flex-row>
                        <img src="../image/${userAvatarPath}" class="image-comment card-img-top img-cover p-3" alt="Raeesh"/>
                        <div class="align-content-center">
                            <a class="fs-5 link-secondarylink-offset-2 link-offset-3-hover link-underline link-underline-opacity-0 link-underline-opacity-75-hover" href="/Account/PersonalAccount?login=${ comment.creator.login}">${comment.creator.login}</a>
                            <label class="text-center align-content-center me-2">${formatDate(comment.creationDate)}</label>
                        </div>
                    </div>
                    <h4>${comment.content}</h4>
                </div>
            `;
    }
}

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