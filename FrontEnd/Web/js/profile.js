document.addEventListener("DOMContentLoaded", async () => {
    const profileImageInput = document.querySelector("#profilePicInput");
    const profileImage = document.querySelector("#profileImage");
    const surName = document.querySelector("#surname");
    const firstName = document.querySelector("#firstname");
    const addressofUser = document.querySelector("#address");
    const phoneNumber = document.querySelector("#phonenumber");
    const ageOfUser = document.querySelector("#age");
    // const profilePics = document.querySelector("#profilePicInput");
    const genderOfUser = document.querySelector("#gender");
    const occupationOfUser = document.querySelector("#occupation");
    const fullnameNok = document.querySelector("#fullnamenok");
    const nokPhonenumber = document.querySelector("#nokphonenumber");
    const submitBtn = document.querySelector("#reg-btn");
    const form = document.querySelector("#form");




    form.addEventListener("submit", async (e) =>{
        console.log("Entered");
        e.preventDefault();
        const token = await getToken()
        console.log(token);
//formData should be used because JsonStrigify() wont work on file so formData should be created instead of object

        const formData = new FormData();
        formData.append("FirstName", firstName.value);
        formData.append("LastName", surName.value);
        formData.append("Age", ageOfUser.value);
        formData.append("Gender", genderOfUser.value);
        formData.append("Occupation", occupationOfUser.value);
        formData.append("Address", addressofUser.value);
        formData.append("PhoneNumber", phoneNumber.value);
        formData.append("FullNameOfNextOfKin", fullnameNok.value);
        formData.append("ContactOfNextOfKin", nokPhonenumber.value);

        //Adding profile image to formData
        console.log(profileImageInput);
        if(profileImageInput.files[0])
        {
            formData.append("ProfilePicture", profileImageInput.files[0]);
            console.log(profileImageInput.files[0])
            
        }
        const response = await fetch("https://localhost:7173/api/p/createprofile",{
            method: "POST",
            headers: {
                'Authorization':`Bearer ${token}`
            },
            body: formData,
        });
        console.log(`Bearer ${token}`);

        if(response.ok){
            const data = await response.json();
            console.log(data.data);
            location.href = "dashboard";
        }
        else
        {
            const errorData = await response.json();
            console.log(errorData.message)
        }

    })


    profileImageInput.addEventListener("change", function(){
        const file = this.files[0];
        if(file)
        {
            const reader = new FileReader();
            reader.onload = function(e){
                profileImage.src = e.target.result
            }

            reader.readAsDataURL(file)
        }
    })

})