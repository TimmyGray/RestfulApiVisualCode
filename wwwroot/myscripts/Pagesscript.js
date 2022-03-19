function PageClick(e) {
    if (document.getElementById("SubHeader").textContent != e.currentTarget.textContent)
        GetPage(e.currentTarget.textContent);

}

function AddLinks(page, ullink) {
    const links = document.createElement("li");
    const linka = document.createElement("a");
    linka.className = "nav-link faq-minilink";
    linka.textContent = page.subheader;
    linka.addEventListener("click", PageClick);
    links.appendChild(linka);
    ullink.appendChild(links);
}


async function GetPages() {
    const response = await fetch("/api/pages", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const pages = await response.json();
        const uls = document.getElementsByClassName("nav collapse faq-ul-minilink-collapse");
        const textpage = document.getElementById("ThirdPage");
        for (let i = 0; i < uls.length; i++) {
            uls[i].addEventListener("click", function () {

            });
            for (let j = 0; j < pages.length; j++) {
                if (uls[i].id == "CamsLinks") {
                    if (pages[j].header == "Камеры, CCU и OCP") {
                        AddLinks(pages[j], uls[i]);
                    }
                }
                else if (uls[i].id == "VideoMixerLinks") {
                    if (pages[j].header == "Видеопульт") {
                        AddLinks(pages[j], uls[i]);
                    }
                }
                else if (uls[i].id == "SoundMixerLinks") {
                    if (pages[j].header == "Звуковые пульты") {
                        AddLinks(pages[j], uls[i]);
                    }
                }
                else if (uls[i].id == "MatrixLinks") {
                    if (pages[j].header == "Матрица") {
                        AddLinks(pages[j], uls[i]);
                    }
                }
                else if (uls[i].id == "SoundDeviceLinks") {
                    if (pages[j].header == "Звуковое оборудование") {
                        AddLinks(pages[j], uls[i]);
                    }
                }
                else if (uls[i].id == "ComputerDeviceLinks") {
                    if (pages[j].header == "Компьютеры, Графические станции, Dalet") {
                        AddLinks(pages[j], uls[i]);
                    }
                }
                else if (uls[i].id == "LightDeviceLinks") {
                    if (pages[j].header == "Световое оборудование") {
                        AddLinks(pages[j], uls[i]);
                    }
                }
                else if (uls[i].id == "StudioLinks") {
                    if (pages[j].header == "Студия") {
                        AddLinks(pages[j], uls[i]);
                    }
                }

            }
        }
    }
}

const navitems = document.getElementsByClassName("navitem");
for (var n = 0; n < navitems.length; n++) {
    navitems[n].addEventListener("click", Activelink);
}

function Activelink(e) {
    for (var k = 0; k < navitems.length; k++) {
        navitems[k].className = "navitem";
        navitems[k].firstElementChild.className = "faqlink softredtext";
    }
    const InfoForm = document.getElementById("InfoForm");
    if (InfoForm.className != "hide-form") {
        InfoForm.className = "hide-form";
    }
    const curlink = e.currentTarget;
    curlink.className = "navitem activenavitem";
    curlink.firstElementChild.className = "faqlink darkredtext";
    const thirdpage = document.getElementById("ThirdPage");
    if (thirdpage.textContent != curlink.firstElementChild.textContent) {
        thirdpage.textContent = curlink.firstElementChild.textContent;
        const subheaderpage = document.getElementById("SubHeader");
        subheaderpage.textContent = "";
        const textpage = document.getElementById("PageText");
        textpage.textContent = "";
    }
    

    if (curlink.firstElementChild.id == "AddInfo") {
        InfoForm.className = "";

    }
}

async function GetPage(subheader) {
    const response = await fetch("/api/pages/" + subheader, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const page = await response.json();
        const headertext = document.getElementById("ThirdPage");
        const subheadertext = document.getElementById("SubHeader");
        const textarea = document.getElementById("PageText");

        subheadertext.textContent = page.subheader;
        textarea.textContent = page.info;

    }
}


const infoform = document.getElementById("InfoForm");
infoform.addEventListener("submit", e => {
    e.preventDefault();
    const headervalue = document.getElementById("HeaderValue").value;
    const subheadervalue = document.getElementById("SubheaderValue").value;
    const infovalue = document.getElementById("InfoValue").value;
    CreatePage(headervalue, subheadervalue, infovalue);
});

async function CreatePage(pageheader, pagesubheader, pageinfo) {
    const response = await fetch("/api/pages", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            header: pageheader,
            subheader: pagesubheader,
            info: pageinfo
        })

    });
    if (response.ok === true) {
        if (pageheader == "Камеры, CCU и OCP") {
            const ullink = document.getElementById("CamsLinks");
            NewPage(pagesubheader, ullink);
        }
        if (pageheader == "Видеопульт") {
            const ullink = document.getElementById("VideoMixerLinks");
            NewPage(pagesubheader, ullink);
        }
        if (pageheader == "Звуковые пульты") {
            const ullink = document.getElementById("SoundMixerLinks");
            NewPage(pagesubheader, ullink);
        }
        if (pageheader == "Матрица") {
            const ullink = document.getElementById("MatrixLinks");
            NewPage(pagesubheader, ullink);
        }
        if (pageheader == "Звуковое оборудование") {
            const ullink = document.getElementById("SoundDeviceLinks");
            NewPage(pagesubheader, ullink);
        }
        if (pageheader == "Компьютеры, Графические станции, Dalet") {
            const ullink = document.getElementById("ComputerDeviceLinks");
            NewPage(pagesubheader, ullink);
        }
        if (pageheader == "Световое оборудование") {
            const ullink = document.getElementById("LightDeviceLinks");
            NewPage(pagesubheader, ullink);
        }
        if (pageheader == "Студия") {
            const ullink = document.getElementById("StudioLinks");
            NewPage(pagesubheader, ullink);
        }
        alert("Статья создана");
    }
}

function NewPage(subheader, pagelink) {
    const newa = document.createElement("a");
    const newlink = document.createElement("li");
    newa.textContent = subheader;
    newa.className = "nav-link faq-minilink";
    newa.addEventListener("click", PageClick);
    newlink.appendChild(newa);
    pagelink.appendChild(newlink);

}