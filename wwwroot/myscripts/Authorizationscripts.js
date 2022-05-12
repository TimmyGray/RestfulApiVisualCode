async function GetLogin() {
    const response = await fetch("/clientview", {
        method: "GET",
        headers: { "Accept": "application/json" },
        
    }
    );
    if (response.ok === true) {
        const log = await response.text();
        const CurrentUser = document.getElementById("CurrentUser");
        CurrentUser.textContent = log;
        curlogin = log;
    }
}
var curlogin;

GetLogin();
