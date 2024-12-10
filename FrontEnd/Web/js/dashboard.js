document.addEventListener("DOMContentLoaded", async () => {
    const profile = document.querySelector("#profile");
    const mood = document.querySelector("#mood");
    const chat = document.querySelector("#chat");
    const logout = document.querySelector("#logout");
    const mainContent = document.querySelector("#main-content");
    const dashboardLink = document.querySelector(".sidebar a.active");
    const name = document.querySelector("#name");
    const profilePics = document.querySelector(".profile-photo img");
    const date = document.querySelector("#date");
    
    //=======================================Chat To get Mood========================================
    const chatWindow = document.querySelector(".window-chat");
    const token = await getToken();

    //===============================================================================================
    await dateTime(date);
    
    //======================================Name================================================
    let profileDetails = await getProfile();
    console.log(profileDetails.Data);
    console.log("reached");

    name.textContent = profileDetails.data.firstName;   

    //======================================Profile pics=========================================
    await getProfilePic(profilePics, token);
    //===========================================Profile================================================
    profile.addEventListener('click', async () =>{
        loadProfileTemplate('/Web/templates/profile-template.html');
                

    })

    //===============================================================================================
})

//=========================================================================================================
async function dateTime(date) {
    const newDate = new Date();
    // const formattedDate = 
    date.value = newDate.toISOString().split('T')[0]; // This will give you the date in the format of yyyy-MM-dd;
    console.log(newDate);
}
//=====================================================================================================================
async function getProfile(){
    let token = await getToken();
    console.log(token);
    const resp = await fetch(completeUrl("p/p"), {
        method: "GET",
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }            
    });
    const data = await resp.json();
    console.log(data);

    return data;
}


let populateProfileTemplate = async () => {
    
    const profileImage = document.querySelector("#profile-pics-img");
    const email = document.querySelector("#email");
    const phonenumber = document.querySelector("#phonenumber");
    const nokFullName = document.querySelector("#nok-fullName");
    const nokPhoneContact = document.querySelector("#nok-contact");
    const age = document.querySelector("#age");
    const gender = document.querySelector("#gender");
    const occupation = document.querySelector("#occupation");
    const address = document.querySelector("#address");
    
    let profile = await getProfile();

    profileImage.src = profile.Data.profilePics;
    email.textContent = profile.Data.email;
    phonenumber = profile.Data.phoneNumber;
    nokFullName = profile.Data.fullNameNOK;
    nokPhoneContact = profile.Data.contactOfNOK
    age = profile.Data.age;
    gender = profile.Data.gender;
    occupation = profile.Data.occupation
    address = profile.Data.address

}
//====================================================Load Profile template to the dashBoard=================================
async function loadProfileTemplate(templatePath)
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
            dateTime(date);
        })
        .catch(error => console.error('Errorloading Template', error));
}
//=======================================================Load Chat================================================================

let getToken = async () => {
    const token = localStorage.getItem('jwt');
    return token;
}
//============================================================================================================================

async function getProfilePic(profilepic, token){
    console.log(token);
    const resp = await fetch(completeUrl("p/profilephoto"), {
        method: "GET",
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }            
    });

    if(resp.ok)
    {
        const imageBlob = await resp.blob();
        const imageUrl = await URL.createObjectURL(imageBlob);
        console.log(imageUrl);
        profilepic.src = imageUrl;
    }
    else
    {
        console.error('Failed to fetch image');
    }
}

//==================================================================Start of the Day===================================================
var startOfDay = async () => {
    var today = new Date().toISOString().split('T')[0];
    
}