const url = "https://localhost:7034/api";


function login()
{
    const userNameElement = document.getElementById('userName');
    const passwordElement = document.getElementById('password');
    const loginResultElement = document.getElementById('login-result');
    if(!userNameElement) return;
    if(!passwordElement) return;
    if(!loginResultElement) return;

    const loginModel = {
        userName: userNameElement.value.trim(),
        password: passwordElement.value.trim(),
      };
    
      fetch(`${url}/User/Login`, {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify(loginModel),
      })
      .then((response) => response.json())
        .then((data) =>
        {
            if(data.isSuccess)
            {
                loginResultElement.textContent = "Login successfully";
                localStorage.setItem("devblog_token", data.data);
            }
        });
}