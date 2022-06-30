
const regbut = document.getElementById("regbut");
const logbut = document.getElementById("logbut");

regbut.addEventListener("click", RegButClick);

logbut.addEventListener("click", LogButClick);

function RegButClick() {
    const headerh1 = document.getElementById("headerh1");
    const passrep = document.getElementById("passrep");

    if (regbut.textContent=="Регистрация") {
        headerh1.textContent = "Регистрация";

        passrep.hidden = false;

        regbut.textContent = "Авторизация";
        logbut.textContent = "Зарегистрироваться";

    }
    else if (regbut.textContent == "Авторизация") {
        headerh1.textContent = "Авторизация";

        passrep.hidden = true;

        regbut.textContent = "Регистрация";
        logbut.textContent = "Войти";
    }
}

async function LogButClick() {
    if (logbut.textContent == "Войти") {
        const loginform = document.forms["loginform"];
        const user = loginform.elements["logininput"].value;
        const pass = loginform.elements["passinput"].value;

        if (user != "" && pass != "") {
            const response = await fetch("/users/login", {
                method: "POST",
                headers: { "Accept": "application/json", "Content-Type": "application/json" },
                body: JSON.stringify({
                    login: user,
                    password: pass
                })
                
            });

            if (response.ok === true) {

                window.location.href = "/clientview.html";
 
            }

        }
        
        else {
            alert("Некорректные данные!");
        }
        
    }

    else if (logbut.textContent == "Зарегистрироваться") {
        const loginform = document.forms["loginform"];
        const user = loginform.elements["logininput"].value;
        const pass = loginform.elements["passinput"].value;
        const passrep = loginform.elements["passrep"].value;
        alert(pass);

        if (user != "" && pass != "" && pass === passrep) {
            const response = await fetch("/users/register", {
                method: "POST",
                headers: { "Accept": "application/json", "Content-Type": "application/json" },
                body: JSON.stringify({
                    login: user,
                    password: pass
                })

            });

            if (response.ok === true) {

                const CurrentUser = document.getElementById("CurrentUser");
                CurrentUser.textContent = user;
                alert(user);

            }

        }

        else {
            alert("Некорректные данные!");
        }

    }
}

