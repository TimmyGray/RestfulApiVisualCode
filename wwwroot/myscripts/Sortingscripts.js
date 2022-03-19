const datesort = document.getElementById("Date");
const numbersort = document.getElementById("Number");
const devicesort = document.getElementById("Device");
const seriossort = document.getElementById("Serios");

numbersort.addEventListener("click", Sorting);
devicesort.addEventListener("click", Sorting);
seriossort.addEventListener("click", Sorting);
datesort.addEventListener("click", Sorting);


function Sorting(event) {

    const sort = event.currentTarget;
    const sorting = document.getElementById("Sorting");
    sorting.innerHTML = "";

    const val = document.createElement("option");
    val.textContent = sort.textContent;
    sorting.appendChild(val);

    const sortingdiv = document.getElementById("sortingdiv");
    sortingdiv.hidden = false;

    const closesorting1 = document.getElementById("closesorting1");
    closesorting1.hidden = false;
    closesorting1.addEventListener("click", function () {
        sortingdiv.hidden = true;
        closesorting1.hidden = true;
        const tempbody = document.querySelector("tbody");
        var trs = tempbody.children;
        for (var i = 0; i < trs.length; i++) {
            trs[i].hidden = false;
        }
    })

    const datediv = document.getElementById("datediv");
    datediv.hidden = true;

    if (sort.textContent == "Дата события") {
        sortingdiv.hidden = true;
        datediv.hidden = false;
        closesorting1.hidden = true;

        const closesorting2 = document.getElementById("closesorting2");
        closesorting2.hidden = false;
        closesorting2.addEventListener("click", function () {
            datediv.hidden = true;
            closesorting2.hidden = true;
            const tempbody = document.querySelector("tbody");
            var trs = tempbody.children;
            for (var i = 0; i < trs.length; i++) {
                trs[i].hidden = false;
            }
        })
        AddSortingDate();
    }
    if (sort.textContent == "Номер АСБ") {

        AddSortingASB(sorting);

    }
    if (sort.textContent == "Устройство") {

        AddSortingDevice(sorting);
    }
    if (sort.textContent == "Серьезность") {

        AddSortingSerios(sorting);
    }
    const tempbody = document.querySelector("tbody");
    var trs = tempbody.children;
    for (var i = 0; i < trs.length; i++) {
        trs[i].hidden = false;
    }

}

function AddSortingDate() {
    const yearselect = document.getElementById("yearselect");
    const monthselect = document.getElementById("monthselect");
    const currentdayselect = document.getElementById("currentdayselect");
    yearselect.addEventListener("change", SortingDate);
    monthselect.addEventListener("change", SortingDate);
    currentdayselect.addEventListener("change", SortingDate);


}

function AddSortingDate() {
    const yearselect = document.getElementById("yearselect");
    const monthselect = document.getElementById("monthselect");
    const currentdayselect = document.getElementById("currentdayselect");
    yearselect.addEventListener("change", SortingDate);
    monthselect.addEventListener("change", SortingDate);
    currentdayselect.addEventListener("change", SortingDate);

}

function AddSortingASB(sorting) {
    sorting.removeEventListener("change", SortingDevice);
    sorting.removeEventListener("change", SortingSerios);
    sorting.addEventListener("change", SortingASB);

    const firstoption = document.createElement("option");
    const thirddoption = document.createElement("option");
    const fourthoption = document.createElement("option");
    const fivesoption = document.createElement("option");
    const sixsoption = document.createElement("option");
    const sevensndoption = document.createElement("option");
    const eigthsoption = document.createElement("option");

    firstoption.textContent = "1";
    thirddoption.textContent = "3";
    fourthoption.textContent = "4";
    fivesoption.textContent = "5";
    sixsoption.textContent = "6";
    sevensndoption.textContent = "7";
    eigthsoption.textContent = "8";

    sorting.appendChild(firstoption);
    sorting.appendChild(thirddoption);
    sorting.appendChild(fourthoption);
    sorting.appendChild(fivesoption);
    sorting.appendChild(sixsoption);
    sorting.appendChild(sevensndoption);
    sorting.appendChild(eigthsoption);

}

function AddSortingDevice(sorting) {
    sorting.removeEventListener("change", SortingASB);
    sorting.removeEventListener("change", SortingSerios);
    sorting.addEventListener("change", SortingDevice);

    const ssloption = document.createElement("option");
    const soundcraftoption = document.createElement("option");
    const karreraoption = document.createElement("option");
    const micoption = document.createElement("option");
    const daletoption = document.createElement("option");
    const platinumoption = document.createElement("option");
    const lightoption = document.createElement("option");
    const decoroption = document.createElement("option");
    const camsoption = document.createElement("option");
    const graphicsoption = document.createElement("option");
    const computeroption = document.createElement("option");
    const otheroption = document.createElement("option");

    ssloption.textContent = "SSL";
    soundcraftoption.textContent = "Sound Craft";
    karreraoption.textContent = "Karrera";
    micoption.textContent = "Микрофон, мониторинг, приемопередатчик";
    daletoption.textContent = "Dalet";
    platinumoption.textContent = "Матрица Platinum";
    lightoption.textContent = "Свет и оборудование";
    decoroption.textContent = "Декорации";
    camsoption.textContent = "Камеры и камерные каналы";
    graphicsoption.textContent = "Графические станции";
    computeroption.textContent = "Компьютеры";
    otheroption.textContent = "Другое";

    sorting.appendChild(ssloption);
    sorting.appendChild(soundcraftoption);
    sorting.appendChild(karreraoption);
    sorting.appendChild(micoption);
    sorting.appendChild(daletoption);
    sorting.appendChild(platinumoption);
    sorting.appendChild(lightoption);
    sorting.appendChild(decoroption);
    sorting.appendChild(camsoption);
    sorting.appendChild(graphicsoption);
    sorting.appendChild(computeroption);
    sorting.appendChild(otheroption);

}

function AddSortingSerios(sorting) {
    sorting.removeEventListener("change", SortingDevice);
    sorting.removeEventListener("change", SortingASB);
    sorting.addEventListener("change", SortingSerios)

    const Seriosoption = document.createElement("option");
    const Notseriosoption = document.createElement("option");

    Seriosoption.textContent = "Серьезное";
    Notseriosoption.textContent = "Несерьезное";

    sorting.appendChild(Seriosoption);
    sorting.appendChild(Notseriosoption);
}

function SortingASB(e) {
    const tempitem = e.currentTarget;
    const tempbody = document.querySelector("tbody");
    var trs = tempbody.children;
    if (tempitem.selectedIndex != 0) {
        for (var i = 0; i < trs.length; i++) {
            trs[i].hidden = false;
            var temptds = trs[i].querySelector("td[name=nameofasb]");
            if (temptds.textContent != tempitem.options[tempitem.selectedIndex].textContent)
                trs[i].hidden = true;
        }
    }
    else {
        for (var i = 0; i < trs.length; i++) {
            trs[i].hidden = false;
        }
    }
}

function SortingDevice(e) {
    const tempitem = e.currentTarget;
    const tempbody = document.querySelector("tbody");
    var trs = tempbody.children;
    if (tempitem.selectedIndex != 0) {
        for (var i = 0; i < trs.length; i++) {
            trs[i].hidden = false;
            var temptds = trs[i].querySelector("td[name=nameofdevice]");
            if (temptds.textContent != tempitem.options[tempitem.selectedIndex].textContent)
                trs[i].hidden = true;
        }
    }
    else {
        for (var i = 0; i < trs.length; i++) {
            trs[i].hidden = false;
        }
    }
}

function SortingSerios(e) {
    const tempitem = e.currentTarget;
    const tempbody = document.querySelector("tbody");
    var trs = tempbody.children;
    if (tempitem.selectedIndex != 0) {
        for (var i = 0; i < trs.length; i++) {
            trs[i].hidden = false;
            var temptds = trs[i].querySelector("td[name=isserios]");
            if (temptds.textContent != `${tempitem.options[tempitem.selectedIndex].textContent} происшествие`)
                trs[i].hidden = true;
        }
    }
    else {
        for (var i = 0; i < trs.length; i++) {
            trs[i].hidden = false;
        }
    }
}

function SortingDate(e) {
    const currentselect = e.currentTarget;
    const tempbody = document.querySelector("tbody");
    var trs = tempbody.children;
    if (currentselect.id == "currentdayselect") {
        for (var i = 0; i < trs.length; i++) {
            trs[i].hidden = false;
            var temptds = trs[i].querySelector("td[name=dateofevent]");
            if (temptds.textContent != currentselect.value) {
                trs[i].hidden = true;
            }
        }
    }
    if (currentselect.selectedIndex != 0) {
        var tempoption = currentselect.options[currentselect.selectedIndex];
        if (currentselect.id == "yearselect") {
            for (var i = 0; i < trs.length; i++) {
                trs[i].hidden = false;
                var temptds = trs[i].querySelector("td[name=dateofevent]");

                if (!temptds.textContent.includes(tempoption.textContent)) {
                    trs[i].hidden = true;
                }

            }
        }
        if (currentselect.id == "monthselect") {
            var tempmonth;
            if (tempoption.textContent == "Январь") {
                tempmonth = "01";
            }
            if (tempoption.textContent == "Февраль") {
                tempmonth = "02";
            }
            if (tempoption.textContent == "Март") {
                tempmonth = "03";
            }
            if (tempoption.textContent == "Апрель") {
                tempmonth = "04";
            }
            if (tempoption.textContent == "Май") {
                tempmonth = "05";
            }
            if (tempoption.textContent == "Июнь") {
                tempmonth = "06";
            }
            if (tempoption.textContent == "Июль") {
                tempmonth = "07";
            }
            if (tempoption.textContent == "Август") {
                tempmonth = "08";
            }
            if (tempoption.textContent == "Сентябрь") {
                tempmonth = "09";
            }
            if (tempoption.textContent == "Октябрь") {
                tempmonth = "10";
            }
            if (tempoption.textContent == "Ноябрь") {
                tempmonth = "11";
            }
            if (tempoption.textContent == "Декабрь") {
                tempmonth = "12";
            }

            for (var i = 0; i < trs.length; i++) {
                trs[i].hidden = false;
                var temptds = trs[i].querySelector("td[name=dateofevent]");

                if (!temptds.textContent.slice(4, 7).includes(tempmonth)) {
                    trs[i].hidden = true;
                }

            }
        }

    }

    else {
        for (var i = 0; i < trs.length; i++) {
            trs[i].hidden = false;
        }
    }

}
