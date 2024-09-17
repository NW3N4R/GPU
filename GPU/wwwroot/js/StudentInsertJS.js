document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('infoForm');
    const divTab1 = document.getElementById('tab1');
    const divTab2 = document.getElementById('tab2');
    const divTab3 = document.getElementById('tab3');
    const divTab4 = document.getElementById('tab4');
    const divTab5 = document.getElementById('tab5');
    const Savebttn = document.getElementById('SaveStudentBttn');
    const printbttn = document.getElementById('PrintStudentBttn');

    const tabs = Array.from(document.querySelectorAll('.pivot-tab'));
    const contents = Array.from(document.querySelectorAll('.pivot-content'));
    let currentIndex = tabs.findIndex(tab => tab.classList.contains('active'));

    function updateActiveTab(index) {
        tabs.forEach((tab, i) => {
            const content = document.querySelector(tab.getAttribute('href'));
            if (i === index) {
                tab.classList.add('active');
                content.classList.add('active');
            } else {
                tab.classList.remove('active');
                content.classList.remove('active');
            }
        });
    }

    function getNextEnabledIndex(startIndex) {
        let index = startIndex;
        do {
            index = (index + 1) % tabs.length;
        } while (tabs[index].classList.contains('disabled') && index !== startIndex);
        return index;
    }

    function checkDivValidity(div) {
        const inputs = div.querySelectorAll('input, select');
        return Array.from(inputs)
            .filter(input => input.id !== 'skip')
            .every(input => input.value.trim() !== '');
    }

    function handleTabValidity() {
        const isTab1Valid = checkDivValidity(divTab1); // Check entire form for tab1
        const isTab2Valid = checkDivValidity(divTab2);
        const isTab3Valid = checkDivValidity(divTab3);
        const isTab4Valid = checkDivValidity(divTab4);
        const isTab5Valid = checkDivValidity(divTab5);

        const tab2 = document.getElementById('pivot-tab2');
        const tab3 = document.getElementById('pivot-tab3');
        const tab4 = document.getElementById('pivot-tab4');
        const tab5 = document.getElementById('pivot-tab5');

        if (isTab1Valid) {
            tab2.classList.remove('disabled');
        } else {
            tab2.classList.add('disabled');
            tab3.classList.add('disabled');
            tab4.classList.add('disabled');
            tab5.classList.add('disabled');
            return;
        }

        if (isTab2Valid) {
            tab3.classList.remove('disabled');
        } else {
            tab3.classList.add('disabled');
            tab4.classList.add('disabled');
            tab5.classList.add('disabled');
            return;
        }

        if (isTab3Valid) {
            tab4.classList.remove('disabled');
        } else {
            tab4.classList.add('disabled');
            tab5.classList.add('disabled');
            return;
        }

        if (isTab4Valid) {
            tab5.classList.remove('disabled');
        } else {
            tab5.classList.add('disabled');
        }

        if (isTab5Valid) {
            Savebttn.classList.add('saveenabled');
            printbttn.classList.add('printEnabled');
            printbttn.disabled = false;
            Savebttn.disabled = false;
        } else {
            Savebttn.classList.remove('saveenabled');
            printbttn.classList.remove('printEnabled');
            Savebttn.disabled = true;
            printbttn.disabled = true;
        }
    }

    form.addEventListener('input', handleTabValidity);

    // Initialize the default active tab
    if (currentIndex === -1) {
        currentIndex = 0;
    }
    updateActiveTab(currentIndex);

    // Handle tab switching
    tabs.forEach(tab => {
        tab.addEventListener('click', function (e) {
            e.preventDefault();
            const index = tabs.indexOf(this);
            if (index !== -1) {
                updateActiveTab(index);
            }
        });
    });

    // Handle next enabled tab button
    document.getElementById('nextTabButton').addEventListener('click', function () {
        currentIndex = getNextEnabledIndex(currentIndex);
        updateActiveTab(currentIndex);
    });
});





$(document).ready(function () {
    $('#AcceptanceTypeSelect').on('change', function () {
        var selectedValue = $(this).val();
        if (selectedValue === 'زانکۆلاین') {
            $('#InvoiceDiv').hide();
        } else {
            $('#InvoiceDiv').show();
        }
    });

    // Initially check the selected value on page load
    if ($('#AcceptanceTypeSelect').val() === 'زانکۆلاین') {
        $('#InvoiceDiv').hide();
    }
});

