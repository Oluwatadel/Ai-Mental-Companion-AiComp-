document.addEventListener("DOMContentLoaded", ()=>{
    const profileImageInput = document.querySelector("#profilePicInput");
    const profileImage = document.querySelector("#profileImage");

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