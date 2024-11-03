document.addEventListener("DOMContentLoaded", ()=>{
    const profileImageInput = document.querySelector("#profilePicInput");
    const profileImage = document.querySelector("#profileImage");
    const surName = document.querySelector("#surname");
    const firstName = document.querySelector("#firstname");
    const addressofUser = document.querySelector("#address");
    const phoneNumber = document.querySelector("#phonenumber");
    const ageOfUser = document.querySelector("#age");
    const profilePics = document.querySelector("#profilePicInput");
    const genderOfUser = document.querySelector("#gender");
    const occupationOfUser = document.querySelector("#occupation");
    const fullnameNok = document.querySelector("#fullnamenok");
    const nokPhonenumber = document.querySelector("#nokphonenumber");
    const submitBtn = document.querySelector("#reg-btn");
    const form = document.querySelector("#form");



    form.addEventListener("submit", async () =>{
        let token = await getToken()
        let profileCreateObj = {
            firstName : firstName.value,
            lastName : surName.value,
            Age : ageOfUser.value,
            gender : genderOfUser.value,
            occupation : occupationOfUser.value,
            address : addressofUser.value,
            pnoneNumber : phoneNumber.value,
            fullNameOfNextOfKin : fullnameNok.value,
            contactOfNextOfKin : nokPhonenumber.value,
            profilePic : profileImageInput.value
        }
        console.log(profileCreateObj)
        const response = fetch("https://localhost:7173/api/p/createprofile", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                'Authorization':`Bearer${token}`
            },
            body: JSON.stringify(profileCreateObj)
        });
        console.log("enter");

        const data = await response.json();
        if(data.status == "success"){
            location.href = "dashboard";
        }
        else
        {
            console.error(data.message)
        }
    })









    profileImageInput.addEventListener("change", function(){
        const file = this.files[0];
        if(file)
        {
            const reader = new FileReader();
            reader.onload = function(e){
                profileImage.src = e.target.result
                console.log(e.target.result)
            }

            reader.readAsDataURL(file)
        }
    })

})