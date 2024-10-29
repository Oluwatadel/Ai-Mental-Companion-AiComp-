document.addEventListener("DOMContentLoaded", (e) => {
    
    let loginUserName = document.querySelector(".input100.email");
    let email = localStorage.getItem('registeredEmail');
    console.log(email)
    if(email)
    {
        loginUserName.value = email;
        localStorage.removeItem('registeredEmail'); 
    }
    else
    {
        console.log("No email in the storage");
    }

    let loginForm = document.querySelector(".login100-form.validate-form");
    let loginPassword = document.querySelector(".input100.password");
    let registerBTN = document.querySelector("#create-account");

    if(loginForm)
    {
        loginForm.addEventListener("submit", async (e) => {
            e.preventDefault();
            let loginObj = {
                email: `${loginUserName.value}`,
                password: `${loginPassword.value}`
            }
            console.log("Form submitted")
            const shareData = await response(loginObj);
            console.log(shareData);

            //Check if the user has a profile
            if(!shareData.profile){
                console.log("Profile is null, redirecting to profile creation.");
                window.location.href = '/create-profile.html';  // Redirect to the profile creation page                
            } 
            else
            {
                console.log("Profile exists, redirecting to dashboard.");
                localStorage.removeItem('jwt');
                localStorage.setItem('jwt', shareData.data.accessToken.result);
                // console.log(`${shareData.data.accessToken.result}`);
                location.href ='/dashboard.html';
                
            }

        })
        
    }

    else
    {
        console.error("Login form not found");
    }

    registerBTN.addEventListener('click', (e) => {
        e.preventDefault();
        window.location.href = "/Web/registration.html"
    })
})



    // var response = localStorage.getItem("response")

    // console.log(JSON.parse(response))
    // console.log("entered")





