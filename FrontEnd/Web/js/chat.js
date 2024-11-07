//=================================onclick companion load chat===================================
const chatTemplate = "Web/templates/chat.html";
const chatWindow = document.querySelector(".window-chat");

chat.addEventListener("click", async() => {
    loadChat(chatTemplate);

})


let loadChat = async (templatePath) =>
    {
        const container = document.querySelector(".container");
        const main = document.querySelector("main");
        const right = document.querySelector(".right");
    
        fetch(templatePath)
            .then(response => response.text())
            .then(html => {
                container.style.gridTemplateColumns = "14rem auto";
                main.innerHTML = "";
                right.remove();
                main.innerHTML = html;
                // dateTime(date);
            })
            .catch(error => console.error('Errorloading Template', error));
    }
    //========================================================fetch chats===============================================================
    let fetchChats = async(url) =>
    {

    }