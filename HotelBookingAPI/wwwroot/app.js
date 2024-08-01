document.getElementById('bookingForm').addEventListener('submit', async function (event) {
    event.preventDefault();

    const roomNumber = document.getElementById('roomNumber').value;
    const guestName = document.getElementById('guestName').value;
    const checkInDate = document.getElementById('checkInDate').value;
    const checkOutDate = document.getElementById('checkOutDate').value;

    const booking = {
        roomNumber: parseInt(roomNumber),
        guestName: guestName,
        checkInData: checkInDate,
        checkOutData: checkOutDate
    };

    try {
        const response = await fetch('https://localhost:7116/api/HotelBooking/Create', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(booking)
        });

        if (response.ok) {
            const result = await response.json();
            document.getElementById('message').innerText = `Booking successful;`;
            // Fetch and display the updated list of bookings
            fetchBookings();
        } else {
            const error = await response.json();
            document.getElementById('message').innerText = `Error: ${error.message}`;
        }
    } catch (error) {
        document.getElementById('message').innerText = `Error: ${error.message}`;
    }
});

async function fetchBookings() {
    try {
        const response = await fetch('https://localhost:7116/api/HotelBooking/GetAll');
        if (response.ok) {
            const bookings = await response.json();
            displayBookings(bookings);
        } else {
            document.getElementById('message').innerText = `Error fetching bookings`;
        }
    } catch (error) {
        document.getElementById('message').innerText = `Error: ${error.message}`;
    }
}

function displayBookings(bookings) {
    const bookingsList = document.getElementById('bookingsList');
    bookingsList.innerHTML = '';

    bookings.forEach(booking => {
        const bookingItem = document.createElement('li');
        bookingItem.innerText = `Room ${booking.roomNumber} - ${booking.guestName} (Check-In: ${booking.checkInData}, Check-Out: ${booking.checkOutData})`;
        bookingsList.appendChild(bookingItem);
    });
}

// Fetch and display the bookings on page load
fetchBookings();