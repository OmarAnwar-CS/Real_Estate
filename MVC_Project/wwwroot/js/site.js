// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



document.addEventListener("DOMContentLoaded", function () {
    // Function to load a partial view dynamically
    async function loadPartialView(url) {
        try {
            let response = await fetch(url);
            if (!response.ok) throw new Error("Network response was not ok");

            let html = await response.text();
            document.getElementById("properties").innerHTML = html;
            createPagination(); // Call pagination after loading new content
        } catch (error) {
            console.error("Error loading partial view:", error);
        }
    }

    //// Add event listeners to both buttons for sorting
    //document.getElementById("sortedPropertiesPrice").addEventListener("click", function () {
    //    loadPartialView('/Home/SortedProperties');
    //});


    // Functions to update price and area values
    window.updatePriceValue = function (value) {
        document.getElementById('priceValue').innerText = value;
    };

    window.updateAreaValue = function (value) {
        document.getElementById('areaValue').innerText = value;
    };

    // Function to filter and search properties
    window.searchProperties = async function () {
        // Gather filter values
        var keyword = document.getElementById('Keyword').value;
        var city = document.getElementById('City').value;
        var status = document.getElementById('PropertyStatus').value;
        var priceRange = document.getElementById('PriceRange').value;
        var areaSize = document.getElementById('AreaSize').value;
        var bedrooms = document.getElementById('Bedrooms').value;
        var bathrooms = document.getElementById('Bathrooms').value;

        var hasElevator = document.getElementById('HasElevator').checked;
        var isFurnished = document.getElementById('IsFurnished').checked;
        var hasGarage = document.getElementById('HasGarage').checked;
        var hasParking = document.getElementById('HasParking').checked;
        var hasGarden = document.getElementById('HasGarden').checked;
        var hasBalcony = document.getElementById('HasBalcony').checked;
        var twoStories = document.getElementById('TwoStories').checked;
        var laundryRoom = document.getElementById('LaundryRoom').checked;
        var hasPool = document.getElementById('HasPool').checked;
        var centralHeating = document.getElementById('CentralHeating').checked;

        const filterResultDiv = document.getElementById("properties");

        // Send data to the server via AJAX
        const url = `/Home/PropertyiesPartial?keyword=${keyword}&city=${city}&status=${status}&maxPrice=${priceRange}&maxArea=${areaSize}&maxBaths=${bathrooms}&maxBed=${bedrooms}&HasGarage=${hasGarage}&Two_Stories=${twoStories}&Laundry_Room=${laundryRoom}&HasPool=${hasPool}&HasGarden=${hasGarden}&HasElevator=${hasElevator}&HasBalcony=${hasBalcony}&HasParking=${hasParking}&HasCentralHeating=${centralHeating}&IsFurnished=${isFurnished}`;

        // Fetch data and update property list
        try {
            const response = await fetch(url);
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const result = await response.text();
            filterResultDiv.innerHTML = result; // Update property list
            createPagination();  // Reinitialize pagination after search
        } catch (error) {
            console.error('There was a problem with the fetch operation:', error);
            filterResultDiv.innerHTML = "<p>Error loading properties. Please try again later.</p>";
        }
    }

    // Pagination functionality
    function createPagination() {
        const content = document.querySelector('#propertyList');
        const itemsPerPage = parseInt(document.getElementById('items_per_page').value, 10) || 3;
        let currentPage = 0;
        const items = Array.from(content.getElementsByClassName('property-item'));
        const paginationContainer = document.getElementById('paginationContainer');

        function showPage(page) {
            const startIndex = page * itemsPerPage;
            const endIndex = startIndex + itemsPerPage;
            items.forEach((item, index) => {
                item.style.display = (index >= startIndex && index < endIndex) ? '' : 'none';
            });
            updatePaginationState(page);
        }

        function updatePaginationState(page) {
            const totalPages = Math.ceil(items.length / itemsPerPage);
            const pageLinks = paginationContainer.querySelectorAll('.page-item');
            pageLinks.forEach((link, index) => {
                link.classList.toggle('active', index === page + 1); // +1 to account for Previous button
            });

            const prevButton = paginationContainer.querySelector('.page-item:first-child');
            const nextButton = paginationContainer.querySelector('.page-item:last-child');
            prevButton.classList.toggle('disabled', page === 0);
            nextButton.classList.toggle('disabled', page === totalPages - 1);
        }

        function createPaginationLinks() {
            paginationContainer.innerHTML = '';
            const totalPages = Math.ceil(items.length / itemsPerPage);

            // Previous Button
            const prevButton = document.createElement('li');
            prevButton.classList.add('page-item');
            const prevLink = document.createElement('a');
            prevLink.classList.add('page-link');
            prevLink.textContent = 'Previous';
            prevLink.href = '#';
            prevLink.onclick = function (e) {
                e.preventDefault();
                if (currentPage > 0) {
                    currentPage--;
                    showPage(currentPage);
                }
            };
            prevButton.appendChild(prevLink);
            paginationContainer.appendChild(prevButton);

            // Page Number Buttons
            for (let i = 0; i < totalPages; i++) {
                const pageButton = document.createElement('li');
                pageButton.classList.add('page-item');
                const pageLink = document.createElement('a');
                pageLink.classList.add('page-link');
                pageLink.textContent = i + 1;
                pageLink.href = '#';
                pageLink.onclick = function (e) {
                    e.preventDefault();
                    currentPage = i;
                    showPage(currentPage);
                };
                pageButton.appendChild(pageLink);
                paginationContainer.appendChild(pageButton);
            }

            // Next Button
            const nextButton = document.createElement('li');
            nextButton.classList.add('page-item');
            const nextLink = document.createElement('a');
            nextLink.classList.add('page-link');
            nextLink.textContent = 'Next';
            nextLink.href = '#';
            nextLink.onclick = function (e) {
                e.preventDefault();
                if (currentPage < totalPages - 1) {
                    currentPage++;
                    showPage(currentPage);
                }
            };
            nextButton.appendChild(nextLink);
            paginationContainer.appendChild(nextButton);
        }

        createPaginationLinks();
        showPage(currentPage);
    }

    document.getElementById('items_per_page').addEventListener('change', createPagination);

    // Initial call for pagination
    createPagination();
});



