    document.addEventListener("DOMContentLoaded", (e) => {
        let loginForm = document.querySelector(".login100-form.validate-form");
        let loginPassword = document.querySelector(".input100.password");
        let submitBtn = document.querySelector(".login100-form-btn");
        let registerBTN = document.querySelector("#create-account");
                
        let loginUserName = document.querySelector(".input100.email");
        let email = localStorage.getItem('registeredEmail');
        if(email)
        {
            console.log(email);
            loginUserName.value = email;
            localStorage.removeItem('registeredEmail'); 
        }
        else
        {
            console.log("No email in the storage");
        }

        loginUserName.addEventListener('input', () =>{
            const emailValue = loginUserName.value;
            const emailError = document.querySelector("#emailError");

            if(!validateEmail(emailValue)){
                emailError.textContent = "Enter email in the format abc@xyz.com";
                emailError.style.color = "red";
                emailError.style.display = "block";
                submitBtn.disabled = true;
            }
            else
            {
                emailError.textContent = "";
                emailError.style.display = "none";
                submitBtn.disabled = false;

            }
        });
        
        if(loginForm)
            {
                loginForm.addEventListener("submit", async (e) => {
                    e.preventDefault();
                    
                    let loginObj = {
                        email: `${loginUserName.value}`,
                        password: `${loginPassword.value}`
                    }
                    const shareData = await response(loginObj);
                    console.log("Form submitted")
                    console.log(shareData);
        
                    //Check if the user has a profile
                    if(!shareData.profile){
                        console.log("Profile is null, redirecting to profile creation.");
                        localStorage.removeItem('jwt');
                        localStorage.setItem('jwt', shareData.data.accessToken.result);
                        location.href = '/Web/create-profile.html';  // Redirect to the profile creation page                
                    } 
                    else
                    {
                        console.log("Profile exists, redirecting to dashboard.");
                        localStorage.removeItem('jwt');
                        localStorage.setItem('jwt', shareData.data.accessToken.result);
                        // console.log(`${shareData.data.accessToken.result}`);
                        location.href ='/dashboard.html';
                        
                    }
        
                });
                
            }
        
            else
            {
                console.error("Login form not found");
            }
        
            registerBTN.addEventListener('click', (e) => {
                e.preventDefault();
                window.location.href = "/Web/registration.html"
            });
    })

    let validateEmail = (email) =>{
        const regexCheck = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return regexCheck.test(email);
    }



        // var response = localStorage.getItem("response")

        // console.log(JSON.parse(response))
        // console.log("entered")





