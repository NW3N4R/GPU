
function OntabClick(x) {
    var tab1 = document.getElementById("pivot-tab1");
    var tab2 = document.getElementById("pivot-tab2");
    var tab3 = document.getElementById("pivot-tab3");
    var tab4 = document.getElementById("pivot-tab4");
    var tab5 = document.getElementById("pivot-tab5");
    var tab6 = document.getElementById("pivot-tab6");
    clearActive();
    if (x == 1) {
        tab1.classList.add('ActiveBG');
    }
    else if (x == 2) {

        tab2.classList.add('ActiveBG');
    }
    else if (x == 3) {

        tab3.classList.add('ActiveBG');
    }
    else if (x == 4) {

        tab4.classList.add('ActiveBG');
    }
    else if (x == 5) {

        tab5.classList.add('ActiveBG');
    }
    else if (x == 6) {

        tab6.classList.add('ActiveBG');
    }
    else {

        tab7.classList.add('ActiveBG');
    }
    document.getElementById(1).classList.remove('active');
    document.getElementById(2).classList.remove('active');
    document.getElementById(3).classList.remove('active');
    document.getElementById(4).classList.remove('active');
    document.getElementById(5).classList.remove('active');
    document.getElementById(6).classList.remove('active');
    document.getElementById(x).classList.add('active');


}
function clearActive() {
    document.getElementById("pivot-tab1").classList.remove('ActiveBG');
    document.getElementById("pivot-tab2").classList.remove('ActiveBG');
    document.getElementById("pivot-tab3").classList.remove('ActiveBG');
    document.getElementById("pivot-tab4").classList.remove('ActiveBG');
    document.getElementById("pivot-tab5").classList.remove('ActiveBG');
    document.getElementById("pivot-tab6").classList.remove('ActiveBG');
}


function Acceptance() {
    var div = document.getElementById("InvoicePart");
    var selction = document.getElementById("AcceptanceSelection");
    if (selction.value == 'زانکۆلاین') {
        div.classList.add('hidden');
    }
    else {

        div.classList.remove('hidden');
    }
}