async function GetLogin() {
    const response = await fetch("/users/index", {
        method: "GET",
        headers: { "Accept": "application/json" },

    }
    );
    if (response.status === 200) {
        const log = await response.text();
        if (log == "") {
            window.location.href = "/authorize.html";
        }
        const CurrentUser = document.getElementById("CurrentUser");
        CurrentUser.textContent = log;
        
    }
    

}

const ExitLink = document.getElementById("ExitLink");
ExitLink.addEventListener("click", LogOut);

async function LogOut() {

    const response = await fetch("/users/logout", {
        method: "GET",
        headers: { "Accept": "application/json" },

    });
    if (response.ok === true) {
        window.location.href = "/authorize.html";
    }

}

GetLogin();

