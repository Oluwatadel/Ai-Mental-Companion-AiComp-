var BaseUrl = "https://localhost:7173/api"
let response = async (loginDetails) => {
    const resp = await fetch(completeUrl("auth/login"), {
        method: "POST",
                headers: {
                    'Content-Type': 'application/json',
                    // 'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiVG9iaSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6IkFkbWluIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjIwZTY4MTgzLTlhOTktNGM0My1hYjEzLTM5NTk5Mjk2NGI0ZCIsImV4cCI6MTcxNDk5NDcxOSwiaXNzIjoiaHR0cHM6Ly9hcGlhc3NpZ25tZW50LmNvbSIsImF1ZCI6Imh0dHBzOi8vYXBpYXNzaWdubWVudC5jb20ifQ.WxPXL6elxbB3BY8ZKTxce1ymzaZr6LnVP_WDDE6dDYY'
        },
        body: JSON.stringify(loginDetails),
    });
    console.log('Request sent');
    if(resp.ok)
    {
        const data = await resp.json();
        console.log(`Login successful`, data);
        return data;
    }

    else if(resp.status === 404)
    {
        const errorData = await resp.json();
        console.log('User not found', errorData.message);
        return errorData;
    }
    else if(resp.status === 401)
    {
        console.log('Unauthorized: Incorrect password');
        return await resp.json();
    }
    else
    {
        console.log('Login failed with status:', resp.status);
    }
}


let completeUrl =  (url) => {
    return `${BaseUrl}/${url}`
}

let emailCheck = async (email) =>{
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    return emailRegex.test(email);
}
