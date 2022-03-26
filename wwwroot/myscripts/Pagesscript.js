function PageClick(e) {
    if (document.getElementById("SubHeader").textContent != e.currentTarget.textContent) {
        const subheadertext = document.getElementById("SubHeader");
        const textarea = document.getElementById("PageText");
        GetPage(e.currentTarget.textContent, subheadertext, textarea);
    }

}

function AddLinks(page, ullink) {
    const links = document.createElement("li");
    const linka = document.createElement("a");
    linka.className = "nav-link faq-minilink";
    linka.textContent = page.subheader;
    linka.addEventListener("click", PageClick);
    links.appendChild(linka);
    ullink.appendChild(links);
    ullink.setAttribute("data-rowid", page.pageId);
}


async function GetPages() {
    const response = await fetch("/api/pages", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const pages = await response.json();
        const uls = document.getElementsByClassName("nav collapse faq-ul-minilink-collapse");
        for (let i = 0; i < uls.length; i++) {
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
    const curlink = e.currentTarget;
    const thirdpage = document.getElementById("ThirdPage");
    if (thirdpage.textContent != curlink.firstElementChild.textContent) {
        for (var k = 0; k < navitems.length; k++) {
            navitems[k].className = "navitem";
            navitems[k].firstElementChild.className = "faqlink softredtext";
        }
        curlink.className = "navitem activenavitem";
        curlink.firstElementChild.className = "faqlink darkredtext";
        thirdpage.textContent = curlink.firstElementChild.textContent;
        const subheaderpage = document.getElementById("SubHeader");
        subheaderpage.textContent = "";
        const textpage = document.getElementById("PageText");
        textpage.textContent = "";
        const InfoForm = document.getElementById("InfoForm");
        const UpdateDiv = document.getElementById("UpdateDiv");
        if (InfoForm.className != "hide-form") {
            InfoForm.className = "hide-form";
        }
        if (UpdateDiv.className != "hide-form") {
            UpdateDiv.className = "hide-form";
        }

        if (curlink.firstElementChild.id == "AddInfo") {
            InfoForm.className = "";

        }

        if (curlink.firstElementChild.id == "UpdateInfo") {
            UpdateDiv.className = "";
            const headerselect = document.getElementById("HeaderUpdate");
            headerselect.addEventListener("change", HeaderSelect);
        }
    }

    
}

function HeaderSelect(e) {
    const headeroption = e.currentTarget;
    const subheaderselect = document.getElementById("SubHeaderUpdate");
    if (headeroption.options[headeroption.selectedIndex].textContent == "Камеры, CCU и OCP") {
        AddSubHeaderOption(subheaderselect, "CamsLinks");
    }
    if (headeroption.options[headeroption.selectedIndex].textContent == "Видеопульт") {
        AddSubHeaderOption(subheaderselect, "VideoMixerLinks");
    }
    if (headeroption.options[headeroption.selectedIndex].textContent == "Звуковые пульты") {
        AddSubHeaderOption(subheaderselect, "SoundMixerLinks");
    }
    if (headeroption.options[headeroption.selectedIndex].textContent == "Матрица") {
        AddSubHeaderOption(subheaderselect, "MatrixLinks");
    }
    if (headeroption.options[headeroption.selectedIndex].textContent == "Звуковое оборудование") {
        AddSubHeaderOption(subheaderselect, "SoundDeviceLinks");
    }
    if (headeroption.options[headeroption.selectedIndex].textContent == "Компьютеры, Графические станции, Dalet") {
        AddSubHeaderOption(subheaderselect, "ComputerDeviceLinks");
    }
    if (headeroption.options[headeroption.selectedIndex].textContent == "Световое оборудование") {
        AddSubHeaderOption(subheaderselect, "LightDeviceLinks");
    }
    if (headeroption.options[headeroption.selectedIndex].textContent == "Студия") {
        AddSubHeaderOption(subheaderselect, "StudioLinks");
    }
  

    subheaderselect.classList.remove("hide-form");
    subheaderselect.addEventListener("change", SubHeaderSelect);

}

function AddSubHeaderOption(subheaderselect, ulsid) {
    subheaderselect.innerHTML = "";
    const defaultsuboption = document.createElement("option");
    defaultsuboption.textContent = "Выбор статьи...";
    subheaderselect.appendChild(defaultsuboption);
    const uls = document.getElementById(ulsid);
    for (var i = 0; i < uls.childElementCount; i++) {
        const subheaderoption = document.createElement("option");
        subheaderoption.textContent = uls.children.item(i).textContent;
        subheaderselect.appendChild(subheaderoption);
    }
}

function SubHeaderSelect(e) {
    const cur = e.currentTarget;
    const cursub = cur.options[cur.selectedIndex];
    const headerselect = document.getElementById("HeaderUpdate");
    const curhead = headerselect.options[headerselect.selectedIndex].textContent;
    const textupdate = document.getElementById("TextUpdate");
    const butupdate = document.getElementById("ButUpdate");
    GetPage(cursub.textContent, null, textupdate);
    textupdate.classList.remove("hide-form");
    butupdate.classList.remove("hide-form");
    butupdate.addEventListener("click", function () {
        EditPage(curhead, cursub.textContent, textupdate);
    });

}

async function EditPage(pageheader, pagesubheader, textupdate) {
    const pageid = textupdate.getAttribute("num");
    alert(pageid);
    const response = await fetch("/api/pages", {
        method: "PUT",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            pageId: parseInt(pageid,10),
            header: pageheader,
            subheader: pagesubheader,
            info: textupdate.value
        })
    });
    if (response.ok === true) {
        const updpage = await response.json();
        alert(updpage.pageId);
        alert("Статья дополнена");
    }
}


async function GetPage(subheader,subheadertext,textarea) {
    const response = await fetch("/api/pages/" + subheader, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const page = await response.json();
        if (subheadertext != null) {
            subheadertext.textContent = page.subheader;
        }
        else if (subheadertext == null) {
            const textupdate = document.getElementById("TextUpdate");
            textupdate.setAttribute("num", page.pageId);
        }
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
        const newpage = await response.json();
        if (pageheader == "Камеры, CCU и OCP") {
            const ullink = document.getElementById("CamsLinks");
            AddLinks(newpage, ullink);
        }
        if (pageheader == "Видеопульт") {
            const ullink = document.getElementById("VideoMixerLinks");
            AddLinks(newpage, ullink);
        }
        if (pageheader == "Звуковые пульты") {
            const ullink = document.getElementById("SoundMixerLinks");
            AddLinks(newpage, ullink);
        }
        if (pageheader == "Матрица") {
            const ullink = document.getElementById("MatrixLinks");
            AddLinks(newpage, ullink);
        }
        if (pageheader == "Звуковое оборудование") {
            const ullink = document.getElementById("SoundDeviceLinks");
            AddLinks(newpage, ullink);
        }
        if (pageheader == "Компьютеры, Графические станции, Dalet") {
            const ullink = document.getElementById("ComputerDeviceLinks");
            AddLinks(newpage, ullink);
        }
        if (pageheader == "Световое оборудование") {
            const ullink = document.getElementById("LightDeviceLinks");
            AddLinks(newpage, ullink);
        }
        if (pageheader == "Студия") {
            const ullink = document.getElementById("StudioLinks");
            AddLinks(newpage, ullink);
        }
        alert("Статья создана");
    }
}



