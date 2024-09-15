document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('infoForm');
    const studentContactInfoForm = document.getElementById('studentContactInfoForm');
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

    function checkFormValidity() {
        const inputs = form.querySelectorAll('input, select');
        const allFilled = Array.from(inputs).every(input => input.value.trim() !== '');

        const tab2 = document.getElementById('pivot-tab2');
        if (allFilled) {
            tab2.classList.remove('disabled');
        } else {
            tab2.classList.add('disabled');
        }
    }

    function checkStudentContactInfoFormValidity() {
        const inputs = studentContactInfoForm.querySelectorAll('input, select');
        const allFilled = Array.from(inputs).every(input => input.value.trim() !== '');

        const tab3 = document.getElementById('pivot-tab3');
        if (allFilled) {
            tab3.classList.remove('disabled');
        } else {
            tab3.classList.add('disabled');
        }
    }

    form.addEventListener('input', function () {
        checkFormValidity();
        checkStudentContactInfoFormValidity();
    });

    studentContactInfoForm.addEventListener('input', checkStudentContactInfoFormValidity);

    // Initialize the default active tab
    if (currentIndex === -1) {
        currentIndex = 0; // Default to the first tab if no active tab found
    }
    updateActiveTab(currentIndex);

    // Handle tab switching
    tabs.forEach(tab => {
        tab.addEventListener('click', function (e) {
            e.preventDefault(); // Prevent default anchor behavior

            // Find the clicked tab index
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
