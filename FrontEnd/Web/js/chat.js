//=================================onclick companion load chat===================================
const chatTemplate = "Web/templates/chat.html";
const chatWindow = document.querySelector(".window-chat");
const url = "https://localhost:7173/api/chat"


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
        const leftChatImg = document.querySelector(".left-chatImg img");
        const leftMessage = document.querySelector(".left-chatImg p");
        const rightChatImg = document.querySelector(".right-chatImg img");
        const rightMessage = document.querySelector(".right-chatImg p");

        const resp = fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        });

        const data = await resp.json();

        if(resp.ok)
        {
            console.log(data.data);
        }
        else
        {
            console.log(data.message);
        }
    }